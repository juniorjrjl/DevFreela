using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.Commands;
using NSubstitute;
using Xunit;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Services;
using Bogus;
using DevFreela.Core.Enums;
using NSubstitute.ExceptionExtensions;

namespace DevFreela.UnitTests.Application.Commands
{
    
    public class CreateUserCommandHandlerTest
    {

        private readonly Faker faker = new("pt_BR");
        private readonly CreateUserCommandHandler createUserCommandHandler;
        private readonly IUserRepository userRepository;
        private readonly IRoleQueryRepository roleQueryRepository;
        private readonly IMapper mapper;
        private readonly IAuthService authService;

        public CreateUserCommandHandlerTest()
        {
            userRepository = Substitute.For<IUserRepository>();
            roleQueryRepository = Substitute.For<IRoleQueryRepository>();
            mapper = Substitute.For<IMapper>();
            authService = Substitute.For<IAuthService>();
            createUserCommandHandler = new CreateUserCommandHandler(userRepository, roleQueryRepository, mapper, authService);
        }

        [Fact]
        public async void InputDataIsOk_Executed_ReturnProject()
        {
            // Arrenge
            var createUserCommand = CreateUserCommandFactory.Instance().Generate();
            var user = UserFactory.Instance().Generate();
            authService.ComputeSha256Hash(createUserCommand.Password).Returns(faker.Random.Uuid().ToString());
            mapper.Map<User>(Arg.Any<CreateUserCommand>()).Returns(user);
            roleQueryRepository.GetByNameAsync(Arg.Any<RoleNameEnum>()).Returns(Task.FromResult(RoleFactory.Instance().Generate()));
            userRepository.AddAsync(user).Returns(Task.FromResult(user));
            // Act
            var actual = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());
            // Assert
            Assert.NotNull(actual);
            _ = authService.ComputeSha256Hash(createUserCommand.Password);
            _ = mapper.Received().Map<User>(Arg.Any<CreateUserCommand>());
            _ = roleQueryRepository.Received(createUserCommand.Roles.Count).GetByNameAsync(Arg.Any<RoleNameEnum>());
            _ = userRepository.Received().AddAsync(Arg.Any<User>());
        }

        [Fact]
        public async void InpuNonStoredRole_Executed_ThrowError()
        {
            // Arrenge
            var createUserCommand = CreateUserCommandFactory.Instance().Generate();
            var user = UserFactory.Instance().Generate();
            var exRole = new ArgumentNullException("Role não encontrada");
            authService.ComputeSha256Hash(createUserCommand.Password).Returns(faker.Random.Uuid().ToString());
            mapper.Map<User>(Arg.Any<CreateUserCommand>()).Returns(user);
            roleQueryRepository.GetByNameAsync(Arg.Any<RoleNameEnum>()).ThrowsAsync(exRole);
            // Act
            var actual = await Assert.ThrowsAsync<ArgumentNullException>(() => createUserCommandHandler.Handle(createUserCommand, new CancellationToken()));
            // Assert
            Assert.NotNull(actual);
            _ = authService.ComputeSha256Hash(createUserCommand.Password);
            _ = mapper.Received().Map<User>(Arg.Any<CreateUserCommand>());
            _ = roleQueryRepository.Received().GetByNameAsync(Arg.Any<RoleNameEnum>());
            _ = userRepository.DidNotReceive().AddAsync(Arg.Any<User>());
        }

    }

}
