using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayersClass
{
    const string armorProfSaveName = "armorProf_";
    const string weaponProfSaveName = "weaponProf_";
    const string saveThrowProfSaveName = "save_";
    const string classSaveName = "class_";

    int healthDice;
    List<Armor.Type> armorProfs;
    List<Weapon.Type> weaponProfs;
    int instrumentsAmount;
    List<string> instrumentProfs;
    int skillsAmount;
    List<int> skillProfs;
    List<int> savethrowProfs;

    protected int level = 0;
    protected int mainState;
    protected GameObject panel;
    protected GameObject basicForm;
    protected GameObject dropdownForm;
    protected int PB;
    protected bool redact = false;

    protected PlayersClass(int healthDice, List<Armor.Type> armorProfs, List<Weapon.Type> weaponProfs, int instrumentsAmount, List<string> instrumentProfs, int skillsAmount, List<int> skillProfs, List<int> savethrowProfs,
        int level, int mainState, GameObject panel, GameObject basicForm, GameObject dropdownForm, int PB, bool redact) : base()
    {
        this.healthDice = healthDice;
        this.armorProfs = armorProfs;
        this.weaponProfs = weaponProfs;
        this.instrumentsAmount = instrumentsAmount;
        this.instrumentProfs = instrumentProfs;
        this.skillsAmount = skillsAmount;
        this.skillProfs = skillProfs;
        this.savethrowProfs = savethrowProfs;
        this.level = level;
        this.mainState = mainState;
        this.panel = panel;
        this.basicForm = basicForm;
        this.dropdownForm = dropdownForm;
        this.PB = PB;
        this.redact = redact;

        if (redact)
        {
            ShowAbilities(level);
        }
        else
        {
            for (int i = 1; i <= level; i++)
            {
                ShowAbilities(i);
            }
        }
    }

    public virtual void ShowAbilities(int level)
    {

    }

    public List<Armor.Type> GetArmorProfs()
    {
        return armorProfs;
    }

    public List<Weapon.Type> GetWeaponProfs()
    {
        return weaponProfs;
    }
    public List<string> GetInstrumentProfs()
    {
        return instrumentProfs;
    }
    public List<int> GetSkillProfs()
    {
        return skillProfs;
    }

    public List<int> GetSavethrowProfs()
    {
        return savethrowProfs;
    }

    public int GetInstrumentsAmount()
    {
        return instrumentsAmount;
    }

    public int GetSkillsAmount()
    {
        return skillsAmount;
    }

    public void CreatAbility(string caption, string level, string discription)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        form.AddText(level, FontStyle.Italic);
        form.AddText(discription);
    }

    public void CreatAbility(string caption, string level, string discription, int i)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        form.AddText(level, FontStyle.Italic);
        form.AddText(discription);
        if (!redact)
            form.AddConsumables(i);
    }

    public void CreatAbility(string caption, string level, List<(string, string)> includedList, List<string> excludedList)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            GameObject newBattleStyle = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
            Dropdown buf = newBattleStyle.GetComponent<Dropdown>();
            Text styleDiscriptionText = form.AddText("");
            buf.onValueChanged.AddListener(delegate { Discription(buf, styleDiscriptionText, includedList); });

            List<string> captionList = new List<string>();
            foreach ((string, string) x in includedList)
                captionList.Add(x.Item1);
            newBattleStyle.GetComponent<SkillsDropdown>().list = captionList;
            newBattleStyle.GetComponent<SkillsDropdown>().excludedList = excludedList;
            List<string> buf1 = new List<string>();
            foreach ((string,string) x in includedList)
            {
                if (!excludedList.Contains(x.Item1))
                    buf1.Add(x.Item1);
            }
            buf.options.Add(new Dropdown.OptionData("Пусто"));
            for (int j = 0; j < buf1.Count; j++)
            {
                buf.options.Add(new Dropdown.OptionData(buf1[j].ToString()));
            }
        }
        else
        {
            foreach (string x in excludedList)
            {
                foreach((string,string) y in includedList)
                {
                    if (x == y.Item1)
                    {
                        form.AddText(x, 50, FontStyle.Bold);
                        form.AddText(y.Item2);
                        break;
                    }
                }
            }
        }
    }

    public void CreatAbility(string caption, string level, List<(string,string)> includedList)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            GameObject newDropDownChoose = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
            Dropdown buf = newDropDownChoose.GetComponent<Dropdown>();
            Text styleDiscriptionText = form.AddText("");
            buf.onValueChanged.AddListener(delegate { Discription(buf, styleDiscriptionText, includedList); });
            buf.onValueChanged.AddListener(delegate { ChooseSubClass(buf); });
            List<string> captionList = new List<string>();
            foreach ((string, string) x in includedList)
                captionList.Add(x.Item1);
            newDropDownChoose.GetComponent<SkillsDropdown>().list = captionList;
            newDropDownChoose.GetComponent<SkillsDropdown>().excludedList = new List<string>();
            List<string> buf1 = new List<string>();
            foreach ((string, string) x in includedList)
            {
                buf1.Add(x.Item1);
            }
            buf.options.Add(new Dropdown.OptionData("Пусто"));
            for (int j = 0; j < buf1.Count; j++)
            {
                buf.options.Add(new Dropdown.OptionData(buf1[j].ToString()));
            }
        }
    }

    void Discription(Dropdown style, Text textField, List<(string, string)> inclcudedList)
    {
        foreach((string,string) x in inclcudedList)
        {
            if(x.Item1 == style.captionText.text)
            {
                textField.text = x.Item2;
                return;
            }
        }
        textField.text = " ";
    }

    public virtual void Save()
    {
        foreach (Weapon.Type x in weaponProfs)
        {
            PlayerPrefs.SetInt(weaponProfSaveName + x, 1);
        }
        foreach (Armor.Type x in armorProfs)
        {
            PlayerPrefs.SetInt(armorProfSaveName + x, 1);
        }
        foreach (int x in savethrowProfs)
        {
            PlayerPrefs.SetInt(saveThrowProfSaveName + x, 1);
        }
    }

    public virtual void ChooseSubClass(Dropdown mySelf)
    {
        FormCreater[] abilitieForms = panel.GetComponentInChildren<FormCreater>().GetComponentInChildren<Discription>().gameObject.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in abilitieForms)
        {
            MonoBehaviour.Destroy(x.gameObject);
        }
    }

}
