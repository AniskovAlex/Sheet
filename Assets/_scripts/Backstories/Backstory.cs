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

    int ID = -1;
    public int id
    {
        get { return ID; }
        protected set { ID = value; }
    }

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("Backstories/" + pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }

    public virtual int[] GetMoney()
    {
        return null;
    }

    public virtual List<(int, Item)> GetItems()
    {
        return null;
    }
}
