using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure;

public static class Extension
{
    
    public static IServiceCollection AddRepositories(this IServiceCollection service, string connectionString)
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

    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IAuthService, AuthService>();
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        return service;
    }

    public static IServiceCollection AddDBContext(this IServiceCollection service, string connectionString)
    {
        service.AddDbContext<DevFreelaDbContext>(o=> o.UseSqlServer(connectionString));
        return service;
    }

}