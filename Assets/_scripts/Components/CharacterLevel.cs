using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InputField>().text = CharacterData.GetLevel().ToString();
    }

}
