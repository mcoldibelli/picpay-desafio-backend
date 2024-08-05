using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> User { get; set; }
    public DbSet<Transfer> Transfer { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.Payer)
            .WithMany()
            .HasForeignKey(t => t.PayerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transfer>()
            .HasOne(t => t.Payee)
            .WithMany()
            .HasForeignKey(t => t.PayeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
