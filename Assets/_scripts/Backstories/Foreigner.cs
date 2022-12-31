using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreigner : Backstory
{
    public Foreigner()
    {
        id = 11;
        name = "Чужеземец";
        LoadAbilities("Foreigner");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(87)),
            (1, new Item(60)),
            (1, new Item("Трофей с убитого животного")),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 10, 0, 0 };
    }
}
