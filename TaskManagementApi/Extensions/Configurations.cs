using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TaskManager.BLL.Interface.WorkerServices;
using TaskManager.BLL.Notification.Implementation;
using TaskManager.BLL.Notification.Interface;
using TaskManager.BLL.Notification.WorkerService;
using TaskManager.BLL.Profile.Implementations;
using TaskManager.BLL.Profile.Interfaces;
using TaskManager.BLL.Projects.Implementation;
using TaskManager.BLL.Projects.Interface;
using TaskManager.BLL.Tasks.Implementation;
using TaskManager.BLL.Tasks.Interface;
using TaskManager.BLL.UserAuth.Implementation;
using TaskManager.BLL.UserAuth.Interface;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Implementation;
using TaskManager.Infrastructure.Implementation;
using TaskManager.Infrastructure.Interface;
using TaskManager.Persistence.Context;
using TaskManager.Persistence.Implementation;
using TaskManager.Persistence.Interface;

namespace TaskManager.Api.Extensions
{
    public static class Configurations
    {

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IUnitOfWork, UnitOfWork<TaskAppDbContext>>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IServiceFactory, ServiceFactory>();
            services.AddTransient<IJwtAuthenticator, JwtAuthenticator>();
            services.AddTransient<IUserAuthService, UserAuthService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddHttpContextAccessor();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddSingleton<IEmailConfiguration>(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<INotificationWorkerService, NotificationWorkerService>();


        }
        public static void AddHangFire(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString));

        }
        public static void AddSwaggerGenerator(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManager", Version = "v1" });


                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1s5af4sf34-4940#93.3er-394-sdfdfd\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                          new OpenApiSecurityScheme
                              {
                                 Reference = new OpenApiReference
                                     {
                                         Type = ReferenceType.SecurityScheme,
                                         Id = "Bearer"
                                     }
                               },
                         Array.Empty<string>()
                      },
                });
            });
        }
        public static void AddIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;

            });

            builder = new IdentityBuilder(typeof(AppUser), typeof(IdentityRole),
            builder.Services);
            builder.AddEntityFrameworkStores<TaskAppDbContext>()
            .AddDefaultTokenProviders();


        }
    }
}
