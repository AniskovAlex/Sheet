using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    public Fighter()
    {
        name = "Воин";
        LoadAbilities("fighter");
        healthDice = 10;
    }

    public override Ability[] ChooseSubClass(string subClasses)
    {
        switch (subClasses)
        {
            case "Мастер боевых искусств":
                subClass = new MasterOfMartialArt();
                break;
            default:
                return null;
        }
        return subClass.GetAbilities();
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist, Weapon.WeaponType.WarDist, Weapon.WeaponType.Shield };
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Heavy, Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 0, 1};
    }

}
