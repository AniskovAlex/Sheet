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
        subClass,
        skills,
        instruments
    }

    public string head;

    public bool isUniq;

    public int chooseCount;
    public string pathToList;
    public string listName;

    public Type type;

    public List<(int, string)> discription = new List<(int, string)>();

    public (string, string)[] list;

    public HashSet<string> common;

    public int level;

    public int consum;
}
