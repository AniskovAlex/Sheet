using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighElf : SubRace
{
    public HighElf()
    {
        id = 1;
        name = "Âûסרטי ‎כüפ";
        LoadAbilities("HighElf");
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return new HashSet<Weapon.BladeType>() { Weapon.BladeType.LongSword, Weapon.BladeType.ShortSword, Weapon.BladeType.ShortBow, Weapon.BladeType.LongBow };
    }
}
