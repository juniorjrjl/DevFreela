namespace DevFreela.Core.Entities;

public class Skill
{
    #pragma warning disable CS8618
    protected Skill(){}
    #pragma warning restore CS8618
    public Skill(int id, string description, DateTime createdAt, ICollection<UserSkill> usersSkills)
    {
        Id = id;
        Description = description;
        CreatedAt = createdAt;
        UsersSkills = usersSkills;
    }

    public int Id { get; private set; }

    public string Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public virtual ICollection<UserSkill>? UsersSkills { get; private set; }
    
}
