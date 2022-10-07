using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpManager : MonoBehaviour
{
    string characterName;
    const string levelCountSaveName = "lvlCount_";
    const string levelSaveName = "lvl_";
    const string levelLabelSaveName = "lvlLabel_";
    public Dropdown chosenClass;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;

    [SerializeField] ClassesAbilities classes;

    private void Start()
    {
        characterName = CharacterCollection.GetName();
    }

    /*public void ClassChanged()
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
                newClass = new Fighter();
                break;
            case 2:
                newClass = new Rogue();
                break;
            case 3:
                newClass = new Artificer();
                break;
        }
    }*/

    public void LoadView()
    {
        characterName = CharacterCollection.GetName();
        if (classes != null)
        {
            DataSaverAndLoader.SaveClass(classes.GetClass().name);
            if (classes.GetClass().GetSubClass() != null)
                DataSaverAndLoader.SaveSubClass(classes.GetClass());
        }
        PresavedLists.SavePrelists();
        PlayerPrefs.Save();
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }
}
