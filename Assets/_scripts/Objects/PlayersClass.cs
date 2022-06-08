using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayersClass
{
    const string armorProfSaveName = "armorProf_";
    const string weaponProfSaveName = "weaponProf_";
    const string saveThrowProfSaveName = "save_";
    const string classSaveName = "class_";

    int healthDice;
    List<Armor.Type> armorProfs;
    List<Weapon.Type> weaponProfs;
    int instrumentsAmount;
    List<string> instrumentProfs;
    int skillsAmount;
    List<int> skillProfs;
    List<int> savethrowProfs;

    protected PlayersClass(int healthDice, List<Armor.Type> armorProfs, List<Weapon.Type> weaponProfs, int instrumentsAmount, List<string> instrumentProfs, int skillsAmount, List<int> skillProfs, List<int> savethrowProfs) : base()
    {
        this.healthDice = healthDice;
        this.armorProfs = armorProfs;
        this.weaponProfs = weaponProfs;
        this.instrumentsAmount = instrumentsAmount;
        this.instrumentProfs = instrumentProfs;
        this.skillsAmount = skillsAmount;
        this.skillProfs = skillProfs;
        this.savethrowProfs = savethrowProfs;
    }

    public List<Armor.Type> GetArmorProfs()
    {
        return armorProfs;
    }

    public List<Weapon.Type> GetWeaponProfs()
    {
        return weaponProfs;
    }
    public List<string> GetInstrumentProfs()
    {
        return instrumentProfs;
    }
    public List<int> GetSkillProfs()
    {
        return skillProfs;
    }

    public List<int> GetSavethrowProfs()
    {
        return savethrowProfs;
    }

    public int GetInstrumentsAmount()
    {
        return instrumentsAmount;
    }

    public int GetSkillsAmount()
    {
        return skillsAmount;
    }

    public virtual void Save()
    {
        foreach (Weapon.Type x in weaponProfs)
        {
            PlayerPrefs.SetInt(weaponProfSaveName + x, 1);
        }
        foreach (Armor.Type x in armorProfs)
        {
            PlayerPrefs.SetInt(armorProfSaveName + x, 1);
        }
        foreach (int x in savethrowProfs)
        {
            PlayerPrefs.SetInt(saveThrowProfSaveName + x, 1);
        }
    }

}
