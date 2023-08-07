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

        public string Comment { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}