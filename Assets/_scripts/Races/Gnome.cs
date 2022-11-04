using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Race
{
    // Start is called before the first frame update
    public Gnome()
    {
        id = 1;
        name = "√ном";
        LoadAbilities("Gnome");
    }

    public override Ability[] ChooseSubRace(int id)
    {
        switch (id)
        {
            case 0:
                subRace = new ForestGnome();
                break;
            default: return null;
        }
        return subRace.GetAbilities();
    }
}
