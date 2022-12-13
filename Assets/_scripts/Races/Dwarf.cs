using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : Race
{
    public Dwarf()
    {
        id = 1;
        name = "Äגאנפ";
        LoadAbilities("Dwarf");
    }

    public override Ability[] ChooseSubRace(int id)
    {
        switch (id)
        {
            case 1:
                subRace = new RockDwarf();
                break;
            case 2:
                subRace = new HillDwarf();
                break;
            default: return null;
        }
        return subRace.GetAbilities();
    }

    public override int GetSpeed()
    {
        return 25;
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return new HashSet<Weapon.BladeType>() { Weapon.BladeType.BattleAxe, Weapon.BladeType.HandAxe, Weapon.BladeType.LightHammer, Weapon.BladeType.BattleHammer };
    }
}
