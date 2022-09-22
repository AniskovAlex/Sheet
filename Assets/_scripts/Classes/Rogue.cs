using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : PlayersClass
{
    public Rogue(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(10,
        new List<Armor.ArmorType> { },
        new List<Weapon.WeaponType> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { },
        level, mainState, panel, basicForm, null, PB, false)
    {

    }

    public Rogue(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(10,
        new List<Armor.ArmorType> {},
        new List<Weapon.WeaponType> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 },
        new List<int> { 0, 1 },
        level, 0, panel, basicForm, dropdownForm, 2, true)
    {

    }

    public Rogue()
    {

    }

    public override void ShowAbilities(int level)
    {
        switch (level)
        {
            case 1:
                
                break;
            case 2:
                
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
        }
    }

    public override void Save()
    {
        Debug.Log(level);
        int count = PlayerPrefs.GetInt(characterName + levelCountSaveName);
        bool flag = false;
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.GetString(characterName + levelLabelSaveName + i) == "Плут")
            {
                PlayerPrefs.SetInt(characterName + levelSaveName + i, level);
                flag = true;
                break;
            }
        }
        switch (level)
        {
            case 1:
                base.Save();
                if (!flag)
                {
                    PlayerPrefs.SetString(characterName + levelLabelSaveName + count, "Плут");
                    PlayerPrefs.SetInt(characterName + levelSaveName + count, level);
                    PlayerPrefs.SetInt(characterName + levelCountSaveName, count + 1);
                }
                break;
        }
    }
}
