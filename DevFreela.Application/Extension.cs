using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application;

public static class Extension
{
    
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services.AddMappers()
            .AddCommands();
    private static IServiceCollection AddMappers(this IServiceCollection services)
        => services.AddScoped<IProjectMapper, ProjectMapper>()
            .AddScoped<IProjectCommentMapper, ProjectCommentMapper>()
            .AddScoped<IUserMapper, UserMapper>();

    private static IServiceCollection AddCommands(this IServiceCollection services)
        => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProjectCommand).Assembly));

}