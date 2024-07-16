using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<StartProjectCommand>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(StartProjectCommand command, CancellationToken cancellationToken)
    {
        var toUpdate = await _unitOfWork.ProjectQueryRepository.GetByIdAsync(command.Id);
        toUpdate.Start();
        await _unitOfWork.ProjectRepository.UpdateAsync(toUpdate);
        await _unitOfWork.CompleteAsync();
        return;
    }
    
}

