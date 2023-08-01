using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectCommentConfiguration : AbstractEntityTypeConfiguration<ProjectComment>
    {
        public ProjectCommentConfiguration() : base("PROJECTS_COMMENTS") { }

        protected override void ConfigurePrimaryKey(EntityTypeBuilder<ProjectComment> builder)
        {
            builder.HasKey(p => p.Id);
        }

        protected override void ConfigureColumnDefinition(EntityTypeBuilder<ProjectComment> builder)
        {
            builder.Property(p=> p.Id)
                .HasColumnName("id");

            builder.Property(p=> p.Comment)
                .HasColumnName("comment");

            builder.Property(p=> p.ProjectId)
                .HasColumnName("project_id");
            builder.HasOne(p => p.Project)
                .WithMany(p => p.Comments)
                .HasForeignKey(p=> p.ProjectId);

            builder.Property(p=> p.UserId)
                .HasColumnName("user_id");

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");
        }

        protected override void ConfigureForeingKey(EntityTypeBuilder<ProjectComment> builder)
        { 
            builder.HasOne(p => p.User)
                .WithMany(p => p.Comments)
                .HasForeignKey(p=> p.UserId);
        }

    }
}