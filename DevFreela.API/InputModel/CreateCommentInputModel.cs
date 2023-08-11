namespace DevFreela.API.InputModel
{
    public class CreateCommentInputModel
    {
        public CreateCommentInputModel(string comment, int userId)
        {
            Comment = comment;
            UserId = userId;
        }

        public string Comment { get; private set; }

        public int UserId { get; private set; }

    }
}