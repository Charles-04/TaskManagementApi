# nullable disable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Projects.DTO.Request
{
    public record CreateProjectRequest
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Description { get; init; }

    }
    public record ViewProjectRequest
    {
        public string Id { get; init; }
    }
    public record UpdateProjectRequest
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public string Id { get; init; }
    }
   
}
