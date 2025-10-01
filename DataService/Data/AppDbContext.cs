using Entites;
using Microsoft.EntityFrameworkCore;

namespace DataService.Data;

public class AppDbContext : DbContext
{

    public AppDbContext()
    {
        
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Achievements -> Driver (Many To One)
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasOne(a => a.Driver)
                .WithMany(d => d.Achievements)
                .HasForeignKey(a => a.DriverId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}