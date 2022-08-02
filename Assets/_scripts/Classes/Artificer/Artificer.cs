using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artificer : PlayersClass
{

    const string ArtificerSubClassSaveName = "ArtificerSubClass_";

    PlayerSubClass subClass = null;
    bool upFlag = false;


    public Artificer(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(8,
        new List<Armor.Type> { },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { },
        level, mainState, panel, basicForm, null, PB, false)
    {
        if (PlayerPrefs.HasKey(characterName + ArtificerSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(characterName + ArtificerSubClassSaveName))
            {
                case 0:
                    subClass = new Alchemist(level, panel, basicForm, dropdownForm, mainState, PB);
                    break;
            }
        }
    }

    public Artificer(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(8,
        new List<Armor.Type> { Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 2, 4, 5, 6, 7, 9, 11 },
        new List<int> { 2, 3 },
        level, 0, panel, basicForm, dropdownForm, 2, true)
    {
        if (PlayerPrefs.HasKey(characterName + ArtificerSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(characterName + ArtificerSubClassSaveName))
            {
                case 0:
                    subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm);
                    break;
            }
        }
    }

    public override void ClassDiscription()
    {
        FormCreater form = panel.GetComponentInParent<FormCreater>();
        if (form != null)
        {
            form.GetComponentInChildren<Text>().text = "������������";
            GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
            FormCreater formInForm = newObject.GetComponent<FormCreater>();
            newObject.GetComponentInChildren<Text>().text = "��������";
            formInForm.AddText("������������ � ���������� ������� ���������� ����� � ������� ���������. ��� ������������� ����� ��� ������� �������, ������� ������� ������������ � ��������� � ����������� � ������������. � ��������� ���������� �������� �� ������ �� ����������� ��� ���� �� ������ �� �������������.\n\n��� ����������� ����� ���������� ���� ��� ���������� ��������� �����������.���������� ����������, ������������ ����� ������������ �������������� �������� ��� �������� ������� ��������, ����� ����������, ����� ���������� ���� ���� �� �������� �������� ��� ����������� ����������, ����� ������� ��������� ������.����� ������������� ������� � �� ������������� � �������������, � ���� ���, ����� ���, ������ ������� ���������� ������� ����������.");
                }
    }

    public override void ShowAbilities(int level)
    {
        switch (level)
        {
            case 1:
                if (redact)
                    AllClassesAbilities.SetSkills(panel, basicForm, dropdownForm, "������", GetSkillProfs(), 2);
                MagickCrafter();
                break;
            case 2:
                Infusion();
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

    void MagickCrafter()
    {
        string caption = "���������� ����������";
        string abilityLevel = "1-� �������, ������ ������������";
        string discription = "�� ��������� ���������� ����� ����� � ������� ��������. ����� ������������ ��� ������, �� ������ ������� � ����� ��������� ����������� ��� ����������� ������������. ����� ��������� �� ��������� ���������� ������������� ������� � ��������� ��� ����� �� ��������� ���������� ������� �� ��� �����:\n\n������������ ������ �������� ����� ���� � ������� 5 ����� � ������� ���� � ������� ��� 5 �����\n������ ����������� ���������� ���������, ������� ����� �������� � �������� 10 ����� ������ ���, ����� �� ���� ������������� ��������.�� ����������� ��� ���������, ����� ��������� ������ ������ ���������, � ���� ������ �� ����� ���� ������� 6 ������.\n������ ���������� ��������� ����� ��� ����� ���� �� ��� �����(�����, �����, ����������� � ������). ��������� ������� ����� ������� �� ���������� 10 �����\n��������� ���������� ������ ���������� �� ����� �� ������������ �������. ���� ������ ����� ���� ������������, ������� �� 25 ����, ������� � ������� ��� ����������� ���� ��������� �� ������ ������.\n��������� �������� �������� �������� �������� �������. ��������� �� ������ ��������� ������� � ������ ��� ����� ��������.\n\n����� ������� ����� �������� ����������� ���������� ��������� ���������, �� �� ������, ��� ���� �������� �� ���� �������.������������ ���������� ��������, ������� �� ������ �������� ������ �� ���� ���, ����� " + mainState + " - ����������� ����������(������� ���� ������).���� �� ��������� ��������� ���� ��������, ����� ������ �������� ���������� �������������, � ����� �������� ����������� ����� ��������.";
        CreatAbility(caption, abilityLevel, discription);
    }

    void Infusion()
    {
        string caption = "�������";
        string abilityLevel = "2-� �������, ������ ������������";
        string discription = "���������� ��������������� �����, �� ������ ����������� �� ������������� ������� � ��������� ��� ������ � ������� �������. ������� ��������� ������ �� �� ���� ��������, ������� ������� � � ��������. ���� ��������� ������� ������� ���������, �� ������ ����������� �� ���� ����� ��. ���� �� ������ ����������� �� ������� �����, �� ������ ������ ������� ���, ��������� ������� ������� ��������� (��. ������ ���������� � ������������ �������).\n\n���� ������� �������� � �������� ���������� �����, �� ����� �� ��������, ��� �������� ����� ���������� ����, ������ ������ ������������ ����������(������� 1 ����).�������� ��� �����, ���� �� ������������� �� ������ ���� �������, ����� ������� ������.\n\n���������� ��������������� �����, �� ������ ��������� ������ ����� ������ ������������� �������. ������������ �� ���������� ���������� � ������� �������� ��������� ������� ��������������. �� ������ �������� ��������, � ������ �� ����� ������� ����� ���� ��������� ������ � ������ ������� �������������. ����� ����, �� ���� ������� �� �������� ������� ���������� ��� ����� ��� ����� ��������.���� �� ����������� ��������� ������������ ���������� �������, ����� ������ �� ��� ���������� �������������, � ����� ����������� �����.\n\n���� �� ��������, � ������� ���������� ������ ����, ��������, ����� ��������[bag of holding], ������������� �������� �������, ��� ���������� ������ ���������� ������ ����.";
        CreatAbility(caption, abilityLevel, discription);
    }

    void SubClass()
    {
        string caption = "������������� ������������";
        string abilityLevel = "������������ ����� ��������� ������ � ��������� ��������. ������������� �������� �� ������.";
        List<(string, string)> subClassList = new List<(string, string)>();
        subClassList.Add(("�������", "������� � ������� �� �������� ���������� �������� ���� �������������� ������������ ���������. �������� ���������� ���� ��������, ����� ������ � �������� �����. ������� � ���������� �� ���������������� ��������, � � ��������������������� ������� �������� � �� ����� ����, � � ���� ����."));
        CreatAbility(caption, abilityLevel, subClassList);
    }

    public override void ChooseSubClass(Dropdown mySelf)
    {
        base.ChooseSubClass(mySelf);
        switch (mySelf.captionText.text)
        {
            case "�������":
                subClass = new Alchemist(level, panel.GetComponentInChildren<FormCreater>().GetComponentInChildren<Discription>().gameObject, basicForm, dropdownForm);
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
            if (PlayerPrefs.GetString(characterName + levelLabelSaveName + i) == "������������")
            {
                PlayerPrefs.SetInt(characterName + levelSaveName + i, level);
                flag = true;
                break;
            }
        }

        if (subClass != null)
        {
            subClass.Save();
        }
        switch (level)
        {
            case 1:
                base.Save();
                if (!flag)
                {
                    PlayerPrefs.SetString(characterName + levelLabelSaveName + count, "������������");
                    PlayerPrefs.SetInt(characterName + levelSaveName + count, level);
                    PlayerPrefs.SetInt(characterName + levelCountSaveName, count + 1);
                }
                break;
            case 3:

                // ��� ��������
                PlayerPrefs.SetInt(characterName + ArtificerSubClassSaveName, 0);
                break;
            case 4:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
            case 8:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
        }
    }
}
