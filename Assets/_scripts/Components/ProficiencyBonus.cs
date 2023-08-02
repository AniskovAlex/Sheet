using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProficiencyBonus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CharacterData.load += Init;
    }

    void Init()
    {
        GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = "+" + CharacterData.GetProficiencyBonus();

    }
}
