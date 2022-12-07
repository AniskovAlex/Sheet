using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainOfLife : PlayerSubClass
{
    public DomainOfLife()
    {
        id = 3;
        name = "Домен жизни";
        LoadAbilities("DomainOfLife");
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Heavy, Armor.ArmorType.Shield };
    }
}
