public class Enemy
{
    public string Name { get; }
    public int Health { get; set; }
    public List<Attack> AttackList { get; }
    public byte MaxHealth { get; internal set; }

    // Constructor
    public Enemy(string name)
    {
        Name = name;
        Health = 100; // Default health
        AttackList = new List<Attack>();
    }

    // Method to perform a random attack
    public void PerformAttack(Enemy target, Attack chosenAttack)
    {
        if (target.Health > 0)
        {
            target.Health -= chosenAttack.DamageAmount;
            Console.WriteLine($"{Name} attacks {target.Name}, dealing {chosenAttack.DamageAmount} damage and reducing {target.Name}'s health to {target.Health}!!");
        }
        else
        {
            Console.WriteLine($"{target.Name} is already defeated. {Name} cannot attack.");
        }
    }
}