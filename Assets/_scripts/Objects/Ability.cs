using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public enum Type
    {
        charUp,
        abilitie,
        consumable,
        withChoose,
        subClass
    }

    public string head;

    public Type type;

    public List<(int, string)> discription = new List<(int, string)>();

    public int level;
}
