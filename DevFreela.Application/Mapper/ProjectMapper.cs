using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Mapper;

public class ProjectMapper : IProjectMapper
{
    public Project ToEntity(CreateProjectCommand command) => 
        new
        (
            command.Title,
            command.Description,
            command.ClientId,
            command.FreelancerId,
            command.TotalCost
        );

    public Project ToEntity(UpdateProjectCommand command, Project entity) => 
        entity.Update
        (
            command.Title, 
            command.Description, 
            command.TotalCost
        );

    public ProjectCreatedNotification ToPublish(Project entity)
    {
        ArgumentNullException.ThrowIfNull(entity.Client);
        ArgumentNullException.ThrowIfNull(entity.Freelancer);
        var publish = new ProjectCreatedNotification
        (
            entity.Title,
            entity.TotalCost,
            entity.Freelancer.Name,
            entity.Freelancer.Email,
            entity.Client.Name,
            entity.Client.Email
        );
        return publish;
    }
}
