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

    public void SetMagic(int add)
    {
        magic = add;
    }

    int MC = 0;
    public int magicChange
    {
        get { return MC; }
        protected set { MC = value; }
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
        if (subClass != null)
            return subClass.GetWeaponProficiency();
        return null;
    }

    public virtual HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        if (subClass != null)
            return subClass.GetBladeProficiency(); ;
        return null;
    }
    public virtual HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        if (subClass != null)
            return subClass.GetArmorProficiency();
        return null;
    }

    public virtual HashSet<Weapon.WeaponType> GetSubWeaponProficiency()
    {
        return GetWeaponProficiency();
    }

    public virtual HashSet<Weapon.BladeType> GetSubBladeProficiency()
    {
        return GetBladeProficiency();
    }
    public virtual HashSet<Armor.ArmorType> GetSubArmorProficiency()
    {
        return GetArmorProficiency();
    }

    public virtual HashSet<int> GetSaveThrows()
    {
        return new HashSet<int>();
    }

    public virtual List<List<List<(int, Item)>>> GetItems()
    {
        return null;
    }

    public virtual int GetMoney()
    {
        return 0;
    }

    public static PlayersClass GetPlayersClassByID(int id)
    {
        PlayersClass playersClass = null;
        switch (id)
        {
            case 1:
                playersClass = new Bard();
                break;
            case 2:
                playersClass = new Barbarian();
                break;
            case 3:
                playersClass = new Fighter();
                break;
            case 4:
                playersClass = new Wizard();
                break;
            case 5:
                playersClass = new Druid();
                break;
            case 6:
                playersClass = new Cleric();
                break;
            case 7:
                playersClass = new Warlock();
                break;
            case 8:
                playersClass = new Monk();
                break;
            case 9:
                playersClass = new Paladin();
                break;
            case 10:
                playersClass = new Rogue();
                break;
            case 11:
                playersClass = new Ranger();
                break;
            case 12:
                playersClass = new Sorcerer();
                break;
        }
        return playersClass;
    } 

}
