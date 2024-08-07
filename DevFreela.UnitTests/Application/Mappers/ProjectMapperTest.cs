using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Utils;
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

    [Fact]
    public void ReceivedProjectEntity_Executed_ReturnProjectCreatedNotification()
    {
        // Arrenge
        var entity = ProjectFactory.Instance().Generate();
        var freelancer = UserFactory.Instance().Generate();
        var client = UserFactory.Instance().Generate();
        entity.SetValueOnPrivateProperty("Client", client);
        entity.SetValueOnPrivateProperty("Freelancer", freelancer);
        // Act
        var actual = mapper.ToPublish(entity);
        // Assert
        actual.Title.Should().Be(entity.Title);
        actual.TotalCost.Should().Be(entity.TotalCost);
        actual.ClientName.Should().Be(entity.Client!.Name);
        actual.ClientEmail.Should().Be(entity.Client.Email);
        actual.FreelancerName.Should().Be(entity.Freelancer!.Name);
        actual.FreelancerEmail.Should().Be(entity.Freelancer.Email);
    }

    [Fact]
    public void ReceivedProjectEntityWithouClient_Executed_ThrowException()
    {
        // Arrenge
        var entity = ProjectFactory.Instance().Generate();
        var freelancer = UserFactory.Instance().Generate();
        entity.SetValueOnPrivateProperty("Freelancer", freelancer);
        // Act
        var actual = () => mapper.ToPublish(entity);
        // Assert
        actual.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ReceivedProjectEntityWithouFreelancer_Executed_ThrowException()
    {
        // Arrenge
        var entity = ProjectFactory.Instance().Generate();
        var client = UserFactory.Instance().Generate();
        entity.SetValueOnPrivateProperty("Client", client);
        // Act
        var actual = () => mapper.ToPublish(entity);
        // Assert
        actual.Should().Throw<ArgumentNullException>();
    }

}