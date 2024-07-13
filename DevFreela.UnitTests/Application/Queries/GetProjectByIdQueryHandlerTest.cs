using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Queries;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries;


public class GetProjectByIdQueryHandlerTest
{

    private readonly GetProjectByIdQueryHandler getProjectByIdQueryHandler;

    private readonly IProjectQueryRepository projectQueryRepositoryMock;

    public GetProjectByIdQueryHandlerTest()
    {
        projectQueryRepositoryMock = Substitute.For<IProjectQueryRepository>();
        getProjectByIdQueryHandler = new GetProjectByIdQueryHandler(projectQueryRepositoryMock);
    }

    [Fact]
    public async void HasStoredProject_Executed_ReturnIt()
    {
        // Arrange
        var project = ProjectFactory.Instance().Generate();
        GetProjectByIdQuery query = GetProjectByIdQueryFactory.Instance().Generate();
        projectQueryRepositoryMock.GetByIdAsync(query.Id).Returns(project);
        // Act
        var actual = await getProjectByIdQueryHandler.Handle(query, new CancellationToken());
        // Assert
        Assert.NotNull(actual);
        Assert.Equal(project, actual);
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(Arg.Any<int>());
    }

    [Fact]
    public void HasNonStoredProject_Executed_ThrowError()
    {
        // Arrange
        var exception = new NotFoundException();
        GetProjectByIdQuery query = GetProjectByIdQueryFactory.Instance().Generate();
        projectQueryRepositoryMock.GetByIdAsync(query.Id).ThrowsAsync(exception);
        // Act
        var actual = Assert.ThrowsAsync<NotFoundException>(() =>  getProjectByIdQueryHandler.Handle(query, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = projectQueryRepositoryMock.Received().GetByIdAsync(Arg.Any<int>());
    }

}
