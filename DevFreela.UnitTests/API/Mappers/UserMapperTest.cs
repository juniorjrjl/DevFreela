using Bogus;
using DevFreela.API.Mappers;
using DevFreela.UnitTests.Factories.Commands;
using DevFreela.UnitTests.Factories.Entities;
using DevFreela.UnitTests.Factories.InputModel;
using FluentAssertions;
using Xunit;

namespace DevFreela.UnitTests.API.Mappers;

public class UserMapperTest
{
    
    private readonly Faker faker = new("pt_BR");
    private readonly IUserMapper mapper;

    public UserMapperTest()
    {
        mapper = new UserMapper();
    }

    [Fact]
    public void ReceivedNewUserInputModel_Executed_ReturnCreateUserCommand()
    {
        // Arrenge
        var inputModel = NewUserInputModelFactory.Instance().Generate();
        // Act
        var actual = mapper.ToCommand(inputModel);
        // Assert
        actual.Should().BeEquivalentTo(inputModel, opt => opt.Excluding(a => a.PasswordConfirm));
    }

    [Fact]
    public void ReceivedLoginInputModel_Executed_ReturnUserLoginCommand()
    {
        // Arrenge
        var inputModel = LoginInputModelFactory.Instance().Generate();
        // Act
        var actual = mapper.ToCommand(inputModel);
        // Assert
        actual.Should().BeEquivalentTo(inputModel);
    }

    [Fact]
    public void ReceivedUserEntity_Executed_ReturnUserViewModel()
    {
        // Arrenge
        var entity = UserFactory.Instance().Generate();
        // Act
        var actual = mapper.ToGetByIdViewModel(entity);
        // Assert
        ICollection<string> toInclude = ["Id", "Name", "Email", "BirthDate"];
        actual.Should().BeEquivalentTo(entity, opt => opt.Including(a => toInclude.Contains(a.Path)));
    }

    [Fact]
    public void ReceivedCredentialDTO_Executed_ReturnCredentialViewModel()
    {
        // Arrenge
        var dto = CredentialDTOFactory.Instance().Generate();
        // Act
        var actual = mapper.ToLoginViewModel(dto);
        // Assert
        actual.Should().BeEquivalentTo(dto);
    }

    [Fact]
    public void ReceivedUserEntity_Executed_ReturnSavedUserViewModel()
    {
        // Arrenge
        var entity = UserFactory.Instance().Generate();
        // Act
        var actual = mapper.ToPostViewModel(entity);
        // Assert
        ICollection<string> toInclude = ["Id", "Name", "Email", "BirthDate", "Skills", "Roles"];
        actual.Should().BeEquivalentTo(entity, opt => opt.Including(a => toInclude.Contains(a.Path)));  
    }

}