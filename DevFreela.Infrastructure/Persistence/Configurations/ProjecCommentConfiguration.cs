using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
    {
        public void Configure(EntityTypeBuilder<ProjectComment> builder)
        {
            builder.ToTable("PROJECTS_COMMENTS");

            builder.Property(p=> p.Id)
                .HasColumnName("id");
            builder.HasKey(p => p.Id);

            builder.Property(p=> p.Comment)
                .HasColumnName("comment");

            builder.Property(p=> p.ProjectId)
                .HasColumnName("project_id");
            builder.HasOne(p => p.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p=> p.ProjectId);

            builder.Property(p=> p.UserId)
                .HasColumnName("user_id");
            builder.HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p=> p.UserId);

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");

        }
    }
}