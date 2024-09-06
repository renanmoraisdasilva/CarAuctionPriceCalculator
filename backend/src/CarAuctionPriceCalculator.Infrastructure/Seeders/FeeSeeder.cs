using CarAuctionPriceCalculator.Domain.Entities;
using CarAuctionPriceCalculator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionPriceCalculator.Infrastructure.Seeders;
internal class FeeSeeder(CarAuctionDbContext dbContext) : IFeeSeeder
{
    public async Task Seed()
    {
        if (dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Fees.Any())
            {
                var feeTypes = getFeeTypes();
                var vehicleTypes = getVehicleTypes();

                await dbContext.FeeTypes.AddRangeAsync(feeTypes);
                await dbContext.VehicleTypes.AddRangeAsync(vehicleTypes);
                await dbContext.SaveChangesAsync();

                var fees = getFees();
                await dbContext.Fees.AddRangeAsync(fees);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<FeeType> getFeeTypes()
    {
        return new List<FeeType>
        {
            new() { Name = "Buyer" },
            new() { Name = "Seller" },
            new() { Name = "Association" },
            new() { Name = "Storage" }
        };
    }

    private IEnumerable<VehicleType> getVehicleTypes()
    {
        return new List<VehicleType>
        {
            new() { Name = "Common" },
            new() { Name = "Luxury" }
        };
    }

    private IEnumerable<Fee> getFees()
    {
        return new List<Fee>
        {
            new ()
            {
                Percentage = 0.1m,
                MinFeeAmount = 10,
                MaxFeeAmount = 50,
                FeeTypeId = 1,
                VehicleTypeId = 1
            },
            new ()
            {
                Percentage = 0.1m,
                MinFeeAmount = 25,
                MaxFeeAmount = 200,
                FeeTypeId = 1,
                VehicleTypeId = 2
            },
            new ()
            {
                Percentage = 0.02m,
                FeeTypeId = 2,
                VehicleTypeId = 1
            },
            new ()
            {
                Percentage = 0.04m,
                FeeTypeId = 2,
                VehicleTypeId = 2
            },
            new ()
            {
                FixedAmount = 5,
                MinPriceAmount = 1,
                MaxPriceAmount = 500,
                FeeTypeId = 3
            },
            new ()
            {
                FixedAmount = 10,
                MinPriceAmount = 500,
                MaxPriceAmount = 1000,
                FeeTypeId = 3
            },
            new ()
            {
                FixedAmount = 15,
                MinPriceAmount = 1000,
                MaxPriceAmount = 3000,
                FeeTypeId = 3
            },
            new ()
            {
                FixedAmount = 20,
                MinPriceAmount = 3000,
                FeeTypeId = 3
            },
            new ()
            {
                FixedAmount = 100,
                FeeTypeId = 4
            }
        };
    }
}