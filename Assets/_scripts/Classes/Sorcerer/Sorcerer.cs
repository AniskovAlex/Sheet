using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Sorcerer : PlayersClass
{
    public Sorcerer()
    {
        id = 12;
        name = "Чародей";
        LoadAbilities("Sorcerer");
        healthDice = 6;
        mainState = 5;
        magic = 1;
        magicChange = 1;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new HeritageOfDragonBlood();
                break;
            case 2:
                subClass = new WildMagic();
                break;
            default: return null;
        }
        return subClass.GetAbilities();
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        HashSet<Weapon.BladeType> list = new HashSet<Weapon.BladeType>() { Weapon.BladeType.Dagger, Weapon.BladeType.Dart, Weapon.BladeType.Sling, Weapon.BladeType.WarStaff, Weapon.BladeType.LightCrossbow };
        HashSet<Weapon.BladeType> buf = base.GetBladeProficiency();
        if (buf != null)
            list = new HashSet<Weapon.BladeType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 2, 5 };
    }
    public override List<List<List<(int, Item)>>> GetItems()
    {
        List<List<List<(int, Item)>>> list = new List<List<List<(int, Item)>>>();
        List<List<(int, Item)>> subList = new List<List<(int, Item)>>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item(206)),
            (20, new Item(4))
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
            (1, new Item(52))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(-6))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(68)),
            (1, new Item(42)),
            (1, new Item(54)),
            (10, new Item(96)),
            (10, new Item(83)),
            (1, new Item(81)),
            (10, new Item(67)),
            (1, new Item(10)),
            (1, new Item(13))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(68)),
            (1, new Item(76)),
            (1, new Item(77)),
            (1, new Item(81)),
            (10, new Item(83)),
            (10, new Item(67)),
            (1, new Item(10)),
            (1, new Item(13))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (2, new Item(199))
        };
        subList.Add(subList1);
        list.Add(subList);

        return list;
    }

    int count = 3;
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
