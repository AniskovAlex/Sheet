using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : PlayersClass
{
    public Barbarian()
    {
        id = 1;
        name = "Варвар";
        LoadAbilities("Barbarian");
        healthDice = 12;
        mainState = 0;
        magic = 0;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new PathOfBerserk();
                break;
            case 2:
                subClass = new PathOfTotemWarrior();
                break;
            default:
                return null;
        }
        int subMagic = subClass.GetMagic();
        if (subMagic != 0)
            magic = subMagic;
        return subClass.GetAbilities();
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist, Weapon.WeaponType.WarDist, Weapon.WeaponType.Shield };
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() {  Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 0, 2 };
    }
    public override List<List<List<(int, Item)>>> GetItems()
    {
        List<List<List<(int, Item)>>> list = new List<List<List<(int, Item)>>>();
        List<List<(int, Item)>> subList = new List<List<(int, Item)>>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item(224))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(-5))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (2, new Item(204))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (2, new Item(-3))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(68)),
            (1, new Item(76)),
            (1, new Item(77)),
            (1, new Item(81)),
            (10, new Item(83)),
            (10, new Item(67)),
            (1, new Item(10)),
            (1, new Item(13)),
            (4, new Item(202))
        };
        subList.Add(subList1);
        list.Add(subList);

        return list;
    }

    int count = 2;
    int k = 4;
    int mult = 10;

    public override int GetMoney()
    {
        int sum = 0;
        for (int i = 0; i < count; i++)
            sum += Random.Range(1, k + 1);
        return sum * mult;
    }
}
