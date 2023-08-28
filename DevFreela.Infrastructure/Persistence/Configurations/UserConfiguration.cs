using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : AbstractEntityTypeConfiguration<User>
    {
        public UserConfiguration() : base("USERS") { }

        protected override void ConfigurePrimaryKey(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
        }

        protected override void ConfigureColumnDefinition(EntityTypeBuilder<User> builder)
        {
            builder.Property(p=> p.Id)
                .HasColumnName("id");

            builder.Property(p=> p.Name)
                .HasColumnName("name");

            builder.Property(p=> p.Email)
                .HasColumnName("email");

            builder.Property(p=> p.BirthDate)
                .HasColumnName("bithdate");

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");

            builder.Property(p=> p.Active)
                .HasColumnName("active");
            
            builder.Property(p => p.Password)
                .HasColumnName("password");
        }

        protected override void ConfigureForeingKey(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(p => p.UsersSkills)
                .WithOne()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.UsersRoles)
                .WithOne()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}