using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public void LoadSelecter()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("CharacterSelecter", LoadSceneMode.Single);
    }

    public void LoadLeveler()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("LevelUp", LoadSceneMode.Single);
    }
}
