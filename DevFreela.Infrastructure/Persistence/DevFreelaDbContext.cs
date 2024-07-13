using System.Reflection;
using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext: DbContext
{
    public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options) : base(options){ }

    public DbSet<Project> Projects { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Skill> Skills { get; set; }

    public DbSet<ProjectComment> ProjectsComments { get; set; }

    public DbSet<UserSkill> UsersSkills { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRole> UsersRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }

}
