using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchFairy : PlayerSubClass
{
    public ArchFairy()
    {
        id = 1;
        name = "Архифея";
        LoadAbilities("ArchFairy");
    }

    public override HashSet<int> GetSpells()
    {
        return new HashSet<int>() { 178, 340, 30, 338, 132, 277, 37, 211, 212, 253};
    }
}
