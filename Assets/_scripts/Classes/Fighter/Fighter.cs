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
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                SubClass();
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                SubClass();
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
                SubClass();
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
        List<string> battleStyleList;
        List<string> discriptionList = new List<string>();
        discriptionList.Add("���� �� ������� ���������� ������ � ����� ����, � �� ����������� ������� ������, �� ��������� ����� +2 � ������� ����� ���� �������.");
        discriptionList.Add("���� ��������, ������� �� ������, ������� �� ���, � ������ ��������, ����������� � �������� 5 ����� �� ���, �� ������ �������� ������� ������ ��� ������ �����. ��� ����� �� ������ ������������ ���.");
        discriptionList.Add("���� �� ������ �������, �� ��������� ����� +1 � ��.");
        discriptionList.Add("���� � ��� ������ �1� ��� �2� �� ����� ����� ������ ��� �����, ������� �� ��������� ���������� �������, ��������� ��� ����� ������, �� �� ������ ����������� ��� �����, � ������ ������������ ����� ���������, ���� ���� ����� ������ �1� ��� �2�. ����� ��������������� ���� �������������, ���� ������ ������ ����� �������� ���������� ��� ��������������.");
        discriptionList.Add("���� �� ���������� ����� ��������, �� ������ �������� ����������� �������������� � ����� �� ������ �����.");
        discriptionList.Add("�� ��������� ����� +2 � ������ �����, ����� �������� ������������ �������.");
        battleStyleList = new List<string> { "�������", "������", "�������", "�������� ������� �������", "�������� ����� ��������", "��������" };
        if (redact)
        {  
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList, discriptionList);
        }
        else
        {
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList, discriptionList);
        }
    }

    void SubClass()
    {
        if(redact)
            subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm);
        else
            subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm, mainState, PB);
        Debug.Log(subClass);
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
