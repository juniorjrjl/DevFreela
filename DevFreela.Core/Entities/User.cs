using System.Collections.Generic;

namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string email, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now;
            Active = true;
            UsersSkills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelancerProjects = new List<Project>();
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Active { get; set; }

        public List<UserSkill> UsersSkills { get; set; }

        public List<Project> OwnedProjects { get; set; }

        public List<Project> FreelancerProjects { get; set; }

        public List<ProjectComment> Comments { get; set; }

    }

}
