using CarAuctionPriceCalculator.Application.Dtos;
using CarAuctionPriceCalculator.Application.FeeCalculator;
using CarAuctionPriceCalculator.Domain.Entities;
using CarAuctionPriceCalculator.Domain.Repositories;

namespace CarAuctionPriceCalculator.Application.CarAuction;
public class CarAuctionService(ICarAuctionRepository carAuctionRepository, IFeeCalculatorService feeCalculator) : ICarAuctionService
{
    public async Task<IEnumerable<Fee>> GetFeesAsync()
    {
        return await carAuctionRepository.GetFeesAsync();
    }

    public async Task<(decimal price, List<FeeDto> feeDetails)> CalculatePriceAsync(decimal vehiclePrice, int vehicleTypeId)
    {
        var fees = await carAuctionRepository.GetFeesAsync();
        var (totalFee, feeDetails) = feeCalculator.CalculateFees(vehiclePrice, vehicleTypeId, fees.ToList());

        var feeDetailDtos = feeDetails.Select(fd => FeeMapper.ToDto(fd.fee, fd.calculatedFee)).ToList();

        return (vehiclePrice + totalFee, feeDetailDtos);
    }

    public async Task<IEnumerable<VehicleType>> GetVehicleTypesAsync()
    {
        return await carAuctionRepository.GetVehicleTypesAsync();
    }

}