using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayersClass
{
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
}
