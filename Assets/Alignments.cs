using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alignments : MonoBehaviour
{
    Dropdown inputField = null;
    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<Dropdown>();
    }

    public void Save()
    {
        if (inputField != null)
        {
            CharacterData.SetAlignments(inputField.value);
            //DataSaverAndLoader.SaveAlignment(inputField.value);
        }
    }
}
