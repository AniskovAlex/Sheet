using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterOfMartialArt : PlayerSubClass
{
    public MasterOfMartialArt()
    {
        id = 0;
        name = "Мастер боевых искусств";
        LoadAbilities("masterOfMartialArt");
    }
}
