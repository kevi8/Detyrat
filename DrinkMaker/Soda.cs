public class Soda : Drink
{
    public bool IsDiet { get; set; }

    // Constructor for Soda
    public Soda(string name, string color, double temp, bool isDiet, int calories)
        : base(name, color, temp, true, calories)
    {
        IsDiet = isDiet;
    }

    // Override method to show information about the soda
    public override void ShowDrink()
    {
        base.ShowDrink();
        Console.WriteLine($"Is Diet: {IsDiet}\n");
    }
}