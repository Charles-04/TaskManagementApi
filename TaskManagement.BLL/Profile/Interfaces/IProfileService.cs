using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Profile.DTO.Response;

namespace TaskManager.BLL.Profile.Interfaces
{
    public interface IProfileService
    {
        public Task<ViewProfileResponse> ViewProfile();
        public Task UpdateProfile();

    }
}
