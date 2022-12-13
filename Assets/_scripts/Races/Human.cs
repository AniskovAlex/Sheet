using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Human : Race
{
    public Human()
    {
        id = 7;
        name = "Человек";
        LoadAbilities("Human");
    }
    public override Ability[] ChooseSubRace(int id)
    {
        switch (id)
        {
            case 1:
                subRace = new HumanClassic();
                break;
            case 2:
                subRace = new HumanAlt();
                break;
            default: return null;
        }
        return subRace.GetAbilities();
    }
}
