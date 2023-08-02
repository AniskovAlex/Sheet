using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeController : MonoBehaviour
{
    [SerializeField] List<GameObject> boxs;
    int add = 0;
    void Start()
    {
        CharacterData.load += Init;
    }

    void Init()
    {
        if (GlobalStatus.alert)
            add += 5;
        foreach (GameObject x in boxs)
        {
            Utilities.SetTextSign(CharacterData.GetModifier(1) + add, x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>());
        }

    }
}
