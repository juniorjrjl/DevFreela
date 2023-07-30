namespace DevFreela.Application.InputModel
{
    public class CreateCommentInputModel
    {
        public CreateCommentInputModel(string content, int projectId, int userId)
        {
            Content = content;
            ProjectId = projectId;
            UserId = userId;
        }

        public string Content { get; private set; }

        public int ProjectId { get; private set; }

        public int UserId { get; private set; }

    }
}