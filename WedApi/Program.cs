using Application;
using Application.Services;
using Application.UseCases;
using Domain.Mappers;
using Domain;
using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Controllers;
using Presentation.Middleware;
using Infrastructure.Services;

namespace EventsMonitoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ����� �� ���������� ����� ������ ��� �������� � ������������ ����������
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // ����������� �������� ����������
            builder.Services
                .AddApplication()    // ������ ���������� (Application)
                .AddDomain()         // ������ ������ (Domain)
                .AddInfrasructure(); // ������ �������������� (Infrastructure)

            // �������������� ��������� ��������
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<Application.Validators.UserValidator>();
            builder.Services.AddScoped<Application.Validators.EventValidator>();
            builder.Services.AddScoped<Application.Validators.ImageValidator>();

            // ����������� ������������ � ��������
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<UserRegistrationUseCase>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // ������������ ���� ������
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly("WebApi")));

            // ������������ AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // ��������� �������������� � JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
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

            // ����������� �����������
            builder.Services.AddAuthorization();

            // ����������� ������������
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(UserController).Assembly)
                .AddApplicationPart(typeof(AuthController).Assembly)
                .AddApplicationPart(typeof(EventController).Assembly);

            // ����������� Swagger ��� API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events Monitoring API", Version = "v1" });
            });

            // �������� ����������
            WebApplication app = builder.Build();

            // ��������� �������������� �� (middleware)
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Middleware ��� ��������� ������
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // ��������� Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events Monitoring API V1");
                c.RoutePrefix = string.Empty;
            });

            // ����������� �����
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // ������ ����������
            app.Run();
        }

        // �������������� ��� ��� ������������� ������� �������� ����� CreateHostBuilder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // ����� �������� DBContext � ������ ������� ����� �������
                        services.AddDbContext<Context>(options =>
                            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                    });

                    // ��������� �� ����� Startup, ���� �� ����, ��� ���������� ���������
                    webBuilder.UseStartup<Program>();
                });
    }
}
