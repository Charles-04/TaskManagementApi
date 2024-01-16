using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskManager.Api.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Produces("application/json")]
        public string GetUserId()
        {
            ClaimsPrincipal user = HttpContext.User;
            string userId = user.FindFirstValue("Id")!;
            return userId;
        }


    }
}