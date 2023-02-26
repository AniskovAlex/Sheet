using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceAbilities : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject form;
    Race playersRace = null;

    public void ChosenRace(Dropdown value)
    {
        playersRace = null;
        switch (value.value)
        {
            case 1:
                playersRace = new Gnome();
                break;
            case 2:
                playersRace = new Dwarf();
                break;
            case 3:
                playersRace = new Dragonborn();
                break;
            case 4:
                playersRace = new HalfOrc();
                break;
            case 5:
                playersRace = new Halfling();
                break;
            case 6:
                playersRace = new HalfElf();
                break;
            case 7:
                playersRace = new Tiefling();
                break;
            case 8:
                playersRace = new Human();
                break;
            case 9:
                playersRace = new Elf();
                break;
        }
        FormCreater[] opener = content.GetComponentsInChildren<FormCreater>();
        for (int i = opener.Length - 1; i >= 0; i--)
        {
            DestroyImmediate(opener[i].gameObject);
        }
        if (playersRace != null)
        {
            Ability[] abilityArr = playersRace.GetAbilities();
            foreach (Ability x in abilityArr)
            {
                Instantiate(form, content.transform).GetComponent<FormCreater>().CreateAbility(x);
            }
        }
    }

    public void ChosenSubRace(Dropdown value, FormCreater formCreater)
    {
        FormCreater[] buf = formCreater.GetComponentInChildren<Discription>().GetComponentsInChildren<FormCreater>();
        for (int i = buf.Length - 1; i >= 0; i--)
            if (buf[i] != null)
                DestroyImmediate(buf[i].gameObject);
        if (playersRace != null)
        {
            Ability[] abilityArr = playersRace.ChooseSubRace(value.value);
            if (abilityArr != null)
                foreach (Ability x in abilityArr)
                {
                    Instantiate(form, formCreater.GetComponentInChildren<Discription>().transform).GetComponent<FormCreater>().CreateAbility(x);
                }
        }
    }
    public Race GetRace()
    {
        return playersRace;
    }
}
