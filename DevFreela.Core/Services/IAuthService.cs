using DevFreela.Core.DTOs;
using DevFreela.Core.Enums;

namespace DevFreela.Core.Services;

public interface IAuthService
{
    
    CredentialDTO GenerateJwtToken(string email, ICollection<RoleNameEnum> roles);

    string ComputeSha256Hash(string password);

}
