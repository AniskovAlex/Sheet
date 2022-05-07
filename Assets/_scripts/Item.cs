using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Item
{
    public enum Type
    {
        item,
        armor,
        weapon
    }

    public enum MType
    {
        goldCoin,
        silverCoin,
        copperCoin
    }
    public string label;
    public int cost;
    public int weight;
    public MType mType;
    public Type type;

    public Item(string label, int cost, int weight, MType mType,Type type)
    {
        this.label = label;
        this.cost = cost;
        this.weight = weight;
        this.mType = mType;
        this.type = type;
    }
}

public struct ItemWeapon
{

}
