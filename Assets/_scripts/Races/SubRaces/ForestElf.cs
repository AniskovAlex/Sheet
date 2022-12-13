using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestElf : SubRace
{
    public ForestElf()
    {
        id = 2;
        name = "Лесной эльф";
        LoadAbilities("ForestElf");
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return new HashSet<Weapon.BladeType>() { Weapon.BladeType.LongSword, Weapon.BladeType.ShortSword, Weapon.BladeType.ShortBow, Weapon.BladeType.LongBow };
    }
}
