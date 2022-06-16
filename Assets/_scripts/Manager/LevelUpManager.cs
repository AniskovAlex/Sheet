using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpManager : MonoBehaviour
{
    const string levelSaveName = "lvl_";
    public Dropdown chosenClass;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;
    PlayersClass newClass = null;

    private void Start()
    {

    }

    public void ClassChanged()
    {
        int level = PlayerPrefs.GetInt(levelSaveName) + 1;
        Debug.Log(level);
        FormCreater[] abilitieForms = abilitiesPanel.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in abilitieForms)
        {
            Destroy(x.gameObject);
        }
        switch (chosenClass.value)
        {
            case 1:
                newClass = new Fighter(level, abilitiesPanel, form, dropdownObject);
                break;
        }
    }

    public void LoadView()
    {
        if (newClass != null)
        {
            newClass.Save();
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }
}
