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
}
