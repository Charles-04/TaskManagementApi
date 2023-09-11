using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.BLL.Tasks.DTO.Response
{
    public record GetTaskResponse
    {
        public string Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Priority { get; init; }
        public string StartDate { get; init; }
        public string EndDate { get; init; }

    }
    public record UpdateTaskResponse
    {

    } 
}
