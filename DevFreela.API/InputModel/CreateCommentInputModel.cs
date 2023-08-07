namespace DevFreela.API.InputModel
{
    public class CreateCommentInputModel
    {
        public CreateCommentInputModel(string content, int userId)
        {
            Content = content;
            UserId = userId;
        }

        public string Content { get; private set; }

        public int UserId { get; private set; }

    }
}