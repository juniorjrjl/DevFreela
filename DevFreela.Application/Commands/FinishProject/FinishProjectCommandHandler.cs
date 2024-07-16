using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.FinishProject;


public class FinishProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<FinishProjectCommand>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(FinishProjectCommand command, CancellationToken cancellationToken)
    {
        var toUpdate = await _unitOfWork.ProjectQueryRepository.GetByIdAsync(command.Id);
        toUpdate.Finish();
        await _unitOfWork.ProjectRepository.UpdateAsync(toUpdate);
        await _unitOfWork.CompleteAsync();
        return;
    }
}