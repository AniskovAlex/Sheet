using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Modifier : MonoBehaviour
{

    public void ChangeModifier(Text _text)
    {
        int value;
        if (int.TryParse(_text.text, out value))
        {
            int modifier = value / 2 - 5;
            string output = "";
            if (modifier >= 0)
                output += "+";
            output += modifier.ToString();
            gameObject.GetComponent<Text>().text = output;
        }

    }

}
