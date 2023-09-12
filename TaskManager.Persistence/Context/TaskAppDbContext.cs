using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Persistence.Context
{
    public class TaskAppDbContext : IdentityDbContext<IdentityUser>
    {
        public TaskAppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
