using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormCreater : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] GameObject charUp;
    [SerializeField] GameObject dropdown;
    public GameObject consumable;
    public GameObject basicText;
    GameObject discription;
    List<(string, string)> list;
    Ability ability = null;

    private void Awake()
    {
        discription = GetComponentInChildren<Discription>().gameObject;
    }

    public void CreateAbility(Ability ability)
    {
        this.ability = ability;
        head.text = ability.head;
        switch (ability.type)
        {
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
                foreach (string x in ability.common)
                    subClass.options.Add(new Dropdown.OptionData(x));
                subClass.options.Add(new Dropdown.OptionData("Пусто"));
                subClass.onValueChanged.AddListener(delegate { GetComponentInParent<ClassesAbilities>().ChosenSubClass(subClass, this); });
                subClass.value = ability.discription.Count;
                break;
            case Ability.Type.subRace:
                Dropdown subRace = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                subRace.ClearOptions();
                foreach (string x in ability.common)
                    subRace.options.Add(new Dropdown.OptionData(x));
                subRace.options.Add(new Dropdown.OptionData("Пусто"));
                subRace.onValueChanged.AddListener(delegate { GetComponentInParent<RaceAbilities>().ChosenSubRace(subRace, this); });
                subRace.value = ability.common.Count;
                break;
            case Ability.Type.withChoose:
                if (ability.isUniq)
                    list = FileSaverAndLoader.LoadList(ability.pathToList);
                else
                    list = new List<(string, string)>(ability.list);
                bool found = false;
                foreach ((string, List<string>) x in PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName))
                {
                    found = true;
                    list.RemoveAll(g => x.Item2.Contains(g.Item1));
                }
                if (!found)
                    PresavedLists.preLists.Add((ability.listName, new List<string>()));
                PresavedLists.ChangePing += UpdateOptions;
                for (int i = 0; i < ability.chooseCount; i++)
                {
                    Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                    Text listDis = Instantiate(basicText, discription.transform).GetComponent<Text>();
                    listDis.text = "";
                    chooseDrop.GetComponent<DropdownExtend>().text = listDis;
                    chooseDrop.ClearOptions();
                    foreach ((string, string) x in list)
                        chooseDrop.options.Add(new Dropdown.OptionData(x.Item1));
                    chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                    chooseDrop.onValueChanged.AddListener(delegate
                    {
                        ChangeSelected(chooseDrop);
                    });
                    chooseDrop.value = ability.list.Length;
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
            if (ability.isUniq)
                list = FileSaverAndLoader.LoadList(ability.pathToList);
            else
                list = new List<(string, string)>(ability.list);
            foreach ((string, List<string>) x in PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName))
                list.RemoveAll(g => x.Item2.Contains(g.Item1));
            foreach (Dropdown x in dropdowns)
            {
                x.options.Clear();
                foreach ((string, string) y in list)
                    x.options.Add(new Dropdown.OptionData(y.Item1));
                x.options.Add(new Dropdown.OptionData("Пусто"));
            }
        }
    }

    void UpdateSkillOptions(string remove)
    {
        Dropdown[] dropdowns = GetComponentsInChildren<Dropdown>();
        HashSet<string> skillList = new HashSet<string>(ability.common);
        skillList.ExceptWith(PresavedLists.skills);
        foreach (Dropdown x in dropdowns)
        {
            x.options.Clear();
            foreach (string y in skillList)
                x.options.Add(new Dropdown.OptionData(y));
            x.options.Add(new Dropdown.OptionData("Пусто"));
            if (x.GetComponent<DropdownExtend>().currentValueText == remove)
                x.value = x.options.Count - 1;
        }

    }
    void UpdateInstrumentsOptions(string remove)
    {
        Dropdown[] dropdowns = GetComponentsInChildren<Dropdown>();
        HashSet<string> instrumentsList = new HashSet<string>(ability.common);
        instrumentsList.ExceptWith(PresavedLists.instruments);
        foreach (Dropdown x in dropdowns)
        {
            x.options.Clear();
            foreach (string y in instrumentsList)
                x.options.Add(new Dropdown.OptionData(y));
            x.options.Add(new Dropdown.OptionData("Пусто"));
            if (x.GetComponent<DropdownExtend>().currentValueText == remove)
                x.value = x.options.Count - 1;
        }

    }
    void UpdateLanguageOptions(string remove)
    {
        Dropdown[] dropdowns = GetComponentsInChildren<Dropdown>();
        HashSet<string> languageList = new HashSet<string>(ability.common);
        languageList.ExceptWith(PresavedLists.languages);
        foreach (Dropdown x in dropdowns)
        {
            x.options.Clear();
            foreach (string y in languageList)
                x.options.Add(new Dropdown.OptionData(y));
            x.options.Add(new Dropdown.OptionData("Пусто"));
            if (x.GetComponent<DropdownExtend>().currentValueText == remove)
                x.value = x.options.Count - 1;
        }

    }
    void ChangeSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        foreach ((string, string) x in list)
            if (x.Item1 == dropdown.captionText.text)
            {
                dropdown.GetComponent<DropdownExtend>().text.text = x.Item2;
                break;
            }
        PresavedLists.UpdatePrelist(ability.listName, oldValue, dropdown.captionText.text);
    }

    void ChangeSkillSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        PresavedLists.UpdateSkills(oldValue, dropdown.captionText.text);
    }

    void ChangeInstrumentsSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        PresavedLists.UpdateInstruments(oldValue, dropdown.captionText.text);
    }

    void ChangeLanguageSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        PresavedLists.UpdateLanguage(oldValue, dropdown.captionText.text);
    }

    private void OnDestroy()
    {
        if (ability.type == Ability.Type.withChoose)
        {
            PresavedLists.ChangePing -= UpdateOptions;
            foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                PresavedLists.RemoveFromPrelist(ability.listName, x.GetComponent<DropdownExtend>().currentValueText);
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

    }
}
