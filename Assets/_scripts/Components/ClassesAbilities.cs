using System.Collections;
using System;
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
    [SerializeField] ChangeChosen changeChosen;
    public Action chosen;
    PlayersClass playersClass = null;
    private void Awake()
    {
        Debug.Log(CharacterCollection.GetName());
    }

    public void ChosenClass(Dropdown value)
    {
        if (chosen != null)
            chosen();
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
        for (int i = 0; i < opener.Length; i++)
        {
            Destroy(opener[i].gameObject);
        }
        HealthUp[] healthUps = content.GetComponentsInChildren<HealthUp>();
        for (int i = 0; i < healthUps.Length; i++)
        {
            Destroy(healthUps[i].gameObject);
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
                {
                    if ((x.type == Ability.Type.skills || x.type == Ability.Type.instruments) && x.changeRule && CharacterData.GetLevel() + 1 == 1) continue;
                    if ((x.type == Ability.Type.skills || x.type == Ability.Type.instruments) && !x.changeRule && CharacterData.GetLevel() + 1 > 1) continue;
                    Instantiate(form, content.transform).GetComponent<FormCreater>().CreateAbility(x);
                }
                if (x.type == Ability.Type.withChoose && !x.hide && x.change && x.level != level)
                {
                    Instantiate(changeChosen, content.transform).SetList(x);
                }
            }
            if (level != 1 && playersClass.magicChange > 0)
            {
                List<Spell> listSpells = new List<Spell>();
                List<Spell> listSpellsAbj = new List<Spell>();
                List<Spell> listSpellsEvo = new List<Spell>();
                List<(int, HashSet<int>)> list = DataSaverAndLoader.LoadSpellKnew();
                FormCreater formForSpells = Instantiate(form, content.transform).GetComponent<FormCreater>();
                GameObject discription = formForSpells.GetComponentInChildren<Discription>().gameObject;
                formForSpells.SetHead("Заклинания");
                bool flag = false;
                foreach ((int, HashSet<int>) x in list)
                {
                    if (x.Item1 == playersClass.id)
                    {
                        Spell[] spells = LoadSpellManager.GetSpells();
                        switch (x.Item1)
                        {
                            case 2:
                                if (playersClass.GetSubClass() != null && playersClass.GetSubClass().id == 2)
                                {

                                    for (int i = 0; i < spells.Length; i++)
                                    {
                                        if (x.Item2.Contains(spells[i].id) && spells[i].level > 0)
                                        {
                                            switch (spells[i].spellType)
                                            {
                                                default:
                                                    listSpells.Add(spells[i]);
                                                    break;
                                                case Spell.Type.Abjuration:
                                                    listSpellsAbj.Add(spells[i]);
                                                    break;
                                                case Spell.Type.Evocation:
                                                    listSpellsEvo.Add(spells[i]);
                                                    break;
                                            }
                                        }

                                    }
                                    flag = true;
                                }
                                break;
                            case 10:
                                if (playersClass.GetSubClass() != null && playersClass.GetSubClass().id == 3)
                                {

                                    for (int i = 0; i < spells.Length; i++)
                                    {
                                        if (x.Item2.Contains(spells[i].id) && spells[i].level > 0)
                                        {
                                            switch (spells[i].spellType)
                                            {
                                                default:
                                                    listSpells.Add(spells[i]);
                                                    break;
                                                case Spell.Type.Illusion:
                                                    listSpellsAbj.Add(spells[i]);
                                                    break;
                                                case Spell.Type.Enchantment:
                                                    listSpellsEvo.Add(spells[i]);
                                                    break;
                                            }
                                        }

                                    }
                                    flag = true;
                                }
                                break;
                            default:
                                for (int i = 0; i < spells.Length; i++)
                                    if (x.Item2.Contains(spells[i].id) && spells[i].level > 0)
                                        listSpells.Add(spells[i]);
                                break;
                        }
                        break;
                    }
                }
                SpellChoose[] buf = GetComponentsInChildren<SpellChoose>();
                bool flag2 = false;
                if (!flag)
                    foreach (SpellChoose x in buf)
                    {
                        if (x.blocked) continue;
                        if (x.GetLevel() != 0)
                        {
                            if (x.isSetted())
                            {
                                x.SetSpells(playersClass.id, listSpells, playersClass.magicChange);
                                Destroy(formForSpells.gameObject);
                            }
                            else
                            {
                                SpellChoose newSpellChoose = Instantiate(spellChoose, discription.transform);
                                newSpellChoose.SetSpells(playersClass.id, 0, -1, playersClass.id);
                                newSpellChoose.SetSpells(playersClass.id, listSpells, playersClass.magicChange);
                            }
                            flag2 = true;
                        }
                    }
                else
                {
                    List<int> left = null;
                    if(playersClass.id == 2)
                        left = new List<int> { -1, -2, -3 };
                    if (playersClass.id == 10)
                        left = new List<int> { -1, -6, -7 };
                    foreach (SpellChoose x in buf)
                    {
                        if (x.blocked) continue;
                        if (x.GetLevel() != 0)
                        {
                            x.mult = true;
                            if (x.isSetted())
                                switch (x.GetLevel())
                                {
                                    default:
                                    case -1:
                                        x.SetSpells(playersClass.id, listSpells, playersClass.magicChange);
                                        left.Remove(-1);
                                        break;
                                    case -2:
                                    case -6:
                                        x.SetSpells(playersClass.id, listSpellsAbj, playersClass.magicChange);
                                        left.Remove(-2);
                                        left.Remove(-6);
                                        break;
                                    case -3:
                                    case -7:
                                        left.Remove(-3);
                                        left.Remove(-7);
                                        x.SetSpells(playersClass.id, listSpellsEvo, playersClass.magicChange);
                                        break;
                                }
                            else
                                Instantiate(spellChoose, discription.transform).SetSpells(playersClass.id, listSpells, playersClass.magicChange);

                        }

                    }
                    foreach (int y in left)
                    {
                        SpellChoose buf1 = Instantiate(spellChoose, discription.transform);
                        switch (y)
                        {
                            default:
                            case -1:
                                buf1.SetSpells(playersClass.id, listSpells, playersClass.magicChange);
                                break;
                            case -2:
                            case -6:
                                buf1.SetSpells(playersClass.id, listSpellsAbj, playersClass.magicChange);
                                break;
                            case -3:
                            case -7:
                                buf1.SetSpells(playersClass.id, listSpellsEvo, playersClass.magicChange);
                                break;
                        }
                        buf1.SetSpells(3, 0, y, playersClass.id);
                        buf1.mult = true;
                        flag2 = true;
                    }
                }
                if (!flag2)
                {
                    SpellChoose newSpellChoose = Instantiate(spellChoose, discription.transform);
                    newSpellChoose.SetSpells(playersClass.id, 0, -1, playersClass.id);
                    newSpellChoose.SetSpells(playersClass.id, listSpells, playersClass.magicChange);
                }
            }
        }
    }

    public void ChosenSubClass(Dropdown value, FormCreater formCreater)
    {
        FormCreater[] buf = formCreater.GetComponentInChildren<Discription>().GetComponentsInChildren<FormCreater>();
        for (int i = buf.Length - 1; i >= 0; i--)
            if (buf[i] != null)
                DestroyImmediate(buf[i].gameObject);
        if (playersClass != null)
        {
            Ability[] abilityArr = playersClass.ChooseSubClass(value.value);
            if (abilityArr != null)
            {
                foreach (Ability x in abilityArr)
                {
                    if (x.level == CharacterData.GetLevel(playersClass) + 1)
                        Instantiate(form, formCreater.GetComponentInChildren<Discription>().transform).GetComponent<FormCreater>().CreateAbility(x);
                }
            }
        }
    }

    public PlayersClass GetClass()
    {
        return playersClass;
    }
}
