using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class ValidateCreateProjectCommandBehavior(IUnitOfWork unitOfWork) : IPipelineBehavior<CreateProjectCommand, Project>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Project> Handle(CreateProjectCommand request, RequestHandlerDelegate<Project> next, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserQueryRepository.GetByIdAsync(request.ClientId);
        await _unitOfWork.UserQueryRepository.GetByIdAsync(request.FreelancerId);
        return await next();
    }
}