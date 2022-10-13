using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRace
{
    protected Ability[] abilities;
    protected string name;

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("subRaces/" + pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }

    public string GetName()
    {
        return name;
    }
}

