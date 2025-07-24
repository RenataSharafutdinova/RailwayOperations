using Xunit;
using System;

public class RailwayOperationsTests
{
    private readonly RailwayOperations _railwayOps = new RailwayOperations();

    //CalculateArrivalTime (Рассчитывает время прибытия поезда на основе времени отправления и времени в пути)
    //Позитивный
  [Theory]
    [InlineData("10:00", 120, "12:00")]
    [InlineData("23:30", 120, "01:30")]
    public void CalculateArrivalTime_ValidInput_ReturnsCorrectTime(string departureTime, int travelMinutes, string expectedArrivalTime)
    {
        var result = _railwayOps.CalculateArrivalTime(departureTime, travelMinutes);
        Assert.Equal(expectedArrivalTime, result);
    }
//Негативный
    [Theory]
    [InlineData("25:00", 120)]
    [InlineData("10:00", -30)]
    public void CalculateArrivalTime_InvalidInput_ThrowsException(string departureTime, int travelMinutes)
    {
        Assert.Throws<ArgumentException>(() => _railwayOps.CalculateArrivalTime(departureTime, travelMinutes));
    }
//Граничный 
    [Fact]
    public void CalculateArrivalTime_ZeroTravelTime_ReturnsDepartureTime()
    {
        var result = _railwayOps.CalculateArrivalTime("10:00", 0);
        Assert.Equal("10:00", result);
    }

    // IsCargoOverweight (Проверяет, превышает ли вес груза допустимый лимит)
    [Theory]
    [InlineData(5000, 10000, false)]
    [InlineData(15000, 10000, true)]
    public void IsCargoOverweight_ValidInput_ReturnsCorrectResult(double cargoWeight, double maxWeight, bool expected)
    {
        var result = _railwayOps.IsCargoOverweight(cargoWeight, maxWeight);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1000, 10000)]
    [InlineData(5000, -10000)]
    public void IsCargoOverweight_NegativeWeight_ThrowsException(double cargoWeight, double maxWeight)
    {
        Assert.Throws<ArgumentException>(() => _railwayOps.IsCargoOverweight(cargoWeight, maxWeight));
    }

    [Fact]
    public void IsCargoOverweight_WeightEqualsMax_ReturnsFalse()
    {
        var result = _railwayOps.IsCargoOverweight(10000, 10000);
        Assert.False(result);
    }

    //CalculateShippingCost (Рассчитывает стоимость перевозки груза с учетом перегруза (если вес > 10000, применяется коэффициент 1.15)
    [Theory]
    [InlineData(100, 0.5, 5000, 100 * 0.5 * 5000)]
    [InlineData(100, 0.5, 15000, 100 * 0.5 * 15000 * 1.15)]
    public void CalculateShippingCost_ValidInput_ReturnsCorrectCost(double distanceKm, double ratePerKm, double cargoWeight, double expectedCost)
    {
        var result = _railwayOps.CalculateShippingCost(distanceKm, ratePerKm, cargoWeight);
        Assert.Equal(expectedCost, result);
    }

    [Theory]
    [InlineData(0, 0.5, 5000)]
    [InlineData(100, -0.5, 5000)]
    public void CalculateShippingCost_InvalidInput_ThrowsException(double distanceKm, double ratePerKm, double cargoWeight)
    {
        Assert.Throws<ArgumentException>(() => _railwayOps.CalculateShippingCost(distanceKm, ratePerKm, cargoWeight));
    }

    [Fact]
    public void CalculateShippingCost_WeightAtThreshold_NoOverloadApplied()
    {
        var result = _railwayOps.CalculateShippingCost(100, 0.5, 10000);
        Assert.Equal(100 * 0.5 * 10000, result);
    }

    // CalculateNetTravelTime Чистое время в пути (общее время минус время остановок).
    [Theory]
    [InlineData(300, 60, 240)]
    [InlineData(300, 0, 300)]
    public void CalculateNetTravelTime_ValidInput_ReturnsCorrectTime(int totalTravelMinutes, int totalStopMinutes, int expected)
    {
        var result = _railwayOps.CalculateNetTravelTime(totalTravelMinutes, totalStopMinutes);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-300, 60)]
    [InlineData(300, -60)]
    [InlineData(300, 400)]
    public void CalculateNetTravelTime_InvalidInput_ThrowsException(int totalTravelMinutes, int totalStopMinutes)
    {
        Assert.Throws<ArgumentException>(() => _railwayOps.CalculateNetTravelTime(totalTravelMinutes, totalStopMinutes));
    }
}
