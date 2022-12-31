using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Criminal : Backstory
{
    public Criminal()
    {
        id = 8;
        name = "Преступник";
        LoadAbilities("Criminal");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(42)),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 15, 0, 0 };
    }
}
