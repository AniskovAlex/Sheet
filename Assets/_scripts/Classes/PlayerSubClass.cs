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
        abilities = FileSaverAndLoader.LoadAbilities("SubClasses/"+ pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }

    public string GetName()
    {
        return name;
    }

    public virtual int GetMagic()
    {
        return 0;
    }

    public virtual HashSet<int> GetSpells()
    {
        return null;
    }

    public virtual HashSet<int> GetSpells(int level)
    {
        return null;
    }

    int ID = -1;
    public int id
    {
        get { return ID; }
        protected set { ID = value; }
    }

    public virtual HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        return null;
    }

    public virtual HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        return null;
    }
    public virtual HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        return null;
    }
}
