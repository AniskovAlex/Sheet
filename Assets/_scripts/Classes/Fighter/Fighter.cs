using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    const string FighterBattleStyleCountSaveName = "FghterBattleStyleCount_";
    const string FighterBattleStyleSaveName = "FghterBattleStyle_";
    const string FighterSubClassSaveName = "FighterSubClass_";
    const string levelSaveName = "lvl_";

    PlayerSubClass subClass = null;
    bool upFlag = false;


    public Fighter(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(10,
        new List<Armor.Type> { },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { },
        level, mainState, panel, basicForm, null, PB, false)
    {
        if (PlayerPrefs.HasKey(FighterSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(FighterSubClassSaveName))
            {
                case 0:
                    subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm, mainState, PB);
                    break;
            }
        }
    }

    public Fighter(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 },
        new List<int> { 0, 1 },
        level, 0, panel, basicForm, dropdownForm, 2, true)
    {
        if (PlayerPrefs.HasKey(FighterSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(FighterSubClassSaveName))
            {
                case 0:
                    subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm);
                    break;
            }
        }
    }

    public override void ShowAbilities(int level)
    {
        switch (level)
        {
            case 1:
                BattleStyle();
                SecondBreath();
                break;
            case 2:
                if (level < 17)
                    ActionSurge(1);
                else
                    ActionSurge(2);
                break;
            case 3:
                SubClass();
                break;
            case 4:
                AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
                upFlag = true;
                break;
            case 5:
                break;
            case 6:
                if (!upFlag)
                    AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
                break;
            case 7:
                break;
            case 8:
                if (!upFlag)
                    AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
                break;
            case 9:
                break;
            case 10:
                if (!upFlag)
                    AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
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

    void SecondBreath()
    {
        string caption = "������ �������";
        string abilityLevel = "1-� �������, ������ �����";
        string discription = "�� ��������� ������������ ���������� ������������, ������� ������ ���������������, ����� ������� ����. � ���� ��� �� ������ �������� ��������� ������������ ���� � ������� 1�10 + " + level + "(��� ������� �����).\n\n����������� ��� ������, �� ������ ��������� �������� ���� ��������������� �����, ����� �������� ����������� ������������ ��� �����.";

        CreatAbility(caption, abilityLevel, discription, 1);
    }

    void ActionSurge(int i)
    {

        string caption = "������� ��������";
        string abilityLevel = "2-� �������, ������ �����";
        string discription = "�� ��������� ����������� �� ��������� ���������� ������� �����������. � ���� ��� �� ������ ��������� ���� �������������� �������� ������ �������� � ��������� ��������. ����������� ��� ������, �� ������ ��������� �������� ��� ��������������� �����, ����� �������� ����������� ������������ ��� �����. ������� � 17-�� ������ �� ������ ������������ ��� ������ ������, ������ ��� ��� ����������� �����, �� � ������� ������ ���� ��� �� ����� ����� ������������ ���� ���� ���.";

        CreatAbility(caption, abilityLevel, discription, i);
    }

    void BattleStyle()
    {
        string caption = "������ �����";
        string abilityLevel = "1-� �������, ������ �����";
        List<string> battleStyleExcludedList = new List<string>();
        if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
        {
            int styles = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
            for (int i = 0; i < styles; i++)
            {
                battleStyleExcludedList.Add(PlayerPrefs.GetString(FighterBattleStyleSaveName + i));
            }
        }
        List<(string, string)> battleStyleList = new List<(string, string)>();
        battleStyleList.Add(("�������", "���� �� ������� ���������� ������ � ����� ����, � �� ����������� ������� ������, �� ��������� ����� +2 � ������� ����� ���� �������."));
        battleStyleList.Add(("������", "���� ��������, ������� �� ������, ������� �� ���, � ������ ��������, ����������� � �������� 5 ����� �� ���, �� ������ �������� ������� ������ ��� ������ �����. ��� ����� �� ������ ������������ ���."));
        battleStyleList.Add(("�������", "���� �� ������ �������, �� ��������� ����� +1 � ��."));
        battleStyleList.Add(("�������� ������� �������", "���� � ��� ������ �1� ��� �2� �� ����� ����� ������ ��� �����, ������� �� ��������� ���������� �������, ��������� ��� ����� ������, �� �� ������ ����������� ��� �����, � ������ ������������ ����� ���������, ���� ���� ����� ������ �1� ��� �2�. ����� ��������������� ���� �������������, ���� ������ ������ ����� �������� ���������� ��� ��������������."));
        battleStyleList.Add(("�������� ����� ��������", "���� �� ���������� ����� ��������, �� ������ �������� ����������� �������������� � ����� �� ������ �����."));
        battleStyleList.Add(("��������", "�� ��������� ����� +2 � ������ �����, ����� �������� ������������ �������."));
        if (redact)
        {
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList);
        }
        else
        {
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList);
        }
    }

    void SubClass()
    {
        string caption = "�������� �������";
        string abilityLevel = "������ ����� ���������� ������ ������� ��� ����������������� ����� �������� ������������. �������� ������� �������� ��������� ���� ������.";
        List<(string, string)> subClassList = new List<(string, string)>();
        subClassList.Add(("������ ������ ��������", "���, ��� ������ ������� ������� ������ ��������, ���������� �� �������, ������������ ����������� ������. ��� ������ ����� �������� ������ ������������� ������, � ����� �������� ����, ������ �� ���, ����� ���������� ���������� ��� �����������. �� ��� ����� �������� ������� ����� �������, ������ � ���������, ��������� � �������� ������� ������ ��������, �� ��, ��� ���� ������� ���, �������� ������� ��������������� �������, ����������� ����������� �������� � ��������."));
        CreatAbility(caption, abilityLevel, subClassList);
    }

    public override void ChooseSubClass(Dropdown mySelf)
    {
        base.ChooseSubClass(mySelf);
        switch (mySelf.captionText.text)
        {
            case "������ ������ ��������":
                subClass = new MasterOfMartialArt(level, panel.GetComponentInChildren<FormCreater>().GetComponentInChildren<Discription>().gameObject, basicForm, dropdownForm);
                break;
        }
    }

    public override void Save()
    {
        Debug.Log(level);
        PlayerPrefs.SetInt(levelSaveName, level);
        if (subClass != null)
        {
            subClass.Save();

        }
        switch (level)
        {
            case 1:
                base.Save();
                StylesSave();
                break;
            case 3:
                PlayerPrefs.SetInt(FighterSubClassSaveName, 0);
                break;
            case 4:
                AllClassesAbilities.SaveAbilitiesUp();
                break;
            case 6:
                AllClassesAbilities.SaveAbilitiesUp();
                break;
            case 8:
                AllClassesAbilities.SaveAbilitiesUp();
                break;
            case 10:
                AllClassesAbilities.SaveAbilitiesUp();
                break;
        }
    }

    void StylesSave()
    {
        Dropdown style = panel.GetComponentInChildren<Dropdown>();
        int count;
        if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
            count = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
        else
            count = 0;
        PlayerPrefs.SetInt(FighterBattleStyleCountSaveName, count + 1);
        PlayerPrefs.SetString(FighterBattleStyleSaveName + count, style.captionText.text);
    }
}
