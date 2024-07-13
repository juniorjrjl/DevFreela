using DevFreela.Application.Commands.FinishProject;
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


public class FinishProjectCommandHandlerTest
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectRepository projectRepositoryMock;
    private readonly IProjectQueryRepository projectQueryRepositoryMock;

    private readonly FinishProjectCommandHandler finishProjectCommandHandler;

    public FinishProjectCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        projectRepositoryMock = Substitute.For<IProjectRepository>();
        projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
        finishProjectCommandHandler = new FinishProjectCommandHandler(unitOfWork);
    }

    [Fact]
    public async void InputDataOk_Executed_Deleted()
    {
        // Arrenge
        var finishProjectCommand = FinishProjectCommandFactort.Instance().Generate();
        var project = ProjectFactory.Instance().RuleFor(p => p.Status, f => ProjectStatusEnum.IN_PROGRESS).Generate();
        Project? capturedProject = null;
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(finishProjectCommand.Id).Returns(Task.FromResult(project));
        unitOfWork.ProjectRepository.Returns(projectRepositoryMock);
        projectRepositoryMock.UpdateAsync(Arg.Do<Project>(arg => capturedProject = arg)).Returns(Task.FromResult(project));
        // Act
        await finishProjectCommandHandler.Handle(finishProjectCommand, new CancellationToken());
         // Assert
        if (capturedProject is null) throw new ArgumentNullException("erro ao capturar o argumento");
        Assert.Equal(ProjectStatusEnum.FINISHED, capturedProject.Status);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(finishProjectCommand.Id);
        _ = unitOfWork.Received().ProjectRepository;
        _ = projectRepositoryMock.Received().UpdateAsync(capturedProject);

    }

    [Theory]
    [InlineData(ProjectStatusEnum.CREATED)]
    [InlineData(ProjectStatusEnum.SUSPENDED)]
    [InlineData(ProjectStatusEnum.CANCELED)]
    [InlineData(ProjectStatusEnum.FINISHED)]
    public void ProjectInInvalidStatus_Executed_ThrowError(ProjectStatusEnum status)
    {
        // Arrenge
        var finishProjectCommand = FinishProjectCommandFactort.Instance().Generate();
        var project = ProjectFactory.Instance().RuleFor(p => p.Status, f => status).Generate();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(finishProjectCommand.Id).Returns(Task.FromResult(project));
        // Act
        var actual = Assert.ThrowsAsync<ProjectStatusException>(() =>finishProjectCommandHandler.Handle(finishProjectCommand, new CancellationToken()));
         // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(finishProjectCommand.Id);
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());

    }

    [Fact]
    public void ProjectNonStored_executed_ThrowError()
    {
        // Arrenge
        var finishProjectCommand = FinishProjectCommandFactort.Instance().Generate();
        var exception = new ArgumentNullException();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepositoryMock);
        projectQueryRepositoryMock.GetByIdAsync(finishProjectCommand.Id).ThrowsAsync(exception);
        // Act
        var actual = Assert.ThrowsAsync<ArgumentNullException>(() => finishProjectCommandHandler.Handle(finishProjectCommand, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(finishProjectCommand.Id);
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());
    }

}