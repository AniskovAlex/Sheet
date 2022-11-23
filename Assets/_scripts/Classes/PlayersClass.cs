using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayersClass : ObjectsBehavior
{
    protected PlayerSubClass subClass = null;

    int HD = 0;
    public int healthDice
    {
        get { return HD; }
        protected set { HD = value; }
    }

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

    int M = 0;
    public int magic
    {
        get { return M; }
        protected set { M = value; }
    }
    protected int level = 0;
    int MS = 0;
    public int mainState
    {
        get { return MS; }
        protected set { MS = value; }
    }

    protected Ability[] abilities;

    protected int PB;

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("Classes/" + pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }

    public virtual Ability[] ChooseSubClass(int subId)
    {
        return null;
    }

    public virtual PlayerSubClass GetSubClass()
    {
        return subClass;
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

    public virtual HashSet<int> GetSaveThrows()
    {
        return new HashSet<int>();
    }

    public virtual List<(List<(int, Item)>, List<(int, Item)>)> GetItems()
    {
        return null;
    }

    public virtual int GetMoney()
    {
        return 0;
    }

}
