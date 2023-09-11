using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Shared
{
    public record RequestParameter
    {
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        
    }
}
