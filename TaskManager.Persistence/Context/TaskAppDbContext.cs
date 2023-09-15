using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using Todo = TaskManager.Domain.Entities.Task;

namespace TaskManager.Persistence.Context
{
    public class TaskAppDbContext : IdentityDbContext<AppUser, IdentityRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public TaskAppDbContext(DbContextOptions options) : base(options)
        {
        }
        DbSet<Notification> Notifications { get; set; }
        DbSet<Todo> Tasks { get; set; }
        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<Project> Projects { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserProfile>(t =>
            {
                
                t.HasMany<Todo>().WithOne(t => t.Assignee).HasForeignKey(t => t.AssigneeId);
            });
            builder.Entity<UserProfile>(t =>
            {
                t.HasMany<Todo>().WithOne(t => t.Author).HasForeignKey(t => t.AuthorId).IsRequired();
            });

            builder.Entity<Todo>(t =>
            {
                t.HasOne<UserProfile>().WithMany(t => t.AssignedTasks);
            });
            builder.Entity<Todo>(t =>
            {
                t.HasOne<UserProfile>().WithMany(t => t.Tasks);
            });
        }
    }
}
