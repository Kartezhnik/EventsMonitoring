using Common;
using Common.Models.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Controllers;

namespace EventsMonitoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events Monitoring API", Version = "v1" });
            });
            builder.Services.AddDbContext<Context>(options => 
                options.UseMySql("Server=localhost;Database=applicationdb;User=root;Password=12345;", 
                new MySqlServerVersion(new Version(8, 0, 34, 0))));
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
            builder.Services.AddAuthorization();
            builder.Services.AddControllers().AddApplicationPart(typeof(UserController).Assembly)
                .AddApplicationPart(typeof(AuthController).Assembly)
                .AddApplicationPart(typeof(EventController).Assembly); 

            WebApplication app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events Monitoring API V1");
                c.RoutePrefix = string.Empty; 
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run();
        }
    }
}
