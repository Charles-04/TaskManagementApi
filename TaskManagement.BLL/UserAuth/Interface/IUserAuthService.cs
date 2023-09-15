using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.UserAuth.DTO.Request;
using TaskManager.BLL.UserAuth.DTO.Response;
using TaskManager.Domain.Shared;

namespace TaskManager.BLL.UserAuth.Interface
{
    public interface IUserAuthService
    {
        Task<Response<SignUpResponse>> SignUpAsync(SignUpRequest request);
        Task<Response<SignInResponse>> SignIn(SignInRequest loginRequest);

    }
}
