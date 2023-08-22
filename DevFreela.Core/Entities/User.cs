using System.Collections.Generic;

namespace DevFreela.Core.Entities
{
    public class User
    {

        public int Id { get; set;}

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public virtual ICollection<UserSkill> UsersSkills { get; set; }

        public virtual ICollection<Project> OwnedProjects { get; set; }

        public virtual ICollection<Project> FreelancerProjects { get; set; }

        public virtual ICollection<ProjectComment> Comments { get; set; }

    }

}
