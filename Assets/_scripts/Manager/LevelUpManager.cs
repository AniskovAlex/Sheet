using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpManager : MonoBehaviour
{
    string characterName = CharacterCollection.GetName();
    const string levelCountSaveName = "lvlCount_";
    const string levelSaveName = "lvl_";
    const string levelLabelSaveName = "lvlLabel_";
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
        int count = PlayerPrefs.GetInt(characterName + levelCountSaveName);
        int level = 1;
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.GetString(characterName + levelLabelSaveName + i) == chosenClass.captionText.text)
            {
                level = PlayerPrefs.GetInt(characterName + levelSaveName + i) + 1;
                break;
            }
        }
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
            case 2:
                newClass = new Rogue(level, abilitiesPanel, form, dropdownObject);
                break;
            case 3:
                newClass = new Artificer(level, abilitiesPanel, form, dropdownObject);
                break;
        }
    }

    public void LoadView()
    {
        if (newClass != null)
        {
            //newClass.Save();
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }
}
