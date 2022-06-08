using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    const string FighterBattleStyleCountSaveName = "FghterBattleStyleCount_";
    const string FighterBattleStyleSaveName = "FghterBattleStyle_";

    int mainState;
    GameObject panel;
    public GameObject basicForm;
    public GameObject dropdownForm;
    int PB;
    bool redact = false;

    public Fighter(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { })
    {
        this.mainState = mainState;
        this.panel = panel;
        this.PB = PB;
        this.basicForm = basicForm;
        redact = false;

        for (int i = 1; i <= level; i++)
        {
            ShowAbilities(i);
        }
    }

    public Fighter(GameObject panel, GameObject basicForm, GameObject dropdownForm, int mainState) : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 },
        new List<int> { 0, 1 })
    {
        PB = 2;
        this.mainState = mainState;
        this.panel = panel;
        this.basicForm = basicForm;
        this.dropdownForm = dropdownForm;
        redact = true;

        ShowAbilities(1);
        
    }

    void ShowAbilities(int level)
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

    void SecondBreath()
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "������ �������";
        form.AddText("1-� �������, ������ �����", FontStyle.Italic);
        form.AddText("�� ��������� ������������ ���������� ������������, ������� ������ ���������������, ����� ������� ����. � ���� ��� �� ������ �������� ��������� ������������ ���� � ������� 1�10 + ��� ������� �����.\n\n����������� ��� ������, �� ������ ��������� �������� ���� ��������������� �����, ����� �������� ����������� ������������ ��� �����.");
        if (!redact)
            form.AddConsumables(1);
    }

    void ActionSurge(int i)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "������� ��������";
        form.AddText("2-� �������, ������ �����", FontStyle.Italic);
        form.AddText("�� ��������� ����������� �� ��������� ���������� ������� �����������. � ���� ��� �� ������ ��������� ���� �������������� �������� ������ �������� � ��������� ��������. ����������� ��� ������, �� ������ ��������� �������� ��� ��������������� �����, ����� �������� ����������� ������������ ��� �����. ������� � 17-�� ������ �� ������ ������������ ��� ������ ������, ������ ��� ��� ����������� �����, �� � ������� ������ ���� ��� �� ����� ����� ������������ ���� ���� ���.");
        if (!redact)
            form.AddConsumables(i);
    }

    void BattleStyle()
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "������ �����";
        if (redact)
        {
            GameObject newBattleStyle = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
            Dropdown buf = newBattleStyle.GetComponent<Dropdown>();
            Text styleDiscriptionText = form.AddText("");
            buf.onValueChanged.AddListener(delegate { BattleStyleDiscription(buf, styleDiscriptionText); }); 
            List<string> battleStyleList = new List<string>{ "�������", "������" , "�������" , "�������� ������� �������" , "�������� ����� ��������", "��������" };
            List<string> battleStyleExcludedList = new List<string>();
            if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
            {
                int styles = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
                for (int i = 0; i < styles; i++)
                {
                    int style = PlayerPrefs.GetInt(FighterBattleStyleSaveName + i);
                    switch (style)
                    {
                        case 0:
                            battleStyleExcludedList.Add("�������");
                            break;
                        case 1:
                            battleStyleExcludedList.Add("������"); 
                            break;
                        case 2:
                            battleStyleExcludedList.Add("�������"); 
                            break;
                        case 3:
                            battleStyleExcludedList.Add("�������� ������� �������"); 
                            break;
                        case 4:
                            battleStyleExcludedList.Add("�������� ����� ��������"); 
                            break;
                        case 5:
                            battleStyleExcludedList.Add("��������"); 
                            break;
                    }
                }
            }
            newBattleStyle.GetComponent<SkillsDropdown>().list = battleStyleList;
            newBattleStyle.GetComponent<SkillsDropdown>().excludedList = battleStyleExcludedList;
            List<string> buf1 = new List<string>();
            foreach(string x in battleStyleList)
            {
                if (!battleStyleExcludedList.Contains(x))
                    buf1.Add(x);
            }
            buf.options.Add(new Dropdown.OptionData("�����"));
            for (int j = 0; j < buf1.Count; j++)
            {
                buf.options.Add(new Dropdown.OptionData(buf1[j].ToString()));
            }
        }
        else
        {
            if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
            {
                int styles = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
                for (int i = 0; i < styles; i++)
                {
                    int style = PlayerPrefs.GetInt(FighterBattleStyleSaveName + i);
                    switch (style)
                    {
                        case 0:
                            form.AddText("�������", 30, FontStyle.Bold);
                            form.AddText("���� �� ������� ���������� ������ � ����� ����, � �� ����������� ������� ������, �� ��������� ����� +2 � ������� ����� ���� �������.");
                            break;
                        case 1:
                            form.AddText("������", 30, FontStyle.Bold);
                            form.AddText("���� ��������, ������� �� ������, ������� �� ���, � ������ ��������, ����������� � �������� 5 ����� �� ���, �� ������ �������� ������� ������ ��� ������ �����. ��� ����� �� ������ ������������ ���.");
                            break;
                        case 2:
                            form.AddText("�������", 30, FontStyle.Bold);
                            form.AddText("���� �� ������ �������, �� ��������� ����� +1 � ��.");
                            break;
                        case 3:
                            form.AddText("�������� ������� �������", 30, FontStyle.Bold);
                            form.AddText("���� � ��� ������ �1� ��� �2� �� ����� ����� ������ ��� �����, ������� �� ��������� ���������� �������, ��������� ��� ����� ������, �� �� ������ ����������� ��� �����, � ������ ������������ ����� ���������, ���� ���� ����� ������ �1� ��� �2�. ����� ��������������� ���� �������������, ���� ������ ������ ����� �������� ���������� ��� ��������������.");
                            break;
                        case 4:
                            form.AddText("�������� ����� ��������", 30, FontStyle.Bold);
                            form.AddText("���� �� ���������� ����� ��������, �� ������ �������� ����������� �������������� � ����� �� ������ �����.");
                            break;
                        case 5:
                            form.AddText("��������", 30, FontStyle.Bold);
                            form.AddText("�� ��������� ����� +2 � ������ �����, ����� �������� ������������ �������.");
                            break;
                    }
                }
            }
        }
    }

    void BattleStyleDiscription(Dropdown style, Text textField)
    {
        switch (style.captionText.text)
        {
            default:
                textField.text = " ";
                break;
            case "�������":
                textField.text = "���� �� ������� ���������� ������ � ����� ����, � �� ����������� ������� ������, �� ��������� ����� +2 � ������� ����� ���� �������.";
                break;
            case "������":
                textField.text = "���� ��������, ������� �� ������, ������� �� ���, � ������ ��������, ����������� � �������� 5 ����� �� ���, �� ������ �������� ������� ������ ��� ������ �����. ��� ����� �� ������ ������������ ���.";
                break;
            case "�������":
                textField.text = "���� �� ������ �������, �� ��������� ����� +1 � ��.";
                break;
            case "�������� ������� �������":
                textField.text = "���� � ��� ������ �1� ��� �2� �� ����� ����� ������ ��� �����, ������� �� ��������� ���������� �������, ��������� ��� ����� ������, �� �� ������ ����������� ��� �����, � ������ ������������ ����� ���������, ���� ���� ����� ������ �1� ��� �2�. ����� ��������������� ���� �������������, ���� ������ ������ ����� �������� ���������� ��� ��������������.";
                break;
            case "�������� ����� ��������":
                textField.text = "���� �� ���������� ����� ��������, �� ������ �������� ����������� �������������� � ����� �� ������ �����.";
                break;
            case "��������":
                textField.text = "�� ��������� ����� +2 � ������ �����, ����� �������� ������������ �������.";
                break;
        }
    }

    public override void Save()
    {
        base.Save();
        Dropdown style = panel.GetComponentInChildren<Dropdown>();
        PlayerPrefs.SetInt(FighterBattleStyleCountSaveName, 1);

        switch (style.captionText.text)
        {
            case "�������":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 0);
                break;
            case "������":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 1);
                break;
            case "�������":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 2);
                break;
            case "�������� ������� �������":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 3);
                break;
            case "�������� ����� ��������":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 4);
                break;
            case "��������":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 5); 
                break;
        }

    }
}
