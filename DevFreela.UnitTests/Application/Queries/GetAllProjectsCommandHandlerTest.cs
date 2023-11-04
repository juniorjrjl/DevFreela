using Bogus;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Queries;
using NSubstitute;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries
{
    
    public class GetAllProjectsCommandHandlerTest
    {

        private readonly Faker faker = new("pt_BR");
        
        private readonly GetAllProjectsQueryHandler getAllProjectsQueryHandler;

        private readonly IProjectQueryRepository projectQueryRepositoryMock;

        public GetAllProjectsCommandHandlerTest()
        {
            projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
            getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectQueryRepositoryMock);
        }

        [Fact]
        public async void HasThreeProjects_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = ProjectFactory.Instance().Generate(3);
            var query = GetAllProjectsQueryFactory.Instance().Generate();
            projectQueryRepositoryMock.GetAllAsync(query.Query).Returns(projects);
            // Act
            var actual = await getAllProjectsQueryHandler.Handle(query, new CancellationToken());
            //Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, projects.Count);
            _ = projectQueryRepositoryMock.Received().GetAllAsync(query.Query);
        }

    }

}