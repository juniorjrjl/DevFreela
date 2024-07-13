using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class Extension
{
    
    public static IServiceCollection AddApplicationMappers(this IServiceCollection services)
    {
        services.AddScoped<IProjectMapper, ProjectMapper>();
        services.AddScoped<IProjectCommentMapper, ProjectCommentMapper>();
        services.AddScoped<IUserMapper, UserMapper>();
        return services;
    }

    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));
        return services;
    }

}