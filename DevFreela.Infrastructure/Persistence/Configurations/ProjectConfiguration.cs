using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("PROJECTS");
            
            builder.HasKey(p => p.Id);
            builder.Property(p=> p.Id)
                .HasColumnName("id");

            builder.Property(p=> p.Title)
                .HasColumnName("title");

            builder.Property(p=> p.Description)
                .HasColumnName("description");

            builder.Property(p=> p.FreelancerId)
                .HasColumnName("freelancer_id");
            builder.HasOne(p => p.Freelancer)
                .WithMany(p => p.FreelancerProjects)
                .HasForeignKey(p => p.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p=> p.ClientId)
                .HasColumnName("client_id");
            builder.HasOne(p => p.Client)
                .WithMany(p => p.OwnedProjects)
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");

            builder.Property(p=> p.StartedAt)
                .HasColumnName("started_at");

            builder.Property(p=> p.FinishedAt)
                .HasColumnName("finished_at");

            builder.Property(p=> p.TotalCost)
                .HasPrecision(15, 2)
                .HasColumnName("total_cost");

            builder.Property(p=> p.CreatedAt)
                .HasColumnName("create_at");

            builder.Property(p=> p.Status)
                .HasColumnName("status");

        }
    }
}