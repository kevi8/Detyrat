class Program
{
    static void Main()
    {
        // Create an instance of an Enemy
        Enemy bandit = new Enemy("Bandit");

        // Create 3 instances of Attacks
        Attack fireball = new Attack("Fireball", 20);
        Attack punch = new Attack("Punch", 15);
        Attack throwAttack = new Attack("Throw", 25);

        // Add the three Attacks to the Enemy's AttackList
        bandit.AttackList.Add(fireball);
        bandit.AttackList.Add(punch);
        bandit.AttackList.Add(throwAttack);

        // Call the RandomAttack method to test random attacks
        Console.WriteLine("Testing Random Attacks:");
        bandit.RandomAttack();
        bandit.RandomAttack();
        bandit.RandomAttack();
    }
}