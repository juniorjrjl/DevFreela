using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;

namespace DevFreela.Application.Mapper;

public interface IUserMapper
{
    User ToEntity(CreateUserCommand command, ICollection<UserRole> usersRoles, ICollection<UserSkill> userSkills);

}