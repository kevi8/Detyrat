class Program
{
    static void Main()
    {
        // Create instances of each class
        Soda cola = new Soda("Cola", "Brown", 5.0, false, 150);
        Coffee americano = new Coffee("Americano", "Black", 70.0, "Medium", "Arabica", 5);
        Wine redWine = new Wine("Red Wine", "Red", 18.0, "France", 2019, 120);

        // Create a List of Drinks and add all instances to it
        List<Drink> AllBeverages = new List<Drink> { cola, americano, redWine };

        // Loop through the List and call ShowDrink() for each
        Console.WriteLine("Showing Drink Information:");
        foreach (var beverage in AllBeverages)
        {
            beverage.ShowDrink();
        }
    }
}