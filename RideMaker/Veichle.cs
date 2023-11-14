class Vehicle
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