namespace DevFreela.Application.InputModel
{
    public class CredentialViewModel
    {
        public CredentialViewModel(string token, long expiresIn)
        {
            Token = token;
            ExpiresIn = expiresIn;
        }

        public string Token { get; private set; }

        public long ExpiresIn { get; private set; }

    }
}