
namespace DevFreela.Core.Entities
{
    public class Skill
    {

        public int Id { get; set;}

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<UserSkill> UsersSkills { get; set; }
    }
}
