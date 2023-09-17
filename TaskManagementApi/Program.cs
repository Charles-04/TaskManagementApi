using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System;
using System.Text;

using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Filters;
using TaskManager.Api.Extensions;
using TaskManager.Persistence.Context;

namespace TaskManagementApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnect");
            builder.Services.AddDbContext<TaskAppDbContext>(options => 
                options.UseSqlServer(connectionString)
                                
            );
            builder.Services.AddControllers(setupAction =>
            {
                setupAction.Filters.Add<ValidateModelAttribute>();

            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(jwt =>
              {

                  string jwtConfig = builder.Configuration.GetSection("JwtConfig:Key").Value;
                  string issuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value;
                  string audience = builder.Configuration.GetSection("JwtConfig:Audience").Value;
                  var key = Encoding.ASCII.GetBytes(jwtConfig);

                  jwt.SaveToken = true;
                  jwt.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(key),
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      RequireExpirationTime = true,
                      ValidIssuer = issuer,
                      ValidAudience = audience,
                      ClockSkew = TimeSpan.Zero
                  };
              });
            builder.Services.AddAuthorization();
            builder.Services.AddIdentity();
            builder.Services.RegisterServices();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGenerator();
            builder.Services.AddHangFire(connectionString);

           var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureException(builder.Environment);
            app.ConfigureJobs(builder.Environment);
            app.MapControllers();

            app.Run();
        }
    }
}