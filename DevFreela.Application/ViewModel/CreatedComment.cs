namespace DevFreela.Application.ViewModel
{
    public class CreatedComment
    {
        public CreatedComment(int id, string content, int projectId, int userId)
        {
            Id = id;
            Content = content;
            ProjectId = projectId;
            UserId = userId;
        }

        public int Id { get; private set; }

        public string Content { get; private set; }

        public int ProjectId { get; private set; }

        public int UserId { get; private set; }

    }
}