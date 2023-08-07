namespace DevFreela.API.InputModel
{
    public class CredentialInputModel
    {
        public CredentialInputModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

    }
}