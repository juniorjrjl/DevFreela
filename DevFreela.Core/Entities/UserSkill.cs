
namespace DevFreela.Core.Entities
{
    public class UserSkill
    {

        public int UserId { get; set;}

        public virtual User User { get; set; }

        public int SkillId { get; set;}

        public virtual Skill Skill { get; set; }
        
    }

}
