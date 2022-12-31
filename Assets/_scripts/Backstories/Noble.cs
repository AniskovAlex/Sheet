using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noble : Backstory
{
    public Noble()
    {
        id = 2;
        name = "Благородный";
        LoadAbilities("Noble");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(58)),
            (1, new Item(30)),
            (1, new Item("Свиток с генеалогическим древом")),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 25, 0, 0 };
    }
}
