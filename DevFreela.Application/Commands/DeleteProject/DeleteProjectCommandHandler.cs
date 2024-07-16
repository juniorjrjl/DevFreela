using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.DeleteProject;


public class DeleteProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectCommand>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
    {
        var toUpdate = await _unitOfWork.ProjectQueryRepository.GetByIdAsync(command.Id);
        toUpdate.Cancel();
        await _unitOfWork.ProjectRepository.UpdateAsync(toUpdate);
        await _unitOfWork.CompleteAsync();
        return;
    }
}
