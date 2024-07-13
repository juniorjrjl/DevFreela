using MediatR;

namespace DevFreela.Application.Commands.FinishProject;

public record FinishProjectCommand(int Id) : IRequest;
