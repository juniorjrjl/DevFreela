using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserSkillConfiguration : AbstractEntityTypeConfiguration<UserSkill>
    {
        public UserSkillConfiguration() : base("USERS_SKILLS") { }

        protected override void ConfigurePrimaryKey(EntityTypeBuilder<UserSkill> builder)
        {
            builder.HasKey(p=> new { p.UserId, p.SkillId });
        }

        protected override void ConfigureColumnDefinition(EntityTypeBuilder<UserSkill> builder)
        {
            builder.Property(p=> p.UserId)
                .HasColumnName("user_id");

            builder.Property(p=> p.SkillId)
                .HasColumnName("skill_id");
        }

        protected override void ConfigureForeingKey(EntityTypeBuilder<UserSkill> builder) { }

    }
}