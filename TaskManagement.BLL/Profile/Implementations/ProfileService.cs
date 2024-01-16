using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Profile.DTO.Response;
using TaskManager.BLL.Profile.Interfaces;

namespace TaskManager.BLL.Profile.Implementations
{
    public class ProfileService : IProfileService
    {

        public ProfileService()
        {
              
        }
        public Task UpdateProfile()
        {
            throw new NotImplementedException();
        }

        public Task<ViewProfileResponse> ViewProfile()
        {

            throw new NotImplementedException();
        }
    }
}
