using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserRoleConfiguration : AbstractEntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration() : base("USERS_ROLES") { }

        protected override void ConfigurePrimaryKey(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(p=> new { p.UserId, p.RoleId });
        }

        protected override void ConfigureColumnDefinition(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(p=> p.UserId)
                .HasColumnName("user_id");

            builder.Property(p=> p.RoleId)
                .HasColumnName("role_id");
        }

        protected override void ConfigureForeingKey(EntityTypeBuilder<UserRole> builder) { 
            builder.HasOne(fk => fk.User)
                .WithMany(fk => fk.UsersRoles)
                .HasForeignKey(fk => fk.UserId);

            builder.HasOne(fk => fk.Role)
                .WithMany(fk => fk.UsersRoles)
                .HasForeignKey(fk => fk.RoleId);
        }

    }
}