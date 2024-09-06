using CarAuctionPriceCalculator.Domain.Entities;
using CarAuctionPriceCalculator.Domain.Repositories;
using Moq;
using CarAuctionPriceCalculator.Application.FeeCalculator;

namespace CarAuctionPriceCalculator.Application.CarAuction.Tests;

[TestClass]
public class CarAuctionServiceTests
{
    private Mock<ICarAuctionRepository>? _mockCarAuctionRepository;
    private Mock<IFeeCalculatorService>? _mockFeeCalculator;
    private CarAuctionService? _carAuctionService;

    [TestInitialize]
    public void Setup()
    {
        _mockCarAuctionRepository = new Mock<ICarAuctionRepository>();
        _mockFeeCalculator = new Mock<IFeeCalculatorService>();
        _carAuctionService = new CarAuctionService(_mockCarAuctionRepository.Object, _mockFeeCalculator.Object);
    }

    [DataTestMethod]
    [DataRow(398.0, 1, 550.76)]
    [DataRow(501.0, 1, 671.02)]
    [DataRow(57.0, 1, 173.14)]
    [DataRow(1800.0, 2, 2167.00)]
    [DataRow(1100.0, 1, 1287.00)]
    [DataRow(1000000.0, 2, 1040320.00)]

    public async Task CalculatePriceAsync_ShouldReturnCorrectPriceAndFeeDetails(double vehiclePrice, int vehicleTypeId, double expectedPrice)
    {
        // Arrange
        var vehiclePriceDecimal = (decimal)vehiclePrice;
        var expectedPriceDecimal = (decimal)expectedPrice;
        var expectedCalculatedFee = expectedPriceDecimal - vehiclePriceDecimal;
        var fees = new List<Fee>
        {
            new Fee { Id = 1, Percentage = 0.1m, VehicleTypeId = vehicleTypeId }
        };
        var feeDetails = new List<(Fee fee, decimal calculatedFee)>
        {
            (fees[0], expectedCalculatedFee)
        };

        _mockCarAuctionRepository!.Setup(repo => repo.GetFeesAsync()).ReturnsAsync(fees);
        _mockFeeCalculator!.Setup(calc => calc.CalculateFees(vehiclePriceDecimal, vehicleTypeId, fees)).Returns((expectedCalculatedFee, feeDetails));

        // Act
        var (price, feeDetailDtos) = await _carAuctionService!.CalculatePriceAsync(vehiclePriceDecimal, vehicleTypeId);

        // Assert
        Assert.AreEqual(expectedPriceDecimal, price);
        Assert.AreEqual(1, feeDetailDtos.Count);
        Assert.AreEqual(1, feeDetailDtos[0].Id);
        Assert.AreEqual(expectedCalculatedFee, feeDetailDtos[0].CalculatedFee);
    }
}
