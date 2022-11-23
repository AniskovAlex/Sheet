using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Sorts : IComparer
{
    string searchString = "";
    public Sorts(string label)
    {
        searchString = label;
    }

    public int Compare(object x, object y)
    {
        int countX = 0;
        int countY = 0;
        if (x is Item && y is Item)
        {
            Item itemX = x as Item, itemY = y as Item;
            countX = itemX.label.IndexOf(searchString);
            countY = itemY.label.IndexOf(searchString);
            if (countX == countY)
                return itemX.label.CompareTo(itemY.label);
        }
        else throw new Exception("Не верный объект");
        return countX - countY;
    }

}
