using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nature : MonoBehaviour
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
            CharacterData.SetNature(inputField.text);
            //DataSaverAndLoader.SaveNature(inputField.text);
        }
    }
}
