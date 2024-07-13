namespace DevFreela.Core.Entities;

public class UserRole
{

    public UserRole(){}
    public UserRole(int roleId)
    {
        RoleId = roleId;
    }

    public int UserId { get; set; }

    public User User { get; set; }

    public int RoleId { get; set; }

    public Role Role { get; set; }
    
}
