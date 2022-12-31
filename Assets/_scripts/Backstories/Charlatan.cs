using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charlatan : Backstory
{
    public Charlatan()
    {
        id = 12;
        name = "Шалатан";
        LoadAbilities("Charlatan");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(58)),
            (1, new Item(132)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 15, 0, 0 };
    }
}
