using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Backstory
{
    public Soldier()
    {
        id = 10;
        name = "Солдат";
        LoadAbilities("Soldier");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item("Знак отличия")),
            (1, new Item("Трофей с убитого врага")),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 10, 0, 0 };
    }
}
