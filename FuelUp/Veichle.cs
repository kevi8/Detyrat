public interface INeedFuel
{
    string FuelType { get; set; }
    int FuelTotal { get; set; }
    void GiveFuel(int amount);
}
public abstract class Vehicle
{
    // Fields
    public string Name { get; set; }
    public int Passengers { get; set; }
    public string Color { get; set; }
    public bool HasEngine { get; set; }
    public int DistanceTraveled { get; private set; }

    // Constructors
    public Vehicle(string name, int passengers, string color, bool hasEngine)
    {
        Name = name;
        Passengers = passengers;
        Color = color;
        HasEngine = hasEngine;
        DistanceTraveled = 0;
    }

    public Vehicle(string name, string color)
    {
        Name = name;
        Color = color;
        Passengers = 4; 
        HasEngine = true; 
        DistanceTraveled = 0;
    }
        public Vehicle(string name,int passengers, string color)
    {
        Name = name;
        Color = color;
        Passengers = 1; 
        HasEngine = true; 
        DistanceTraveled = 0;
    }

    // Methods
    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Passengers: {Passengers}");
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Has Engine: {HasEngine}");
        Console.WriteLine($"Distance Traveled: {DistanceTraveled} miles\n");
    }

    public void Travel(int distance)
    {
        DistanceTraveled += distance;
        Console.WriteLine($"{Name} has traveled {distance} miles. Total distance traveled: {DistanceTraveled} miles\n");
    }
}

public class Car : Vehicle, INeedFuel
{
    public string FuelType { get; set; }
    public int FuelTotal { get; set; }

    public Car(string name, int passengers, string color, bool hasEngine, string fuelType = "Gas")
        : base(name, passengers, color, hasEngine)
    {
        FuelType = fuelType;
        FuelTotal = 10;
    }

    public void GiveFuel(int amount)
    {
        FuelTotal += amount;
        Console.WriteLine($"{Name} has been fueled with {amount} units of {FuelType}.");
    }
}

// Horse class
public class Horse : Vehicle, INeedFuel
{
    public string FuelType { get; set; }
    public int FuelTotal { get; set; }

    public Horse(string name, string color)
        : base(name, color)
    {
        FuelType = "Horese Food";
        FuelTotal = 10;
        HasEngine = false; // Horses don't have engines
    }

    public void GiveFuel(int amount)
    {
        FuelTotal += amount;
        Console.WriteLine($"{Name} has been fed with {amount} units of {FuelType}.");
    }
}

// Bicycle class
public class Bicycle : Vehicle
{
    public Bicycle(string name, string color)
        : base(name, color)
    {
        // Bicycles don't need fuel
    }
}