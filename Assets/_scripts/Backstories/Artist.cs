using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist : Backstory
{

    public Artist()
    {
        name = "������";
        LoadAbilities("Artist");
    }

    public override int[] GetMoney()
    {
        return new int[3] { 20, 0, 0 };
    }
}
