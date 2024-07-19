using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Notification;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure;

public static class Extension
{
    
    public static IServiceCollection AddInfraStructure(
        this IServiceCollection service, 
        string connectionString,
        IConfiguration configuration
        )
        => service.AddRepositories(connectionString)
            .AddServices()
            .AddDBContext(connectionString)
            .AddMailNotification(configuration);
    private static IServiceCollection AddRepositories(this IServiceCollection service, string connectionString)
    {
        service.AddScoped<IProjectQueryRepository, ProjectQueryRepository>();
        service.AddScoped<IProjectRepository, ProjectRepository>();
        service.AddScoped<ISkillQueryRepository>(p => new SkillQueryRepository(connectionString));
        service.AddScoped<IUserQueryRepository, UserQueryRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IRoleQueryRepository, RoleQueryRepository>();
        service.AddScoped<IRoleRepository, RoleRepository>();
        return service;
    }

    private static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IAuthService, AuthService>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        return service;
    }

    private static IServiceCollection AddDBContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<DevFreelaDbContext>(o=> o.UseSqlServer(connectionString));
        return service;
    }

    private static IServiceCollection AddMailNotification(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(cfg => {
            var configuration = cfg.GetService<IConfiguration>();
            ArgumentNullException.ThrowIfNull(configuration);

            var mailConfig = configuration.GetSection("Notifications").Get<MailConfig>();
            ArgumentNullException.ThrowIfNull(mailConfig);

            return mailConfig;
        });
        services.AddScoped<INotificationService, NotificationService>();
        return services;
    }

}
