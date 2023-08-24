using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public record CreateUserCommand(
        string Name, 
        string Email, 
        DateTime BirthDate, 
        string Password, 
        string Role, 
        ICollection<int>? SkillsId
    ) : IRequest<User>;

}
