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

        public string Name { get; private set; }

        public string Email { get; private set; }

        public DateTime BirthDate { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public bool Active { get; private set; }

        public List<UserSkill> UsersSkills { get; private set; }

        public List<Project> OwnedProjects { get; private set; }

        public List<Project> FreelancerProjects { get; private set; }

    }

}
