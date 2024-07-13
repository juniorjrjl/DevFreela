using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands;


public class UpdateProjectCommandHandlerTest
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectRepository projectRepositoryMock;
    private readonly IProjectQueryRepository projectQueryRepositoryMock;

    private readonly IProjectMapper mapperMock;

    private readonly UpdateProjectCommandHandler updateProjectCommandHandler;

    public UpdateProjectCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        projectRepositoryMock = Substitute.For<IProjectRepository>();
        projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
        mapperMock = Substitute.For<IProjectMapper>();
        updateProjectCommandHandler = new UpdateProjectCommandHandler(unitOfWork, mapperMock);
    }

    [Fact]
    public async void InputDataOk_Executed_Deleted()
    {
        // Arrenge
        var updateProjectCommand = UpdateProjectCommandFactort.Instance().Generate();
        var project = ProjectFactory.Instance().Generate();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(updateProjectCommand.Id).Returns(Task.FromResult(project));
        mapperMock.ToEntity(updateProjectCommand, project).Returns(project);
        unitOfWork.ProjectRepository.Returns(projectRepositoryMock);
        projectRepositoryMock.UpdateAsync(project).Returns(Task.FromResult(project));
        // Act
        await updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken());
         // Assert
        Assert.NotNull(project);
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(updateProjectCommand.Id);
        _ = mapperMock.Received().ToEntity(updateProjectCommand, project);
        _ = unitOfWork.Received().ProjectRepository;
        _ = projectRepositoryMock.Received().UpdateAsync(project);

    }

    [Fact]
    public void ProjectNonStored_executed_ThrowError()
    {
        // Arrenge
        var updateProjectCommand = UpdateProjectCommandFactort.Instance().Generate();
        var exception = new NotFoundException();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(updateProjectCommand.Id).ThrowsAsync(exception);
        // Act
        var actual = Assert.ThrowsAsync<NotFoundException>(() => updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(updateProjectCommand.Id);
        _ = mapperMock.DidNotReceive().ToEntity(Arg.Any<UpdateProjectCommand>(), Arg.Any<Project>());
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());
    }

}