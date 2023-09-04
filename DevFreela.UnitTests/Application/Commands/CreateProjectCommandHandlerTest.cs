using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Commands;
using NSubstitute;
using Xunit;
using DevFreela.Application.Commands.CreateProject;

namespace DevFreela.UnitTests.Application.Commands
{
    
    public class CreateCommentCommandHandlerTest
    {

        private readonly CreateProjectCommandHandler createProjectCommandHandler;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public CreateCommentCommandHandlerTest()
        {
            projectRepository = Substitute.For<IProjectRepository>();
            mapper = Substitute.For<IMapper>();
            createProjectCommandHandler = new CreateProjectCommandHandler(projectRepository, mapper);
        }

        [Fact]
        public async void InputDataIsOk_Executed_ReturnProject()
        {
            // Arrenge
            var createProjectCommand = CreateProjectCommandFactory.Instance().Generate();
            var project = ProjectFactory.Instance().Generate();
            mapper.Map<Project>(createProjectCommand).Returns(project);
            projectRepository.AddAsync(project).Returns(Task.FromResult(project));
            // Act
            var actual = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());
            // Assert
            Assert.NotNull(actual);
            _ = mapper.Received().Map<Project>(Arg.Any<CreateProjectCommand>());
            _ = projectRepository.Received().AddAsync(Arg.Any<Project>());
        }

    }

}
