using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerSubClass
{

    protected int level = 0;
    protected int mainState;
    protected GameObject panel;
    protected GameObject basicForm;
    protected GameObject dropdownForm;
    protected int PB;
    protected bool redact = false;

    protected PlayerSubClass(int level, int mainState, GameObject panel, GameObject basicForm, GameObject dropdownForm, int PB, bool redact) : base()
    {
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

    public void CreatAbility(string caption, string level, List<string> includedList, List<string> excludedList, List<string> discriptionList, int count)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject newBattleStyle = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
                Dropdown buf = newBattleStyle.GetComponent<Dropdown>();
                Text styleDiscriptionText = form.AddText("");
                buf.onValueChanged.AddListener(delegate { Discription(buf, styleDiscriptionText, includedList, discriptionList); });

                newBattleStyle.GetComponent<SkillsDropdown>().list = includedList;
                newBattleStyle.GetComponent<SkillsDropdown>().excludedList = excludedList;
                List<string> buf1 = new List<string>();
                foreach (string x in includedList)
                {
                    if (!excludedList.Contains(x))
                        buf1.Add(x);
                }
                buf.options.Add(new Dropdown.OptionData("Пусто"));
                for (int j = 0; j < buf1.Count; j++)
                {
                    buf.options.Add(new Dropdown.OptionData(buf1[j].ToString()));
                }
            }
        }
        else
        {
            foreach (string x in excludedList)
            {
                form.AddText(x, 50, FontStyle.Bold);
                form.AddText(discriptionList[includedList.IndexOf(x)]);
            }
        }
    }

    public void CreatAbility(string caption, string level, List<string> includedList, List<string> excludedList, List<string> discriptionList)
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
            buf.onValueChanged.AddListener(delegate { Discription(buf, styleDiscriptionText, includedList, discriptionList); });

            newBattleStyle.GetComponent<SkillsDropdown>().list = includedList;
            newBattleStyle.GetComponent<SkillsDropdown>().excludedList = excludedList;
            List<string> buf1 = new List<string>();
            foreach (string x in includedList)
            {
                if (!excludedList.Contains(x))
                    buf1.Add(x);
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
                form.AddText(x, 50,FontStyle.Bold);
                form.AddText(discriptionList[includedList.IndexOf(x)]);
            }
        }
    }

    void Discription(Dropdown style, Text textField, List<string> includedList, List<string> discriptionList)
    {
        if (includedList.Contains(style.captionText.text))
        {
            textField.text = discriptionList[includedList.IndexOf(style.captionText.text)];
        }
        else
        {
            textField.text = " ";
        }
    }

    public virtual void Save()
    {
        
    }
}
