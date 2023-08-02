using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;

[Preserve]
public abstract class PlayersClass : ObjectsBehavior
{
    [Preserve] protected PlayerSubClass subClass = null;

    [Preserve] int HD = 0;
    [Preserve]
    public int healthDice
    {
        get { return HD; }
        protected set { HD = value; }
    }

    [Preserve] string N = null;
    [Preserve]
    public string name
    {
        get { return N; }
        protected set { N = value; }
    }

    int ID = -1;
    [Preserve]
    public int id
    {
        get { return ID; }
        protected set { ID = value; }
    }

    int M = 0;
    [Preserve]
    public int magic
    {
        get { return M; }
        protected set { M = value; }
    }

    [Preserve]
    public void SetMagic(int add)
    {
        magic = add;
    }

    [Preserve] int MC = 0;
    [Preserve]
    public int magicChange
    {
        get { return MC; }
        protected set { MC = value; }
    }

    [Preserve] protected int level = 0;

    [Preserve] int MS = 0;
    [Preserve]
    public int mainState
    {
        get { return MS; }
        protected set { MS = value; }
    }

    [Preserve] protected Ability[] abilities;

    [Preserve] protected int PB;

    [Preserve]
    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("Classes/" + pathName);
    }

    [Preserve]
    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }

    [Preserve]
    public virtual Ability[] ChooseSubClass(int subId)
    {
        return null;
    }

    [Preserve]
    public virtual PlayerSubClass GetSubClass()
    {
        return subClass;
    }

    [Preserve]
    public virtual HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        if (subClass != null)
            return subClass.GetWeaponProficiency();
        return null;
    }

    [Preserve]
    public virtual HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        if (subClass != null)
            return subClass.GetBladeProficiency(); ;
        return null;
    }
    [Preserve]
    public virtual HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        if (subClass != null)
            return subClass.GetArmorProficiency();
        return null;
    }

    [Preserve]
    public virtual HashSet<Weapon.WeaponType> GetSubWeaponProficiency()
    {
        return GetWeaponProficiency();
    }

    [Preserve]
    public virtual HashSet<Weapon.BladeType> GetSubBladeProficiency()
    {
        return GetBladeProficiency();
    }
    [Preserve]
    public virtual HashSet<Armor.ArmorType> GetSubArmorProficiency()
    {
        return GetArmorProficiency();
    }

    [Preserve]
    public virtual HashSet<int> GetSaveThrows()
    {
        return new HashSet<int>();
    }

    [Preserve]
    public virtual List<List<List<(int, Item)>>> GetItems()
    {
        return null;
    }

    [Preserve]
    public virtual int GetMoney()
    {
        return 0;
    }

    [Preserve]
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
