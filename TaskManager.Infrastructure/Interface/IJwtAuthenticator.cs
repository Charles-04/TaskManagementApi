using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Models;

namespace TaskManager.Infrastructure.Interface
{
    public interface IJwtAuthenticator
    {
        Task<JwtToken> GenerateJwtToken(AppUser user, string expires = null, List<Claim> additionalClaims = null);
    }
}
