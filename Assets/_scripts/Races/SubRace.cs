using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRace
{
    protected Ability[] abilities;
    protected string name;

    int ID = -1;
    public int id
    {
        get { return ID; }
        protected set { ID = value; }
    }

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("SubRaces/" + pathName);
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

