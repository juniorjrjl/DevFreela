using AutoMapper;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    
    public class UpdateProjectCommandHandlerTest
    {

        private readonly IProjectRepository projectRepositoryMock;
        private readonly IProjectQueryRepository projectQueryRepositoryMock;

        private readonly IMapper mapperMock;

        private readonly UpdateProjectCommandHandler updateProjectCommandHandler;

        public UpdateProjectCommandHandlerTest()
        {
            projectRepositoryMock = Substitute.For<IProjectRepository>();
            projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
            mapperMock = Substitute.For<IMapper>();
            updateProjectCommandHandler = new UpdateProjectCommandHandler(projectRepositoryMock, projectQueryRepositoryMock, mapperMock);
        }

        [Fact]
        public async void InputDataOk_Executed_Deleted()
        {
            // Arrenge
            var updateProjectCommand = UpdateProjectCommandFactort.Instance().Generate();
            var project = ProjectFactory.Instance().Generate();
            projectQueryRepositoryMock.GetByIdAsync(updateProjectCommand.Id?? throw new ArgumentNullException()).Returns(Task.FromResult(project));
            mapperMock.Map(updateProjectCommand, project).Returns(project);
            projectRepositoryMock.UpdateAsync(project).Returns(Task.FromResult(project));
            // Act
            await updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken());
             // Assert
            Assert.NotNull(project);
            _ = projectQueryRepositoryMock.Received().GetByIdAsync(updateProjectCommand.Id?? throw new ArgumentNullException());
            _ = mapperMock.Received().Map(updateProjectCommand, project);
            _ = projectRepositoryMock.Received().UpdateAsync(project);

        }

        [Fact]
        public void ProjectNonStored_executed_ThrowError()
        {
            // Arrenge
            var updateProjectCommand = UpdateProjectCommandFactort.Instance().Generate();
            var exception = new ArgumentNullException();
            projectQueryRepositoryMock.GetByIdAsync(updateProjectCommand.Id?? throw new ArgumentNullException()).ThrowsAsync(exception);
            // Act
            var actual = Assert.ThrowsAsync<ArgumentNullException>(() => updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken()));
            // Assert
            Assert.NotNull(actual);
            _ = projectQueryRepositoryMock.Received().GetByIdAsync(updateProjectCommand.Id?? throw new ArgumentNullException());
            _ = mapperMock.DidNotReceive().Map(Arg.Any<UpdateProjectCommand>(), Arg.Any<Project>());
            _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());
        }

        [Fact]
        public async void CommandWithoutId_Executed_ThrowError()
        {
            // Arrenge
            var updateProjectCommand = UpdateProjectCommandFactort.Instance()
                .RuleFor(p => p.Id, f=> null)
                .Generate();
            // Act
            var actual = await Assert.ThrowsAsync<ArgumentNullException>(() => updateProjectCommandHandler.Handle(updateProjectCommand, new CancellationToken()));
            // Assert
            Assert.NotNull(actual);
            Assert.Contains("id", actual.Message);
            _ = projectQueryRepositoryMock.DidNotReceive().GetByIdAsync(Arg.Any<int>());
            _ = mapperMock.DidNotReceive().Map(Arg.Any<UpdateProjectCommand>(), Arg.Any<Project>());
            _ = projectRepositoryMock.DidNotReceive().UpdateAsync(Arg.Any<Project>());
        }

    }

}