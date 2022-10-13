using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Race : ObjectsBehavior
{
    protected Ability[] abilities;
    protected SubRace subRace = null;

    string N = null;
    public string name
    {
        get { return N; }
        protected set { N = value; }
    }

    public enum Size
    {
        little,
        medium,
        large
    }

    public virtual int GetSpeed()
    {
        return 30;
    }

    public virtual Size GetSize()
    {
        return Size.medium;
    }

    public bool GetVision()
    {
        return false;
    }

    public SubRace GetSubRace()
    {
        return subRace;
    }

    public virtual Ability[] ChooseSubRace(string subRaces)
    {
        return null;
    }

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("races/" + pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }
}
