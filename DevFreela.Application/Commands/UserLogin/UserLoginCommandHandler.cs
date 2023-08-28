using System.Security.Authentication;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;

namespace DevFreela.Application.Commands.UserLogin
{

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, CredentialDTO>
    {
        private readonly IAuthService _authService;
        private readonly IUserQueryRepository _userQueryRepository;

        public UserLoginCommandHandler(IAuthService authService, IUserQueryRepository userQueryRepository)
        {
            _authService = authService;
            _userQueryRepository = userQueryRepository;
        }

        public async Task<CredentialDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.ComputeSha256Hash(request.Password);
            User entity;
            try{
                entity = await _userQueryRepository.GetByEmailAndPasswordAsync(request.Login, passwordHash);
            }catch(ArgumentNullException ex)
            {
                throw new InvalidCredentialException("Usuário e/ou senha inválidos", ex);
            }
            var roles = entity.UsersRoles.Select(r => r.Role.Name).ToList();
            return _authService.GenerateJwtToken(entity.Email, roles);
        }
    }

}
