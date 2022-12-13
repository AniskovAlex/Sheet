using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : PlayersClass
{
    public Monk()
    {
        id = 8;
        name = "Монах";
        LoadAbilities("Monk");
        healthDice = 8;
        mainState = 4;
        magic = 0;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new PathOfOpenPalm();
                break;
            case 2:
                subClass = new PathOfShadow();
                break;
            case 3:
                subClass = new PathOfFourElements();
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
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist };
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return new HashSet<Weapon.BladeType>() { Weapon.BladeType.ShortSword };
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 0, 1 };
    }
    public override List<List<List<(int, Item)>>> GetItems()
    {
        List<List<List<(int, Item)>>> list = new List<List<List<(int, Item)>>>();
        List<List<(int, Item)>> subList = new List<List<(int, Item)>>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item(219))
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
            (10, new Item(207))
        };
        subList.Add(subList1);
        list.Add(subList);

        return list;
    }

    int count = 5;
    int k = 4;
    int mult = 1;

    public override int GetMoney()
    {
        int sum = 0;
        for (int i = 0; i < count; i++)
            sum += Random.Range(1, k + 1);
        return sum * mult;
    }

}
