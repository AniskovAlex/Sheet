using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : PlayersClass
{
    public Paladin()
    {
        id = 9;
        name = "�������";
        LoadAbilities("Paladin");
        healthDice = 10;
        mainState = 5;
        magic = 2;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new OathOfAllegiance();
                break;
            case 2:
                subClass = new OathOfTheAcients();
                break;
            case 3:
                subClass = new OathOfRevenge();
                break;
            case 4:
                subClass = new Oathbreaker();
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
        HashSet<Weapon.WeaponType> list = new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist, Weapon.WeaponType.WarDist, Weapon.WeaponType.Shield };
        HashSet<Weapon.WeaponType> buf = base.GetWeaponProficiency();
        if (buf != null)
            list = new HashSet<Weapon.WeaponType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        HashSet<Armor.ArmorType> list = new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield, Armor.ArmorType.Heavy };
        HashSet<Armor.ArmorType> buf = base.GetArmorProficiency();
        if (buf != null)
            list = new HashSet<Armor.ArmorType>(list.Concat(buf).ToArray());
        return list;
    }

    public override HashSet<Armor.ArmorType> GetSubArmorProficiency()
    {
        HashSet<Armor.ArmorType> list = new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
        HashSet<Armor.ArmorType> buf = base.GetArmorProficiency();
        if (buf != null)
            list = new HashSet<Armor.ArmorType>(list.Concat(buf).ToArray());
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
            (1, new Item(-3)),
            (1, new Item(195)),
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
            (5, new Item(202))
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
            (1, new Item(68)),
            (1, new Item(59)),
            (10, new Item(70)),
            (1, new Item(81)),
            (10, new Item("������� ��� �������������")),
            (10, new Item("������")),
            (2, new Item(67)),
            (1, new Item(10))
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
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (5, new Item(192))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(-9))
        };
        subList.Add(subList1);
        list.Add(subList);

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
