public class Enemy
{
    // Fields
    public string Name { get; set; }
    public int Health { get; private set; }
    public List<Attack> AttackList { get; private set; }

    // Constructor
    public Enemy(string name)
    {
        Name = name;
        Health = 100; // Default health value
        AttackList = new List<Attack>();
    }

    // Method to perform a random attack
    public void RandomAttack()
    {
        if (AttackList.Count == 0)
        {
            Console.WriteLine($"{Name} has no attacks available.");
            return;
        }

        Random random = new Random();
        int randomIndex = random.Next(AttackList.Count);
        Attack randomAttack = AttackList[randomIndex];

        Console.WriteLine($"{Name} performs {randomAttack.Name} and deals {randomAttack.DamageAmount} damage!");
    }
}