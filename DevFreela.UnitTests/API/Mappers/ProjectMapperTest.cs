using Bogus;
using DevFreela.API.Mappers;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.InputModel;
using DevFreela.UnitTests.InputModel.Commands;
using FluentAssertions;
using Xunit;

namespace DevFreela.UnitTests.API.Mappers;

public class ProjectMapperTest
{

    private readonly Faker faker = new("pt_BR");
    private readonly IProjectMapper mapper;

    public ProjectMapperTest()
    {
        mapper = new ProjectMapper();
    }

    [Fact]
    public void ReceivedNewProjectInputModel_Executed_ReturnNewProjectCommand()
    {
        // Arrenge
        var inputModel = NewProjectInputModelFactory.Instance().Generate();
        // Act
        var actual = mapper.ToCommand(inputModel);
        // Assert
        actual.Should().BeEquivalentTo(inputModel);
    }

    [Fact]
    public void ReceivedUpdateProjectInputModel_Executed_ReturnNewProjectCommand()
    {
        // Arrenge
        var inputModel = UpdateProjectInputModelFactory.Instance().Generate();
        var id = faker.Random.Int();
        // Act
        var actual = mapper.ToCommand(inputModel, id);
        // Assert
        inputModel.Should().BeEquivalentTo(actual, opt => opt.Excluding(a => a.Id));
        actual.Id.Should().Be(id);
    }

    [Fact]
    public void ReceivedCreateCommentInputModel_Executed_ReturnCreateProjectCommentCommand()
    {
        // Arrenge
        var inputModel = CreateProjectCommentInputModelFactory.Instance().Generate();
        var projectId = faker.Random.Int();
        // Act
        var actual = mapper.ToCommand(inputModel, projectId);
        // Assert
        inputModel.Should().BeEquivalentTo(actual, opt => opt.Excluding(a => a.ProjectId));
        actual.ProjectId.Should().Be(projectId);
    }

    [Fact]
    public void ReceivedProjectEntity_Executed_ReturnCreatedProjectViewModel()
    {
        // Arrenge
        var entity = ProjectFactory.Instance().Generate();
        // Act
        var actual = mapper.ToCreatedViewModel(entity);
        // Assert
                    
        ICollection<string> propsToUse = ["Id", "Title", "Description", "ClientId", "FreelancerId", "TotalCost"];
        entity.Should().BeEquivalentTo(actual, opt => opt.Including(a => propsToUse.Contains(a.Path)));
    }

    [Fact]
    public void ReceivedProjectEntity_Executed_ReturnProjectDetailsViewModel()
    {
        // Arrenge
        var entity = ProjectFactory.Instance().Generate();
        // Act
        var actual = mapper.ToDetailsViewModel(entity);
        // Assert
        ICollection<string> propsToUse = ["Id", "Title", "Description", "TotalCost", "CreatedAt", "FinishedAt"];
        entity.Should().BeEquivalentTo(actual, opt => opt.Including(a => propsToUse.Contains(a.Path)));
    }

    [Fact]
    public void ReceivedProjectEntity_Executed_ReturnUpdatedProjectViewModel()
    {
        // Arrenge
        var entity = ProjectFactory.Instance().Generate();
        // Act
        var actual = mapper.ToViewModel(entity);
        // Assert
        ICollection<string> propsToUse = ["Id", "Title", "Description", "TotalCost"];
        entity.Should().BeEquivalentTo(actual, opt => opt.Including(a => propsToUse.Contains(a.Path)));
    }

    [Fact]
    public void ReceivedPagedProjectEntity_Executed_ReturnPagedProjectViewModel()
    {
        // Arrenge
        var entity = PaginationResultProjectFactory.Instance().Generate();
        // Act
        var actual = mapper.ToPagedViewModel(entity);
        // Assert
        ICollection<string> propsToUse = ["Page", "TotalPages", "PageSize", "ItemCount", "Data.Id", "Data.Title", "Data.CreatedAt"];
        entity.Should().BeEquivalentTo(actual, opt => opt.Including(a => propsToUse.Contains(a.Path)));
    }

}
