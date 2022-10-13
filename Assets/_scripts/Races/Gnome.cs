using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnome : Race
{
    // Start is called before the first frame update
    public Gnome()
    {
        name = "����";
        LoadAbilities("Gnome");
    }

    public override Ability[] ChooseSubRace(string subRaces)
    {
        switch (subRaces)
        {
            case "������ ����":
                subRace = new ForestGnome();
                break;
            default: return null;
        }
        return subRace.GetAbilities();
    }
}
