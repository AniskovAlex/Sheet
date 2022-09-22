using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harengon : Race
{
    public Harengon(GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(panel, basicForm, dropdownForm, true, 30)
    {
        AllClassesAbilities.AttributiesUp(panel, basicForm, dropdownForm, 3, false);
        PresavedLists.languages.Add(PresavedLists.Language.common);
        AllClassesAbilities.ChooseLanguage(panel, basicForm, dropdownForm, 1);
        PresavedLists.skills.Add("��������������");
        Trigger();
        Dodge();
        Jump(2);
    }

    public Harengon(GameObject panel, GameObject basicForm, int PB) : base(panel, basicForm, null, false, 30)
    {
        Trigger();
        Dodge();
        Jump(PB);
    }

    public Harengon()
    {

    }

    public override void RaceDiscription()
    {
        FormCreater form = panel.GetComponentInParent<FormCreater>();
        if (form != null)
        {
            form.GetComponentInChildren<Text>().text = "��������";
            GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
            FormCreater formInForm = newObject.GetComponent<FormCreater>();
            newObject.GetComponentInChildren<Text>().text = "��������";
            formInForm.AddText("��������� ����� �� ������ ���, ��� ��� �������� �� �������� � ��������� ��� ������� � �����������. �� �������� ��� ��������� ����������� � ������ ����, ������� � ����� ��������� ������ ��� � �� ���� ���� ������ ����� �����.\n\n��������� ��������, � ������������ ���������� �������� ������, �� ������� ��� � ������, � ��� �� ����� ��������� ������.��� �������� ������� ��������� � ������� ������ �������� ������� � ��������� ��������, ��� ���������� �������.��������� �������� ��������� ������, � �� ����� ���� ��������� � ���������� ���������� ����� �� ���������� �� ����� �����������.");
        }
    }

    void Trigger()
    {

        string caption = "������ �������";
        string abilityLevel = "";
        string discription = "�� ������ �������� ���� ����� ���������� � ������ ������ ����������.";
        CreatAbility(caption, abilityLevel, discription);
    }
    void Dodge()
    {

        string caption = "������� �����";
        string abilityLevel = "";
        string discription = "���� �� ������������ ���������� ��������, �� ������ �������� ������� �4 � �������� ��� � ����������, ������������ ��������� ������ � �����. �� �� ������ ������������ ��� �������, ���� �� ����� � ��� ��� ���� �������� ����� 0.";
        CreatAbility(caption, abilityLevel, discription);
    }
    void Jump(int i)
    {

        string caption = "�������� ������";
        string abilityLevel = "";
        string discription = "�������� ��������� �� ������ �������� �� ���������� ����� ������ ������ ������������ ������ ����������, �� ������� ��������������� ����. �� ������ ������������ ��� ����������� ���� ���� �������� ������ 0. �� ������ ������������ ��� ����������� ���������� ���, ������ " + i + " (����� ����������). �� ���������������� ��� ��������������� ���������� ����� ��������� ���������������� ������.";
        CreatAbility(caption, abilityLevel, discription, i);
    }

    public override void Erase()
    {
        PresavedLists.languages.Remove(PresavedLists.Language.common);
        PresavedLists.skills.Remove("��������������");
    }
    public override void Save()
    {
        base.Save();
        AllClassesAbilities.SaveAttributies();
        PlayerPrefs.SetString(characterName + raceSaveName, "��������");
    }

}
