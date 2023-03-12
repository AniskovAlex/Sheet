using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Bard : PlayersClass
{
    public Bard()
    {
        id = 0;
        name = "Бард";
        LoadAbilities("Bard");
        healthDice = 8;
        mainState = 5;
        magic = 1;
        magicChange = 1;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new CollegeOfValor();
                break;
            case 2:
                subClass = new CollegeOfKnowledge();
                break;
            default: return null;
        }
        return subClass.GetAbilities();
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        HashSet<Weapon.WeaponType> list = new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist };
        HashSet<Weapon.WeaponType> buf = base.GetWeaponProficiency();
        if (buf != null)
            list = new HashSet<Weapon.WeaponType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        HashSet<Weapon.BladeType> list = new HashSet<Weapon.BladeType>() { Weapon.BladeType.LongSword, Weapon.BladeType.ShortSword, Weapon.BladeType.Rapier, Weapon.BladeType.HandedCrossbow };
        HashSet<Weapon.BladeType> buf = base.GetBladeProficiency();
        if (buf != null)
            list = new HashSet<Weapon.BladeType>(list.Concat(buf).ToArray());
        return list;
    }


    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        HashSet<Armor.ArmorType> list = new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light };
        HashSet<Armor.ArmorType> buf = base.GetArmorProficiency();
        if (buf != null)
            list = new HashSet<Armor.ArmorType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 1, 5 };
    }
    public override List<List<List<(int, Item)>>> GetItems()
    {
        List<List<List<(int, Item)>>> list = new List<List<List<(int, Item)>>>();
        List<List<(int, Item)>> subList = new List<List<(int, Item)>>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item(223))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(217))
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
            (1, new Item(78)),
            (2, new Item(35)),
            (1, new Item(58)),
            (1, new Item(93)),
            (1, new Item(64)),
            (1, new Item(40)),
            (2, new Item(48)),
            (5, new Item(9)),
            (1, new Item(18)),
            (1, new Item(16)),
            (1, new Item(55))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(68)),
            (1, new Item(76)),
            (2, new Item(57)),
            (5, new Item(70)),
            (5, new Item(67)),
            (1, new Item(10)),
            (1, new Item(132))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(-4))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(184)),
            (2, new Item(199))
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
