using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Race
{
    // Start is called before the first frame update
    public Gnome()
    {
        id = 0;
        name = "√ном";
        LoadAbilities("Gnome");
    }

    public override Ability[] ChooseSubRace(int id)
    {
        switch (id)
        {
            case 1:
                subRace = new ForestGnome();
                break;
            case 2:
                subRace = new RockGnome();
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
