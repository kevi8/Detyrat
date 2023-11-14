class Program
{

    static void Main()
    {
        
        // Create vehicles using different constructors
        Vehicle car1 = new Vehicle("Honda Accord", 5, "Blue", true);
        Vehicle bike1 = new Vehicle("Mountain Bike",1, "Red");
        Vehicle rollerblades = new Vehicle("Rollerblades", "Black");

        // Use the Travel method to update distance traveled
        Random rand = new Random();
        Random randAir = new Random();
        car1.Travel(50);
        bike1.Travel(rand.Next(50,150));
        rollerblades.Travel(randAir.Next(200,500));

        // Create a vehicle using the default constructor (assumed to be a car)
        Vehicle defaultCar = new Vehicle("Generic Car", "Silver");

        // Put all vehicles into a List
        List<Vehicle> vehicles = new List<Vehicle> { car1, bike1, rollerblades, defaultCar };

        // Loop through the List and run ShowInfo() for each vehicle
        foreach (var vehicle in vehicles)
        {
            vehicle.ShowInfo();
        }
    }
}