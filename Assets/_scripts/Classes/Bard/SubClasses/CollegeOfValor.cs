using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollegeOfValor : PlayerSubClass
{
    public CollegeOfValor()
    {
        id = 1;
        name = "Коллегия доблести";
        LoadAbilities("CollegeOfValor");
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Heavy, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.WarDist };
    }
}
