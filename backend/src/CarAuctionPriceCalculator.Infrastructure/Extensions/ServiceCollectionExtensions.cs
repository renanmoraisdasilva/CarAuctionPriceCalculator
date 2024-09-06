using CarAuctionPriceCalculator.Domain.Repositories;
using CarAuctionPriceCalculator.Infrastructure.Persistence;
using CarAuctionPriceCalculator.Infrastructure.Repositories;
using CarAuctionPriceCalculator.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuctionPriceCalculator.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarAuctionDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("CarAuctionDb")));

        services.AddScoped<IFeeSeeder, FeeSeeder>();
        services.AddScoped<ICarAuctionRepository, CarAuctionRepository>();
    }
}
