using Microsoft.AspNetCore.Identity;
using TaskManager.BLL.Extensions;
using TaskManager.BLL.UserAuth.DTO.Request;
using TaskManager.BLL.UserAuth.DTO.Response;
using TaskManager.BLL.UserAuth.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Shared;
using TaskManager.Infrastructure.Interface;
using TaskManager.Infrastructure.Models;
using TaskManager.Persistence.Interface;

namespace TaskManager.BLL.UserAuth.Implementation
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtAuthenticator _jWTAuthenticator;
        private readonly IRepository<UserProfile> _profileManager;
        private readonly IServiceFactory _serviceFactory;
        public UserAuthService(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _userManager = _serviceFactory.GetService<UserManager<AppUser>>();
            _roleManager = _serviceFactory.GetService<RoleManager<IdentityRole>>();
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _jWTAuthenticator = _serviceFactory.GetService<IJwtAuthenticator>();
            _profileManager = _unitOfWork.GetRepository<UserProfile>();
        }
        public async Task<Response<SignInResponse>> SignIn(SignInRequest loginRequest)
        {
            var email = loginRequest.Email.Trim().ToLower();
            AppUser existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser is null)
                throw new InvalidOperationException($"User with Email {loginRequest.Email} Doesn't Exist");

            var isCredentialCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
            if (!isCredentialCorrect)
                throw new InvalidOperationException("Wrong Password");
            var userProfile = await _profileManager.GetSingleByAsync(x => x.UserId == existingUser.Id);
            userProfile.CheckNull("User Profile not found");
            if (!userProfile.Active)
                throw new InvalidOperationException("User is inactive");
           
            JwtToken token = await _jWTAuthenticator.GenerateJwtToken(existingUser, expires: null, additionalClaims: null);
            var result = new SignInResponse { Token = token };
            return new Response<SignInResponse>
            {
                Success = true,
                Message = "Login successful",
                Result = result,

            };
        }

        public async Task<Response<SignUpResponse>> SignUpAsync(SignUpRequest request)
        {
            AppUser existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with Email {request.Email}");

            existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"User already exists with username {request.UserName}");



            AppUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email.ToLower(),
                UserName = request.UserName.Trim().ToLower(),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                PhoneNumber = request.PhoneNumber,

            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);


            if (!result.Succeeded)
            {
                var message = $"Failed to create user: {(result.Errors.FirstOrDefault())?.Description}";
                throw new InvalidOperationException(message);

            }

            string? role = "User";
            bool roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
                await _userManager.AddToRoleAsync(user, role);
            }
            else
                await _userManager.AddToRoleAsync(user, role);


            var userProfile = new UserProfile
            {
                Id = Guid.NewGuid().ToString(),
                Gender = request.Gender,
                UserId = user.Id,
                FullName = $"{user.FirstName} {user.LastName}"


            };
            var newProfile = await _profileManager.AddAsync(userProfile);
            if (newProfile == null)
                throw new InvalidOperationException("Problem creating user profile");
           
            JwtToken userToken = await _jWTAuthenticator.GenerateJwtToken(user, expires: null, additionalClaims: null);
            var response = new SignUpResponse
            {
                UserId = user.Id,
                Token = userToken,
                UserName = user.UserName
            };
            return new Response<SignUpResponse>
            {
                Success = true,
                Result = response,
                Message = "Account creation successful"
            };
        }
    }
}
