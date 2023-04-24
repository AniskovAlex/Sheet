using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfElf : Race
{
    public HalfElf()
    {
        id = 5;
        name = "ֿמכף‎כüפ";
        LoadAbilities("HalfElf");
    }

    public override Vision GetVision()
    {
        return Vision.dark;
    }
}
