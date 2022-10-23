using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayersClass : ObjectsBehavior
{
    protected string characterName = CharacterCollection.GetName();
    const string armorProfSaveName = "armorProf_";
    const string weaponProfSaveName = "weaponProf_";
    const string saveThrowProfSaveName = "save_";
    protected const string levelCountSaveName = "lvlCount_";
    protected const string levelSaveName = "lvl_";
    protected const string levelLabelSaveName = "lvlLabel_";

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

    protected int level = 0;
    protected int mainState = 0;
    protected Ability[] abilities;

    protected int PB;

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("classes/" + pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }

    public virtual Ability[] ChooseSubClass(string subClasses)
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
