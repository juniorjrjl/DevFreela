using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Role
{

    public Role(){}
    public Role(RoleNameEnum name)
    {
        Name = name;
    }

    public int Id { get; private set; }
    public RoleNameEnum Name { get; private set; }

    public virtual ICollection<UserRole> UsersRoles { get; private set; }
    
}
