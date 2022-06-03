using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsDropdown : MonoBehaviour
{
    public List<string> list;
    string buf3 = "�����";
    public void DropdownChanged()
    {
        Dropdown[] drops = GetComponentInParent<Box>().GetComponentsInChildren<Dropdown>();
        Dropdown mySelf = GetComponent<Dropdown>();
        if (buf3 == mySelf.captionText.text)
            return;
        if (list.Count <= 0)
            return;
        buf3 = mySelf.captionText.text;
        List<string> buf = new List<string>();
        foreach (Dropdown x in drops)
        {
            if (x.captionText.text != "�����")
                buf.Add(x.captionText.text);
        }
        foreach (Dropdown x in drops)
        {
            if (x != mySelf)
            {
                string buf2 = x.captionText.text;
                x.ClearOptions();
                x.options.Add(new Dropdown.OptionData("�����"));
                if (buf2 == "�����")
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