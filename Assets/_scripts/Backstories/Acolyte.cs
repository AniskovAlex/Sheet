using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acolyte : Backstory
{
    public Acolyte()
    {
        id = 9;
        name = "Прислужник";
        LoadAbilities("Acolyte");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item("Молитвенник")),
            (5, new Item("Благовония")),
            (5, new Item(69)),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 15, 0, 0 };
    }
}
