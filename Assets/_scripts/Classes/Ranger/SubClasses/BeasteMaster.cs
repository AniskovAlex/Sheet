using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeasteMaster : PlayerSubClass
{
    public BeasteMaster()
    {
        id = 2;
        name = "Повелитель зверей";
        LoadAbilities("BeasteMaster");
    }
}
