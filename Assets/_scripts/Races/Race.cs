using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Race : ObjectsBehavior
{
    const string LanguageCountSaveName = "LanguageCount_";
    const string LanguageSaveName = "Language_";
    public enum Language
    {
        giant,
        common
    }

    public enum Size
    {
        little,
        medium,
        large
    }

    int[] attrUp = new int[2];
    protected List<Language> languages = new List<Language>();
    protected Size size;
    int speed;
    protected Race(GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact, int speed) : base(panel, basicForm, dropdownForm, redact)
    {
        this.speed = speed;
    }

    protected void ChooseLanguage(int i)
    {
        string caption = "�����";
        List<(string, string)> inclidedList = new List<(string, string)>();
        inclidedList.Add(("�����", "�������� �������������: ����\n������������: �����"));
        inclidedList.Add(("���������", "�������� �������������: ����, ��������\n������������: ���������"));
        List<string> excludedList = new List<string>();
        foreach(Language x in languages)
        {
            switch (x)
            {
                case Language.common:
                    excludedList.Add("�����");
                    break;
                case Language.giant:
                    excludedList.Add("���������");
                    break;
            }
        }
        CreatAbility(caption, "", inclidedList, excludedList, i);
    }
}
