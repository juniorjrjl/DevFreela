using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("USERS");

            builder.Property(p=> p.Id)
                .HasColumnName("id");
            builder.HasKey(p => p.Id);

            builder.Property(p=> p.Name)
                .HasColumnName("name");

            builder.Property(p=> p.Email)
                .HasColumnName("email");

            builder.Property(p=> p.BirthDate)
                .HasColumnName("bithdate");

            builder.HasMany(p => p.UsersSkills)
                .WithOne()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");

            builder.Property(p=> p.Active)
                .HasColumnName("active");

        }
    }
}