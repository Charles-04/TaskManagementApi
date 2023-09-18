using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Tasks.DTO.Response;

namespace TaskManager.BLL.Projects.DTO.Response
{
    public record CreateProjectResponse
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }


    }
    public record UpdateProjectResponse
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }

    }
    public record ViewProjectResponse
    {
        public string Name { get; init;}
        public string Description { get; init;}
        public IEnumerable<GetTaskResponse> Tasks { get; init;}
    }
  
    public record DeleteProjectResponse
    {
        public string Id { get; init; }
    }
}
