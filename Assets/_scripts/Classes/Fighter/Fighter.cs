using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    const string FighterBattleStyleCountSaveName = "FghterBattleStyleCount_";
    const string FighterBattleStyleSaveName = "FghterBattleStyle_";
    const string FighterSubClassSaveName = "FighterSubClass_";
    public Fighter()
    {
        name = "Воин";
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

    public override Ability[] ChooseSubClass(string subClasses)
    {
        switch (subClasses)
        {
            case "Мастер боевых искусств":
                subClass = new MasterOfMartialArt();
                break;
            default:
                return null;
        }
        return subClass.GetAbilities();
    }
}
