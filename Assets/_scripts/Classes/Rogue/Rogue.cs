using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : PlayersClass
{
    public Rogue()
    {
        id = 10;
        name = "Плут";
        LoadAbilities("Rogue");
        healthDice = 8;
        mainState = 1;
        magic = 0;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        magicChange = 0;
        switch (subId)
        {
            case 1:
                subClass = new Thief();
                break;
            case 2:
                subClass = new Assassin();
                break;
            case 3:
                magicChange = 1;
                subClass = new MysticalDouble();
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
        HashSet<Weapon.WeaponType> list = new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist };
        HashSet<Weapon.WeaponType> buf = base.GetWeaponProficiency();
        if (buf != null)
            list = new HashSet<Weapon.WeaponType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        HashSet<Weapon.BladeType> list = new HashSet<Weapon.BladeType>() { Weapon.BladeType.HandedCrossbow, Weapon.BladeType.LongSword, Weapon.BladeType.Rapier, Weapon.BladeType.ShortSword };
        HashSet<Weapon.BladeType> buf = base.GetBladeProficiency();
        if (buf != null)
            list = new HashSet<Weapon.BladeType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<Weapon.BladeType> GetSubBladeProficiency()
    {
        if (subClass == null) return null;
        return subClass.GetBladeProficiency();
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        HashSet<Armor.ArmorType> list = new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light };
        list = new HashSet<Armor.ArmorType>(list.Concat(base.GetArmorProficiency()));
        return list;
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 1, 3 };
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
            (1, new Item(219))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(208)),
            (1, new Item(29)),
            (20, new Item(7)),
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(219))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(68)),
            (1, new Item(50)),
            (1, new Item("Леска (10 футов)")),
            (1, new Item(28)),
            (5, new Item(70)),
            (1, new Item(42)),
            (1, new Item(54)),
            (10, new Item(96)),
            (1, new Item(90)),
            (2, new Item(48)),
            (5, new Item(67)),
            (1, new Item(81)),
            (1, new Item(10)),
            (1, new Item(13))
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
            (1, new Item(184)),
            (2, new Item(199)),
            (1, new Item(98))
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
