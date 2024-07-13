using System.Security.Authentication;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UserLogin;


public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, CredentialDTO>
{
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;

    public UserLoginCommandHandler(IAuthService authService, IUnitOfWork unitOfWork)
    {
        _authService = authService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CredentialDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);
        User entity;
        try
        {
            entity = await _unitOfWork.UserQueryRepository.GetByEmailAndPasswordAsync(request.Login, passwordHash);
        }
        catch(NotFoundException ex)
        {
            throw new InvalidCredentialException("Usuário e/ou senha inválidos", ex);
        }
        var roles = entity.UsersRoles.Select(r => r.Role.Name).ToList();
        return _authService.GenerateJwtToken(entity.Email, roles);
    }
}
