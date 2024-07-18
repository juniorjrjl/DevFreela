using System.Security.Authentication;
using System.Security.Cryptography;
using Bogus;
using DevFreela.Application.Commands.UserLogin;
using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands;

public class UserLoginCommandHandlerTest
{
    
    private readonly Faker faker = new("pt_BR");
    private readonly UserLoginCommandHandler commandHandler;
    private readonly IAuthService authService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserQueryRepository userQueryRepository;

    public UserLoginCommandHandlerTest()
    {
        authService = Substitute.For<IAuthService>();
        unitOfWork = Substitute.For<IUnitOfWork>();
        userQueryRepository = Substitute.For<IUserQueryRepository>();
        commandHandler = new (authService, unitOfWork);
    }


    [Fact]
    public async Task ReceivedValidCredentials_Executed_ReturnTokenInfo()
    {
        // Arrenge
        var command = UserLoginCommandFactory.Instance().Generate();
        var passwordHash = faker.Lorem.Word();
        var entity = UserFactory.Instance().Generate();
        var credentials = CredentialDTOFactory.Instance().Generate();
        authService.ComputeSha256Hash(command.Password).Returns(passwordHash);
        unitOfWork.UserQueryRepository.Returns(userQueryRepository);
        userQueryRepository.GetByEmailAndPasswordAsync(command.Login, passwordHash).Returns(entity);
        authService.GenerateJwtToken(entity.Email, Arg.Any<ICollection<RoleNameEnum>>()).Returns(credentials);
        // Act
        var action = async () => await commandHandler.Handle(command, new CancellationToken());
        // Assert
        _ = await action.Should().NotThrowAsync();
        _ = authService.Received(1).ComputeSha256Hash(command.Password);
        _ = unitOfWork.Received(1).UserQueryRepository;
        _ = userQueryRepository.Received(1).GetByEmailAndPasswordAsync(command.Login, passwordHash);
        _ = authService.Received(1).GenerateJwtToken(entity.Email, Arg.Any<ICollection<RoleNameEnum>>());
    }

    [Fact]
    public async Task ReceivedInvalidCredentials_Executed_ThrowError()
    {
        // Arrenge
        var command = UserLoginCommandFactory.Instance().Generate();
        var passwordHash = faker.Lorem.Word();
        authService.ComputeSha256Hash(command.Password).Returns(passwordHash);
        unitOfWork.UserQueryRepository.Returns(userQueryRepository);
        userQueryRepository.GetByEmailAndPasswordAsync(command.Login, passwordHash).ThrowsAsync(new NotFoundException());
        // Act
        var action = async () => await commandHandler.Handle(command, new CancellationToken());
        // Assert
        _ = await action.Should().ThrowAsync<InvalidCredentialException>().WithMessage("Usuário e/ou senha inválidos");
        _ = authService.Received(1).ComputeSha256Hash(command.Password);
        _ = unitOfWork.Received(1).UserQueryRepository;
        _ = userQueryRepository.Received(1).GetByEmailAndPasswordAsync(command.Login, passwordHash);
        _ = authService.DidNotReceive().GenerateJwtToken(Arg.Any<string>(), Arg.Any<ICollection<RoleNameEnum>>());
    }

}