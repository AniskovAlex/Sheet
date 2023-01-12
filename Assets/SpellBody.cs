using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBody : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] Text headCaption;
    [SerializeField] Text body;

    Spell _spell;

    public void SetSpell(Spell spell)
    {
        _spell = spell;
        head.text += spell.name;

        if (spell.level == 0)
            headCaption.text += "�������, ";
        else
            headCaption.text += spell.level + " �������, ";

        switch (spell.spellType)
        {
            default:
            case Spell.Type.Abjuration:
                headCaption.text += "����������";
                break;
            case Spell.Type.Conjuration:
                headCaption.text += "�����";
                break;
            case Spell.Type.Divination:
                headCaption.text += "����������";
                break;
            case Spell.Type.Enchantment:
                headCaption.text += "����������";
                break;
            case Spell.Type.Evocation:
                headCaption.text += "����������";
                break;
            case Spell.Type.Illusion:
                headCaption.text += "�������";
                break;
            case Spell.Type.Necromancy:
                headCaption.text += "�����������";
                break;
            case Spell.Type.Transmutation:
                headCaption.text += "��������������";
                break;
        }
        if (spell.ritual)
            headCaption.text += " (������)";

        string text = "<b>�����:</b> ";
        switch (spell.time)
        {
            default:
            case 1:
                text += "1 ��������";
                break;
            case 2:
                text += "1 �������";
                if (spell.reactionDis != null)
                    text += ", " + spell.reactionDis;
                break;
            case 3:
                text += "1 ���";
                break;
            case 4:
                text += "�������� ��������";
                break;
            case 5:
                text += "1 ������";
                break;
            case 6:
                text += "10 �����";
                break;
            case 7:
                text += "12 �����";
                break;
            case 8:
                text += "8 �����";
                break;
            case 9:
                text += "24 ����";
                break;
        }

        text += "\n<b>���������:</b> ";

        switch (spell.dist)
        {
            case -6:
                text += "500 ����";
                break;
            case -5:
                text += "��������������";
                break;
            case -4:
                text += "1 ����";
                break;
            case -3:
                text += "� �������� ���������";
                break;
            case -2:
                text += "������";
                break;
            case -1:
                text += "�� ����";
                break;
            case 0:
                text += "�������";
                break;
            default:
                text += spell.dist + " ��.";
                break;
        }
        text += "\n<b>����������:</b> ";
        foreach (Spell.Component x in spell.comp)
        {
            switch (x)
            {
                case Spell.Component.V:
                    text += "�";
                    break;
                case Spell.Component.S:
                    text += "�";
                    break;
                case Spell.Component.M:
                    text += "�";
                    if (spell.materialDis != null)
                        text += "(" + spell.materialDis + ")";
                    break;
            }
            text += ", ";
        }
        text.Remove(text.Length - 2);
        text += "\n<b>������������:</b> ";
        string dur;
        switch (spell.duration)
        {
            default:
            case 1:
                dur = "����������";
                break;
            case 2:
                dur = "1 �����";
                break;
            case 3:
                dur = "1 ������";
                break;
            case 4:
                dur = "10 �����";
                break;
            case 5:
                dur = "10 ����";
                break;
            case 6:
                dur = "1 ���";
                break;
            case 7:
                dur = "24 ����";
                break;
            case 8:
                dur = "8 �����";
                break;
            case 9:
                dur = "���� �� ���������";
                break;
            case 10:
                dur = "2 ����";
                break;
            case 11:
                dur = "7 ����";
                break;
            case 12:
                dur = "���� �� ��������� ��� �� ���������";
                break;
            case 13:
                dur = "30 ����";
                break;
            case 14:
                dur = "������";
                break;
            case 15:
                dur = "6 �������";
                break;
        }
        if (spell.concentration)
            text += "������������ (" + dur + ")";
        else
            text += dur;
        text += "\n<b>��������:</b>\n";
        text += spell.discription;
        body.text = text;
    }

    public Spell GetSpell()
    {
        return _spell;
    }
}
