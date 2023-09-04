using AutoMapper;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Commands;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    
    public class CreateProjectCommentCommandHandlerTest
    {

        private readonly CreateProjectCommentCommandHandler createProjectCommentCommandHandler;
        private readonly IProjectRepository projectRepository; 
        private readonly IProjectQueryRepository projectQueryRepository;
        private readonly IMapper mapper;

        public CreateProjectCommentCommandHandlerTest()
        {
            projectRepository = Substitute.For<IProjectRepository>();
            projectQueryRepository = Substitute.For<IProjectQueryRepository>();
            mapper = Substitute.For<IMapper>();
            createProjectCommentCommandHandler = new CreateProjectCommentCommandHandler(projectRepository, projectQueryRepository, mapper);
        }

        [Fact]
        public async void InputDataIsOk_Executed_ReturnProject()
        {
            // Arrenge
            var createProjectCommentCommand = CreateProjectCommentCommandFactory.Instance().Generate();
            var project = ProjectFactory.Instance().Generate();
            var projectComment = ProjectCommentFactory.Instance().Generate();
            projectQueryRepository.GetByIdAsync(createProjectCommentCommand.ProjectId?? throw new ArgumentException()).Returns(Task.FromResult(project));
            mapper.Map<ProjectComment>(createProjectCommentCommand).Returns(projectComment);
            projectRepository.AddCommentAsync(projectComment).Returns(Task.FromResult(projectComment));
            // Act
            var actual = await createProjectCommentCommandHandler.Handle(createProjectCommentCommand, new CancellationToken());
            // Assert
            Assert.NotNull(actual);
            _ = projectQueryRepository.Received().GetByIdAsync(Arg.Any<int>());
            _ = mapper.Received().Map<ProjectComment>(Arg.Any<CreateProjectCommentCommand>());
            _ = projectRepository.Received().AddCommentAsync(Arg.Any<ProjectComment>());
        }
        
        [Fact]
        public async void InputDataWithoutProjectID_Executed_ThrowError()
        {
            // Arrenge
            var createProjectCommentCommand = CreateProjectCommentCommandFactory.Instance().RuleFor(p => p.ProjectId,f => null).Generate();
            // Act
            var actual = await Assert.ThrowsAsync<ArgumentException>(() => createProjectCommentCommandHandler.Handle(createProjectCommentCommand, new CancellationToken()));
            // Assert
            Assert.NotNull(actual);
            Assert.Equal("O comentário deve referenciar um projeto", actual.Message);
            _ = projectQueryRepository.DidNotReceive().GetByIdAsync(Arg.Any<int>());
            _ = mapper.DidNotReceive().Map<ProjectComment>(Arg.Any<CreateProjectCommentCommand>());
            _ = projectRepository.DidNotReceive().AddCommentAsync(Arg.Any<ProjectComment>());
        }

        [Fact]
        public async void InputDataReferANonStoredProject_Executed_ThrowError()
        {
            // Arrenge
            var createProjectCommentCommand = CreateProjectCommentCommandFactory.Instance().Generate();
            var exception = new ArgumentNullException($"Projeto {createProjectCommentCommand.ProjectId} não encontrado");
            projectQueryRepository.GetByIdAsync(createProjectCommentCommand.ProjectId?? throw new ArgumentException()).ThrowsAsync(exception);
            // Act
            var actual = await Assert.ThrowsAsync<ArgumentNullException>(() => createProjectCommentCommandHandler.Handle(createProjectCommentCommand, new CancellationToken()));
            // Assert
            Assert.NotNull(actual);
            _ = projectQueryRepository.Received().GetByIdAsync(Arg.Any<int>());
            _ = mapper.DidNotReceive().Map<ProjectComment>(Arg.Any<CreateProjectCommentCommand>());
            _ = projectRepository.DidNotReceive().AddCommentAsync(Arg.Any<ProjectComment>());
        }

    }

}
