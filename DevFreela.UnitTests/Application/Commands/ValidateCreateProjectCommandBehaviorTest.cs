using DevFreela.Application.Commands.CreateProject;
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

public class ValidateCreateProjectCommandBehaviorTest
{
    
    private readonly ValidateCreateProjectCommandBehavior commandBehavior;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserQueryRepository userQueryRepository;
    private readonly RequestHandlerDelegate<Project> next;
    public ValidateCreateProjectCommandBehaviorTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        userQueryRepository = Substitute.For<IUserQueryRepository>();
        next = Substitute.For<RequestHandlerDelegate<Project>>();
        commandBehavior = new (unitOfWork);
    }

    [Fact]
    public async Task ComandWithValidsIds_Executed_CallNext()
    {
        // Arrenge
        var request = CreateProjectCommandFactory.Instance().Generate();
        unitOfWork.UserQueryRepository.Returns(userQueryRepository);
        userQueryRepository.GetByIdAsync(request.ClientId).Returns(UserFactory.Instance().Generate());
        userQueryRepository.GetByIdAsync(request.FreelancerId).Returns(UserFactory.Instance().Generate());
        // Act
        var action = () => commandBehavior.Handle(request, next, new CancellationToken());
        // Assert
        _ = action.Should().NotThrowAsync();
        _ = userQueryRepository.Received(2).GetByIdAsync(Arg.Any<int>());
        _ = await next.Received(1).Invoke();
    }

    [Fact]
    public async Task ComandWithInalidFreelancer_Executed_ThrowError()
    {
        // Arrenge
        var request = CreateProjectCommandFactory.Instance().Generate();
        unitOfWork.UserQueryRepository.Returns(userQueryRepository);
        userQueryRepository.GetByIdAsync(request.ClientId).Returns(UserFactory.Instance().Generate());
        userQueryRepository.GetByIdAsync(request.FreelancerId).ThrowsAsync(new NotFoundException());
        // Act
        var action = () => commandBehavior.Handle(request, next, new CancellationToken());
        // Assert
        _ = action.Should().NotThrowAsync();
        _ = userQueryRepository.Received(2).GetByIdAsync(Arg.Any<int>());
        _ = await next.DidNotReceive().Invoke();
    }

    [Fact]
    public async Task ComandWithInalidClient_Executed_ThrowError()
    {
        // Arrenge
        var request = CreateProjectCommandFactory.Instance().Generate();
        unitOfWork.UserQueryRepository.Returns(userQueryRepository);
        userQueryRepository.GetByIdAsync(request.ClientId).ThrowsAsync(new NotFoundException());
        userQueryRepository.GetByIdAsync(request.FreelancerId).Returns(UserFactory.Instance().Generate());
        // Act
        var action = () => commandBehavior.Handle(request, next, new CancellationToken());
        // Assert
        _ = action.Should().NotThrowAsync();
        _ = userQueryRepository.Received(1).GetByIdAsync(Arg.Any<int>());
        _ = await next.DidNotReceive().Invoke();
    }

}
