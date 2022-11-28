using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;

public class ClassesAbilities : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject form;
    [SerializeField] GameObject health;
    PlayersClass playersClass = null;
    private void Awake()
    {
        Debug.Log(CharacterCollection.GetName());
    }

    public void ChosenClass(Dropdown value)
    {
        playersClass = null;
        switch (value.value)
        {
            case 1:
                playersClass = new Fighter();
                break;
        }
        FormCreater[] opener = content.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in opener)
        {
            DestroyImmediate(x.gameObject);
        }
        Instantiate(health, content.transform);
        if (playersClass != null)
        {
            Ability[] abilitieSubClassArr = playersClass.ChooseSubClass(DataSaverAndLoader.LoadSubClass(playersClass));
            Ability[] abilityArr = playersClass.GetAbilities();
            if (abilityArr != null && abilitieSubClassArr != null)
                abilityArr = abilityArr.Concat(playersClass.GetSubClass().GetAbilities()).ToArray();
            int level = CharacterData.GetLevel(playersClass) + 1;
            foreach (Ability x in abilityArr)
            {
                if (x.level == level)
                    Instantiate(form, content.transform).GetComponent<FormCreater>().CreateAbility(x);
            }
        }
    }

    public void ChosenSubClass(Dropdown value, FormCreater formCreater)
    {
        FormCreater buf = formCreater.GetComponentInChildren<Discription>().GetComponentInChildren<FormCreater>();
        if (buf != null)
            DestroyImmediate(buf.gameObject);
        if (playersClass != null)
        {
            Ability[] abilityArr = playersClass.ChooseSubClass(value.value);
            if (abilityArr != null)
                foreach (Ability x in abilityArr)
                {
                    if (x.level == CharacterData.GetLevel(playersClass) + 1)
                        Instantiate(form, formCreater.GetComponentInChildren<Discription>().transform).GetComponent<FormCreater>().CreateAbility(x);
                }
        }
    }

    public PlayersClass GetClass()
    {
        return playersClass;
    }
}
