using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drow : SubRace
{
    public Drow()
    {
        id = 3;
        name = "Äðîó";
        LoadAbilities("Drow");
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return new HashSet<Weapon.BladeType>() { Weapon.BladeType.Rapier, Weapon.BladeType.ShortSword, Weapon.BladeType.HandedCrossbow};
    }
}
