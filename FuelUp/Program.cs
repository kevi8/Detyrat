class Program
{
    static void Main()
    {
        // Create instances of each child (car, horse, bicycle)
        Car car = new Car("Sedan", 4, "Blue", true);
        Horse horse = new Horse("Black Beauty", "Black");
        Bicycle bicycle = new Bicycle("Mountain Bike", "Red");

        // Try to create an instance of Vehicle (commented out since it's abstract)
        // Vehicle vehicle = new Vehicle("Generic Vehicle", "Silver"); // This will result in a compilation error

        // Create a List of Vehicles and put all instances inside it
        List<Vehicle> vehicles = new List<Vehicle> { car, horse, bicycle };

        // Create a List of type INeedFuel, but do not add anything to it yet
        List<INeedFuel> vehiclesWithFuel = new List<INeedFuel>();

        // Loop through the List of Vehicles
        foreach (Vehicle vehicle in vehicles)
        {
            // If an item has the INeedFuel interface applied, add it to the INeedFuel List
            if (vehicle is INeedFuel vehicleWithFuel)
            {
                vehiclesWithFuel.Add(vehicleWithFuel);
            }
        }

        // Loop through the List of type INeedFuel and give each item 10 units of fuel
        foreach (INeedFuel vehicleWithFuel in vehiclesWithFuel)
        {
            vehicleWithFuel.GiveFuel(10);
        }

        // Loop through the List again and print out the Name of the vehicle and how much fuel it has
        foreach (INeedFuel vehicleWithFuel in vehiclesWithFuel)
        {
            Console.WriteLine($"{vehicleWithFuel.GetType().Name} - {vehicleWithFuel.FuelTotal} units of {vehicleWithFuel.FuelType} fuel");
        }
    }
}