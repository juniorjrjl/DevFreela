namespace DevFreela.Core.Entities;

public class ProjectComment
{

    #pragma warning disable CS8618
    protected ProjectComment(){}
    #pragma warning restore CS8618

    public ProjectComment(string content, int projectId, int userId)
    {
        Comment = content;
        ProjectId = projectId;
        UserId = userId;
    }

    public int Id { get; private set; }

    public string Comment { get; private set; }

    public int ProjectId { get; private set; }
    
    public virtual Project? Project { get; private set; }

    public int UserId { get; private set; }

    public virtual User? User { get; private set; }

    public DateTime CreatedAt { get; private set; }

}
