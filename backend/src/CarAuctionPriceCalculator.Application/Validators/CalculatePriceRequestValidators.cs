using CarAuctionPriceCalculator.Application.CarAuction;
using CarAuctionPriceCalculator.Application.Dtos;
using FluentValidation;

namespace CarAuctionPriceCalculator.Application.Validators;
public class CalculatePriceRequestValidators : AbstractValidator<CalculatePriceRequest>
{
    private readonly ICarAuctionService _carAuctionService;

    public CalculatePriceRequestValidators(ICarAuctionService carAuctionService)
    {
        _carAuctionService = carAuctionService;

        RuleFor(x => x.VehiclePrice)
            .GreaterThan(0)
            .WithMessage("Vehicle Price must be greater than 0.")
            .Must(HaveTwoDecimalPlacesOrLess)
            .WithMessage("Vehicle Price must have at most 2 decimal places.");

        RuleFor(x => x.VehicleTypeId)
            .GreaterThan(0)
            .WithMessage("Vehicle Type ID must be greater than 0.");
    }

    private bool HaveTwoDecimalPlacesOrLess(decimal vehiclePrice)
    {
        return decimal.Round(vehiclePrice, 2) == vehiclePrice;
    }
}
