using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMainClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<(int, PlayersClass)> classes = CharacterData.GetClasses();
        if (classes.Count > 0)
            GetComponent<InputField>().text = CharacterData.GetClasses()[0].Item2.name;
    }

}
