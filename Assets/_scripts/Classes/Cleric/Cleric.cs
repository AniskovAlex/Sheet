using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : PlayersClass
{
    public Cleric()
    {
        id = 5;
        name = "Жрец";
        LoadAbilities("Cleric");
        healthDice = 8;
        mainState = 4;
        magic = 1;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        switch (subId)
        {
            case 1:
                subClass = new DomainOfStorm();
                break;
            case 2:
                subClass = new DomainOfWar();
                break;
            case 3:
                subClass = new DomainOfLife();
                break;
            case 4:
                subClass = new DomainOfKnowledge();
                break;
            case 5:
                subClass = new DomainOfDeception();
                break;
            case 6:
                subClass = new DomainOfNature();
                break;
            case 7:
                subClass = new DomainOfLight();
                break;
            case 8:
                subClass = new DomainOfDeath();
                break;
            default: return null;
        }
        return subClass.GetAbilities();
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.CommonDist, Weapon.WeaponType.CommonMelee};
    }

    public override HashSet<int> GetSaveThrows()
    {
        return new HashSet<int> { 3, 5 };
    }
    public override List<List<List<(int, Item)>>> GetItems()
    {
        List<List<List<(int, Item)>>> list = new List<List<List<(int, Item)>>>();
        List<List<(int, Item)>> subList = new List<List<(int, Item)>>();
        List<(int, Item)> subList1 = new List<(int, Item)>()
        {
            (1, new Item(197))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(212))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(188))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(184))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(192))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
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
            (1, new Item(68)),
            (1, new Item(59)),
            (10, new Item(70)),
            (1, new Item(81)),
            (10, new Item("Коробка для пожертвований")),
            (10, new Item("Кодило")),
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
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(195)),
            (1, new Item(-9))
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
