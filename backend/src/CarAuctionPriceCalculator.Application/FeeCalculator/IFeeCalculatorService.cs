using CarAuctionPriceCalculator.Domain.Entities;

namespace CarAuctionPriceCalculator.Application.FeeCalculator;
public interface IFeeCalculatorService
{
    (decimal totalFee, List<(Fee fee, decimal calculatedFee)> feeDetails) CalculateFees(decimal vehiclePrice, int vehicleTypeId, List<Fee> fees);
}