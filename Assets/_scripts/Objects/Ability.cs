using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public enum Type
    {
        charUp,
        abilitie,
        consumable
    }

    public string head;

    public Type type;

    public List<(int, string)> discription = new List<(int, string)>();
}
