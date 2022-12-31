using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hermit : Backstory
{
    public Hermit()
    {
        id = 7;
        name = "Отшельник";
        LoadAbilities("Hermit");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(35)),
            (1, new Item(59)),
            (1, new Item(134)),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 5, 0, 0 };
    }
}
