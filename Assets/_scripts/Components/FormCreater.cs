using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormCreater : MonoBehaviour
{
    public GameObject consumable;
    public GameObject basicText;
    public Text AddText(string text)
    {
        GameObject newObject = Instantiate(basicText, GetComponentInChildren<Discription>().transform);
        newObject.GetComponent<Text>().text = text;
        return newObject.GetComponent<Text>();
    }

    public void AddText(string text, int fontSize, FontStyle fontStyle)
    {
        GameObject newObject = Instantiate(basicText, GetComponentInChildren<Discription>().transform);
        newObject.GetComponent<Text>().text = text;
        newObject.GetComponent<Text>().fontStyle = fontStyle;
        newObject.GetComponent<Text>().fontSize = fontSize;
    }

    public void AddText(string text, FontStyle fontStyle)
    {
        GameObject newObject = Instantiate(basicText, GetComponentInChildren<Discription>().transform);
        newObject.GetComponent<Text>().text = text;
        newObject.GetComponent<Text>().fontStyle = fontStyle;
    }


    public void AddText(string text, int fontSize)
    {
        GameObject newObject = Instantiate(basicText, GetComponentInChildren<Discription>().transform);
        newObject.GetComponent<Text>().text = text;
        newObject.GetComponent<Text>().fontSize = fontSize;
    }



    public void AddConsumables(int amount)
    {
        GameObject newObject = Instantiate(consumable, GetComponentInChildren<Opener>().transform);
        newObject.GetComponent<ConsumablePanel>().SpawnToggles(amount);
    }
}
