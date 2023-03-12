using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : PlayersClass
{
    public Ranger()
    {
        id = 11;
        name = "Следопыт";
        LoadAbilities("Ranger");
        healthDice = 10;
        mainState = 4;
        magic = 2; 
        magicChange = 1;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new Hunter();
                break;
            case 2:
                subClass = new BeasteMaster();
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
        HashSet<Weapon.WeaponType> list = new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist, Weapon.WeaponType.WarDist };
        HashSet<Weapon.WeaponType> buf = base.GetWeaponProficiency();
        if (buf != null)
            list = new HashSet<Weapon.WeaponType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        HashSet<Armor.ArmorType> list = new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
        HashSet<Armor.ArmorType> buf = base.GetArmorProficiency();
        if (buf != null)
            list = new HashSet<Armor.ArmorType>(list.Concat(buf).ToArray());
        return list;
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
            (1, new Item(188))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(184))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (2, new Item(219))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (2, new Item(-7))
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
            (1, new Item(230)),
            (1, new Item(29)),
            (20, new Item(7))
        };
        subList.Add(subList1);
        list.Add(subList);

        return list;
    }

    int count = 5;
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
