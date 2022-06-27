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
                        listTransform.Add("��������");
                        break;
                    case 1:
                        listTransform.Add("����������");
                        break;
                    case 2:
                        listTransform.Add("�������� ���");
                        break;
                    case 3:
                        listTransform.Add("����������");
                        break;
                    case 4:
                        listTransform.Add("������");
                        break;
                    case 5:
                        listTransform.Add("�������");
                        break;
                    case 6:
                        listTransform.Add("�����");
                        break;
                    case 7:
                        listTransform.Add("�������");
                        break;
                    case 8:
                        listTransform.Add("�������");
                        break;
                    case 9:
                        listTransform.Add("��������������");
                        break;
                    case 10:
                        listTransform.Add("���������");
                        break;
                    case 11:
                        listTransform.Add("��������");
                        break;
                    case 12:
                        listTransform.Add("����������������");
                        break;
                    case 13:
                        listTransform.Add("���� �� ���������");
                        break;
                    case 14:
                        listTransform.Add("�����������");
                        break;
                    case 15:
                        listTransform.Add("�����������");
                        break;
                    case 16:
                        listTransform.Add("�����");
                        break;
                    case 17:
                        listTransform.Add("���������");
                        break;

                }
            }
            for (int i = 0; i < newClass.GetSkillsAmount(); i++)
            {
                GameObject newObject = Instantiate(dropdownObject, panel.transform);
                Dropdown buf = newObject.GetComponent<Dropdown>();
                newObject.GetComponent<SkillsDropdown>().list = listTransform;
                newObject.GetComponent<SkillsDropdown>().excludedList = PresavedLists.skills;
                buf.options.Add(new Dropdown.OptionData("�����"));
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
