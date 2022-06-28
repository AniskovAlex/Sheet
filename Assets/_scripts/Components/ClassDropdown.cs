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
    }

    public void SaveClass()
    {
        if (newClass != null)
        {
            newClass.Save();
        }
    }
}
