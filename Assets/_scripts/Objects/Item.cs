using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{

    public enum MType
    {
        goldCoin,
        silverCoin,
        copperCoin
    }
    public int id;
    public string label;
    public int cost;
    public int weight;
    public MType mType;

    public Item()
    {

    }

    public Item(int id)
    {
        this.id = id;
    }
    public Item(string label)
    {
        id = -1;
        this.label = label;
    }
}
