using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackstoryDropdown : MonoBehaviour
{
    public Dropdown mySelf;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;
    Backstory newBackstory = null;
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
                newBackstory = new Artist(abilitiesPanel, form, dropdownObject);
                break;
        }
    }

    public void SaveBackstory()
    {
        if (newBackstory != null)
        {
            newBackstory.Save();
        }
    }
}
