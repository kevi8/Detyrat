public class RangedFighter : Enemy
{
    public int Distance { get; private set; } = 5;

    public RangedFighter(string name)
        : base(name)
    {
        Health = 100;
        AttackList.Add(new Attack("Shoot an Arrow", 20));
        AttackList.Add(new Attack("Throw a Knife", 15));
    }

    public void Dash()
    {
        Distance = 20;
        Console.WriteLine($"{Name} dashes, setting Distance to {Distance}.");
    }
}