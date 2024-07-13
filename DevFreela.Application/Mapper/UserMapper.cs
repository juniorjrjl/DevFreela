using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Mapper;

public class UserMapper : IUserMapper
{
    
    public User ToEntity(CreateUserCommand command, ICollection<UserRole> usersRoles, ICollection<UserSkill> userSkills) => 
    new
    (
        command.Name,
        command.Email,
        command.BirthDate,
        command.Password,
        usersRoles,
        userSkills
    );

}