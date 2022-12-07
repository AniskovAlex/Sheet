using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormCreater : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] GameObject charUp;
    [SerializeField] GameObject dropdown;
    [SerializeField] SpellChoose spellChoose;
    public GameObject consumable;
    public GameObject basicText;
    GameObject discription;
    List<(int, string, string)> list;
    List<(int, string, string)> constList;
    Ability ability = null;

    private void Awake()
    {
        discription = GetComponentInChildren<Discription>().gameObject;
    }

    public void SetHead(string name)
    {
        head.text = name;
    }

    public void CreateAbility(Ability ability)
    {
        this.ability = ability;
        head.text = ability.head;
        switch (ability.type)
        {
            case Ability.Type.consumable:
            case Ability.Type.abilitie:
                foreach ((int, string) x in ability.discription)
                    SetText(x);
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
                    constList = new List<(int, string, string)>(ability.list);
                list = new List<(int, string, string)>(constList);
                bool found = false;
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
                if (!found)
                    PresavedLists.preLists.Add((ability.listName, new List<int>()));
                PresavedLists.ChangePing += UpdateOptions;
                for (int i = 0; i < ability.chooseCount; i++)
                {
                    Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                    Text listDis = Instantiate(basicText, discription.transform).GetComponent<Text>();
                    listDis.text = "";
                    chooseDrop.GetComponent<DropdownExtend>().text = listDis;
                    chooseDrop.ClearOptions();
                    foreach ((int, string, string) x in list)
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
                PresavedLists.ChangeCompetencePing += UpdateCompetenceOptions;
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
                SpellChoose buf = GetComponentInParent<ClassesAbilities>().GetComponentInChildren<SpellChoose>();
                if (ability.chooseCount == 0)
                {
                    if (ability.common != null)
                    {
                        bool flag = false;
                        foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                            if (x.Item1 == ability.buf2Int)
                            {
                                foreach ((int, int) y in ability.consum)
                                    x.Item2.Add(y.Item1);
                                flag = true;
                            }
                        if (!flag)
                        {
                            (int, HashSet<int>) add = (ability.buf2Int, new HashSet<int>());
                            foreach ((int, int) y in ability.consum)
                                add.Item2.Add(y.Item1);
                            PresavedLists.spellKnew.Add(add);
                        }
                    }
                    break;
                }
                if (buf != null && buf.changeable && ability.bufInt != 0)
                {
                    buf.SetSpells(ability.buf2Int, ability.chooseCount, ability.bufInt);
                    Destroy(gameObject);
                }
                else
                    Instantiate(spellChoose, discription.transform).SetSpells(ability.buf2Int, ability.chooseCount, ability.bufInt);
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
            case 0:
                textSize = 40;
                fontStyle = FontStyle.Normal;
                break;
            case 1:
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
            list = new List<(int, string, string)>(constList);
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
            foreach (Dropdown x in dropdowns)
            {
                x.options.Clear();
                foreach ((int, string, string) y in list)
                    x.options.Add(new Dropdown.OptionData(y.Item2));
                x.options.Add(new Dropdown.OptionData(x.captionText.text));
                x.value = x.options.Count - 1;
            }
        }
    }

    void UpdateSkillOptions(string remove)
    {
        HashSet<string> skillList = new HashSet<string>(ability.common);
        skillList.ExceptWith(PresavedLists.skills);
        ChangeDropdownOptions(skillList, remove);
    }

    void UpdateCompetenceOptions(string remove)
    {
        HashSet<string> skillList = PresavedLists.skills;
        skillList.ExceptWith(PresavedLists.competence);
        ChangeDropdownOptions(skillList, remove);

    }
    void UpdateInstrumentsOptions(string remove)
    {
        HashSet<string> instrumentsList = new HashSet<string>(ability.common);
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
            x.options.Add(new Dropdown.OptionData(currentValue));
            x.value = x.options.Count - 1;
            if (x.GetComponent<DropdownExtend>().currentValueText == remove)
                x.value = x.options.Count - 1;
        }
    }

    void ChangeSkillSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (oldValue == dropdown.captionText.text) return;
        PresavedLists.UpdateSkills(oldValue, dropdown.captionText.text);
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

    private void OnDestroy()
    {
        if (ability == null) return;
        if (ability.type == Ability.Type.withChoose)
        {
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

            PresavedLists.ChangeCompetencePing -= UpdateCompetenceOptions;
            foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                PresavedLists.RemoveFromCompetence(x.GetComponent<DropdownExtend>().currentValueText);

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
            if (ability.common != null)
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

    }
}
