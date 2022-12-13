using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : PlayerSubClass
{
    public Hunter()
    {
        id = 1;
        name = "Охотник";
        LoadAbilities("Hunter");
    }
}
