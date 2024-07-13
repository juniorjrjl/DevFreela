using Bogus;
using DevFreela.Core.Entities;

namespace DevFreela.UnitTests.Factories.Entities;


public class UserFactory: Faker<User>
{

    private ICollection<UserRole> _usersRoles = new List<UserRole>();
    private ICollection<UserSkill> _usersSkills = new List<UserSkill>();
    private UserFactory()
    {
        Locale = "pt_BR";
        CustomInstantiator
        (p =>
            new
            (
                p.Lorem.Word(),
                p.Internet.Email(),
                p.Date.Recent(),
                p.Lorem.Word(),
                _usersRoles,
                _usersSkills
            )
        );
    }

    

    public static UserFactory Instance() => new();

    public UserFactory WithUsersSkills(ICollection<UserSkill> usersSkills)
    {
        _usersSkills = usersSkills;
        return this;
    }

        public UserFactory WithUsersRoles(ICollection<UserRole> usersRoles)
    {
        _usersRoles = usersRoles;
        return this;
    }

}
