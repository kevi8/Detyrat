// Create instances of the Melee, Ranged, and Magic Caster classes
MeleeFighter meleeFighter = new MeleeFighter("Melee Fighter");
RangedFighter rangedFighter = new RangedFighter("Ranged Fighter");
MagicCaster magicCaster = new MagicCaster("Magic Caster");

// Perform the Kick Attack from Melee class character on Ranged character
meleeFighter.PerformAttack(rangedFighter, meleeFighter.AttackList.Find(a => a.Name == "Kick"));

// Perform the Rage method from Melee class character on Magic Caster character
meleeFighter.Rage();

// Perform the Shoot an Arrow Attack from Ranged character on Melee character (should not work due to the Distance constraint)
rangedFighter.PerformAttack(meleeFighter, rangedFighter.AttackList.Find(a => a.Name == "Shoot an Arrow"));

// Perform the Dash method from Ranged character
rangedFighter.Dash();

// Perform another Shoot an Arrow Attack from Ranged character (should work now)
rangedFighter.PerformAttack(meleeFighter, rangedFighter.AttackList.Find(a => a.Name == "Shoot an Arrow"));

// Perform a Fireball Attack from Magic Caster on Melee character
magicCaster.PerformAttack(meleeFighter, magicCaster.AttackList.Find(a => a.Name == "Fireball"));

// Perform the Heal method from Magic Caster on Ranged character
magicCaster.Heal(rangedFighter);

// Perform the Heal method from Magic Caster on themselves
magicCaster.Heal(magicCaster);
