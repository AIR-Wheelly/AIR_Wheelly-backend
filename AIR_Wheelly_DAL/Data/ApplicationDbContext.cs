using AIR_Wheelly_Common.Models;
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
    public DbSet<Manafacturer> Manafacturers { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<CarListing> CarListings { get; set; }
    public DbSet<CarListingPicture> CarListingPictures { get; set; }
    
    

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
        modelBuilder.Entity<Manafacturer>(en =>
        {
            en.HasKey(manafacturer => manafacturer.Id);
            en.Property(manafacturer => manafacturer.Name).IsRequired().HasMaxLength(50);
        });
        modelBuilder.Entity<Model>(en =>
        {
            en.HasKey(model => model.Id);
            en.Property(model => model.Name).IsRequired().HasMaxLength(50);
            en.HasOne(model => model.Manafacturer).WithMany(model => model.Models).HasForeignKey(model => model.ManafacturerId);
        });
        modelBuilder.Entity<CarListing>(en =>
        {
            en.HasKey(carListing => carListing.Id);
            en.Property(carListing => carListing.Description).IsRequired().HasMaxLength(10000);
            en.HasOne(carListing => carListing.Model).WithMany(carListing => carListing.CarListings).HasForeignKey(carListing => carListing.ModelId);
        });
        modelBuilder.Entity<CarListingPicture>(en =>
        {
            en.HasKey(picture => picture.Id);
            en.HasOne(picture => picture.CarListing).WithMany(picture => picture.CarListingPictures).HasForeignKey(picture => picture.CarListingId);
        });
    }

    
}