using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    public record CreateUserCommand(string Name, string Email, DateTime BirthDate, List<int>? SkillsId) : IRequest<User>;

}
