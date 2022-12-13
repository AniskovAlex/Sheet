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
            headCaption.text += "Заговор, ";
        else
            headCaption.text += spell.level + " уровень, ";

        switch (spell.spellType)
        {
            default:
            case Spell.Type.Abjuration:
                headCaption.text += "ограждение";
                break;
            case Spell.Type.Conjuration:
                headCaption.text += "вызов";
                break;
            case Spell.Type.Divination:
                headCaption.text += "прорицание";
                break;
            case Spell.Type.Enchantment:
                headCaption.text += "очарование";
                break;
            case Spell.Type.Evocation:
                headCaption.text += "воплащение";
                break;
            case Spell.Type.Illusion:
                headCaption.text += "иллюзия";
                break;
            case Spell.Type.Necromancy:
                headCaption.text += "некромантия";
                break;
            case Spell.Type.Transmutation:
                headCaption.text += "преобразование";
                break;
        }
        if (spell.ritual)
            headCaption.text += " (ритуал)";

        string text = "<b>Время:</b> ";
        switch (spell.time)
        {
            default:
            case 1:
                text += "1 действие";
                break;
            case 2:
                text += "1 реакция";
                if (spell.reactionDis != null)
                    text += ", " + spell.reactionDis;
                break;
        }

        text += "\n<b>Дистанция:</b> ";

        switch (spell.dist)
        {
            case -1:
                text += "На себя";
                break;
            case 0:
                text += "Касание";
                break;
            default:
                text += spell.dist + " фт.";
                break;
        }
        text += "\n<b>Компоненты:</b> ";
        foreach (Spell.Component x in spell.comp)
        {
            switch (x)
            {
                case Spell.Component.V:
                    text += "В";
                    break;
                case Spell.Component.S:
                    text += "С";
                    break;
                case Spell.Component.M:
                    text += "М";
                    if (spell.materialDis != null)
                        text += "(" + spell.materialDis + ")";
                    break;
            }
            text += ", ";
        }
        text.Remove(text.Length - 2);
        text += "\n<b>Длительность:</b> ";
        string dur;
        switch (spell.duration)
        {
            default:
            case 1:
                dur = "Мгновенное";
                break;
            case 2:
                dur = "1 раунд";
                break;
            case 3:
                dur = "1 минута";
                break;
            case 4:
                dur = "10 минут";
                break;
        }
        if (spell.concentration)
            text += "Концентрация (" + dur + ")";
        else
            text += dur;
        text += "\n<b>Описание:</b>\n";
        text += spell.discription;
        body.text = text;
    }

    public Spell GetSpell()
    {
        return _spell;
    }
}
