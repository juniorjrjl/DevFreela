using DevFreela.Core.Entities;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Commands;
using NSubstitute;
using Xunit;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Application.Mapper;

namespace DevFreela.UnitTests.Application.Commands;


public class CreateCommentCommandHandlerTest
{

    private readonly CreateProjectCommandHandler createProjectCommandHandler;

    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectMapper mapper;

    public CreateCommentCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        mapper = Substitute.For<IProjectMapper>();
        createProjectCommandHandler = new CreateProjectCommandHandler(unitOfWork, mapper);
    }

    [Fact]
    public async Task InputDataIsOk_Executed_ReturnProject()
    {
        // Arrenge
        var createProjectCommand = CreateProjectCommandFactory.Instance().Generate();
        var project = ProjectFactory.Instance().Generate();
        mapper.ToEntity(createProjectCommand).Returns(project);
        unitOfWork.ProjectRepository.AddAsync(project).Returns(Task.FromResult(project));
        // Act
        var actual = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());
        // Assert
        Assert.NotNull(actual);
        _ = mapper.Received().ToEntity(Arg.Any<CreateProjectCommand>());
        _ = unitOfWork.ProjectRepository.Received().AddAsync(Arg.Any<Project>());
    }

}
