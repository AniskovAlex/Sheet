using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseAttr : MonoBehaviour
{
    [SerializeField] GameObject dropdown;
    public int maxValue = 1;
    public Action check;
    List<string> attrAdd = new List<string>();
    HashSet<string> attrs = new HashSet<string> { "Сила", "Ловкость", "Телосложение", "Интеллект", "Мудрость", "Харизма" };

    public void SetDropdowns(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(dropdown, gameObject.transform);
            List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
            foreach (string y in attrs)
            {
                list.Add(new Dropdown.OptionData(y));
            }
            list.Add(new Dropdown.OptionData("Пусто"));
            list.Add(new Dropdown.OptionData("Пусто"));
            newObject.GetComponent<Dropdown>().options = list;
            newObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { ChangeSelected(newObject.GetComponent<Dropdown>()); });

        }
    }
    public void SetDropdowns(int count, List<string> exclude)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newObject = Instantiate(dropdown, gameObject.transform);
            List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
            attrs.ExceptWith(exclude);
            foreach (string y in attrs)
            {
                list.Add(new Dropdown.OptionData(y));
            }
            list.Add(new Dropdown.OptionData("Пусто"));
            list.Add(new Dropdown.OptionData("Пусто"));
            newObject.GetComponent<Dropdown>().options = list;
            newObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { ChangeSelected(newObject.GetComponent<Dropdown>()); });

        }
    }

    public Dropdown[] GetDropdowns()
    {
        return GetComponentsInChildren<Dropdown>();
    }

    void ChangeSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        attrAdd.Remove(oldValue);
        if (dropdown.captionText.text != "Пусто")
            attrAdd.Add(dropdown.captionText.text);
        if (attrAdd.FindAll(g => g == dropdown.captionText.text).Count >= maxValue)
            attrs.Remove(dropdown.captionText.text);
        if ((oldValue != "" && oldValue != "Пусто") && attrAdd.FindAll(g => g == oldValue).Count < maxValue)
            attrs.Add(oldValue);
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        foreach (string y in attrs)
        {
            list.Add(new Dropdown.OptionData(y));
        }
        list.Add(new Dropdown.OptionData("Пусто"));
        list.Add(new Dropdown.OptionData("Пусто"));
        foreach (Dropdown x in GetDropdowns())
        {
            x.onValueChanged.RemoveAllListeners();
            string buf = x.captionText.text;
            x.options = list;
            x.value = x.options.Count - 1;
            x.captionText.text = buf;
            x.onValueChanged.AddListener(delegate { ChangeSelected(x); });
        }
        PresavedLists.UpdateAttrAdd(oldValue, dropdown.captionText.text);
        if (check != null)
            check();
    }

    private void OnDestroy()
    {
        foreach (string x in attrAdd)
            PresavedLists.RemoveFromAttrAdd(x);
    }

    private void OnBecameInvisible()
    {
        foreach (string x in attrAdd)
            PresavedLists.RemoveFromAttrAdd(x);
    }
}
