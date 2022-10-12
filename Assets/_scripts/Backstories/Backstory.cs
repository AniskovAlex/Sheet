using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstory : ObjectsBehavior
{
    protected Ability[] abilities;

    string N = null;
    public string name
    {
        get { return N; }
        protected set { N = value; }
    }

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("backstories/" + pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }
}
