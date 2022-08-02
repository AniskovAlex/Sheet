using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist : Backstory
{
    public Artist(GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(panel, basicForm, dropdownForm, true)
    {
        PresavedLists.skills.Add("����������");
        PresavedLists.skills.Add("�����������");
        Special();
    }

    public Artist(GameObject panel, GameObject basicForm) : base(panel, basicForm, null, false)
    {
        Special();
    }

    void Special()
    {
        string caption = "������: �� �������������� ��������";
        string discription = "�� ������ ������ ����� ����� ��� �����������. ������ ��� ������� ��� ��������� ����, �� ��� ����� ���� ����, ����� ��� ���� ���� �������� ���������. � ���� ����� �� ��������� ���������� ������ � ��� �� �������� ��� ���������� ���������� (� ����������� �� �������� ���������), ���� �� ���������� ������ �����. ����� ����, ���� ����������� ������ ��� ������� �������������. ����� ����������� ������ ��� � ������, � ������� �� ������ �������������, ���, ������ �����, ����� � ��� ���������� ������.";
        CreatAbility(caption, "", discription);
    }

    public override void Save()
    {
        base.Save();
        PlayerPrefs.SetString(characterName + backstorySaveName, "������");
    }
}
