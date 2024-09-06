using CarAuctionPriceCalculator.Application.Dtos;
using CarAuctionPriceCalculator.Domain.Entities;

namespace CarAuctionPriceCalculator.Application.CarAuction;
public interface ICarAuctionService
{
    Task<IEnumerable<Fee>> GetFeesAsync();
    Task<(decimal price, List<FeeDto> feeDetails)> CalculatePriceAsync(decimal vehiclePrice, int vehicleTypeId);
    Task<IEnumerable<VehicleType>> GetVehicleTypesAsync();
}
