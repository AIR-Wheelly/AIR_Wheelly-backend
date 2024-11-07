using AIR_Wheelly_DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AIR_Wheelly_DAL.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(en =>
        {
            en.HasKey(e => e.Id);
            en.Property(user => user.FirstName).IsRequired().HasMaxLength(50);
            en.Property(user => user.LastName).IsRequired().HasMaxLength(50);
            en.Property(user => user.Email).IsRequired().HasMaxLength(100);
            en.HasIndex(user => user.Email).IsUnique();
            en.Property(user => user.Password).IsRequired().HasMaxLength(20);
        });
    }

    
}