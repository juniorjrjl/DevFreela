using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Queries.GetUserById;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Queries;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Queries;


public class GetUserByIdQueryHandlerTest
{

    private readonly GetUserByIdQueryHandler getUserByIdQueryHandler;

    private readonly IUserQueryRepository userQueryRepositoryMock;

    public GetUserByIdQueryHandlerTest()
    {
        userQueryRepositoryMock = Substitute.For<IUserQueryRepository>();
        getUserByIdQueryHandler = new GetUserByIdQueryHandler(userQueryRepositoryMock);
    }

    [Fact]
    public async void HasStoredProject_Executed_ReturnIt()
    {
        // Arrange
        var user = UserFactory.Instance().Generate();
        GetUserByIdQuery query = GetUserByIdQueryFactory.Instance().Generate();
        userQueryRepositoryMock.GetByIdAsync(query.Id).Returns(user);
        // Act
        var actual = await getUserByIdQueryHandler.Handle(query, new CancellationToken());
        // Assert
        Assert.NotNull(actual);
        Assert.Equal(user, actual);
        _ = userQueryRepositoryMock.Received().GetByIdAsync(Arg.Any<int>());
    }

    [Fact]
    public void HasNonStoredProject_Executed_ThrowError()
    {
        // Arrange
        var exception = new NotFoundException();
        GetUserByIdQuery query = GetUserByIdQueryFactory.Instance().Generate();
        userQueryRepositoryMock.GetByIdAsync(query.Id).ThrowsAsync(exception);
        // Act
        var actual = Assert.ThrowsAsync<NotFoundException>(() =>  getUserByIdQueryHandler.Handle(query, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = userQueryRepositoryMock.Received().GetByIdAsync(Arg.Any<int>());
    }

}
