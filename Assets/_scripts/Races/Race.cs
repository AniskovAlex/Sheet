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

    int ID = -1;
    public int id
    {
        get { return ID; }
        protected set { ID = value; }
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
    public virtual HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        if (subRace != null)
            return subRace.GetBladeProficiency(); ;
        return null;
    }
    public virtual HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        if (subRace != null)
            return subRace.GetArmorProficiency();
        return null;
    }
    public bool GetVision()
    {
        return false;
    }

    public SubRace GetSubRace()
    {
        return subRace;
    }

    public virtual Ability[] ChooseSubRace(int subId)
    {
        return null;
    }

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("Races/" + pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }
}
