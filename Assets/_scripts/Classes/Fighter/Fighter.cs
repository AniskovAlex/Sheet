using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    public Fighter()
    {
        name = "����";
        LoadAbilities("fighter");
        healthDice = 10;
    }

    public override Ability[] ChooseSubClass(string subClasses)
    {
        switch (subClasses)
        {
            case "������ ������ ��������":
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
    public override List<(List<(int, Item)>, List<(int, Item)>)> GetItems()
    {
        List<(List<(int, Item)>, List<(int, Item)>)> list = new List<(List<(int, Item)>, List<(int, Item)>)>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item("����")),
            (1, new Item("�� ����"))
        };
        List<(int, Item)> subList2 = new List<(int, Item)>()
        {
            (1, new Item(-2))
        };
        list.Add((subList1, subList2));
        return list;
    }

}
