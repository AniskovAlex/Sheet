using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artificer : PlayersClass
{

    const string ArtificerSubClassSaveName = "ArtificerSubClass_";

    PlayerSubClass subClass = null;

    public Artificer()
    {

        //LoadAbilities("fighter");
        healthDice = 8;
        instrumentsAmount = 0;
        instrumentProfs = new List<string>();
        skillsAmount = 2;
        skillProfs = new List<int> { 2, 4, 5, 6, 7, 9, 11 };
        savethrowProfs = new List<int> { 2, 3 };
    }
}
