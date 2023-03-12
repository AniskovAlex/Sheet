using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : PlayersClass
{
    public Warlock()
    {
        id = 7;
        name = "Колдун";
        LoadAbilities("Warlock");
        healthDice = 8;
        mainState = 5;
        magic = -1;
        magicChange = 1;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new ArchFairy();
                break;
            case 2:
                subClass = new Fiend();
                break;
            case 3:
                subClass = new GreatAncient();
                break;
            default: return null;
        }
        return subClass.GetAbilities();
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        HashSet<Armor.ArmorType> list = new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light };
        HashSet<Armor.ArmorType> buf = base.GetArmorProficiency();
        if (buf != null)
            list = new HashSet<Armor.ArmorType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        HashSet<Weapon.WeaponType> list = new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist };
        HashSet<Weapon.WeaponType> buf = base.GetWeaponProficiency();
        if (buf != null)
            list = new HashSet<Weapon.WeaponType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 4, 5 };
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
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(184)),
            (1, new Item(-3)),
            (2, new Item(199))
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
