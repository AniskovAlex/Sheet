using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist : Backstory
{

    public Artist()
    {
        id = 0;
        name = "Артист";
        LoadAbilities("Artist");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item("Подарок от поклонницы")),
            (1, new Item(57)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 15, 0, 0 };
    }
}
