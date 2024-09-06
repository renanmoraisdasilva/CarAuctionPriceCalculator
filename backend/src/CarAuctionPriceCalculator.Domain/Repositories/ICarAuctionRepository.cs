using CarAuctionPriceCalculator.Domain.Entities;

namespace CarAuctionPriceCalculator.Domain.Repositories;
public interface ICarAuctionRepository
{
    Task<IEnumerable<Fee>> GetFeesAsync();
    Task<IEnumerable<VehicleType>> GetVehicleTypesAsync();
}
