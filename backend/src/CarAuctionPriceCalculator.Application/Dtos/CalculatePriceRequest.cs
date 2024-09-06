namespace CarAuctionPriceCalculator.Application.Dtos;
public class CalculatePriceRequest
{
    public decimal VehiclePrice { get; set; }
    public int VehicleTypeId { get; set; } = default!;
}
