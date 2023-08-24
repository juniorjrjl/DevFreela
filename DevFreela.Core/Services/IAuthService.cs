using DevFreela.Core.DTOs;

namespace DevFreela.Core.Services
{
    
    public interface IAuthService
    {
        
        CredentialDTO GenerateJwtToken(string email, string role);

        string ComputeSha256Hash(string password);

    }

}
