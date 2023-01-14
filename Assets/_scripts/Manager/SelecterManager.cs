using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelecterManager : MonoBehaviour
{
    
    const string charactersCountSaveName = "@charactersCount_";
    const string charactersSaveName = "@characters_";

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
            newObject.GetComponent<Character>().SetName(charName);
        }
    }

    public void ConfiermDelete(Character character)
    {
        confirm.Show(character);
    }

    public void DeleteCharacter(Character character)
    {
        DataSaverAndLoader.DeleteCharacter(character.name);
        Destroy(character.gameObject);
    }

    public void LoadCharacter(string name)
    {
        CharacterCollection.SetName(name);
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    public void LoadBuilder()
    {
        SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Single);
    }

}
