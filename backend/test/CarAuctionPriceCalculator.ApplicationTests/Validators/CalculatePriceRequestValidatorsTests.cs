using CarAuctionPriceCalculator.Application.CarAuction;
using CarAuctionPriceCalculator.Application.Dtos;
using FluentValidation.TestHelper;
using Moq;

namespace CarAuctionPriceCalculator.Application.Validators.Tests;

[TestClass]
public class CalculatePriceRequestValidatorsTests
{
    private CalculatePriceRequestValidators? _validator;
    private Mock<ICarAuctionService>? _mockCarAuctionService;

    [TestInitialize]
    public void Setup()
    {
        _mockCarAuctionService = new Mock<ICarAuctionService>();
        _validator = new CalculatePriceRequestValidators(_mockCarAuctionService.Object);
    }

    [TestMethod]
    public void Should_Have_Error_When_VehiclePrice_Is_Zero()
    {
        var model = new CalculatePriceRequest { VehiclePrice = 0, VehicleTypeId = 1 };
        var result = _validator!.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.VehiclePrice);
    }

    [TestMethod]
    public void Should_Have_Error_When_VehiclePrice_Is_Negative()
    {
        var model = new CalculatePriceRequest { VehiclePrice = -1, VehicleTypeId = 1 };
        var result = _validator!.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.VehiclePrice);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_VehiclePrice_Is_Positive()
    {
        var model = new CalculatePriceRequest { VehiclePrice = 100, VehicleTypeId = 1 };
        var result = _validator!.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.VehiclePrice);
    }

    [TestMethod]
    public void Should_Have_Error_When_VehicleTypeId_Is_Zero()
    {
        var model = new CalculatePriceRequest { VehiclePrice = 100, VehicleTypeId = 0 };
        var result = _validator!.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.VehicleTypeId);
    }

    [TestMethod]
    public void Should_Have_Error_When_VehicleTypeId_Is_Negative()
    {
        var model = new CalculatePriceRequest { VehiclePrice = 100, VehicleTypeId = -1 };
        var result = _validator!.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.VehicleTypeId);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_VehicleTypeId_Is_Positive()
    {
        var model = new CalculatePriceRequest { VehiclePrice = 100, VehicleTypeId = 1 };
        var result = _validator!.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.VehicleTypeId);
    }
}

