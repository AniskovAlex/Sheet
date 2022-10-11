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

    int IA = 0;
    public int instrumentsAmount
    {
        get { return IA; }
        protected set { IA = value; }
    }

    List<string> IP = new List<string>();
    public List<string> instrumentProfs
    {
        get { return IP; }
        protected set { IP = value; }
    }
    int SA = 0;
    public int skillsAmount
    {
        get { return SA; }
        protected set { SA = value; }
    }
    List<int> SP = new List<int>();
    public List<int> skillProfs
    {
        get { return SP; }
        protected set { SP = value; }
    }
    List<int> STP = new List<int>();
    public List<int> savethrowProfs
    {
        get { return STP; }
        protected set { STP = value; }
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
}
