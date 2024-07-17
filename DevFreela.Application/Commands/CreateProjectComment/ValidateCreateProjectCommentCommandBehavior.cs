using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment;

public class ValidateCreateProjectCommentCommandBehavior(IUnitOfWork unitOfWork) : IPipelineBehavior<CreateProjectCommentCommand, ProjectComment>
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ProjectComment> Handle(CreateProjectCommentCommand request, RequestHandlerDelegate<ProjectComment> next, CancellationToken cancellationToken)
    {
        await _unitOfWork.UserQueryRepository.GetByIdAsync(request.UserId);
        return await next();
    }
}