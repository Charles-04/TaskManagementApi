﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Domain.Shared
{
    public class PagedResponse<T> where T : class
    {
        public MetaData MetaData { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
