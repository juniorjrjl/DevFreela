using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : AbstractEntityTypeConfiguration<Role>
    {
        public RoleConfiguration() : base("ROLES") { }

        protected override void ConfigurePrimaryKey(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(p => p.Id);
        }

        protected override void ConfigureColumnDefinition(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p=> p.Id)
                .HasColumnName("id");

            builder.Property(p=> p.Name)
                .HasConversion(new EnumToStringConverter<RoleNameEnum>())
                .HasColumnName("name");
        }

        protected override void ConfigureForeingKey(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(p => p.UsersRoles)
                .WithOne()
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
