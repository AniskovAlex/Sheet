using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassDropdown : MonoBehaviour
{
    const string skillSaveName = "skill_";

    public Dropdown mySelf;
    public GameObject panel;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;
    PlayersClass newClass = null;
    public void SetSkills()
    {
        Dropdown[] buf1 = panel.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in buf1)
        {
            Destroy(x.gameObject);
        }
        FormCreater[] abilitieForms = abilitiesPanel.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in abilitieForms)
        {
            Destroy(x.gameObject);
        }
        switch (mySelf.value)
        {
            case 1:
                newClass = new Fighter(abilitiesPanel, form, dropdownObject,0);
                break;
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
                buf.options.Add(new Dropdown.OptionData("Пусто"));
                for (int j = 0; j < list.Count; j++)
                {
                    buf.options.Add(new Dropdown.OptionData(listTransform[j].ToString()));
                }
            }
        }
        Debug.Log(panel.GetComponentsInChildren<Dropdown>().Length);
    }

    public void SaveClass()
    {
        if (newClass != null)
        {
            Dropdown[] buf1 = panel.GetComponentsInChildren<Dropdown>();
            foreach (Dropdown x in buf1)
            {
                switch (x.captionText.text)
                {
                    case "Атлетика":
                        PlayerPrefs.SetInt(skillSaveName + 0, 1);
                        break;
                    case "Акробатика":
                        PlayerPrefs.SetInt(skillSaveName + 1, 1);
                        break;
                    case "Ловкость рук":
                        PlayerPrefs.SetInt(skillSaveName + 2, 1);
                        break;
                    case "Скрытность":
                        PlayerPrefs.SetInt(skillSaveName + 3, 1);
                        break;
                    case "Анализ":
                        PlayerPrefs.SetInt(skillSaveName + 4, 1);
                        break;
                    case "История":
                        PlayerPrefs.SetInt(skillSaveName + 5, 1);
                        break;
                    case "Магия":
                        PlayerPrefs.SetInt(skillSaveName + 6, 1);
                        break;
                    case "Природа":
                        PlayerPrefs.SetInt(skillSaveName + 7, 1);
                        break;
                    case "Религия":
                        PlayerPrefs.SetInt(skillSaveName + 8, 1);
                        break;
                    case "Внимательность":
                        PlayerPrefs.SetInt(skillSaveName + 9, 1);
                        break;
                    case "Выживание":
                        PlayerPrefs.SetInt(skillSaveName + 10, 1);
                        break;
                    case "Медицина":
                        PlayerPrefs.SetInt(skillSaveName + 11, 1);
                        break;
                    case "Проницательность":
                        PlayerPrefs.SetInt(skillSaveName + 12, 1);
                        break;
                    case "Уход за животными":
                        PlayerPrefs.SetInt(skillSaveName + 13, 1);
                        break;
                    case "Выступление":
                        PlayerPrefs.SetInt(skillSaveName + 14, 1);
                        break;
                    case "Запугивание":
                        PlayerPrefs.SetInt(skillSaveName + 15, 1);
                        break;
                    case "Обман":
                        PlayerPrefs.SetInt(skillSaveName + 16, 1);
                        break;
                    case "убеждение":
                        PlayerPrefs.SetInt(skillSaveName + 17, 1);
                        break;
                }
            }
            newClass.Save();
        }
    }
}
