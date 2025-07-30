using System;

public class RailwayOperations
{
    public string CalculateArrivalTime(string departureTime, int travelMinutes)
    {
        var time = TimeSpan.Parse(departureTime);
        var arrivalTime = time.Add(TimeSpan.FromMinutes(travelMinutes));

        return arrivalTime.ToString(@"hh\:mm");
    }

    public bool IsCargoOverweight(double cargoWeight, double maxWeight)
    {
        if (cargoWeight < 0 || maxWeight < 0)
            throw new ArgumentException("Weight cannot be negative");

        return cargoWeight > maxWeight;
    }

    public double CalculateShippingCost(double distanceKm, double ratePerKm, double cargoWeight)
    {
        if (distanceKm <= 0 || ratePerKm <= 0 || cargoWeight <= 0)
            throw new ArgumentException("All parameters must be positive");

        double baseCost = distanceKm * ratePerKm * cargoWeight;
        double overloadCoefficient = cargoWeight > 10000 ? 1.15 : 1.0;

        return baseCost * overloadCoefficient;
    }
    public int CalculateNetTravelTime(int totalTravelMinutes, int totalStopMinutes)
    {
        if (totalTravelMinutes < 0 || totalStopMinutes < 0)
            throw new ArgumentException("Время не может быть отрицательным.");
    
        if (totalStopMinutes > totalTravelMinutes)
            throw new ArgumentException("Время остановок не может превышать общее время в пути.");
    
        return totalTravelMinutes - totalStopMinutes;
    }
}
