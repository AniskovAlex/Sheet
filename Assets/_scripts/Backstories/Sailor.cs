using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sailor : Backstory
{
    public Sailor()
    {
        id = 4;
        name = "Моряк";
        LoadAbilities("Sailor");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(198)),
            (1, new Item(14)),
            (1, new Item("Талисман")),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 10, 0, 0 };
    }
}
