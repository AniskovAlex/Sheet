using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : PlayersClass
{

    public Rogue()
    {
        id = 2;
        LoadAbilities("Rogue");
        healthDice = 10;
    }

}
