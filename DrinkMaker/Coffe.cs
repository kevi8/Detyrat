public class Coffee : Drink
{
    public string Roast { get; set; }
    public string BeansType { get; set; }

    // Constructor for Coffee
    public Coffee(string name, string color, double temp, string roast, string beansType, int calories)
        : base(name, color, temp, false, calories)
    {
        Roast = roast;
        BeansType = beansType;
    }

    // Override method to show information about the coffee
    public override void ShowDrink()
    {
        base.ShowDrink();
        Console.WriteLine($"Roast: {Roast}");
        Console.WriteLine($"Beans Type: {BeansType}\n");
    }
}