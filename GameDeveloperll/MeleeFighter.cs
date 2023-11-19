public class MeleeFighter : Enemy
{
    public MeleeFighter(string name)
        : base(name)
    {
        Health = 120;
        AttackList.Add(new Attack("Punch", 20));
        AttackList.Add(new Attack("Kick", 15));
        AttackList.Add(new Attack("Tackle", 25));
    }

    public void Rage()
    {
        Attack chosenAttack = AttackList[Random.Next(AttackList.Count)];
        chosenAttack.DamageAmount += 10;

        PerformAttack(GetRandomTarget(), chosenAttack);

        // Reset the damage amount to its original value
        chosenAttack.DamageAmount -= 10;
    }

    private Enemy GetRandomTarget()
    {
        throw new NotImplementedException();
    }
}