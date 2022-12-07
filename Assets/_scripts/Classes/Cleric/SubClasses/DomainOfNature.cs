using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainOfNature : PlayerSubClass
{
    public DomainOfNature()
    {
        id = 6;
        name = "Домен природы";
        LoadAbilities("DomainOfNature");
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Heavy, Armor.ArmorType.Shield };
    }
}
