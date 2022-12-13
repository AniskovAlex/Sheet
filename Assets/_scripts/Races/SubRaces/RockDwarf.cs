using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDwarf : SubRace
{
    public RockDwarf()
    {
        id = 1;
        name = "Горный дварф";
        LoadAbilities("RockDwarf");
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium };
    }
}
