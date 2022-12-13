using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : PlayersClass
{
    public Rogue()
    {
        id = 10;
        name = "����";
        LoadAbilities("Rogue");
        healthDice = 8;
        mainState = 1;
        magic = 0;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new Thief();
                break;
            case 2:
                subClass = new Assassin();
                break;
            case 3:
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
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist};
    }

    public override HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return new HashSet<Weapon.BladeType>() {Weapon.BladeType.HandedCrossbow, Weapon.BladeType.LongSword, Weapon.BladeType.Rapier, Weapon.BladeType.ShortSword};
    }

    public override HashSet<Weapon.BladeType> GetSubBladeProficiency()
    {
        return null;
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light};
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
            (1, new Item("����� (10 �����)")),
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
