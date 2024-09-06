namespace CarAuctionPriceCalculator.Domain.Entities;
public class Fee
{
    public int Id { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? MinPriceAmount { get; set; }
    public decimal? MaxPriceAmount { get; set; }
    public decimal? MinFeeAmount { get; set; }
    public decimal? MaxFeeAmount { get; set; }
    public decimal? FixedAmount { get; set; }
    public FeeType FeeType { get; set; } = default!;
    public VehicleType? VehicleType { get; set; }

    public int FeeTypeId { get; set; }
    public int? VehicleTypeId { get; set; }
}

public class FeeType
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class VehicleType
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
