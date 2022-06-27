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
        Dropdown[] drops = GetComponentInParent<Box>().GetComponentsInChildren<Dropdown>();
        Dropdown mySelf = GetComponent<Dropdown>();
        if (buf3 == mySelf.captionText.text)
            return;
        if (list.Count <= 0)
            return;
        excludedList.Remove(buf3);
        buf3 = mySelf.captionText.text;
        List<string> buf = excludedList;
        foreach (Dropdown x in drops)
        {
            if (x.captionText.text != "Пусто" && !excludedList.Contains(x.captionText.text))
                buf.Add(x.captionText.text);
        }
        excludedList = buf;
        foreach (Dropdown x in drops)
        {
            if (x != mySelf)
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
                foreach (string y in list)
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
}
