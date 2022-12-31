using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeoplesHero : Backstory
{
    public PeoplesHero()
    {
        id = 6;
        name = "Народный герой";
        LoadAbilities("PeoplesHero");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(43)),
            (1, new Item(17)),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 10, 0, 0 };
    }
}
