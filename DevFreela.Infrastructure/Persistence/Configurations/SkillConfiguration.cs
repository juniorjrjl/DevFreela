using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class SkillConfiguration : AbstractEntityTypeConfiguration<Skill>
    {
        public SkillConfiguration() : base("SKILLS") { }

        protected override void ConfigurePrimaryKey(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(p => p.Id);
        }

        protected override void ConfigureColumnDefinition(EntityTypeBuilder<Skill> builder)
        {
            builder.Property(p=> p.Id)
                .HasColumnName("id");

            builder.Property(p=> p.Description)
                .HasColumnName("description");

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");
        }

        protected override void ConfigureForeingKey(EntityTypeBuilder<Skill> builder)
        {
            builder.HasMany(p => p.UsersSkills)
                .WithOne()
                .HasForeignKey(p => p.SkillId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}