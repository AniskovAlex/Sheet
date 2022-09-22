using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Initiative : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>();
        int init = CharacterData.GetModifier(1);
        Utilities.SetTextSign(init, text);
    }
}
