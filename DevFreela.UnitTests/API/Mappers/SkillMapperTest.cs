using Bogus;
using DevFreela.API.Mappers;
using DevFreela.UnitTests.Factories.Entities;
using FluentAssertions;
using Xunit;

namespace DevFreela.UnitTests.API.Mappers;

public class SkillMapperTest
{
    
    private readonly Faker faker = new("pt_BR");
    private readonly ISkillMapper mapper;

    public SkillMapperTest()
    {
        mapper = new SkillMapper();
    }

    [Fact]
    public void ReceivedNewProjectInputModel_Executed_ReturnNewProjectCommand()
    {
        // Arrenge
        var entity = SkillFactory.Instance().Generate(faker.Random.Int(1, 5));
        // Act
        var actual = mapper.ToViewModel(entity);
        // Assert
        ICollection<string> toExclude = ["CreatedAt", "UsersSkills"];
        entity.Should().BeEquivalentTo(actual, opt => opt.Excluding(s => toExclude.Contains(s.Path)));
    }

}