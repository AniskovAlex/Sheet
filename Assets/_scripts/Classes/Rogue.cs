using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : PlayersClass
{

    public Rogue()
    {
        //LoadAbilities("fighter");
        healthDice = 10;
        instrumentsAmount = 0;
        instrumentProfs = new List<string>();
        skillsAmount = 2;
        skillProfs = new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 };
        savethrowProfs = new List<int> { 0, 1 };
    }

}
