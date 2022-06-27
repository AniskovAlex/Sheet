using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterOfMartialArt : PlayerSubClass
{
    const string MOMABattleSupriorityCountSaveName = "MOMABattleSupriorityCount_";
    const string MOMABattleSuprioritySaveName = "MOMABattleSupriority_";
    bool flagBatSup= false;

    public MasterOfMartialArt(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm) : base (level, 0, panel, basicForm, dropdownForm, 0, true)
    {
    }

    public MasterOfMartialArt(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm, int mainState, int PB) : base(level, mainState, panel, basicForm, dropdownForm, PB, false)
    {
    }

    public override void ShowAbilities(int level)
    {
        switch (level)
        {
            case 3:
                BattleSupriority(3);
                break;
            case 7:
                BattleSupriority(2);
                break;
            case 10:
                BattleSupriority(2);
                break;
            case 15:
                BattleSupriority(2);
                break;
        }
    }

    void BattleSupriority(int count)
    {
        string caption = "������ �������������";
        string abilityLevel = "3-� �������, ������ ������� ������ ��������";
        List<string> battleStyleExcludedList = new List<string>();
        if (PlayerPrefs.HasKey(MOMABattleSupriorityCountSaveName))
        {
            int styles = PlayerPrefs.GetInt(MOMABattleSupriorityCountSaveName);
            for (int i = 0; i < styles; i++)
            {
                battleStyleExcludedList.Add(PlayerPrefs.GetString(MOMABattleSuprioritySaveName + i));
            }
        }
        List<(string,string)> battleStyleList = new List<(string, string)>();
        List<string> discriptionList = new List<string>();
        battleStyleList.Add(("�������� ���������", "��� ����������� �� ������ ��������� ���� ����� �������������, ��������� � ������ � �������� �������� �������� � ��, ���� �� ���������� �����������."));
        battleStyleList.Add(("����� � �������","���� �� � ���� ��� ���������� ���������� ����� �������, �� ������ ��������� ���� ����� �������������, ����� ��������� ������������ ���� ����� �� 5 �����. � ������ ��������� �� ���������� ����� ������������� � ������ ����� ���� �����."));
        battleStyleList.Add(("����� � ��������","���� �� ��������� �� �������� ������ �������, �� ������ ��������� ���� ����� �������������, ����� ���� �� ����� ��������� ���� ������������� � ����� �������� ���������. �� ���������� ����� ������������� � ������ ����� ���� ����� � ��������� ������������� ��������, ������� ����� ������ ��� ������� ���. ��� �������� ����� �������� ������������� �� ���������� �� �������� ����� ��������, �� ���������� ��� ���� ����� �� ���� ����� �����."));
        battleStyleList.Add(("����� � �������","���� �� ��������� �� �������� ������ �������, �� ������ ��������� ���� ����� �������������, ����� ���������� �������� ����. �� ���������� ����� ������������� � ������ ����� ���� �����, � ���� ������ ��������� ���������� ��������. ��� ������� ���� �������� ���� �� ����� ������ ���������� ����."));
        battleStyleList.Add(("����� � ������","�� ������ � ���� ��� ��������� ���� ����� ������������� � �������� ��������� ��������� ����, ������ � �������� ���� ���� �������� � �������� 5 �����. ��������� ������ ����� �� ����� �������� � ���� ���� �� ���������� � �������������. ���� ����� ��������, �������� ����� ������������� � ������ ����� ���� �����. ��� ������������ ���������, ���� �� �� ����������� �� � ��� �� ����, � ������� �������� ��."));
        battleStyleList.Add(("��������������� �����","���� �� ��������� �� �������� ������ �������, �� ������ ��������� ���� ����� �������������, ����� ���������� ����������� ����������, ��������� ��� �������� ���� ������� �� ������ ������, ������� �� ������. �� ���������� ����� ������������� � ������ ����� �����, � ���� ������ ��������� ���������� ����. � ������ ������� ��� ������ ��������� ���� �������. ������� ������ � � ���."));
        battleStyleList.Add(("�������������� �����","���� �� ��������� �� �������� ������ �������, �� ������ ��������� ���� ����� �������������, ����� ���������� ����� ���� � ���. �� ���������� ����� ������������� � ������ ����� �����, �, ���� ������ ���� ������� ��� ������, ��� ������ ��������� ���������� ����. � ������ ������� �� �������� ���� � ���."));
        battleStyleList.Add(("�������� ����","���� �������� ������������� �� ��� ���������� ������, �� ������ �������� ��������� ���� ����� �������������, ����� ��������� ���������� ����� ������� �� ����� ��������. ���� �� ���������, �� ���������� ����� ������������� � ������ ����� ���� �����."));
        battleStyleList.Add(("����������� ����","���� �� ��������� �� �������� ������ �������, �� ������ ��������� ���� ����� �������������, ����� ������� ��������, �������� ��� ��� ����� ���������. �� ���������� ����� ������������� � ������ ����� ���� �����. ��������� ������ ����� �� ���� ���� ������ �������� ����� ��� ����������� � �������������, ���� ����� ����������� �� ������ ������ ���������� ����."));
        battleStyleList.Add(("�����������","���� ������ �������� ��������� ��� ���� ���������� ������, �� ������ �������� ��������� ���� ����� �������������, ����� ��������� ���� �� ��������, ������ ������ ����� ����� ������������� + ��� ����������� ��������."));
        battleStyleList.Add(("������������� �����","���� �� ��������� �� �������� ������ �������, �� ������ ��������� ���� ����� �������������, ����� ���������� �������������� ���������� ��������� ���. �� ���������� ����� ������������� � ������ ����� ���� �����, � ���� ������ ��������� ���������� ��������. ��� ������� ���� �� ����� ������ ���������� ���� ��������� � ������� ������ ����� �� ���� �����, ����� ���."));
        battleStyleList.Add(("���������","�� ������ � ���� ��� �������� ��������� ��������� ���� ����� �������������, ����� ���������� ��������� ������ �� ����� ���������. ���� �� ��� ��������, �������� ������������� ��������, ������� ����� ������ ��� ������� ���. ��� �������� �������� ��������� ����, ������ ������ ����� ������������� + ��� ����������� �������."));
        battleStyleList.Add(("��������� �����","���� �� ��������� �� �������� ������ �������, �� ������ ��������� ���� ����� �������������, ����� ���������� ���������� ����. �� ���������� ����� ������������� � ������ ����� �����, �, ���� ������ ���� ������� ��� ������, ��� ������ ��������� ���������� ����. ��� ������� �� �������� ���� �� ���������� �� 15 ����� �� ����."));
        battleStyleList.Add(("������ �����","���� �� ���������� ������ ����� ������� �� ��������, �� ������ ��������� ���� ����� �������������, ����� �������� � � ������. �� ������ ������������ ���� ���� �� ��� ����� ���������� ������ �����, �� �� ���������� �������� �����."));
        battleStyleList.Add(("���� ������������","���� �� ���������� � ���� ��� �������� �����, �� ������ ���������� �� ����� �� ����� ���� � �������� ��������� ��������� ���� ������ �� ����� ����������. ���� �� ��� ��������, �������� ������������� ��������, ������� ����� ������ ��� ������� ��� � ��������� ���� ����� �������������. ��� �������� ����� ���������� ��������� �������� ���� ����� �������, ������� ����� ������������� � ������ ����� ���� �����."));
        battleStyleList.Add(("������� �����","���� �� ��������� �� �������� ������ ���������� �������, �� ������ ��������� ���� ����� �������������, ����� ���������� ��������� ���� ������� �������� ���� �� ������. �������� ������ �������� � �������� 5 ����� �� �������������� ���� � � �������� ����� ������������. ���� �������� ������ ����� ����� �� �� ������� ��������, ��� �������� ����, ������ ������ ����� �������������. ���� ���� �� ����, ��� � ��� �������� �����."));
        if (redact)
        {
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList, count);
        }
        else
        {
            if (!flagBatSup)
            {
                CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList, count);
                flagBatSup = true;            
            }
        }
    }

    public override void Save()
    {
        switch (level)
        {
            case 3:
                base.Save();
                BattleSuprioritySave();
                break;
            case 7:
                BattleSuprioritySave();
                break;
            case 10:
                BattleSuprioritySave();
                break;
            case 15:
                BattleSuprioritySave();
                break;
        }
    }

    void BattleSuprioritySave()
    {
        Dropdown[] styles = panel.GetComponentsInChildren<Dropdown>();
        int count;
        if (PlayerPrefs.HasKey(MOMABattleSupriorityCountSaveName))
            count = PlayerPrefs.GetInt(MOMABattleSupriorityCountSaveName);
        else
            count = 0;
        foreach (Dropdown x in styles)
        {
            PlayerPrefs.SetString(MOMABattleSuprioritySaveName + count, x.captionText.text);
            count++;
        }
        PlayerPrefs.SetInt(MOMABattleSupriorityCountSaveName, count);
    }
}