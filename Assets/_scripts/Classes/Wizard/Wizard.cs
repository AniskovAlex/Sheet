using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : PlayersClass
{
    public Wizard()
    {
        id = 3;
        name = "Волшебник";
        LoadAbilities("Wizard");
        healthDice = 6;
        mainState = 3;
        magic = 1;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new SchoolOfAbjuration();
                break;
            case 2:
                subClass = new SchoolOfConjuration();
                break;
            case 3:
                subClass = new SchoolOfDivination();
                break;
            case 4:
                subClass = new SchoolOfEnchantment();
                break;
            case 5:
                subClass = new SchoolOfEvocation();
                break;
            case 6:
                subClass = new SchoolOfIllusion();
                break;
            case 7:
                subClass = new SchoolOfNecromancy();
                break;
            case 8:
                subClass = new SchoolOfTransmutation();
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
        return new HashSet<int> { 3, 4 };
    }
    public override List<List<List<(int, Item)>>> GetItems()
    {
        List<List<List<(int, Item)>>> list = new List<List<List<(int, Item)>>>();
        List<List<(int, Item)>> subList = new List<List<(int, Item)>>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item(196))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(199))
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
            (1, new Item("Научная книга")),
            (1, new Item(93)),
            (1, new Item(64)),
            (10, new Item(62)),
            (1, new Item("Небольшая сумочка с песком")),
            (1, new Item("Небольшой нож"))
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
            (1, new Item(2))
        };
        subList.Add(subList1);
        list.Add(subList);

        return list;
    }

    int count = 4;
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
