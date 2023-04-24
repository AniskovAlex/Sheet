using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiefling : Race
{
    public Tiefling()
    {
        id = 6;
        name = "Тифлинг";
        LoadAbilities("Tiefling");
    }

    public override Vision GetVision()
    {
        return Vision.dark;
    }
}
