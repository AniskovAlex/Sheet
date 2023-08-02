using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weakness : MonoBehaviour
{
    InputField inputField = null;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void Save()
    {
        if (inputField != null)
        {
            CharacterData.SetWeakness(inputField.text);
            //DataSaverAndLoader.SaveWeakness(inputField.text);
        }
    }
}
