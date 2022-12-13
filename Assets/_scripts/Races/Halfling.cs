using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halfling : Race
{
    public Halfling()
    {
        id = 4;
        name = "Полурослик";
        LoadAbilities("Halfling");
    }

    public override Ability[] ChooseSubRace(int id)
    {
        switch (id)
        {
            case 1:
                subRace = new Stocky();
                break;
            case 2:
                subRace = new LightLeged();
                break;
            default: return null;
        }
        return subRace.GetAbilities();
    }

    public override int GetSpeed()
    {
        return 25;
    }

    public override Size GetSize()
    {
        return Size.little;
    }
}
