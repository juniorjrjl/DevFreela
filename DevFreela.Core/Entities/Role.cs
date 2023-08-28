using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Role
    {
        
        public int Id { get; set; }
        public RoleNameEnum Name { get; set; }

        public virtual ICollection<UserRole> UsersRoles { get; set; }
        
    }
}
