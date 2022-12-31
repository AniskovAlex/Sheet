using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    public Fighter()
    {
        id = 2;
        name = "Воин";
        LoadAbilities("Fighter");
        healthDice = 10;
        mainState = 0;
        magic = 0;
    }

    public override Ability[] ChooseSubClass(int subId)
    {
        magicChange = 0;
        switch (subId)
        {
            case 1:
                subClass = new MasterOfMartialArt();
                break;
            case 2:
                magicChange = 1;
                subClass = new MysticalKnight();
                break;
            case 3:
                subClass = new Champion();
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
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist, Weapon.WeaponType.WarDist, Weapon.WeaponType.Shield };
    }

    public override HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Heavy, Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
    }

    public override HashSet<Armor.ArmorType> GetSubArmorProficiency()
    {
        return new HashSet<Armor.ArmorType>() { Armor.ArmorType.Light, Armor.ArmorType.Medium, Armor.ArmorType.Shield };
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
            (1, new Item(192))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (1, new Item(184)),
            (1, new Item(230)),
            (20, new Item(7))
        };
        subList.Add(subList1);
        list.Add(subList);
        subList = new List<List<(int, Item)>>();

        subList1 = new List<(int, Item)>()
        {
            (1, new Item(-2)),
            (1, new Item(195))
        };
        subList.Add(subList1);
        subList1 = new List<(int, Item)>()
        {
            (2, new Item(-2))
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
            (2, new Item(204))
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

        return list;
    }

    public override int GetMoney()
    {
        int sum = 0;
        for (int i = 0; i < 5; i++)
            sum += Random.Range(1, 5);
        return sum * 10;
    }

}
