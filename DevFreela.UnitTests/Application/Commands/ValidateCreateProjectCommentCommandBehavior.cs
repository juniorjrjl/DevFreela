using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Core.Entities;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands;

public class ValidateCreateProjectCommentCommandBehaviorTest
{
    
    private readonly ValidateCreateProjectCommentCommandBehavior commandBehavior;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserQueryRepository userQueryRepository;
    private readonly RequestHandlerDelegate<ProjectComment> next;
    public ValidateCreateProjectCommentCommandBehaviorTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        userQueryRepository = Substitute.For<IUserQueryRepository>();
        next = Substitute.For<RequestHandlerDelegate<ProjectComment>>();
        commandBehavior = new (unitOfWork);
    }

    [Fact]
    public async Task ComandWithValidsIds_Executed_CallNext()
    {
        // Arrenge
        var request = CreateProjectCommentCommandFactory.Instance().Generate();
        unitOfWork.UserQueryRepository.Returns(userQueryRepository);
        userQueryRepository.GetByIdAsync(request.UserId).Returns(UserFactory.Instance().Generate());
        // Act
        var action = () => commandBehavior.Handle(request, next, new CancellationToken());
        // Assert
        _ = action.Should().NotThrowAsync();
        _ = userQueryRepository.Received(1).GetByIdAsync(Arg.Any<int>());
        _ = await next.Received(1).Invoke();
    }

    [Fact]
    public async Task ComandWithInalidUserdId_Executed_ThrowError()
    {
        // Arrenge
        var request = CreateProjectCommentCommandFactory.Instance().Generate();
        unitOfWork.UserQueryRepository.Returns(userQueryRepository);
        userQueryRepository.GetByIdAsync(request.UserId).ThrowsAsync(new NotFoundException());
        // Act
        var action = () => commandBehavior.Handle(request, next, new CancellationToken());
        // Assert
        _ = action.Should().NotThrowAsync();
        _ = userQueryRepository.Received(1).GetByIdAsync(Arg.Any<int>());
        _ = await next.DidNotReceive().Invoke();
    }

}
