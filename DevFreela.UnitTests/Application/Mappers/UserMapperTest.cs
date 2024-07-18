using Bogus;
using DevFreela.Application.Mapper;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using FluentAssertions;
using Xunit;

namespace DevFreela.UnitTests.Application.Mappers;

public class UserMapperTest
{
    
    private readonly Faker faker = new("pt_BR");
    private readonly IUserMapper mapper;

    public UserMapperTest()
    {
        mapper = new UserMapper();
    }

    [Fact]
    public void ReceivedCreateUserCommandRolesAndSkills_Executed_ReturnUserEntity()
    {
        // Arrenge
        var command = CreateUserCommandFactory.Instance().Generate();
        var userRoles = UserRoleFactory.Instance().Generate(faker.Random.Int(2, 5));
        var userSkills = UserSkillFactory.Instance().Generate(faker.Random.Int(2, 5));
        // Act
        var actual = mapper.ToEntity(command, userRoles, userSkills);
        // Assert
        ICollection<string> toInclude = ["Name", "Email", "BirthDate", "Password"];
        actual.Should().BeEquivalentTo(command, opt => opt.Including(a => toInclude.Contains(a.Path)));
        actual.UsersRoles.Should().BeEquivalentTo(userRoles);
        actual.UsersSkills.Should().BeEquivalentTo(userSkills);

    }

}