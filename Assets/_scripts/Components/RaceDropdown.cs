using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceDropdown : MonoBehaviour
{
    public Dropdown mySelf;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;
    Race newRace = null;
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
                newRace = new Human(abilitiesPanel, form, dropdownObject);
                break;
            case 2:
                newRace = new Harengon(abilitiesPanel, form, dropdownObject);
                break;
        }
    }

    public void SaveRace()
    {
        if (newRace != null)
        {
            newRace.Save();
        }
    }
}
