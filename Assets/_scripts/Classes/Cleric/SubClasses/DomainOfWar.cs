using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainOfWar : PlayerSubClass
{
    public DomainOfWar()
    {
        id = 2;
        name = "Домен войны";
        LoadAbilities("DomainOfWar");
    }
    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Heavy, Armor.ArmorType.Shield };
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist, Weapon.WeaponType.WarDist };

    }
}
