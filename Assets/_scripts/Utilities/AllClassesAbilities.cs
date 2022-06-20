using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class AllClassesAbilities
{
    const string FeatCountSaveName = "FeatCount_";
    const string FeatSaveName = "Feat_";
    const string atrSaveName = "atr_";

    public static void AbilitiesUp(GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact)
    {
        List<string> excludedList = new List<string>();
        if (PlayerPrefs.HasKey(FeatCountSaveName))
        {
            int styles = PlayerPrefs.GetInt(FeatCountSaveName);
            for (int i = 0; i < styles; i++)
            {
                excludedList.Add(PlayerPrefs.GetString(FeatSaveName + i));
            }
        }
        List<(string, string)> includedList = new List<(string, string)>();
        includedList.Add(("Атлетичный", "Вы прошли интенсивную физическую подготовку и получаете следующие преимущества:\n\n-Увеличьте значение Силы или Ловкости на 1 при максимуме 20.\n-Если вы лежите ничком, вставание использует только 5 футов перемещения.\n-Лазание не заставляет вас тратить дополнительное перемещение.\n-Вы можете совершать прыжок в длину или высоту с разбега, переместившись только на 5 футов, а не на 10."));
        string caption;
        if (redact)
        {
            List<string> atributies = new List<string> { "Сила", "Телосложение", "Ловкость", "Интеллект", "Мудрость", "Харизма" };
            caption = "Повышение зарактеристик";
            CreatCharacteristicUpper(panel, basicForm, dropdownForm, redact, caption, atributies, 2);
            caption = "Черты";
            CreatFeat(panel, basicForm, dropdownForm, redact, caption, includedList, excludedList);
        }
        else
        {
            if (excludedList.Count > 0)
            {
                caption = "Черты";
                CreatFeat(panel, basicForm, dropdownForm, redact, caption, includedList, excludedList);
            }
        }
    }

    static void CreatCharacteristicUpper(GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact, string caption, List<string> includedList, int count)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        AttributeUpper attributeUpper = newObject.AddComponent<AttributeUpper>();
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        //form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject newCharacteristicChoose = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
                Dropdown buf = newCharacteristicChoose.GetComponent<Dropdown>();
                Text styleDiscriptionText = form.AddText("");
                buf.onValueChanged.AddListener(delegate { CharacteristicChecker(newObject.GetComponentInChildren<Discription>().gameObject, attributeUpper, count); Validate(); });

                newCharacteristicChoose.GetComponent<SkillsDropdown>().list = includedList;
                newCharacteristicChoose.GetComponent<SkillsDropdown>().excludedList = new List<string>();
                buf.options.Add(new Dropdown.OptionData("Пусто"));
                for (int j = 0; j < includedList.Count; j++)
                {
                    buf.options.Add(new Dropdown.OptionData(includedList[j].ToString()));
                }
            }
        }
    }

    static void CreatFeat(GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact, string caption, List<(string, string)> includedList, List<string> excludedList)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        Feat feat = newObject.AddComponent<Feat>();
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        //form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            GameObject newBattleStyle = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);

            Dropdown buf = newBattleStyle.GetComponent<Dropdown>();
            Text styleDiscriptionText = form.AddText("");
            buf.onValueChanged.AddListener(delegate
            {
                Discription(buf, styleDiscriptionText, includedList);
                FeatAttributies(feat, buf, dropdownForm, newObject.GetComponentInChildren<Discription>().gameObject);
                Validate();
            });
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
                        form.AddText(x, 50, FontStyle.Bold);
                        form.AddText(y.Item2);
                        break;
                    }
                }
            }
        }
    }

    static void CharacteristicChecker(GameObject Form, AttributeUpper attributeUpper, int count)
    {
        Dropdown[] characteristics = Form.GetComponentsInChildren<Dropdown>();
        Weight[] texts = Form.GetComponentsInChildren<Weight>();
        Dropdown main = null;
        bool flag = false;
        int counter = 0;
        int index = 0;
        int buf = 0;
        int attrMain = -1;
        foreach (Dropdown x in characteristics)
        {
            int attr = GetAttributeNumber(x.captionText.text);
            counter++;
            if (attr != -1)
            {
                if (!flag)
                {
                    main = x;
                    flag = true;
                    buf = index;
                    attrMain = attr;
                }
                else
                {
                    texts[index].GetComponent<Text>().text = "+1";
                    attributeUpper.attributies[index].Item1 = attr;
                    attributeUpper.attributies[index].Item2 = 1;
                }


            }
            else
            {
                counter--;
                texts[index].GetComponent<Text>().text = "";
                attributeUpper.attributies[index].Item1 = -1;
                attributeUpper.attributies[index].Item2 = -1;
            }
            index++;
        }
        if (main != null)
            if (counter != count)
            {
                texts[buf].GetComponent<Text>().text = "+2";
                attributeUpper.attributies[buf].Item1 = attrMain;
                attributeUpper.attributies[buf].Item2 = 2;
            }
            else
            {
                texts[buf].GetComponent<Text>().text = "+1";
                attributeUpper.attributies[buf].Item1 = attrMain;
                attributeUpper.attributies[buf].Item2 = 1;
            }
    }

    static void Discription(Dropdown style, Text textField, List<(string, string)> inclcudedList)
    {
        foreach ((string, string) x in inclcudedList)
        {
            if (x.Item1 == style.captionText.text)
            {
                textField.text = x.Item2;
                return;
            }
        }
        textField.text = " ";
    }

    static void FeatAttributies(Feat feat, Dropdown dropdown, GameObject choose, GameObject panel)
    {
        Dropdown[] buf = panel.GetComponentsInChildren<Dropdown>();
        for (int i = 1; i < buf.Length; i++)
        {
            GameObject.DestroyImmediate(buf[i].gameObject);
        }
        switch (dropdown.captionText.text)
        {
            case "Атлетичный":
                SetDropdown("Сила", "Ловкость", choose, panel, feat);
                break;
        }
    }

    static void SetDropdown(string item1, string item2, GameObject choose, GameObject panel, Feat feat)
    {
        GameObject newGameObject = GameObject.Instantiate(choose, panel.transform);
        Dropdown dropdown = newGameObject.GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { ChoosenFeatChanged(feat, dropdown); Validate(); });
        dropdown.options.Add(new Dropdown.OptionData("Пусто"));
        dropdown.options.Add(new Dropdown.OptionData(item1));
        dropdown.options.Add(new Dropdown.OptionData(item2));
    }

    static void ChoosenFeatChanged(Feat feat, Dropdown dropdown)
    {
        feat.attr = GetAttributeNumber(dropdown.captionText.text);
    }

    static void Validate()
    {
        AttributeUpper attr = GameObject.FindObjectOfType<AttributeUpper>();
        Feat feat = GameObject.FindObjectOfType<Feat>();
        Dropdown[] attrDrop = attr.GetComponentsInChildren<Dropdown>();
        Dropdown[] featDrop = feat.GetComponentsInChildren<Dropdown>();
        bool found = false;
        foreach (Dropdown x in attrDrop)
        {
            if (x.captionText.text != "Пусто")
            {
                foreach (Dropdown y in featDrop)
                {
                    y.value = 0;
                    y.interactable = false;
                }
                found = true;
                break;
            }
        }
        if (!found)
            foreach (Dropdown y in featDrop)
            {
                y.interactable = true;
            }
        found = false;

        foreach (Dropdown x in featDrop)
        {
            if (x.captionText.text != "Пусто")
            {
                foreach (Dropdown y in attrDrop)
                {
                    y.value = 0;
                    y.interactable = false;
                }
                found = true;
                break;
            }
        }
        if (!found)
            foreach (Dropdown y in attrDrop)
            {
                y.interactable = true;
            }
    }

    public static void SaveAbilitiesUp()
    {
        AttributeUpper attr = GameObject.FindObjectOfType<AttributeUpper>();
        Feat feat = GameObject.FindObjectOfType<Feat>();
        if (feat != null && attr != null)
        {
            foreach ((int, int) x in attr.attributies)
            {
                if (x.Item1 != -1)
                {
                    PlayerPrefs.SetInt(atrSaveName + x.Item1, PlayerPrefs.GetInt(atrSaveName + x.Item1) + x.Item2);
                }
            }
            if (feat.attr != -1)
                PlayerPrefs.SetInt(atrSaveName + feat.attr, PlayerPrefs.GetInt(atrSaveName + feat.attr) + 1);
            string featName = feat.gameObject.GetComponentInChildren<Dropdown>().captionText.text;
            if (featName != "Пусто")
            {
                int count = PlayerPrefs.GetInt(FeatCountSaveName);
                PlayerPrefs.SetString(FeatSaveName + count, featName);
                PlayerPrefs.SetInt(FeatCountSaveName, count + 1);
            }
        }
    }

    static int GetAttributeNumber(string index)
    {
        switch (index)
        {
            case "Сила":
                return 0;
            case "Телосложение":
                return 1;
            case "Ловкость":
                return 2;
            case "Интеллект":
                return 3;
            case "Мудрость":
                return 4;
            case "Харизма":
                return 5;
            default:
                return -1;
        }
    }
}
