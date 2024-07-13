using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers;

public class UserMapper : IUserMapper
{
    public CreateUserCommand ToCommand(NewUserInputModel command) => 
        new (
            command.Name,
            command.Email,
            command.BirthDate,
            command.Password,
            command.Roles,
            command.SkillsId
        );

    public UserLoginCommand ToCommand(LoginInputModel command) => new (command.Login, command.Password);

    public UserViewModel ToGetByIdViewModel(User entity) => new (entity.Id, entity.Name, entity.Email, entity.BirthDate);

    public CredentialViewModel ToLoginViewModel(CredentialDTO dto) => new (dto.Token, dto.ExpiresIn);

    public SavedUserViewModel ToPostViewModel(User entity) => 
        new (
            entity.Id,
            entity.Name,
            entity.Email,
            entity.BirthDate,
            entity.UsersSkills.Select(s => ToSkillViewModel(s.Skill)).ToList(),
            entity.UsersRoles.Select(r => ToRoleViewModel(r.Role)).ToList()
        );

    private static SavedUserSkillViewModel ToSkillViewModel(Skill entity) => new (entity.Id, entity.Description);

    private static SavedUserRoleViewModel ToRoleViewModel(Role entity) => new (entity.Id, entity.Name.ToString());
    
}