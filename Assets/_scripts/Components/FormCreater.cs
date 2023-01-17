using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormCreater : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] GameObject charUp;
    [SerializeField] ChooseAttr attrUp;
    [SerializeField] GameObject dropdown;
    [SerializeField] SpellChoose spellChoose;
    [SerializeField] ChooseFeat featChoose;
    public GameObject consumable;
    public GameObject basicText;
    GameObject discription;
    List<(int, string, string, int)> list;
    List<(int, string, string, int)> constList;
    Ability ability = null;
    bool bufFlag = false;
    bool bufFlag1 = false;

    private void Awake()
    {
        discription = GetComponentInChildren<Discription>().gameObject;
    }

    public void SetHead(string name)
    {
        head.text = name;
    }

    public string GetHead()
    {
        return head.text;
    }

    public void RemoveHead()
    {
        head.transform.parent.gameObject.SetActive(false);
    }

    public void CreateAbility(Ability ability)
    {
        SpellChoose newSpellChoose = null;
        this.ability = ability;
        head.text = ability.head;
        foreach ((int, string) x in ability.discription)
            SetText(x);
        switch (ability.type)
        {
            case Ability.Type.consumable:
                switch (ability.listName)
                {
                    case "SpecialSpells":
                        Instantiate(spellChoose, discription.transform).SetSpells(3, 2, -5, 3);
                        break;
                    case "shifter":
                        bool flag = false;
                        foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                            if (x.Item1 == 3)
                            {
                                x.Item2.Add(98);
                                flag = true;
                            }
                        if (!flag)
                            PresavedLists.spellKnew.Add((3, new HashSet<int> { 98 }));
                        break;
                    case "Arcanum6":
                        newSpellChoose = Instantiate(spellChoose, discription.transform);
                        newSpellChoose.SetSpells(7, 1, 6, 7);
                        newSpellChoose.blocked = true;
                        break;
                    case "Arcanum7":
                        newSpellChoose = Instantiate(spellChoose, discription.transform);
                        newSpellChoose.SetSpells(7, 1, 7, 7);
                        newSpellChoose.blocked = true;
                        break;
                    case "Arcanum8":
                        newSpellChoose = Instantiate(spellChoose, discription.transform);
                        newSpellChoose.SetSpells(7, 1, 8, 7);
                        newSpellChoose.blocked = true;
                        break;
                    case "Arcanum9":
                        newSpellChoose = Instantiate(spellChoose, discription.transform);
                        newSpellChoose.SetSpells(7, 1, 9, 7);
                        newSpellChoose.blocked = true;
                        break;
                    case "DedicatedToMagic":
                        Dropdown DedicatedClass = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                        DedicatedClass.ClearOptions();
                        DedicatedClass.options.Add(new Dropdown.OptionData("Пусто"));
                        foreach ((int, string, string, int) x in ability.list)
                            DedicatedClass.options.Add(new Dropdown.OptionData(x.Item2));
                        DedicatedClass.options.Add(new Dropdown.OptionData("Пусто"));
                        DedicatedClass.onValueChanged.AddListener(delegate { DedicatedMagic(DedicatedClass); });
                        DedicatedClass.value = 0;
                        break;
                }
                break;
            case Ability.Type.abilitie:
                if (ability.changeRule)
                    switch (ability.listName)
                    {
                        case "DwarfHold":
                            PresavedLists.addHealth += 1;
                            break;
                        case "LightlyArmored":
                            if (PresavedLists.armorTypes.Contains(Armor.ArmorType.Light))
                                bufFlag = true;
                            PresavedLists.armorTypes.Add(Armor.ArmorType.Light);
                            break;
                        case "ModeratelyArmored":
                            if (PresavedLists.armorTypes.Contains(Armor.ArmorType.Medium))
                                bufFlag = true;
                            if (PresavedLists.armorTypes.Contains(Armor.ArmorType.Shield))
                                bufFlag1 = true;
                            PresavedLists.armorTypes.Add(Armor.ArmorType.Medium);
                            PresavedLists.armorTypes.Add(Armor.ArmorType.Shield);
                            break;
                        case "HeavilyArmored":
                            if (PresavedLists.armorTypes.Contains(Armor.ArmorType.Heavy))
                                bufFlag = true;
                            PresavedLists.armorTypes.Add(Armor.ArmorType.Heavy);
                            break;
                        case "Tough":
                            PresavedLists.addHealth += 2;
                            PresavedLists.addMaxHealth += CharacterData.GetLevel() + 1;
                            break;
                        case "RitualCaster":
                            Dropdown DedicatedClass = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                            DedicatedClass.ClearOptions();
                            DedicatedClass.options.Add(new Dropdown.OptionData("Пусто"));
                            foreach ((int, string, string, int) x in ability.list)
                                DedicatedClass.options.Add(new Dropdown.OptionData(x.Item2));
                            DedicatedClass.options.Add(new Dropdown.OptionData("Пусто"));
                            DedicatedClass.onValueChanged.AddListener(delegate { DedicatedMagic(DedicatedClass); });
                            DedicatedClass.value = 0;
                            break;
                        case "Skilled":
                            HashSet<string> skillList = new HashSet<string>(ability.common);
                            foreach ((int, string, string, int) x in ability.list)
                                skillList.Add(x.Item2);
                            skillList.ExceptWith(PresavedLists.instruments);
                            skillList.ExceptWith(PresavedLists.skills);
                            PresavedLists.ChangeSkillPing += UpdateSkillOptions;
                            PresavedLists.ChangeIntrumentsPing += UpdateInstrumentsOptions;
                            for (int i = 0; i < ability.chooseCount; i++)
                            {
                                Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                                chooseDrop.ClearOptions();
                                foreach (string x in skillList)
                                    chooseDrop.options.Add(new Dropdown.OptionData(x));
                                chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                                chooseDrop.onValueChanged.AddListener(delegate
                                {
                                    ChangeSkillSelected(chooseDrop);
                                    ChangeInstrumentsSelected(chooseDrop);
                                });
                                chooseDrop.value = skillList.Count;
                            }
                            break;
                        case "WeaponMaster":
                            List<(int, string, string, int)> weaponList = new List<(int, string, string, int)>(ability.list);
                            WeaponRemove(weaponList);
                            for (int i = 0; i < ability.chooseCount; i++)
                            {
                                Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                                chooseDrop.ClearOptions();
                                foreach ((int, string, string, int) x in weaponList)
                                    chooseDrop.options.Add(new Dropdown.OptionData(x.Item2));
                                chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                                chooseDrop.onValueChanged.AddListener(delegate
                                {
                                    ChangeWeaponBladeSelected(chooseDrop);
                                });
                                chooseDrop.value = weaponList.Count;
                            }
                            break;
                    }
                break;
            case Ability.Type.charUp:
                Instantiate(charUp, discription.transform);
                break;
            case Ability.Type.subClass:
                Dropdown subClass = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                subClass.ClearOptions();
                subClass.options.Add(new Dropdown.OptionData("Пусто"));
                foreach (string x in ability.common)
                    subClass.options.Add(new Dropdown.OptionData(x));
                subClass.options.Add(new Dropdown.OptionData("Пусто"));
                subClass.onValueChanged.AddListener(delegate { GetComponentInParent<ClassesAbilities>().ChosenSubClass(subClass, this); });
                subClass.value = 0;
                break;
            case Ability.Type.subRace:
                Dropdown subRace = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                subRace.ClearOptions();
                subRace.options.Add(new Dropdown.OptionData("Пусто"));
                foreach (string x in ability.common)
                    subRace.options.Add(new Dropdown.OptionData(x));
                subRace.options.Add(new Dropdown.OptionData("Пусто"));
                subRace.onValueChanged.AddListener(delegate { GetComponentInParent<RaceAbilities>().ChosenSubRace(subRace, this); });
                subRace.value = 0;
                break;
            case Ability.Type.withChoose:
                if (ability.isUniq)
                    constList = FileSaverAndLoader.LoadList(ability.pathToList);
                else
                    constList = new List<(int, string, string, int)>(ability.list);
                list = new List<(int, string, string, int)>(constList);
                bool found = false;
                foreach ((string, List<int>) x in PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName))
                {
                    foreach (int y in x.Item2)
                    {
                        for (int i = 0; i < list.Count; i++)
                            if (list[i].Item1 == y)
                            {
                                list.RemoveAt(i);
                                break;
                            }
                    }
                    found = true;
                }
                if (!found)
                    PresavedLists.preLists.Add((ability.listName, DataSaverAndLoader.LoadCustom(ability.listName)));
                if (ability.chooseCount == 0)
                {
                    foreach ((string, List<int>) x in PresavedLists.preLists)
                        if (x.Item1 == ability.listName)
                        {
                            foreach ((int, int) y in ability.consum)
                                x.Item2.Add(y.Item1);
                            return;

                        }
                }
                SelectFromList(list);
                PresavedLists.ChangePing += UpdateOptions;
                for (int i = 0; i < ability.chooseCount; i++)
                {
                    Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                    Text listDis = Instantiate(basicText, discription.transform).GetComponent<Text>();
                    listDis.text = "";
                    chooseDrop.GetComponent<DropdownExtend>().text = listDis;
                    chooseDrop.ClearOptions();
                    foreach ((int, string, string, int) x in list)
                        chooseDrop.options.Add(new Dropdown.OptionData(x.Item2));
                    chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                    chooseDrop.onValueChanged.AddListener(delegate
                    {
                        ChangeSelected(chooseDrop);
                    });
                    chooseDrop.value = list.Count;
                }
                break;
            case Ability.Type.skills:
                if (ability.chooseCount > 0)
                {
                    HashSet<string> skillList = new HashSet<string>(ability.common);
                    skillList.ExceptWith(PresavedLists.skills);
                    skillList.ExceptWith(PresavedLists.competence);
                    PresavedLists.ChangeSkillPing += UpdateSkillOptions;
                    for (int i = 0; i < ability.chooseCount; i++)
                    {
                        Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                        chooseDrop.ClearOptions();
                        foreach (string x in skillList)
                            chooseDrop.options.Add(new Dropdown.OptionData(x));
                        chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                        chooseDrop.onValueChanged.AddListener(delegate
                        {
                            ChangeSkillSelected(chooseDrop);
                        });
                        chooseDrop.value = skillList.Count;
                    }
                }
                else
                    foreach (string x in ability.common)
                        PresavedLists.UpdateSkills("", x);
                break;
            case Ability.Type.competence:
                HashSet<string> skillCompList = PresavedLists.skills;
                skillCompList.ExceptWith(PresavedLists.competence);
                if (ability.isUniq)
                    skillCompList.Add("Воровские инстурменты");
                skillCompList.ExceptWith(PresavedLists.compInstruments);
                PresavedLists.ChangeCompetencePing += UpdateCompetenceOptions;
                PresavedLists.ChangeSkillPing += UpdateCompetenceOptions;
                for (int i = 0; i < ability.chooseCount; i++)
                {
                    Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                    chooseDrop.ClearOptions();
                    foreach (string x in skillCompList)
                        chooseDrop.options.Add(new Dropdown.OptionData(x));
                    chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                    chooseDrop.onValueChanged.AddListener(delegate
                    {
                        ChangeCompetenceSelected(chooseDrop);
                    });
                    chooseDrop.value = skillCompList.Count;
                }
                break;
            case Ability.Type.instruments:
                if (ability.chooseCount > 0)
                {
                    HashSet<string> instrumentsList = new HashSet<string>(ability.common);
                    instrumentsList.ExceptWith(PresavedLists.instruments);
                    PresavedLists.ChangeIntrumentsPing += UpdateInstrumentsOptions;
                    for (int i = 0; i < ability.chooseCount; i++)
                    {
                        Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                        chooseDrop.ClearOptions();
                        foreach (string x in instrumentsList)
                            chooseDrop.options.Add(new Dropdown.OptionData(x));
                        chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                        chooseDrop.onValueChanged.AddListener(delegate
                        {
                            ChangeInstrumentsSelected(chooseDrop);
                        });
                        chooseDrop.value = instrumentsList.Count;
                    }
                }
                else
                    foreach (string x in ability.common)
                        PresavedLists.UpdateInstruments("", x);
                break;
            case Ability.Type.language:
                if (ability.chooseCount > 0)
                {
                    HashSet<string> languageList = new HashSet<string>(ability.common);
                    languageList.ExceptWith(PresavedLists.languages);
                    PresavedLists.ChangeLanguagePing += UpdateLanguageOptions;
                    for (int i = 0; i < ability.chooseCount; i++)
                    {
                        Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                        chooseDrop.ClearOptions();
                        foreach (string x in languageList)
                            chooseDrop.options.Add(new Dropdown.OptionData(x));
                        chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                        chooseDrop.onValueChanged.AddListener(delegate
                        {
                            ChangeLanguageSelected(chooseDrop);
                        });
                        chooseDrop.value = languageList.Count;
                    }
                }
                else
                    foreach (string x in ability.common)
                        PresavedLists.UpdateLanguage("", x);
                break;
            case Ability.Type.spellChoose:
                ClassesAbilities classesAbilities = GetComponentInParent<ClassesAbilities>();
                if (ability.chooseCount == 0)
                {
                    if (ability.isUniq)
                    {
                        bool flag = false;
                        foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                            if (ability.consum.Length > 0 && x.Item2.Contains(ability.consum[0].Item1))
                                flag = true;
                        if (flag)
                        {
                            Instantiate(spellChoose, discription.transform).SetSpells(ability.buf2Int, 1, ability.bufInt, classesAbilities.GetClass().id);
                            break;
                        }
                    }
                    if (ability.consum != null)
                    {
                        bool flag = false;
                        List<(int, HashSet<int>)> list;
                        list = PresavedLists.spellKnew;
                        foreach ((int, HashSet<int>) x in list)
                            if (classesAbilities == null && x.Item1 == -1)
                            {
                                foreach ((int, int) y in ability.consum)
                                    x.Item2.Add(y.Item1);
                                flag = true;
                            }
                            else
                            {
                                if (x.Item1 == classesAbilities.GetClass().id)
                                {
                                    foreach ((int, int) y in ability.consum)
                                        x.Item2.Add(y.Item1);
                                    flag = true;
                                }
                            }
                        if (!flag)
                        {
                            int addClassId = 0;
                            if (classesAbilities != null)
                                addClassId = classesAbilities.GetClass().id;
                            else
                                addClassId = -1;
                            (int, HashSet<int>) add = (addClassId, new HashSet<int>());
                            foreach ((int, int) y in ability.consum)
                                add.Item2.Add(y.Item1);
                            list.Add(add);
                        }
                    }
                    break;
                }
                //SpellChoose buf = classesAbilities.GetComponentInChildren<SpellChoose>();
                if (classesAbilities != null)
                {
                    if (classesAbilities.GetClass().id == 2 || classesAbilities.GetClass().id == 10)
                    {
                        switch (ability.bufInt)
                        {
                            default:
                                Instantiate(spellChoose, discription.transform).SetSpells(ability.buf2Int, ability.chooseCount, ability.bufInt, classesAbilities.GetClass().id);
                                break;
                            case -4:
                                SpellChoose bufMyf1 = Instantiate(spellChoose, discription.transform);
                                bufMyf1.SetSpells(ability.buf2Int, ability.chooseCount, -2, classesAbilities.GetClass().id);
                                bufMyf1.multAdd = true;
                                bufMyf1 = Instantiate(spellChoose, discription.transform);
                                bufMyf1.SetSpells(ability.buf2Int, ability.chooseCount, -3, classesAbilities.GetClass().id);
                                bufMyf1.multAdd = true;
                                break;
                            case -8:
                                SpellChoose bufMyf2 = Instantiate(spellChoose, discription.transform);
                                bufMyf2.SetSpells(ability.buf2Int, ability.chooseCount, -6, classesAbilities.GetClass().id);
                                bufMyf2.multAdd = true;
                                bufMyf2 = Instantiate(spellChoose, discription.transform);
                                bufMyf2.SetSpells(ability.buf2Int, ability.chooseCount, -7, classesAbilities.GetClass().id);
                                bufMyf2.multAdd = true;
                                break;
                        }
                    }
                    else
                        Instantiate(spellChoose, discription.transform).SetSpells(ability.buf2Int, ability.chooseCount, ability.bufInt, classesAbilities.GetClass().id);
                }
                else
                    Instantiate(spellChoose, discription.transform).SetSpells(ability.buf2Int, ability.chooseCount, ability.bufInt, -1);
                break;
            case Ability.Type.attr:
                if (ability.chooseCount == 0)
                    foreach (string x in ability.common)
                        PresavedLists.attrAdd.Add(x);
                else
                {
                    ChooseAttr attr = Instantiate(attrUp, discription.transform);
                    attr.maxValue = ability.bufInt;
                    attr.SetDropdowns(ability.chooseCount, ability.common);
                }
                break;
            case Ability.Type.item:
                if (ability.chooseCount > 0)
                {
                    HashSet<string> list = new HashSet<string>(ability.common);
                    for (int i = 0; i < ability.chooseCount; i++)
                    {
                        Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                        chooseDrop.ClearOptions();
                        foreach (string x in list)
                            chooseDrop.options.Add(new Dropdown.OptionData(x));
                        chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                        chooseDrop.onValueChanged.AddListener(delegate
                        {
                            ChangeItemSelected(chooseDrop);
                        });
                        chooseDrop.value = list.Count;
                    }
                }
                break;
            case Ability.Type.feat:
                Instantiate(featChoose, discription.transform);
                break;
        }

    }

    void SetText((int, string) preText)
    {
        int textSize;
        FontStyle fontStyle;
        Text newObjectText = Instantiate(basicText, discription.transform).GetComponent<Text>();
        switch (preText.Item1)
        {
            default:
            case 1:
                textSize = 40;
                fontStyle = FontStyle.Normal;
                break;
            case 3:
                textSize = 40;
                fontStyle = FontStyle.Italic;
                break;
            case 2:
                textSize = 60;
                fontStyle = FontStyle.Bold;
                break;
        }
        newObjectText.text = preText.Item2;
        newObjectText.fontSize = textSize;
        newObjectText.fontStyle = fontStyle;

    }



    void UpdateOptions(string listName)
    {
        if (listName == ability.listName)
        {
            Dropdown[] dropdowns = GetComponentsInChildren<Dropdown>();
            list = new List<(int, string, string, int)>(constList);
            foreach ((string, List<int>) x in PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName))
                foreach (int y in x.Item2)
                {
                    for (int i = 0; i < list.Count; i++)
                        if (list[i].Item1 == y)
                        {
                            list.RemoveAt(i);
                            break;
                        }
                }
            SelectFromList(list);
            foreach (Dropdown x in dropdowns)
            {
                x.options.Clear();
                foreach ((int, string, string, int) y in list)
                    x.options.Add(new Dropdown.OptionData(y.Item2));
                x.options.Add(new Dropdown.OptionData(x.captionText.text));
                x.value = x.options.Count - 1;
            }
        }
    }

    void UpdateSkillOptions(string remove)
    {
        HashSet<string> skillList = new HashSet<string>(ability.common);
        if (ability.listName == "Skilled")
        {
            foreach ((int, string, string, int) x in ability.list)
                skillList.Add(x.Item2);
            skillList.ExceptWith(PresavedLists.instruments);
        }
        skillList.ExceptWith(PresavedLists.skills);
        skillList.ExceptWith(PresavedLists.competence);
        ChangeDropdownOptions(skillList, remove);
    }

    void UpdateCompetenceOptions(string remove)
    {
        HashSet<string> skillList = PresavedLists.skills;
        if (ability.isUniq)
            skillList.Add("Воровские инстурменты");
        skillList.ExceptWith(PresavedLists.competence);
        skillList.ExceptWith(PresavedLists.compInstruments);
        ChangeDropdownOptions(skillList, remove);

    }
    void UpdateInstrumentsOptions(string remove)
    {
        HashSet<string> instrumentsList = new HashSet<string>(ability.common);
        if (ability.listName == "Skilled")
        {
            foreach ((int, string, string, int) x in ability.list)
                instrumentsList.Add(x.Item2);
            instrumentsList.ExceptWith(PresavedLists.skills);
        }
        instrumentsList.ExceptWith(PresavedLists.instruments);
        ChangeDropdownOptions(instrumentsList, remove);

    }
    void UpdateLanguageOptions(string remove)
    {
        HashSet<string> languageList = new HashSet<string>(ability.common);
        languageList.ExceptWith(PresavedLists.languages);
        ChangeDropdownOptions(languageList, remove);

    }
    void ChangeSelected(Dropdown dropdown)
    {
        if (dropdown.GetComponent<DropdownExtend>().currentValueText == dropdown.captionText.text) return;
        int oldValue = -1;
        int newValue = -1;
        for (int i = 0; i < constList.Count; i++)
        {
            if (constList[i].Item2 == dropdown.captionText.text)
            {
                dropdown.GetComponent<DropdownExtend>().text.text = constList[i].Item3;
                newValue = constList[i].Item1;
            }
            if (dropdown.GetComponent<DropdownExtend>().currentValueText == constList[i].Item2)
                oldValue = constList[i].Item1;
        }
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        PresavedLists.UpdatePrelist(ability.listName, oldValue, newValue);
    }

    void ChangeDropdownOptions(HashSet<string> list, string remove)
    {
        Dropdown[] dropdowns = GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in dropdowns)
        {
            x.ClearOptions();
            foreach (string y in list)
                x.options.Add(new Dropdown.OptionData(y));
            string currentValue = x.GetComponent<DropdownExtend>().currentValueText;
            if (x.GetComponent<DropdownExtend>().currentValueText == remove)
            {
                x.options.Add(new Dropdown.OptionData("Пусто"));
                x.value = x.options.Count - 1;
                continue;
            }
            x.options.Add(new Dropdown.OptionData(currentValue));
            x.options.Add(new Dropdown.OptionData("Пусто"));
            x.value = x.options.Count - 2;
            if (x.value == 0 && x.options.Count == 2)
                x.RefreshShownValue();
        }
    }

    void ChangeSkillSelected(Dropdown dropdown)
    {
        if (ability.listName == "Skilled")
        {
            if (!ability.common.Contains(dropdown.captionText.text)) return;
        }
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (oldValue == dropdown.captionText.text) return;
        PresavedLists.UpdateSkills(oldValue, dropdown.captionText.text);
        if (ability.isUniq && ability.listName == "KnowledgeDivine")
            PresavedLists.UpdateCompentence(oldValue, dropdown.captionText.text);
    }
    void ChangeCompetenceSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (oldValue == dropdown.captionText.text) return;
        PresavedLists.UpdateCompentence(oldValue, dropdown.captionText.text);
    }

    void ChangeInstrumentsSelected(Dropdown dropdown)
    {
        if (ability.listName == "Skilled")
        {
            bool flag = false;
            foreach ((int, string, string, int) x in ability.list)
                if (dropdown.captionText.text == x.Item2) flag = true;
            if (!flag) return;
        }
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (oldValue == dropdown.captionText.text) return;
        PresavedLists.UpdateInstruments(oldValue, dropdown.captionText.text);
    }

    void ChangeLanguageSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (oldValue == dropdown.captionText.text) return;
        PresavedLists.UpdateLanguage(oldValue, dropdown.captionText.text);
    }

    void ChangeItemSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (oldValue == dropdown.captionText.text) return;
        PresavedLists.items.Remove(oldValue);
        PresavedLists.items.Add(dropdown.captionText.text);
    }

    void ChangeWeaponBladeSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (oldValue == dropdown.captionText.text) return;
        foreach ((int, string, string, int) x in ability.list)
            if (dropdown.captionText.text == x.Item2)
            {
                PresavedLists.bladeTypes.Add(Weapon.BladeType.WarStaff + x.Item1);
                break;
            }
        List<(int, string, string, int)> weaponList = new List<(int, string, string, int)>(ability.list);
        WeaponRemove(weaponList);
        Dropdown[] dropdowns = GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in dropdowns)
        {
            x.ClearOptions();
            foreach ((int, string, string, int) y in weaponList)
                x.options.Add(new Dropdown.OptionData(y.Item2));
            string currentValue = x.GetComponent<DropdownExtend>().currentValueText;
            x.options.Add(new Dropdown.OptionData(currentValue));
            x.value = x.options.Count - 1;
            if (x.GetComponent<DropdownExtend>().currentValueText == dropdown.captionText.text)
                x.value = x.options.Count - 1;
        }

    }

    private void OnDestroy()
    {
        if (ability == null) return;
        if (ability.type == Ability.Type.withChoose)
        {
            if (ability.chooseCount == 0)
            {
                foreach ((int, int) x in ability.consum)
                    PresavedLists.RemoveFromPrelist(ability.listName, x.Item1);
                return;
            }
            PresavedLists.ChangePing -= UpdateOptions;
            foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (dropdown.GetComponent<DropdownExtend>().currentValueText == list[i].Item2)
                    {
                        PresavedLists.RemoveFromPrelist(ability.listName, list[i].Item1);
                        break;
                    }
                }
            }
            if (PresavedLists.ChangePing != null) PresavedLists.ChangePing("");
            return;
        }
        if (ability.type == Ability.Type.skills)
        {
            if (ability.chooseCount > 0)
            {
                PresavedLists.ChangeSkillPing -= UpdateSkillOptions;
                foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                    PresavedLists.RemoveFromSkills(x.GetComponent<DropdownExtend>().currentValueText);

            }
            else
                foreach (string x in ability.common)
                    PresavedLists.RemoveFromSkills(x);
            if (PresavedLists.ChangeSkillPing != null) PresavedLists.ChangeSkillPing("");
            return;
        }
        if (ability.type == Ability.Type.competence)
        {
            if (ability.chooseCount > 0)
            {
                PresavedLists.ChangeCompetencePing -= UpdateCompetenceOptions;
                foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                    PresavedLists.RemoveFromCompetence(x.GetComponent<DropdownExtend>().currentValueText);
            }
            else
                foreach (string x in ability.common)
                    PresavedLists.RemoveFromCompetence(x);

            if (PresavedLists.ChangeCompetencePing != null) PresavedLists.ChangeCompetencePing("");
            return;
        }
        if (ability.type == Ability.Type.instruments)
        {
            if (ability.chooseCount > 0)
            {
                PresavedLists.ChangeIntrumentsPing -= UpdateInstrumentsOptions;
                foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                    PresavedLists.RemoveFromInstruments(x.GetComponent<DropdownExtend>().currentValueText);
            }
            else
                foreach (string x in ability.common)
                    PresavedLists.RemoveFromInstruments(x);
            if (PresavedLists.ChangeIntrumentsPing != null) PresavedLists.ChangeIntrumentsPing("");
            return;
        }
        if (ability.type == Ability.Type.language)
        {
            if (ability.chooseCount > 0)
            {
                PresavedLists.ChangeLanguagePing -= UpdateLanguageOptions;
                foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                    PresavedLists.RemoveFromLanguage(x.GetComponent<DropdownExtend>().currentValueText);
            }
            else
                foreach (string x in ability.common)
                    PresavedLists.RemoveFromLanguage(x);
            if (PresavedLists.ChangeLanguagePing != null)
                PresavedLists.ChangeLanguagePing("");
            return;
        }
        if (ability.type == Ability.Type.spellChoose && ability.chooseCount == 0)
        {
            if (ability.consum != null)
            {
                foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                    if (x.Item1 == ability.buf2Int)
                    {
                        foreach ((int, int) y in ability.consum)
                            x.Item2.Remove(y.Item1);
                        if (x.Item2.Count <= 0)
                        {
                            PresavedLists.spellKnew.Remove(x);
                        }
                        break;
                    }
            }
            return;
        }
        if (ability.type == Ability.Type.attr)
        {
            if (ability.chooseCount == 0)
                foreach (string x in ability.common)
                    PresavedLists.attrAdd.Remove(x);
            return;
        }
        if (ability.type == Ability.Type.item)
        {
            if (ability.chooseCount == 0 && PresavedLists.items != null && ability.common != null)
                foreach (string x in ability.common)
                    PresavedLists.items.Remove(x);
            return;
        }
        if (ability.type == Ability.Type.abilitie)
        {
            if (ability.listName == "DwarfHold")
                DataSaverAndLoader.SaveAddHealth(DataSaverAndLoader.LoadAddHealth() - 1);
            if (ability.listName == "LightlyArmored")
                if (!bufFlag)
                    PresavedLists.armorTypes.Remove(Armor.ArmorType.Light);
            if (ability.listName == "ModeratelyArmored")
            {
                if (!bufFlag)
                    PresavedLists.armorTypes.Remove(Armor.ArmorType.Medium);
                if (!bufFlag1)
                    PresavedLists.armorTypes.Remove(Armor.ArmorType.Shield);

            }
            if (ability.listName == "HeavilyArmored")
            {
                if (!bufFlag)
                    PresavedLists.armorTypes.Remove(Armor.ArmorType.Heavy);
            }
            if (ability.listName == "Skilled")
            {
                PresavedLists.ChangeIntrumentsPing -= UpdateInstrumentsOptions;
                PresavedLists.ChangeSkillPing -= UpdateSkillOptions;
                if (dropdown.GetComponent<DropdownExtend>() == null) return;
                foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                {
                    for (int i = 0; i < ability.list.Length; i++)
                    {
                        if (dropdown.GetComponent<DropdownExtend>().currentValueText == ability.list[i].Item2)
                        {
                            PresavedLists.instruments.Remove(ability.list[i].Item2);
                            break;
                        }
                    }
                    if (ability.common.Contains(dropdown.GetComponent<DropdownExtend>().currentValueText))
                    {
                        PresavedLists.skills.Remove(dropdown.GetComponent<DropdownExtend>().currentValueText);
                        break;
                    }
                }
                if (PresavedLists.ChangeSkillPing != null) PresavedLists.ChangeSkillPing("");
                if (PresavedLists.ChangeIntrumentsPing != null) PresavedLists.ChangeIntrumentsPing("");
                return;
            }
            if (ability.listName == "WeaponMaster")
            {
                foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                    foreach ((int, string, string, int) y in ability.list)
                    {
                        if (y.Item2 == x.GetComponent<DropdownExtend>().currentValueText)
                        {
                            PresavedLists.bladeTypes.Remove(Weapon.BladeType.WarStaff + y.Item1);
                            break;
                        }
                    }
                return;
            }
            if (ability.listName == "Tough")
            {
                PresavedLists.addHealth -= 2;
                PresavedLists.addMaxHealth -= CharacterData.GetLevel() + 1;
                return;
            }
        }
    }

    void SelectFromList(List<(int, string, string, int)> list)
    {
        ClassesAbilities classesAbilities = GetComponentInParent<ClassesAbilities>();
        PlayersClass playersClass = null;
        int level = -1;
        if (classesAbilities != null)
            playersClass = classesAbilities.GetClass();
        if (playersClass != null)
            foreach ((int, PlayersClass) x in CharacterData.GetClasses())
                if (x.Item2.id == playersClass.id)
                    level = x.Item1 + 1;
        switch (ability.listName)
        {
            case "Appeals":
                bool flag = false;
                int item = -1;
                foreach ((string, List<int>) x in PresavedLists.preLists)
                    if (x.Item1 == "ItemOfContract")
                    {
                        if (x.Item2.Count > 0)
                        {
                            flag = true;
                            item = x.Item2[0];
                        }
                    }
                for (int i = 0; i < list.Count; i++)
                    switch (list[i].Item4)
                    {
                        default:
                            if (level < 0) break;
                            if (level < list[i].Item4)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -1:
                            if (!flag || item != 0)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -2:
                            if (!flag || item != 1)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -3:
                            if (!flag || item != 2)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -4:
                            bool flag1 = false;
                            foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                                if (x.Item2.Contains(139))
                                    flag1 = true;
                            foreach ((int, HashSet<int>) x in DataSaverAndLoader.LoadSpellKnew())
                                if (x.Item2.Contains(139))
                                    flag1 = true;
                            if (!flag1)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -5:
                            if (!flag || !(item == 1 && level >= 5))
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -6:
                            if (!flag || !(item == 1 && level >= 12))
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -7:
                            if (!flag || !(item == 2 && level >= 15))
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                    }
                break;
            case "ElementalPractice":
                for (int i = 0; i < list.Count; i++)
                {
                    if (level < 0) break;
                    if (level < list[i].Item4)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
                break;
            case "BattleStyles":
                if (ability.consum == null || ability.consum.Length <= 0) break;
                for (int i = 0; i < list.Count; i++)
                {
                    bool flagList = true; ;
                    foreach ((int, int) x in ability.consum)
                    {
                        if (x.Item1 == list[i].Item1)
                        {
                            flagList = false;
                            break;
                        }
                    }
                    if (flagList)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
                break;
        }
    }

    void DedicatedMagic(Dropdown dropdown)
    {
        SpellChoose[] spells = discription.GetComponentsInChildren<SpellChoose>();
        bool flag = false;
        if (ability.listName == "RitualCaster")
            flag = true;
        for (int i = 0; i < spells.Length; i++)
            Destroy(spells[i].gameObject);
        foreach ((int, string, string, int) x in ability.list)
            if (x.Item2 == dropdown.captionText.text)
            {
                if (!flag)
                    Instantiate(spellChoose, discription.transform).SetSpells(x.Item1, 2, 1, -1);
                else
                    Instantiate(spellChoose, discription.transform).SetSpells(x.Item1, 2, -9, -1);
                break;
            }
    }

    void WeaponRemove(List<(int, string, string, int)> weaponList)
    {
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (PresavedLists.weaponTypes.Contains(Weapon.WeaponType.CommonMelee) && weaponList[i].Item1 <= 9)
            {
                weaponList.RemoveAt(i);
                i--;
                continue;
            }
            if (PresavedLists.weaponTypes.Contains(Weapon.WeaponType.CommonDist) && weaponList[i].Item1 >= 10 && weaponList[i].Item1 <= 13)
            {
                weaponList.RemoveAt(i);
                i--;
                continue;
            }
            if (PresavedLists.weaponTypes.Contains(Weapon.WeaponType.WarMelee) && weaponList[i].Item1 >= 14 && weaponList[i].Item1 <= 31)
            {
                weaponList.RemoveAt(i);
                i--;
                continue;
            }
            if (PresavedLists.weaponTypes.Contains(Weapon.WeaponType.WarDist) && weaponList[i].Item1 >= 32)
            {
                weaponList.RemoveAt(i);
                i--;
                continue;
            }
            if (PresavedLists.bladeTypes.Contains(Weapon.BladeType.WarStaff + weaponList[i].Item1))
            {
                weaponList.RemoveAt(i);
                i--;
                continue;
            }
        }
    }
}
