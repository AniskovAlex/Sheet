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
    [SerializeField] SpellChoose spellChoose;
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
                playersClass = new Bard();
                break;
            case 2:
                playersClass = new Barbarian();
                break;
            case 3:
                playersClass = new Fighter();
                break;
            case 4:
                playersClass = new Wizard();
                break;
            case 5:
                playersClass = new Druid();
                break;
            case 6:
                playersClass = new Cleric();
                break;
            case 7:
                playersClass = new Warlock();
                break;
            case 8:
                playersClass = new Monk();
                break;
            case 9:
                playersClass = new Paladin();
                break;
            case 10:
                playersClass = new Rogue();
                break;
            case 11:
                playersClass = new Ranger();
                break;
            case 12:
                playersClass = new Sorcerer();
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
            if (level != 1 && playersClass.magicChange > 0)
            {
                List<Spell> listSpells = new List<Spell>();
                List<(int, HashSet<int>)> list = DataSaverAndLoader.LoadSpellKnew();
                foreach ((int, HashSet<int>) x in list)
                    if (x.Item1 == playersClass.id)
                    {
                        Spell[] spells = LoadSpellManager.GetSpells();
                        for (int i = 0; i < spells.Length; i++)
                            if (x.Item2.Contains(spells[i].id) && spells[i].level > 0)
                                listSpells.Add(spells[i]);
                        break;
                    }
                FormCreater formForSpells = Instantiate(form, content.transform).GetComponent<FormCreater>();
                GameObject discription = formForSpells.GetComponentInChildren<Discription>().gameObject;
                formForSpells.SetHead("Заклинания");
                Instantiate(spellChoose, discription.transform).SetSpells(playersClass.id, listSpells, playersClass.magicChange);
            }
            foreach (Ability x in abilityArr)
            {
                if (x.level == level)
                {
                    if ((x.type == Ability.Type.skills || x.type == Ability.Type.instruments) && x.changeRule && CharacterData.GetLevel() + 1 == 1) continue;
                    if ((x.type == Ability.Type.skills || x.type == Ability.Type.instruments) && !x.changeRule && CharacterData.GetLevel() + 1 > 1) continue;
                    Instantiate(form, content.transform).GetComponent<FormCreater>().CreateAbility(x);
                }
            }
        }
    }

    public void ChosenSubClass(Dropdown value, FormCreater formCreater)
    {
        FormCreater[] buf = formCreater.GetComponentInChildren<Discription>().GetComponentsInChildren<FormCreater>();
        for (int i = buf.Length-1; i >= 0; i--)
            if (buf[i] != null)
                DestroyImmediate(buf[i].gameObject);
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
