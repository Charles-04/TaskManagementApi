using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TaskManager.Api.Controllers.Shared;
using TaskManager.BLL.Projects.DTO.Request;
using TaskManager.BLL.UserAuth.DTO.Request;
using TaskManager.BLL.UserAuth.Interface;
using TaskManager.Persistence.Interface;

namespace TaskManager.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IServiceFactory _serviceFactory;
        private IUserAuthService _userAuthService;
        public AuthController(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _userAuthService = _serviceFactory.GetService<IUserAuthService>();
              
        }

        [HttpPost("sign-up")]
        [SwaggerOperation("Creates a new  user account")]
        public async Task<IActionResult> CreateAccount(SignUpRequest request)
        {
            
            var response = await _userAuthService.SignUpAsync( request);
            return Ok(response);

        }
        [HttpPost("sign-in")]
        [SwaggerOperation("logs a user in")]
        public async Task<IActionResult> Login(SignInRequest request)
        {
            
            var response = await _userAuthService.SignIn( request);
            return Ok(response);

        }
    }
}
