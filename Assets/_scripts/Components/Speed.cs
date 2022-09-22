using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>();
        text.text = CharacterData.GetRace().GetSpeed().ToString();
    }


}
