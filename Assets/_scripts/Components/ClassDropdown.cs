using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassDropdown : MonoBehaviour
{

    public Dropdown mySelf;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;
    PlayersClass newClass = null;
    public void SetSkills()
    {
        FormCreater[] abilitieForms = abilitiesPanel.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in abilitieForms)
        {
            Destroy(x.gameObject);
        }
        switch (mySelf.value)
        {
            case 1:
                newClass = new Fighter(1 ,abilitiesPanel, form, dropdownObject);
                break;
            case 2:
                newClass = new Rogue(1, abilitiesPanel, form, dropdownObject);
                break;
        }
        /*if (newClass != null)
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
                    case 2:
                        listTransform.Add("Ловкость рук");
                        break;
                    case 3:
                        listTransform.Add("Скрытность");
                        break;
                    case 4:
                        listTransform.Add("Анализ");
                        break;
                    case 5:
                        listTransform.Add("История");
                        break;
                    case 6:
                        listTransform.Add("Магия");
                        break;
                    case 7:
                        listTransform.Add("Природа");
                        break;
                    case 8:
                        listTransform.Add("Религия");
                        break;
                    case 9:
                        listTransform.Add("Внимательность");
                        break;
                    case 10:
                        listTransform.Add("Выживание");
                        break;
                    case 11:
                        listTransform.Add("Медицина");
                        break;
                    case 12:
                        listTransform.Add("Проницательность");
                        break;
                    case 13:
                        listTransform.Add("Уход за животными");
                        break;
                    case 14:
                        listTransform.Add("Выступление");
                        break;
                    case 15:
                        listTransform.Add("Запугивание");
                        break;
                    case 16:
                        listTransform.Add("Обман");
                        break;
                    case 17:
                        listTransform.Add("убеждение");
                        break;

                }
            }
            for (int i = 0; i < newClass.GetSkillsAmount(); i++)
            {
                GameObject newObject = Instantiate(dropdownObject, panel.transform);
                Dropdown buf = newObject.GetComponent<Dropdown>();
                newObject.GetComponent<SkillsDropdown>().list = listTransform;
                newObject.GetComponent<SkillsDropdown>().excludedList = PresavedLists.skills;
                buf.options.Add(new Dropdown.OptionData("Пусто"));
                for (int j = 0; j < list.Count; j++)
                {
                    buf.options.Add(new Dropdown.OptionData(listTransform[j].ToString()));
                }
            }
        }*/
        //Debug.Log(panel.GetComponentsInChildren<Dropdown>().Length);
    }

    public void SaveClass()
    {
        if (newClass != null)
        {
            newClass.Save();
        }
    }
}
