namespace DevFreela.Core.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string comment, int projectId, int userId)
        {
            Comment = comment;
            ProjectId = projectId;
            UserId = userId;
            CreatedAt = DateTime.Now;
        }

        public string Comment { get; private set; }

        public int ProjectId { get; private set; }

        public int UserId { get; private set; }

        public DateTime CreatedAt { get; private set; }

    }
}