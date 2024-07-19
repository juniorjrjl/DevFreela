using DevFreela.Core.Entities;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Commands;
using NSubstitute;
using Xunit;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Application.Mapper;
using MediatR;
using DevFreela.UnitTests.Factories.DTOs;
using System.Linq.Expressions;

namespace DevFreela.UnitTests.Application.Commands;


public class CreateCommentCommandHandlerTest
{

    private readonly CreateProjectCommandHandler createProjectCommandHandler;

    private readonly IUnitOfWork unitOfWork;
    private readonly IProjectMapper mapper;
    private readonly IMediator mediator;

    public CreateCommentCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        mapper = Substitute.For<IProjectMapper>();
        mediator = Substitute.For<IMediator>();
        createProjectCommandHandler = new CreateProjectCommandHandler(unitOfWork, mapper, mediator);
    }

    [Fact]
    public async Task InputDataIsOk_Executed_ReturnProject()
    {
        // Arrenge
        var createProjectCommand = CreateProjectCommandFactory.Instance().Generate();
        var project = ProjectFactory.Instance().Generate();
        var publish = ProjectCreatedNotificationFactory.Instance().Generate();
        mapper.ToEntity(createProjectCommand).Returns(project);
        mapper.ToPublish(project).Returns(publish);
        unitOfWork.ProjectRepository.AddAsync(project).Returns(Task.FromResult(project));
        // Act
        var actual = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());
        // Assert
        Assert.NotNull(actual);
        _ = mapper.Received().ToEntity(Arg.Any<CreateProjectCommand>());
        _ = unitOfWork.ProjectRepository.Received().AddAsync(Arg.Any<Project>());
        _ = unitOfWork.Received(2).IncludeAsync(Arg.Any<Project>(), Arg.Any<Expression<Func<Project, User?>>>());
        _ = mapper.Received().ToPublish(project);
        _ = mediator.Received().Publish(publish);
    }

}
