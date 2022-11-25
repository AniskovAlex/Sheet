using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : PlayerSubClass
{
    public Champion()
    {
        id = 3;
        name = "Чемпион";
        LoadAbilities("Champion");
    }
}
