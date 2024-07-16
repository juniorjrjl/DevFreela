namespace DevFreela.Core.Entities;

public class User
{

    protected User(){}
    public User(string name, string email, DateTime birthDate, string password, ICollection<UserRole> usersRoles, ICollection<UserSkill> userSkills)
    {
        Name = name;
        Email = email;
        BirthDate = birthDate;
        CreatedAt = DateTime.Now;
        Active = true;
        Password = password;
        UsersRoles = usersRoles;
        UsersSkills = userSkills;
        
        OwnedProjects = new List<Project>();
        FreelancerProjects = new List<Project>();
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string Email { get; private set; }

    public DateTime BirthDate { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool Active { get; private set; }

    public string Password { get; private set; }

    public virtual ICollection<UserRole> UsersRoles { get; private set; }

    public virtual ICollection<UserSkill> UsersSkills { get; private set; }

    public virtual ICollection<Project> OwnedProjects { get; private set; }

    public virtual ICollection<Project> FreelancerProjects { get; private set; }

    public virtual ICollection<ProjectComment> Comments { get; private set; }

}
