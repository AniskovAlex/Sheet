using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Box[] boxList = GetComponentsInChildren<Box>();
        foreach (Box x in boxList)
        {
            int value = CharacterData.GetAtribute(x.index);
            x.GetComponentInChildren<Attribute>().gameObject.GetComponent<Text>().text = value.ToString();
            int modifier = CharacterData.GetModifier(x.index);
            string str;
            if (modifier >= 0)
                str = "+" + modifier.ToString();
            else
                str = modifier.ToString();
            x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = str;

        }
    }
}
