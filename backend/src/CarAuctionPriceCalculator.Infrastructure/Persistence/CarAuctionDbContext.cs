using CarAuctionPriceCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionPriceCalculator.Infrastructure.Persistence;
internal class CarAuctionDbContext(DbContextOptions<CarAuctionDbContext> options) : DbContext(options)
{
    public DbSet<Fee> Fees { get; set; }
    public DbSet<FeeType> FeeTypes { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Fee>()
            .HasOne(f => f.FeeType)
            .WithMany()
            .HasForeignKey(f => f.FeeTypeId);

        modelBuilder.Entity<Fee>()
            .HasOne(f => f.VehicleType)
            .WithMany()
            .HasForeignKey(f => f.VehicleTypeId);
    }
}
