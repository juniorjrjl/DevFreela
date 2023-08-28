using System.Net;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public record CreateUserCommand(
        string Name, 
        string Email, 
        DateTime BirthDate, 
        string Password, 
        ICollection<RoleNameEnum> Roles, 
        ICollection<int>? SkillsId
    ) : IRequest<User>;

}
