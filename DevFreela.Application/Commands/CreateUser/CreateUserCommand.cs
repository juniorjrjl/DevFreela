using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{
    
    public class CreateUserCommand : IRequest<User>
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public List<int>? SkillsId { get; private set; }
    }

}
