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

    public UserLoginCommand ToCommand(LoginInputModel command) => 
        new 
        (
            command.Login, 
            command.Password
        );

    public UserViewModel ToGetByIdViewModel(User entity) => 
        new 
        (
            entity.Id, 
            entity.Name, 
            entity.Email, 
            entity.BirthDate
        );

    public CredentialViewModel ToLoginViewModel(CredentialDTO dto) => 
        new 
        (
            dto.Token, 
            dto.ExpiresIn
        );

    public SavedUserViewModel ToPostViewModel(User entity) => 
        new (
            entity.Id,
            entity.Name,
            entity.Email,
            entity.BirthDate,
            entity.UsersSkills.Select(s => s.Skill)
                .Where(s => s is not null)
                .Select(s => ToSkillViewModel(s!))
                .ToList(),
            entity.UsersRoles.Select(r => r.Role)
                .Where(r => r is not null)
                .Select(r => ToRoleViewModel(r!))
                .ToList()
        );

    private static SavedUserSkillViewModel ToSkillViewModel(Skill entity) => 
        new 
        (
            entity.Id, 
            entity.Description
        );

    private static SavedUserRoleViewModel ToRoleViewModel(Role entity) => 
        new 
        (
            entity.Id, 
            entity.Name.ToString()
        );
    
}