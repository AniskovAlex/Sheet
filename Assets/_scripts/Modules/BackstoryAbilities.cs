using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackstoryAbilities : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject form;
    public Action chosen;
    Backstory playersBackstory = null;

    public void ChosenBackstory(Dropdown value)
    {
        if (chosen != null)
            chosen();
        playersBackstory = null;
        switch (value.value)
        {
            case 1:
                playersBackstory = new Artist();
                break;
            case 2:
                playersBackstory = new Waif();
                break;
            case 3:
                playersBackstory = new Noble();
                break;
            case 4:
                playersBackstory = new GuildArtiser();
                break;
            case 5:
                playersBackstory = new Sailor();
                break;
            case 6:
                playersBackstory = new Sage();
                break;
            case 7:
                playersBackstory = new PeoplesHero();
                break;
            case 8:
                playersBackstory = new Hermit();
                break;
            case 9:
                playersBackstory = new Criminal();
                break;
            case 10:
                playersBackstory = new Acolyte();
                break;
            case 11:
                playersBackstory = new Soldier();
                break;
            case 12:
                playersBackstory = new Foreigner();
                break;
            case 13:
                playersBackstory = new Charlatan();
                break;
        }
        FormCreater[] opener = content.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in opener)
        {
            DestroyImmediate(x.gameObject);
        }
        if (playersBackstory != null)
        {
            Ability[] abilityArr = playersBackstory.GetAbilities();
            foreach (Ability x in abilityArr)
            {
                Instantiate(form, content.transform).GetComponent<FormCreater>().CreateAbility(x);
            }
        }
    }

    public Backstory GetBackstory()
    {
        return playersBackstory;
    }
}
