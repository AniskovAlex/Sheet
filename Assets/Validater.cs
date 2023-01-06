using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Validater : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text characterName;
    const string charactersCountSaveName = "@charactersCount_";
    const string charactersSaveName = "@characters_";

    public bool Validate()
    {
        Dropdown[] dropdowns = canvas.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in dropdowns)
            if (x.captionText.text == "" || x.captionText.text == "Пусто")
                return false;
        if (characterName != null && characterName.text == "")
            return false;
        int count = PlayerPrefs.GetInt(charactersCountSaveName);
        for (int i = count; i > 0; i--)
        {
            string charName = PlayerPrefs.GetString(charactersSaveName + i);
            if (charName == characterName.text)
                return false;
        }
        return true;
    }

}
