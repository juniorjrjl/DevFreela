using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.UpdateProject;
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

}
