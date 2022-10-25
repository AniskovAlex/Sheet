using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artificer : PlayersClass
{
    public Artificer()
    {

        LoadAbilities("Artificer");
        healthDice = 8;
    }

    public override int GetMoney()
    {
        return 0;
    }
}
