using DevFreela.Application.Mapper;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using FluentAssertions;
using Xunit;

namespace DevFreela.UnitTests.Application.Mappers;

public class ProjectMapperTest
{
    
    private readonly IProjectMapper mapper;

    public ProjectMapperTest()
    {
        mapper = new ProjectMapper();
    }

    [Fact]
    public void ReceivedCreateProjectCommand_Executed_ReturnProjectEntity()
    {
        // Arrenge
        var command = CreateProjectCommandFactory.Instance().Generate();
        // Act
        var actual = mapper.ToEntity(command);
        // Assert
        ICollection<string> toInclude = ["Title", "Description", "ClientId", "FreelancerId", "TotalCost"];
        actual.Should().BeEquivalentTo(command, opt => opt.Including(a => toInclude.Contains(a.Path)));
    }

    [Fact]
    public void ReceivedUpdateProjectCommandAndProjectEntity_Executed_ReturnProjectEntityUpdated()
    {
        // Arrenge
        var command = UpdateProjectCommandFactort.Instance().Generate();
        var entity = ProjectFactory.Instance().Generate();
        // Act
        var actual = mapper.ToEntity(command, entity);
        // Assert
        ICollection<string> toInclude = ["Title", "Description", "TotalCost"];
        actual.Should().BeEquivalentTo(command, opt => opt.Including(a => toInclude.Contains(a.Path)));
    }

}