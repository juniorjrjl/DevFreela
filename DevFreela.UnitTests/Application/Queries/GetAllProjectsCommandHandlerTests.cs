using System.Reflection;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories;
using NSubstitute;
using Xunit;
using Xunit.Sdk;

namespace DevFreela.UnitTests.Application.Queries
{
    
    public class GetAllProjectsCommandHandlerTests
    {

        private GetAllProjectsQueryHandler getAllProjectsQueryHandler;

        private IProjectQueryRepository projectQueryRepositoryMock;

        public GetAllProjectsCommandHandlerTests()
        {
            projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
            getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectQueryRepositoryMock);
        }

        [Fact]
        public async void HasThreeProjects_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = new ProjectFactory().Generate(3);
            projectQueryRepositoryMock.GetAllAsync().Returns(projects);
            var query = new GetAllProjectsQueryFactory().Generate();
            // Act
            var actual = await getAllProjectsQueryHandler.Handle(query, new CancellationToken());
            //Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, projects.Count);
            _ = projectQueryRepositoryMock.Received().GetAllAsync();
        }

    }

}