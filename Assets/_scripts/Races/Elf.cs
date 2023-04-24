using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : Race
{
    public Elf()
    {
        id = 8;
        name = "έλότ";
        LoadAbilities("Elf");
    }

    public override Ability[] ChooseSubRace(int id)
    {
        switch (id)
        {
            case 1:
                subRace = new HighElf();
                break;
            case 2:
                subRace = new ForestElf();
                break;
            case 3:
                subRace = new Drow();
                break;
            default: return null;
        }
        return subRace.GetAbilities();
    }

    public override Vision GetVision()
    {
        return Vision.dark;
    }
}
