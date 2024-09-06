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
            .WithMessage("Vehicle Price must be greater than 0.");

        RuleFor(x => x.VehicleTypeId)
            .GreaterThan(0)
            .WithMessage("Vehicle Type ID must be greater than 0.");
    }
}
