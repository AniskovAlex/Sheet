using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsDropdown : MonoBehaviour
{
    public List<string> list = new List<string>();
    public List<string> excludedList = new List<string>();
    string buf3 = "Пусто";
    public void DropdownChanged()
    {
        Dropdown mySelf = GetComponent<Dropdown>();
        if (buf3 == mySelf.captionText.text)
            return;
        if (list.Count <= 0)
            return;
        SkillsDropdown[] buf1 = FindObjectsOfType<SkillsDropdown>();
        List<Dropdown> drops = new List<Dropdown>();
        foreach (SkillsDropdown x in buf1)
        {
            drops.Add(x.GetComponent<Dropdown>());
        }
        excludedList.Remove(buf3);
        buf3 = mySelf.captionText.text;
        List<string> buf = excludedList;
        /*foreach (Dropdown x in drops)
        {
            if (x.captionText.text != "Пусто" && !excludedList.Contains(x.captionText.text))
                buf.Add(x.captionText.text);
        }*/
        buf.Add(buf3);
        excludedList = buf;
        foreach (Dropdown x in drops)
        {
            if (x != mySelf && x.GetComponent<SkillsDropdown>().excludedList == excludedList)
            {
                string buf2 = x.captionText.text;
                x.ClearOptions();
                x.options.Add(new Dropdown.OptionData("Пусто"));
                if (buf2 == "Пусто")
                {
                    x.captionText.text = buf2;
                    x.value = 0;
                }
                int i = 1;
                foreach (string y in x.GetComponent<SkillsDropdown>().list)
                {
                    if (!buf.Contains(y) || buf2 == y)
                    {
                        x.options.Add(new Dropdown.OptionData(y));
                    }
                    else
                        i--;
                    if (buf2 == y)
                    {
                        x.captionText.text = buf2;
                        x.value = i;
                    }
                    i++;
                }
            }
        }

    }
    private void OnDestroy()
    {
        excludedList.Remove(buf3);
        SkillsDropdown[] buf1 = FindObjectsOfType<SkillsDropdown>();
        List<Dropdown> drops = new List<Dropdown>();
        foreach (SkillsDropdown x in buf1)
        {
            drops.Add(x.GetComponent<Dropdown>());
        }
        foreach (Dropdown x in drops)
        {

            string buf2 = x.captionText.text;
            x.ClearOptions();
            x.options.Add(new Dropdown.OptionData("Пусто"));
            if (buf2 == "Пусто")
            {
                x.captionText.text = buf2;
                x.value = 0;
            }
            int i = 1;
            foreach (string y in x.GetComponent<SkillsDropdown>().list)
            {
                if (!excludedList.Contains(y) || buf2 == y)
                {
                    x.options.Add(new Dropdown.OptionData(y));
                }
                else
                    i--;
                if (buf2 == y)
                {
                    x.captionText.text = buf2;
                    x.value = i;
                }
                i++;
            }
        }
    }
}
