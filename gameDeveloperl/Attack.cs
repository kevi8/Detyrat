public class Attack
{
    // Fields
    public string Name { get; set; }
    public int DamageAmount { get; set; }

    // Constructor
    public Attack(string name, int damageAmount)
    {
        Name = name;
        // Ensure the damage is within the specified range (5 to 25)
        DamageAmount = Math.Clamp(damageAmount, 5, 25);
    }
}