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
                playersRace = new Human();
                break;
            case 2:
                playersRace = new Gnome();
                break;
        }
        FormCreater[] opener = content.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in opener)
        {
            Destroy(x.gameObject);
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
        FormCreater buf = formCreater.GetComponentInChildren<Discription>().GetComponentInChildren<FormCreater>();
        if (buf != null)
            Destroy(buf.gameObject);
        if (playersRace != null)
        {
            Ability[] abilityArr = playersRace.ChooseSubRace(value.captionText.text);
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
