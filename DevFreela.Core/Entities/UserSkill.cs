
namespace DevFreela.Core.Entities;

public class UserSkill
{

    public UserSkill(){}

    public UserSkill(int skillId)
    {
        SkillId = skillId;
    }
    
    public int UserId { get; private set; }

    public virtual User User { get; private set; }

    public int SkillId { get; set;}

    public virtual Skill Skill { get; private set; }
    
}
