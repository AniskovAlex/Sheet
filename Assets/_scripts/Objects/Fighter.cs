using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    int mainState;
    GameObject panel;
    public GameObject basicForm;
    int PB;

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

        for (int i = 1; i <= level; i++)
        {
            switch (i)
            {
                case 1:
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
    }

    public Fighter() : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1},
        new List<int> {0,1 })
    {
        PB = 2;
    }

    void SecondBreath()
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "������ �������";
        form.AddText("1-� �������, ������ �����", FontStyle.Italic);
        form.AddText("�� ��������� ������������ ���������� ������������, ������� ������ ���������������, ����� ������� ����. � ���� ��� �� ������ �������� ��������� ������������ ���� � ������� 1�10 + ��� ������� �����.\n\n����������� ��� ������, �� ������ ��������� �������� ���� ��������������� �����, ����� �������� ����������� ������������ ��� �����.");
        form.AddConsumables(1);
    }

    void ActionSurge(int i)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "������� ��������";
        form.AddText("2-� �������, ������ �����", FontStyle.Italic);
        form.AddText("�� ��������� ����������� �� ��������� ���������� ������� �����������. � ���� ��� �� ������ ��������� ���� �������������� �������� ������ �������� � ��������� ��������. ����������� ��� ������, �� ������ ��������� �������� ��� ��������������� �����, ����� �������� ����������� ������������ ��� �����. ������� � 17-�� ������ �� ������ ������������ ��� ������ ������, ������ ��� ��� ����������� �����, �� � ������� ������ ���� ��� �� ����� ����� ������������ ���� ���� ���.");
        form.AddConsumables(i);
    }
}
