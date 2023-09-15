using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;

namespace TaskManager.BLL.UserAuth.DTO.Request
{
    public record SignUpRequest
    {
        [Required]
        public string FirstName { get; init; }
        [Required]
        public Gender Gender { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [StringLength(17, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

    }
    public record SignInRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public record UpdateProfileRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
    }

}
