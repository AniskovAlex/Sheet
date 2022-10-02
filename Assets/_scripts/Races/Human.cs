using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Human : Race
{
    public Human(GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(panel, basicForm, dropdownForm, true, 30)
    {

        //.AddFeat(panel, basicForm, dropdownForm, redact, false);
        //AllClassesAbilities.AttributiesUp(panel, basicForm, dropdownForm, 2, true);
        List<int> includedList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        //AllClassesAbilities.SetSkills(panel, basicForm, dropdownForm, "�����", includedList, 1);
        PresavedLists.languages.Add(PresavedLists.Language.common);
        //AllClassesAbilities.ChooseLanguage(panel, basicForm, dropdownForm, 1);
    }

    public Human(GameObject panel, GameObject basicForm) : base(panel, basicForm, null, false, 30)
    {

    }

    public Human()
    {

    }

    public override void Erase()
    {
        PresavedLists.languages.Remove(PresavedLists.Language.common);
    }

    public override void RaceDiscription()
    {
        FormCreater form = panel.GetComponentInParent<FormCreater>();
        if (form != null)
        {
            form.GetComponentInChildren<Text>().text = "�������";
            GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
            FormCreater formInForm = newObject.GetComponent<FormCreater>();
            newObject.GetComponentInChildren<Text>().text = "��������";
            //formInForm.AddText("� ����������� ����� ���� � ��� ����� ������� �� ��������������� ���. ��� ������ ����� �� ������� ����� � ����� ������� ������, ��� ������, ����� � �������. ��������, ������ ��������� �� ������ ���������� �� ���������� ���������� ��� ����� �������� � ��������� �� ����. � ���� �����, ��� ����� ���-�� �������� ������� �����, � ������� ������� ������� �������, ���������� �� ����������� � ��������. ��� �� �� ������� ���, ���� ������ ���� ������������ � ��������� �� ���� �����.", FontStyle.Italic);
        }
    }

    public override void Save()
    {
        base.Save();
        //AllClassesAbilities.SaveFeat();
        //AllClassesAbilities.SaveAttributies();
        PlayerPrefs.SetString(characterName + raceSaveName, "�������");
    }
}
