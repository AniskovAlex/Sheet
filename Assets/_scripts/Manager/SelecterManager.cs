using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecterManager : MonoBehaviour
{

    const string charactersCountSaveName = "@charactersCount_";
    const string charactersSaveName = "@characters_";
    const string levelCountSaveName = "@lvlCount_";
    const string levelSaveName = "@lvl_";
    const string levelLabelSaveName = "@lvlLabel_";

    public GameObject panel;
    public GameObject prefab;

    [SerializeField] PopoutConfirm confirm;


    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        CharacterCollection.SetName("");
        int count = PlayerPrefs.GetInt(charactersCountSaveName);
        for (int i = count; i > 0; i--)
        {
            string charName = PlayerPrefs.GetString(charactersSaveName + i);
            GameObject newObject = GameObject.Instantiate(prefab, panel.transform);
            int countClasses = PlayerPrefs.GetInt(charName + levelCountSaveName);
            int level = 0;
            string className = "";
            for (int j = 0; j < countClasses; j++)
            {
                if (PlayerPrefs.HasKey(charName + levelSaveName + j))
                {
                    int classLevel = PlayerPrefs.GetInt(charName + levelSaveName + j);
                    if (j == 0)
                        switch (PlayerPrefs.GetInt(charName + levelLabelSaveName + j))
                        {
                            case 0:
                                className = new Bard().name;
                                break;
                            case 1:
                                className = new Barbarian().name;
                                break;
                            case 2:
                                className = new Fighter().name;
                                break;
                            case 3:
                                className = new Wizard().name;
                                break;
                            case 4:
                                className = new Druid().name;
                                break;
                            case 5:
                                className = new Cleric().name;
                                break;
                            case 7:
                                className = new Warlock().name;
                                break;
                            case 8:
                                className = new Monk().name;
                                break;
                            case 9:
                                className = new Paladin().name;
                                break;
                            case 10:
                                className = new Rogue().name;
                                break;
                            case 11:
                                className = new Ranger().name;
                                break;
                            case 12:
                                className = new Sorcerer().name;
                                break;
                        }
                    level += classLevel;
                }
            }
            if (countClasses > 1)
                className += " +" + (countClasses - 1);
            newObject.GetComponent<Character>().SetCharacter(charName, className, level);
        }
    }

    public void ConfiermDelete(Character character)
    {
        confirm.Show(character);
    }

    public void DeleteCharacter(Character character)
    {
        DataSaverAndLoader.DeleteCharacter(character.GetName());
        Destroy(character.gameObject);
    }

    public void LoadCharacter(string name)
    {
        CharacterCollection.SetName(name);
        LoadSceneManager.Instance.LoadScene("view");
        //SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    public void LoadBuilder()
    {
        LoadSceneManager.Instance.LoadScene("CharacterBuilder");
        //SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Single);
    }

}
