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
            headCaption.text = "Заговор, ";
        else
            headCaption.text = spell.level + "уровень, ";

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

        switch (spell.time)
        {
            default:
            case 1:
                time.text = "1 действие";
                break;
            case 2:
                time.text = "1 реакция";
                if (spell.reactionDis != null)
                    time.text += ", " + spell.reactionDis;
                break;
        }

        switch (spell.dist)
        {
            case 0:
                distance.text = "Касание";
                break;
            default:
                distance.text = spell.dist + " фт.";
                break;
        }

        foreach (Spell.Component x in spell.comp)
        {
            switch (x)
            {
                case Spell.Component.V:
                    components.text += "В";
                    break;
                case Spell.Component.S:
                    components.text += "С";
                    break;
                case Spell.Component.M:
                    components.text += "М";
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
                dur = "Мгновенное";
                break;
            case 2:
                dur = "1 раунд";
                break;
        }
        if (spell.concentration)
            duration.text = "Концентрация (" + dur + ")";
        else
            duration.text = dur;

        discription.text = spell.discription;
    }
}
