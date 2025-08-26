using CoreLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityLayer.AppDbContext;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Citizen> Citizen { get; set; }

    public virtual DbSet<Dept> Dept { get; set; }

    public virtual DbSet<Emp> Emp { get; set; }

    public virtual DbSet<Group> Group { get; set; }

    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<GroupUser> GroupUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupUser>()
            .ToTable("joinUserGroup")
            .HasKey(g => new { g.UserId, g.GroupId });
        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.User)
            .WithMany(u => u.GroupUsers)
            .HasForeignKey(gu => gu.UserId);

        modelBuilder.Entity<GroupUser>()
            .HasOne(gu => gu.Group)
            .WithMany(g => g.GroupUsers)
            .HasForeignKey(gu => gu.GroupId);

    }
}
