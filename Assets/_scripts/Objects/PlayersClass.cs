using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayersClass : MonoBehaviour
{
    int healthDice;
    List<Armor.Type> armorProfs;
    List<Weapon.Type> weaponProfs;
    List<string> instrumentProfs;
    List<int> skillProfs;
    List<int> savethrowProfs;
    protected List<string> staticAbility;
    protected List<string> activeAbility;

    protected PlayersClass(int healthDice, List<Armor.Type> armorProfs, List<Weapon.Type> weaponProfs, List<string> instrumentProfs, List<int> skillProfs, List<int> savethrowProfs)
    {
        this.healthDice = healthDice;
        this.armorProfs = armorProfs;
        this.weaponProfs = weaponProfs;
        this.instrumentProfs = instrumentProfs;
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
}
