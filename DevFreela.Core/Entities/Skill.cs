
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

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<UserSkill> UsersSkills { get; set; }
    }
}
