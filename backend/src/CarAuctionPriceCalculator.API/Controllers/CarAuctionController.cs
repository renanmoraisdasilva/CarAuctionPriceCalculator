using CarAuctionPriceCalculator.Application.CarAuction;
using CarAuctionPriceCalculator.Application.Dtos;
using CarAuctionPriceCalculator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionPriceCalculator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarAuctionController(ICarAuctionService carAuctionService) : ControllerBase
{
    [HttpGet("fees")]
    public async Task<IActionResult> GetFeesAsync()
    {
        var fees = await carAuctionService.GetFeesAsync();
        return Ok(fees);
    }

    [HttpGet("vehicleTypes")]
    public async Task<IActionResult> GetVehicleTypesAsync()
    {
        var vehicleTypes = await carAuctionService.GetVehicleTypesAsync();
        return Ok(vehicleTypes);
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> CalculatePriceAsync([FromBody] CalculatePriceRequest request)
    {
        var (price, fees) = await carAuctionService.CalculatePriceAsync(request.VehiclePrice, request.VehicleTypeId);
        return Ok(new { Price = price, Fees = fees });
    }
}
