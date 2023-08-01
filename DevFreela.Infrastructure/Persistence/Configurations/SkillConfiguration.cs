using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {

            builder.ToTable("SKILLS");

            builder.HasKey(p => p.Id);
            builder.Property(p=> p.Id)
                .HasColumnName("id");

            builder.Property(p=> p.Description)
                .HasColumnName("description");

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");

            builder.HasMany(p => p.UsersSkills)
                .WithOne()
                .HasForeignKey(p => p.SkillId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}