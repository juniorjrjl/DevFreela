using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {

            builder.ToTable("USERS_SKILLS");

            builder.HasKey(p=> new {p.UserId, p.SkillId});
            builder.Property(p=> p.UserId)
                .HasColumnName("user_id");
            builder.Property(p=> p.SkillId)
                .HasColumnName("skill_id");

        }
    }
}