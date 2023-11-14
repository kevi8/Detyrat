List<string> cars = new List<string>();

cars.Add("volkswagen");
cars.Add("Mercedes");
cars.Add("BMW");
cars.Add("Audi");
cars.Add("Peguot");

int markat = cars.Count();

System.Console.WriteLine(markat);
System.Console.WriteLine(cars[2]);

cars.RemoveAt(2);
int markat2 =cars.Count();
System.Console.WriteLine(markat2);