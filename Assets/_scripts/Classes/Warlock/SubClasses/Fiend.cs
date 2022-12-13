using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fiend : PlayerSubClass
{
    public Fiend()
    {
        id = 2;
        name = "Исчадие";
        LoadAbilities("Fiend");
    }

    public override HashSet<int> GetSpells()
    {
        return new HashSet<int>() { 174, 249, 44, 197, 84, 176, 173, 177, 152, 283 };
    }
}
