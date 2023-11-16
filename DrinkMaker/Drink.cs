public class Drink
{
    public string Name { get; set; }
    public string Color { get; set; }
    public double Temperature { get; set; }
    public bool IsCarbonated { get; set; }
    public int Calories { get; set; }

    // Basic constructor
    public Drink(string name, string color, double temp, bool isCarb, int calories)
    {
        Name = name;
        Color = color;
        Temperature = temp;
        IsCarbonated = isCarb;
        Calories = calories;
    }

    // Method to show information about the drink
    public virtual void ShowDrink()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Temperature: {Temperature}°C");
        Console.WriteLine($"Is Carbonated: {IsCarbonated}");
        Console.WriteLine($"Calories: {Calories}\n");
    }
}

