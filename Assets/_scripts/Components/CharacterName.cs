using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InputField>().text = CharacterCollection.GetName();
    }

}
