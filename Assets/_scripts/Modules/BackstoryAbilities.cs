using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackstoryAbilities : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject form;
    Backstory playersBackstory = null;

    public void ChosenBackstory(Dropdown value)
    {
        playersBackstory = null;
        switch (value.value)
        {
            case 1:
                playersBackstory = new Artist();
                break;
        }
        FormCreater[] opener = content.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in opener)
        {
            Destroy(x.gameObject);
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
