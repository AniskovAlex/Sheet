using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
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
        /*List<Ability> listAbilities = new List<Ability>();
        string JSONAbilities = File.ReadAllText("Assets/Resources/Abilities.txt");
        /*Abilitie abil = new Abilitie();
        abil.type = 0;
        abil.discription.Add((0, "ert"));
        abil.discription.Add((1, "grt"));
        abil.discription.Add((2, "egt"));
        abil.discription.Add((3, "erg"));
        listAbilities.Add(abil);
        string wee = JsonConvert.SerializeObject(listAbilities);
        File.WriteAllText("Assets/Resources/Abilities.txt", wee);
        listAbilities = JsonConvert.DeserializeObject<List<Ability>>(JSONAbilities);
        Debug.Log(listAbilities[1].discription[1].Item2);*/
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
            Ability[] abilityArr = playersClass.GetAbilities();
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
                    if (x.level == CharacterData.GetLevel(playersClass))
                        Instantiate(form, formCreater.GetComponentInChildren<Discription>().transform).GetComponent<FormCreater>().CreateAbility(x);
                }
        }
    }

    public PlayersClass GetClass()
    {
        return playersClass;
    }
}
