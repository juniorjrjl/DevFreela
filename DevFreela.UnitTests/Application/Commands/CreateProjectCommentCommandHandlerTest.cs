using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Commands;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using DevFreela.Application.Mapper;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.UnitTests.Application.Commands;


public class CreateProjectCommentCommandHandlerTest
{

    private readonly IUnitOfWork unitOfWork;
    private readonly CreateProjectCommentCommandHandler createProjectCommentCommandHandler;
    private readonly IProjectRepository projectRepository; 
    private readonly IProjectQueryRepository projectQueryRepository;
    private readonly IProjectCommentMapper mapper;

    public CreateProjectCommentCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        projectRepository = Substitute.For<IProjectRepository>();
        projectQueryRepository = Substitute.For<IProjectQueryRepository>();
        mapper = Substitute.For<IProjectCommentMapper>();
        createProjectCommentCommandHandler = new CreateProjectCommentCommandHandler(unitOfWork, mapper);
    }

    [Fact]
    public async Task InputDataIsOk_Executed_ReturnProject()
    {
        // Arrenge
        var createProjectCommentCommand = CreateProjectCommentCommandFactory.Instance().Generate();
        var project = ProjectFactory.Instance().Generate();
        var projectComment = ProjectCommentFactory.Instance().Generate();
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepository);
        unitOfWork.ProjectRepository.Returns(projectRepository);
        projectQueryRepository.GetByIdAsync(createProjectCommentCommand.ProjectId).Returns(Task.FromResult(project));
        mapper.ToEntity(createProjectCommentCommand).Returns(projectComment);
        projectRepository.AddCommentAsync(projectComment).Returns(Task.FromResult(projectComment));
        // Act
        var actual = await createProjectCommentCommandHandler.Handle(createProjectCommentCommand, new CancellationToken());
        // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepository.Received().GetByIdAsync(Arg.Any<int>());
        _ = mapper.Received().ToEntity(Arg.Any<CreateProjectCommentCommand>());
        _ = unitOfWork.Received().ProjectRepository;
        _ = projectRepository.Received().AddCommentAsync(Arg.Any<ProjectComment>());
    }

    [Fact]
    public async Task InputDataReferANonStoredProject_Executed_ThrowError()
    {
        // Arrenge
        var createProjectCommentCommand = CreateProjectCommentCommandFactory.Instance().Generate();
        var exception = new ArgumentNullException($"Projeto {createProjectCommentCommand.ProjectId} não encontrado");
        unitOfWork.ProjectQueryRepository.Returns(projectQueryRepository);
        projectQueryRepository.GetByIdAsync(createProjectCommentCommand.ProjectId).ThrowsAsync(exception);
        // Act
        var actual = await Assert.ThrowsAsync<ArgumentNullException>(() => createProjectCommentCommandHandler.Handle(createProjectCommentCommand, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().ProjectQueryRepository;
        _ = projectQueryRepository.Received().GetByIdAsync(Arg.Any<int>());
        _ = mapper.DidNotReceive().ToEntity(Arg.Any<CreateProjectCommentCommand>());
        _ = unitOfWork.DidNotReceive().ProjectRepository;
        _ = projectRepository.DidNotReceive().AddCommentAsync(Arg.Any<ProjectComment>());
    }

}
