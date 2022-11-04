using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerSubClass
{
    protected Ability[] abilities;
    protected int level = 0;
    protected int mainState;
    protected int PB;
    protected string name;

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("subClasses/"+ pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }

    public string GetName()
    {
        return name;
    }

    int ID = -1;
    public int id
    {
        get { return ID; }
        protected set { ID = value; }
    }
}
