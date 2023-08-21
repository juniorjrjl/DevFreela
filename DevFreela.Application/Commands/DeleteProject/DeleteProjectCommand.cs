using System.Net;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject
{
    public record DeleteProjectCommand(int Id) : IRequest;

}
