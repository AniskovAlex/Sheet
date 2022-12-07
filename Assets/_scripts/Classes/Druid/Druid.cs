using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : PlayersClass
{
    public Druid()
    {
        id = 4;
        name = "Äðóèä";
        LoadAbilities("Druid");
        healthDice = 8;
        mainState = 4;
        magic = 1;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new CircleOfEarth();
                break;
            case 2:
                subClass = new CircleOfMoon();
                break;
            default: return null;
        }
        return subClass.GetAbilities();
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return new HashSet<Weapon.BladeType>() { Weapon.BladeType.Mace, Weapon.BladeType.Dart, Weapon.BladeType.Club, Weapon.BladeType.Dagger, Weapon.BladeType.Spear, Weapon.BladeType.ThrowingSpear, Weapon.BladeType.WarStaff, Weapon.BladeType.Sling, Weapon.BladeType.Sickle, Weapon.BladeType.Scimitar };
    }

    public override HashSet<Weapon.BladeType> GetSubBladeProficiency()
    {
        return null;
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 3, 4 };
    }
    public override List<List<List<(int, Item)>>> GetItems()
    {
        List<List<List<(int, Item)>>> list = new List<List<List<(int, Item)>>>();
        List<List<(int, Item)>> subList = new List<List<(int, Item)>>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item(195))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(-3))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(225))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(-7))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(184)),
            (1, new Item(68)),
            (1, new Item(76)),
            (1, new Item(77)),
            (1, new Item(81)),
            (10, new Item(83)),
            (10, new Item(67)),
            (1, new Item(10)),
            (1, new Item(13)),
            (1, new Item(-8))
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
