using AIR_Wheelly_DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AIR_Wheelly_DAL.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }

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
            en.Property(user => user.Password).IsRequired();
        });
    }

    
}