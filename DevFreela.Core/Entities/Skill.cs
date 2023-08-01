
namespace DevFreela.Core.Entities
{
    public class Skill : BaseEntity
    {

        public Skill(string description)
        {
            Description = description;
            CreatedAt = DateTime.Now;
            UsersSkills = new List<UserSkill>();
        }

        public string Description { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public List<UserSkill> UsersSkills { get; private set; }
    }
}
