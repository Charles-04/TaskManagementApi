using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Shared
{
    public record Response<T>
    {
        public string Message { get; init; }
        public T Result { get; init; }
        public bool Success { get; init; }
    }
}
