using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Profile.DTO.Response
{
    public record UpdateProfileResponse
    {
        public string ProfileId { get; set; }
    }
    public record ViewProfileResponse
    {
        public string Image { get; set; }
        public string Fullname {  get; set; }
        public int Projects { get; set; }
        public int ActiveTasks { get; set; }
        public int CompletedTasks {  get; set; }
        public string Gender {  get; set; }
    }
}
