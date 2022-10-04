using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    const string FighterBattleStyleCountSaveName = "FghterBattleStyleCount_";
    const string FighterBattleStyleSaveName = "FghterBattleStyle_";
    const string FighterSubClassSaveName = "FighterSubClass_";

    PlayerSubClass subClass = null;
    public Fighter()
    {
        LoadAbilities("fighter");
        healthDice = 10;
        armorProfs = new List<Armor.ArmorType>();
        weaponProfs = new List<Weapon.WeaponType>();
        instrumentsAmount = 0;
        instrumentProfs = new List<string>();
        skillsAmount = 2;
        skillProfs = new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 };
        savethrowProfs = new List<int> { 0, 1 };
    }

    public override Ability[] ChooseSubClass(int subClasses)
    {
        switch (subClasses)
        {
            case 0:
                subClass = new MasterOfMartialArt();
                break;
            default:
                return null;
        }
        return subClass.GetAbilities();
    }

}
