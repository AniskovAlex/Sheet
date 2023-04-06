using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellBody : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] Text headCaption;
    [SerializeField] Text time;
    [SerializeField] Text distance;
    [SerializeField] Text components;
    [SerializeField] Text duration;
    [SerializeField] GameObject discription;
    [SerializeField] Opener opener;
    [SerializeField] GameObject ent;

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
            case 3:
                text += "1 час";
                break;
            case 4:
                text += "Бонусное действие";
                break;
            case 5:
                text += "1 минута";
                break;
            case 6:
                text += "10 минут";
                break;
            case 7:
                text += "12 часов";
                break;
            case 8:
                text += "8 часов";
                break;
            case 9:
                text += "24 часа";
                break;
        }

        //time.text = text;
        text += "\n<b>Дистанция:</b> ";

        switch (spell.dist)
        {
            case -6:
                text += "500 миль";
                break;
            case -5:
                text += "Неограниченная";
                break;
            case -4:
                text += "1 миля";
                break;
            case -3:
                text += "В пределах видимости";
                break;
            case -2:
                text += "Особая";
                break;
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
        //distance.text = text;
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
        //components.text = text;
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
            case 5:
                dur = "10 дней";
                break;
            case 6:
                dur = "1 час";
                break;
            case 7:
                dur = "24 часа";
                break;
            case 8:
                dur = "8 часов";
                break;
            case 9:
                dur = "Пока не рассеется";
                break;
            case 10:
                dur = "2 часа";
                break;
            case 11:
                dur = "7 дней";
                break;
            case 12:
                dur = "Пока не рассеется или не сработает";
                break;
            case 13:
                dur = "30 дней";
                break;
            case 14:
                dur = "Особая";
                break;
            case 15:
                dur = "6 раундов";
                break;
        }
        if (spell.concentration)
            text += "Концентрация (" + dur + ")";
        else
            text += dur;
        //duration.text = text;
        text += "\n<b>Описание: </b>";
        text += spell.discription;
        //discription.text = text;
        Text buf;
        for (int i = text.IndexOf('\n'); i != -1; i = text.IndexOf('\n'))
        {
            buf = Instantiate(discription, ent.transform).GetComponent<Text>();
            buf.text = text.Remove(i);
            buf.fontSize = 40;
            buf.fontStyle = FontStyle.Normal;
            text = text.Substring(i + 1);
        }
        buf = Instantiate(discription, ent.transform).GetComponent<Text>();
        buf.text = text;
        buf.fontSize = 40;
        buf.fontStyle = FontStyle.Normal;
        //opener.init = false;
        //opener.HieghtSizeInit();
    }

    public Spell GetSpell()
    {
        return _spell;
    }
}
