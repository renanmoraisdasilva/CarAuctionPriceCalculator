using CarAuctionPriceCalculator.Domain.Entities;

namespace CarAuctionPriceCalculator.Application.FeeCalculator;

public class FeeCalculatorService : IFeeCalculatorService
{
    public (decimal calculatedFee, List<Fee> appliedFees) CalculateFee(decimal vehiclePrice, int vehicleTypeId, Fee fee)
    {
        if (fee == null)
            throw new ArgumentNullException(nameof(fee));

        if (IsVehicleTypeMismatch(fee, vehicleTypeId))
            return (0, new List<Fee>());

        var appliedFees = new List<Fee>();
        decimal calculatedFee = 0;

        if (HasFixedAmountWithoutMinPrice(fee))
        {
            appliedFees.Add(fee);
            return (fee.FixedAmount.Value, appliedFees);
        }

        if (HasPercentage(fee))
        {
            calculatedFee = vehiclePrice * fee.Percentage.Value;
            appliedFees.Add(fee);
        }

        if (IsBelowMinFee(fee, calculatedFee))
            calculatedFee = fee.MinFeeAmount.Value;


        if (IsAboveMaxFee(fee, calculatedFee))
            calculatedFee = fee.MaxFeeAmount.Value;


        if (IsWithinPriceRange(fee, vehiclePrice) && HasFixedAmount(fee))
        {
            calculatedFee = fee.FixedAmount.Value;
            appliedFees.Add(fee);
        }

        return (calculatedFee, appliedFees);
    }

    public (decimal totalFee, List<(Fee fee, decimal calculatedFee)> feeDetails) CalculateFees(decimal vehiclePrice, int vehicleTypeId, List<Fee> fees)
    {
        decimal totalFee = 0;
        var feeDetails = new List<(Fee fee, decimal calculatedFee)>();

        foreach (var fee in fees)
        {
            var (calculatedFee, appliedFees) = CalculateFee(vehiclePrice, vehicleTypeId, fee);
            if (calculatedFee == 0)
                continue;

            calculatedFee = Math.Round(calculatedFee, 2);
            totalFee += calculatedFee;
            feeDetails.Add((fee, calculatedFee));
        }

        totalFee = Math.Round(totalFee, 2);
        return (totalFee, feeDetails);
    }

    private bool IsVehicleTypeMismatch(Fee fee, int vehicleTypeId)
    {
        return fee.VehicleType != null && fee.VehicleType.Id != vehicleTypeId;
    }

    private bool HasFixedAmountWithoutMinPrice(Fee fee)
    {
        return fee.FixedAmount.HasValue && !fee.MinPriceAmount.HasValue;
    }

    private bool HasPercentage(Fee fee)
    {
        return fee.Percentage.HasValue;
    }

    private bool IsBelowMinFee(Fee fee, decimal calculatedFee)
    {
        return fee.MinFeeAmount.HasValue && calculatedFee < fee.MinFeeAmount.Value;
    }

    private bool IsAboveMaxFee(Fee fee, decimal calculatedFee)
    {
        return fee.MaxFeeAmount.HasValue && calculatedFee > fee.MaxFeeAmount.Value;
    }

    private bool IsWithinPriceRange(Fee fee, decimal vehiclePrice)
    {
        return fee.MinPriceAmount.HasValue && vehiclePrice > fee.MinPriceAmount &&
               (vehiclePrice < fee.MaxPriceAmount || !fee.MaxPriceAmount.HasValue);
    }

    private bool HasFixedAmount(Fee fee)
    {
        return fee.FixedAmount.HasValue;
    }
}