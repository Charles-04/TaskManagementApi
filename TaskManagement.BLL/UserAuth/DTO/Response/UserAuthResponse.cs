using TaskManager.Infrastructure.Models;

namespace TaskManager.BLL.UserAuth.DTO.Response
{
    public record ViewProfileResponse
    {

    }
    public record SignInResponse
    {
        public JwtToken Token { get; init; }
    }
    public record SignUpResponse
    {
        public string UserId { get; init; }
        public string UserName { get; init; }
        public JwtToken Token { get; init; }
    }
    public record UpdateProfileResponse
    {

    }
    public record DeactivateAccountResponse
    {

    }
}
