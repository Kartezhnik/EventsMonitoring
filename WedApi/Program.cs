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
            // Здесь мы используем новый подход для создания и конфигурации приложения
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Регистрация сервисов приложения
            builder.Services
                .AddApplication()    // Сервис приложения (Application)
                .AddDomain()         // Сервис домена (Domain)
                .AddInfrasructure(); // Сервис инфраструктуры (Infrastructure)

            // Дополнительная настройка сервисов
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<Application.Validators.UserValidator>();
            builder.Services.AddScoped<Application.Validators.EventValidator>();
            builder.Services.AddScoped<Application.Validators.ImageValidator>();

            // Регистрация репозиториев и сервисов
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<UserRegistrationUseCase>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Конфигурация базы данных
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly("WebApi")));

            // Конфигурация AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Настройка аутентификации с JWT
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

            // Регистрация авторизации
            builder.Services.AddAuthorization();

            // Регистрация контроллеров
            builder.Services.AddControllers()
                .AddApplicationPart(typeof(UserController).Assembly)
                .AddApplicationPart(typeof(AuthController).Assembly)
                .AddApplicationPart(typeof(EventController).Assembly);

            // Регистрация Swagger для API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events Monitoring API", Version = "v1" });
            });

            // Создание приложения
            WebApplication app = builder.Build();

            // Настройка промежуточного ПО (middleware)
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Middleware для обработки ошибок
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Настройка Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events Monitoring API V1");
                c.RoutePrefix = string.Empty;
            });

            // Статические файлы
            app.UseDefaultFiles();
            app.UseStaticFiles();

            // Запуск приложения
            app.Run();
        }

        // Дополнительный код для использования старого хостинга через CreateHostBuilder
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Можно добавить DBContext и другие сервисы через сервисы
                        services.AddDbContext<Context>(options =>
                            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                    });

                    // Указываем на класс Startup, если он есть, для дальнейшей настройки
                    webBuilder.UseStartup<Program>();
                });
    }
}
