using CarAuctionPriceCalculator.Application.CarAuction;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using CarAuctionPriceCalculator.Application.FeeCalculator;

namespace CarAuctionPriceCalculator.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICarAuctionService, CarAuctionService>();
        services.AddScoped<IFeeCalculatorService, FeeCalculatorService>();
        services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly)
            .AddFluentValidationAutoValidation();
    }

}
