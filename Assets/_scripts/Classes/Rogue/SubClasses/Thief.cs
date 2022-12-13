using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : PlayerSubClass
{
    public Thief()
    {
        id = 1;
        name = "Вор";
        LoadAbilities("Thief");
    }
}
