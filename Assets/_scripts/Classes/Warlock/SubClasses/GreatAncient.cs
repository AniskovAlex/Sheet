using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatAncient : PlayerSubClass
{
    public GreatAncient()
    {
        id = 3;
        name = "Великий древний";
        LoadAbilities("GreatAncient");
    }

    public override HashSet<int> GetSpells()
    {
        return new HashSet<int>() { 55, 69, 30, 167, 210, 228, 211, 356, 212, 318 };
    }
}
