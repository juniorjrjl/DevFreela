using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Notification.ProjectCreated;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Mapper;

public interface IProjectMapper
{

    Project ToEntity(CreateProjectCommand command);

    Project ToEntity(UpdateProjectCommand command, Project entity);

    ProjectCreatedNotification ToPublish(Project entity);

}