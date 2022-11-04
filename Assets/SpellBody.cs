using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBody : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] Text headCaption;
    [SerializeField] Text time;
    [SerializeField] Text distance;
    [SerializeField] Text components;
    [SerializeField] Text duration;
    [SerializeField] Text discription;

    public void SetSpell(Spell spell)
    {
        head.text = spell.name;

        if (spell.level == 0)
            headCaption.text = "�������, ";
        else
            headCaption.text = spell.level + "�������, ";

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

        switch (spell.time)
        {
            default:
            case 1:
                time.text = "1 ��������";
                break;
            case 2:
                time.text = "1 �������";
                if (spell.reactionDis != null)
                    time.text += ", " + spell.reactionDis;
                break;
        }

        switch (spell.dist)
        {
            case 0:
                distance.text = "�������";
                break;
            default:
                distance.text = spell.dist + " ��.";
                break;
        }

        foreach (Spell.Component x in spell.comp)
        {
            switch (x)
            {
                case Spell.Component.V:
                    components.text += "�";
                    break;
                case Spell.Component.S:
                    components.text += "�";
                    break;
                case Spell.Component.M:
                    components.text += "�";
                    if (spell.materialDis != null)
                        components.text += "(" + spell.materialDis + ")";
                    break;
            }
            components.text += ", ";
        }
        components.text.Remove(components.text.Length - 2);

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
        }
        if (spell.concentration)
            duration.text = "������������ (" + dur + ")";
        else
            duration.text = dur;

        discription.text = spell.discription;
    }
}
