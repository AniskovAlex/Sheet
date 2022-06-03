using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassDropdown : MonoBehaviour
{
    public Dropdown mySelf;
    public GameObject panel;
    public GameObject dropdownObject;
    public void SetSkills()
    {
        PlayersClass newClass = null;
        switch (mySelf.value)
        {
            case 1:
                newClass = new Fighter();
                break;
        }
        Dropdown[] buf1 = panel.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in buf1)
        {
            Destroy(x.gameObject);
        }
        if (newClass != null)
        {
            List<int> list = newClass.GetSkillProfs();
            List<string> listTransform = new List<string>();
            foreach (int x in list)
            {
                switch (x)
                {
                    case 0:
                        listTransform.Add("Атлетика");
                        break;
                    case 1:
                        listTransform.Add("Акробатика");
                        break;
                }
            }
            for (int i = 0; i < newClass.GetSkillsAmount(); i++)
            {
                GameObject newObject = Instantiate(dropdownObject, panel.transform);
                Dropdown buf = newObject.GetComponent<Dropdown>();
                dropdownObject.GetComponent<SkillsDropdown>().list = listTransform;
                buf.options.Add(new Dropdown.OptionData("Пусто"));
                for (int j = 0; j < list.Count; j++)
                {
                    buf.options.Add(new Dropdown.OptionData(listTransform[j].ToString()));
                }
            }
        }
    }
}
