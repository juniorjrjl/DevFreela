using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;

public record DeleteProjectCommand(int Id) : IRequest;
