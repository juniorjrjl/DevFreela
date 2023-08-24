using DevFreela.Core.DTOs;
using MediatR;

namespace DevFreela.Application.Commands
{
    
    public record UserLoginCommand(string Login, string Password): IRequest<CredentialDTO>;

}
