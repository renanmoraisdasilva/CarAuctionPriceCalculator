using CarAuctionPriceCalculator.Domain.Entities;
using CarAuctionPriceCalculator.Domain.Repositories;
using CarAuctionPriceCalculator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarAuctionPriceCalculator.Infrastructure.Repositories;
internal class CarAuctionRepository(CarAuctionDbContext dbContext) : ICarAuctionRepository
{
    public async Task<IEnumerable<Fee>> GetFeesAsync()
    {
        return await dbContext.Fees
            .Include(f => f.FeeType)
            .Include(f => f.VehicleType)
            .ToListAsync();
    }
}
