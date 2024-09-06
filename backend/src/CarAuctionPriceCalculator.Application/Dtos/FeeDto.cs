using CarAuctionPriceCalculator.Domain.Entities;

namespace CarAuctionPriceCalculator.Application.Dtos;

public class FeeDto
{
    public int Id { get; set; }
    public FeeType FeeType { get; set; } = default!;
    public decimal CalculatedFee { get; set; }
}

public static class FeeMapper
{
    public static FeeDto ToDto(Fee fee, decimal calculatedFee)
    {
        return new FeeDto
        {
            Id = fee.Id,
            FeeType = fee.FeeType,
            CalculatedFee = calculatedFee
        };
    }
}
