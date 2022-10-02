using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ObjectsBehavior
{
    protected GameObject panel;
    protected GameObject basicForm;
    protected GameObject dropdownForm;
    protected bool redact = false;

    protected ObjectsBehavior(GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact)
    {
        this.panel = panel;
        this.basicForm = basicForm;
        this.dropdownForm = dropdownForm;
        this.redact = redact;
    }

    protected ObjectsBehavior()
    {

    }

    public void CreatAbility(string caption, string level, string discription)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
       /*form.AddText(level, FontStyle.Italic);
        form.AddText(discription);*/
    }

    public void CreatAbility(string caption, string level, string discription, int i)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        /*form.AddText(level, FontStyle.Italic);
        form.AddText(discription);
        if (!redact)
            form.AddConsumables(i);*/
    }

    public void CreatAbility(string caption, string level, List<(string, string)> includedList, List<string> excludedList, int count)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        //form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject newBattleStyle = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
                Dropdown buf = newBattleStyle.GetComponent<Dropdown>();
                //Text styleDiscriptionText = form.AddText("");
                //buf.onValueChanged.AddListener(delegate { Discription(buf, styleDiscriptionText, includedList); });

                List<string> captionList = new List<string>();
                foreach ((string, string) x in includedList)
                    captionList.Add(x.Item1);
                newBattleStyle.GetComponent<SkillsDropdown>().list = captionList;
                newBattleStyle.GetComponent<SkillsDropdown>().excludedList = excludedList;
                List<string> buf1 = new List<string>();
                foreach ((string, string) x in includedList)
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
        }
        else
        {
            foreach (string x in excludedList)
            {
                foreach ((string, string) y in includedList)
                {
                    if (x == y.Item1)
                    {
                        /*form.AddText(x, 50, FontStyle.Bold);
                        form.AddText(y.Item2);*/
                        break;
                    }
                }
            }
        }
    }

    public void CreatAbility(string caption, string level, List<(string, string)> includedList, List<string> excludedList)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        //form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            GameObject newBattleStyle = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
            Dropdown buf = newBattleStyle.GetComponent<Dropdown>();
            //Text styleDiscriptionText = form.AddText("");
            //buf.onValueChanged.AddListener(delegate { Discription(buf, styleDiscriptionText, includedList); });

            List<string> captionList = new List<string>();
            foreach ((string, string) x in includedList)
                captionList.Add(x.Item1);
            newBattleStyle.GetComponent<SkillsDropdown>().list = captionList;
            newBattleStyle.GetComponent<SkillsDropdown>().excludedList = excludedList;
            List<string> buf1 = new List<string>();
            foreach ((string, string) x in includedList)
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
                foreach ((string, string) y in includedList)
                {
                    if (x == y.Item1)
                    {
                        /*form.AddText(x, 50, FontStyle.Bold);
                        form.AddText(y.Item2);*/
                        break;
                    }
                }
            }
        }
    }

    void Discription(Dropdown style, Text textField, List<(string, string)> includedList)
    {
        foreach ((string, string) x in includedList)
        {
            if (x.Item1 == style.captionText.text)
            {
                textField.text = x.Item2;
                return;
            }
        }
        textField.text = " ";
    }
}
