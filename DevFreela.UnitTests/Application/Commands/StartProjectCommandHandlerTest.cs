using DevFreela.Application.Commands.StartProject;
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


public class StartProjectCommandHandlerTest
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectRepository projectRepositoryMock;
    private readonly IProjectQueryRepository projectQueryRepositoryMock;

    private readonly StartProjectCommandHandler startProjectCommandHandler;

    public StartProjectCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        projectRepositoryMock = Substitute.For<IProjectRepository>();
        projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
        startProjectCommandHandler = new StartProjectCommandHandler(unitOfWork);
    }

    [Fact]
    public async Task InputDataOk_Executed_Deleted()
    {
        // Arrenge
        var startProjectCommand = StartProjectCommandFactort.Instance().Generate();
        var project = ProjectFactory.Instance()
            .RuleFor(p => p.Status, f => ProjectStatusEnum.CREATED)
            .RuleFor(p => p.StartedAt, f => null)
            .Generate();
        Project? capturedProject = null;
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(startProjectCommand.Id).Returns(Task.FromResult(project));
        unitOfWork.ProjectRepository.Returns(projectRepositoryMock);
        projectRepositoryMock.UpdateAsync(Arg.Do<Project>(arg => capturedProject = arg)).Returns(Task.FromResult(project));
        // Act
        await startProjectCommandHandler.Handle(startProjectCommand, new CancellationToken());
         // Assert
        if (capturedProject is null) throw new ArgumentNullException("erro ao capturar o argumento");
        Assert.Equal(ProjectStatusEnum.IN_PROGRESS, capturedProject.Status);
        Assert.NotNull(capturedProject.StartedAt);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(startProjectCommand.Id);
        _ = unitOfWork.Received().ProjectRepository;
        _ = projectRepositoryMock.Received().UpdateAsync(capturedProject);

    }

    [Theory]
    [InlineData(ProjectStatusEnum.IN_PROGRESS)]
    [InlineData(ProjectStatusEnum.SUSPENDED)]
    [InlineData(ProjectStatusEnum.CANCELED)]
    [InlineData(ProjectStatusEnum.FINISHED)]
    public void ProjectInInvalidStatus_Executed_ThrowError(ProjectStatusEnum status)
    {
        // Arrenge
        var startProjectCommand = StartProjectCommandFactort.Instance().Generate();
        var project = ProjectFactory.Instance().RuleFor(p => p.Status, f => status).Generate();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(startProjectCommand.Id).Returns(Task.FromResult(project));
        // Act
        var actual = Assert.ThrowsAsync<ProjectStatusException>(() =>startProjectCommandHandler.Handle(startProjectCommand, new CancellationToken()));
         // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(startProjectCommand.Id);
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());

    }

    [Fact]
    public void ProjectNonStored_executed_ThrowError()
    {
        // Arrenge
        var startProjectCommand = StartProjectCommandFactort.Instance().Generate();
        var exception = new ArgumentNullException();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(startProjectCommand.Id).ThrowsAsync(exception);
        // Act
        var actual = Assert.ThrowsAsync<ArgumentNullException>(() => startProjectCommandHandler.Handle(startProjectCommand, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(startProjectCommand.Id);
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());
    }

}