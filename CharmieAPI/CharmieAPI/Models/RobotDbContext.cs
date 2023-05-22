using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CharmieAPI.Models;

public partial class RobotDbContext : IdentityUserContext<IdentityUser>
{
    public RobotDbContext()
    {
    }

    public RobotDbContext(DbContextOptions<RobotDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Environment> Environments { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<QuantityMaterial> QuantityMaterials { get; set; }

    public virtual DbSet<Robot> Robots { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskRobot> TasksRobots { get; set; }

    public virtual DbSet<Identity> Identities { get; set; }

    public virtual DbSet<Warning> Warnings { get; set; }

}