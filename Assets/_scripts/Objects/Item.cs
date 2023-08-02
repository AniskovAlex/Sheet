using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Item
{

    [Preserve]
    public enum MType
    {
        goldCoin,
        silverCoin,
        copperCoin
    }
    [Preserve] public int id;
    [Preserve] public string label;
    [Preserve] public int cost;
    [Preserve] public int weight;
    [Preserve] public MType mType;

    [Preserve] public int amount;

    [Preserve]
    public Item()
    {

    }

    [Preserve]
    public Item(int id)
    {
        this.id = id;
    }
    [Preserve]
    public Item(string label)
    {
        id = -1;
        this.label = label;
    }
}
