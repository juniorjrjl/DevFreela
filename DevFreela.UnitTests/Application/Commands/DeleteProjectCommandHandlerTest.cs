using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands;


public class DeleteProjectCommandHandlerTest
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectRepository projectRepositoryMock;
    private readonly IProjectQueryRepository projectQueryRepositoryMock;

    private readonly DeleteProjectCommandHandler deleteProjectCommandHandler;

    public DeleteProjectCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        projectRepositoryMock = Substitute.For<IProjectRepository>();
        projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
        deleteProjectCommandHandler = new DeleteProjectCommandHandler(unitOfWork);
    }

    [Theory]
    [InlineData(ProjectStatusEnum.CREATED)]
    [InlineData(ProjectStatusEnum.IN_PROGRESS)]
    [InlineData(ProjectStatusEnum.SUSPENDED)]
    public async Task InputDataOk_Executed_Deleted(ProjectStatusEnum status)
    {
        // Arrenge
        var deleteProjectCommand = DeleteProjectCommandFactort.Instance().Generate();
        var project = ProjectFactory.Instance().RuleFor(p => p.Status, f => status).Generate();
        Project? capturedProject = null;
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(deleteProjectCommand.Id).Returns(Task.FromResult(project));
        unitOfWork.ProjectRepository.Returns(projectRepositoryMock);
        projectRepositoryMock.UpdateAsync(Arg.Do<Project>(arg => capturedProject = arg)).Returns(Task.FromResult(project));
        // Act
        await deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken());
         // Assert
        if (capturedProject is null) throw new ArgumentNullException("erro ao capturar o argumento");
        Assert.Equal(ProjectStatusEnum.CANCELED, capturedProject.Status);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(deleteProjectCommand.Id);
        _ = unitOfWork.Received().ProjectRepository;
        _ = projectRepositoryMock.Received().UpdateAsync(capturedProject);

    }

    [Theory]
    [InlineData(ProjectStatusEnum.CANCELED)]
    [InlineData(ProjectStatusEnum.FINISHED)]
    public void ProjectInInvalidStatus_Executed_ThrowError(ProjectStatusEnum status)
    {
        // Arrenge
        var deleteProjectCommand = DeleteProjectCommandFactort.Instance().Generate();
        var project = ProjectFactory.Instance().RuleFor(p => p.Status, f => status).Generate();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(deleteProjectCommand.Id).Returns(Task.FromResult(project));
        // Act
        var actual = Assert.ThrowsAsync<ProjectStatusException>(() =>deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken()));
         // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(deleteProjectCommand.Id);
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());

    }

    [Fact]
    public void ProjectNonStored_executed_ThrowError()
    {
        // Arrenge
        var deleteProjectCommand = DeleteProjectCommandFactort.Instance().Generate();
        var exception = new ArgumentNullException();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(deleteProjectCommand.Id).ThrowsAsync(exception);
        // Act
        var actual = Assert.ThrowsAsync<ArgumentNullException>(() =>deleteProjectCommandHandler.Handle(deleteProjectCommand, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(deleteProjectCommand.Id);
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());
    }

}