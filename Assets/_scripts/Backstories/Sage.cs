using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sage : Backstory
{
    public Sage()
    {
        id = 5;
        name = "������";
        LoadAbilities("Sage");
    }

    public override List<(int, Item)> GetItems()
    {
        return new List<(int, Item)>()
        {
            (1, new Item(93)),
            (1, new Item(64)),
            (1, new Item("��������� ���")),
            (1, new Item("������ �� ������� �������")),
            (1, new Item(233)),
            (1, new Item(37))
        };
    }

    public override int[] GetMoney()
    {
        return new int[3] { 10, 0, 0 };
    }
}
