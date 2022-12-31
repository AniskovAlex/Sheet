using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildArtiser : Backstory
{
    public GuildArtiser()
    {
        id = 3;
        name = "Гильдейский ремеленник";
        LoadAbilities("GuildArtiser");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item("Рекомендательное письмо из гильдии")),
            (1, new Item(56)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 15, 0, 0 };
    }
}
