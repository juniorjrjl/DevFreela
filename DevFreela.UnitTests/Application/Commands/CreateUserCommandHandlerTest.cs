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
using DevFreela.Application.Mapper;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Core.Exceptions;

namespace DevFreela.UnitTests.Application.Commands;

public class CreateUserCommandHandlerTest
{

    private readonly Faker faker = new("pt_BR");
    private readonly CreateUserCommandHandler createUserCommandHandler;
    private readonly IUnitOfWork unitOfWork;
    private readonly IUserRepository userRepository;
    private readonly IRoleQueryRepository roleQueryRepository;
    private readonly IUserMapper mapper;
    private readonly IAuthService authService;

    public CreateUserCommandHandlerTest()
    {
        unitOfWork = Substitute.For<IUnitOfWork>();
        userRepository = Substitute.For<IUserRepository>();
        roleQueryRepository = Substitute.For<IRoleQueryRepository>();
        mapper = Substitute.For<IUserMapper>();
        authService = Substitute.For<IAuthService>();
        createUserCommandHandler = new CreateUserCommandHandler(unitOfWork, mapper, authService);
    }

    [Fact]
    public async Task InputDataIsOk_Executed_ReturnProject()
    {
        // Arrenge
        var createUserCommand = CreateUserCommandFactory.Instance().Generate();
        var user = UserFactory.Instance()
            .WithUsersRoles(createUserCommand.Roles.Select(r => new UserRole(faker.Random.Int(1, int.MaxValue))).ToList())
            .Generate();
        authService.ComputeSha256Hash(createUserCommand.Password).Returns(faker.Random.Uuid().ToString());
        mapper.ToEntity(Arg.Any<CreateUserCommand>(), Arg.Any<ICollection<UserRole>>(), Arg.Any<ICollection<UserSkill>>()).Returns(user);
        unitOfWork.RoleQueryRepository.Returns(roleQueryRepository);
        roleQueryRepository.GetByNameAsync(Arg.Any<RoleNameEnum>()).Returns(Task.FromResult(RoleFactory.Instance().Generate()));
        unitOfWork.UserRepository.Returns(userRepository);
        userRepository.AddAsync(user).Returns(Task.FromResult(user));
        // Act
        var actual = await createUserCommandHandler.Handle(createUserCommand, new CancellationToken());
        // Assert
        Assert.NotNull(actual);
        _ = authService.ComputeSha256Hash(createUserCommand.Password);
        _ = mapper.Received().ToEntity(Arg.Any<CreateUserCommand>(), Arg.Any<ICollection<UserRole>>(), Arg.Any<ICollection<UserSkill>>());
        _ = unitOfWork.Received().RoleQueryRepository;
        _ = roleQueryRepository.Received(createUserCommand.Roles.Count).GetByNameAsync(Arg.Any<RoleNameEnum>());
        _ = unitOfWork.Received().UserRepository;
        _ = userRepository.Received().AddAsync(Arg.Any<User>());
        _ = unitOfWork.Received().BeginTransactionAsync();
        _ = unitOfWork.Received().CommitAsync();
        _ = unitOfWork.DidNotReceive().RollBackAsync();
    }

    [Fact]
    public async Task InputNonStoredRole_Executed_ThrowError()
    {
        // Arrenge
        var createUserCommand = CreateUserCommandFactory.Instance().Generate();
        ArgumentNullException.ThrowIfNull(createUserCommand.SkillsId, "Factory must generate SkillsId");
        var user = UserFactory.Instance()
            .WithUsersRoles(createUserCommand.Roles.Select(r => new UserRole(faker.Random.Int(1, int.MaxValue))).ToList())
            .WithUsersSkills(createUserCommand.SkillsId.Select(s => new UserSkill(s)).ToList())
            .Generate();
        var exRole = new NotFoundException("Role não encontrada");
        authService.ComputeSha256Hash(createUserCommand.Password).Returns(faker.Random.Uuid().ToString());
        mapper.ToEntity(Arg.Any<CreateUserCommand>(), Arg.Any<ICollection<UserRole>>(), Arg.Any<ICollection<UserSkill>>()).Returns(user);
        unitOfWork.RoleQueryRepository.Returns(roleQueryRepository);
        roleQueryRepository.GetByNameAsync(Arg.Any<RoleNameEnum>()).ThrowsAsync(exRole);
        // Act
        var actual = await Assert.ThrowsAsync<NotFoundException>(() => createUserCommandHandler.Handle(createUserCommand, new CancellationToken()));
        // Assert
        Assert.NotNull(actual);
        _ = unitOfWork.Received().BeginTransactionAsync();
        _ = unitOfWork.Received().RollBackAsync();
        _ = unitOfWork.DidNotReceive().CommitAsync();
        _ = authService.ComputeSha256Hash(createUserCommand.Password);
        _ = unitOfWork.Received().RoleQueryRepository;
        _ = roleQueryRepository.Received().GetByNameAsync(Arg.Any<RoleNameEnum>());
        _ = mapper.DidNotReceive().ToEntity(Arg.Any<CreateUserCommand>(), Arg.Any<ICollection<UserRole>>(), Arg.Any<ICollection<UserSkill>>());
        _ = unitOfWork.DidNotReceive().UserRepository;
        _ = userRepository.DidNotReceive().AddAsync(Arg.Any<User>());
    }

}
