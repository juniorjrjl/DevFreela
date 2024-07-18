using DevFreela.Application.Mapper;
using DevFreela.UnitTests.Factories.Commands;
using FluentAssertions;
using Xunit;

namespace DevFreela.UnitTests.Application.Mappers;

public class ProjectCommentMapperTest
{
    private readonly IProjectCommentMapper mapper;

    public ProjectCommentMapperTest()
    {
        mapper = new ProjectCommentMapper();
    }

    [Fact]
    public void ReceivedCreateProjectCommand_Executed_ReturnProjectEntity()
    {
        // Arrenge
        var command = CreateProjectCommentCommandFactory.Instance().Generate();
        // Act
        var actual = mapper.ToEntity(command);
        // Assert
        ICollection<string> toInclude = ["Comment", "ProjectId", "UserId"];
        actual.Should().BeEquivalentTo(command, opt => opt.Including(a => toInclude.Contains(a.Path)));
    }
}