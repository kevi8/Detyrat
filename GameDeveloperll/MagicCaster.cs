public class MagicCaster : Enemy
{
    public MagicCaster(string name)
        : base(name)
    {
        Health = 80;
        AttackList.Add(new Attack("Fireball", 25));
        AttackList.Add(new Attack("Lightning Bolt", 20));
        AttackList.Add(new Attack("Staff Strike", 10));
    }

    public void Heal(Enemy target)
    {
        target.Health = Math.Min(target.Health + 40, target.MaxHealth);
        Console.WriteLine($"{Name} heals {target.Name} for 40 health. {target.Name}'s health is now {target.Health}.");
    }
}