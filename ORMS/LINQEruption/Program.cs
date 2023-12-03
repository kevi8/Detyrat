List<Eruption> eruptions = new List<Eruption>()
{
    new Eruption("La Palma", 2021, "Canary Is", 2426, "Stratovolcano"),
    new Eruption("Villarrica", 1963, "Chile", 2847, "Stratovolcano"),
    new Eruption("Chaiten", 2008, "Chile", 1122, "Caldera"),
    new Eruption("Kilauea", 2018, "Hawaiian Is", 1122, "Shield Volcano"),
    new Eruption("Hekla", 1206, "Iceland", 1490, "Stratovolcano"),
    new Eruption("Taupo", 1910, "New Zealand", 760, "Caldera"),
    new Eruption("Lengai, Ol Doinyo", 1927, "Tanzania", 2962, "Stratovolcano"),
    new Eruption("Santorini", 46,"Greece", 367, "Shield Volcano"),
    new Eruption("Katla", 950, "Iceland", 1490, "Subglacial Volcano"),
    new Eruption("Aira", 766, "Japan", 1117, "Stratovolcano"),
    new Eruption("Ceboruco", 930, "Mexico", 2280, "Stratovolcano"),
    new Eruption("Etna", 1329, "Italy", 3320, "Stratovolcano"),
    new Eruption("Bardarbunga", 1477, "Iceland", 2000, "Stratovolcano")
};


Eruption? cErupt = eruptions.FirstOrDefault(p => p.Location == "Chile");


IEnumerable<Eruption> hawaiiIsland = eruptions.Where(h => h.Location == "Hawaiian Is");
Console.WriteLine(hawaiiIsland.FirstOrDefault()!=null?("Hawaiian Is" + hawaiiIsland.First().Volcano): "No Hawaiian Is Eruption found");



IEnumerable<Eruption> nz1900 = eruptions.Where(nz => nz.Location =="New Zealand" && nz.Year > 1900);


IEnumerable<Eruption> over2000 = eruptions.Where(e => e.ElevationInMeters > 2000);


IEnumerable<Eruption> zVolcano = eruptions.Where(z => z.Volcano.StartsWith("Z"));



int maxElevation = eruptions.Max(eruption => eruption.ElevationInMeters);
Eruption? tallestVolcano = eruptions.FirstOrDefault(big => big.ElevationInMeters == maxElevation);



IEnumerable<Eruption> abcVolcano = eruptions.OrderBy(abc=>abc.Volcano).ToList();



IEnumerable<Eruption> oldAbcVolcano = eruptions.Where(a => a.Year < 1000 ).OrderBy(a => a.Volcano);

PrintEach(oldAbcVolcano);


static void PrintEach(IEnumerable<dynamic> items, string msg = "")
{
    Console.WriteLine("\n" + msg);
    foreach (var item in items)
    {
        Console.WriteLine(item.ToString());
    }
}