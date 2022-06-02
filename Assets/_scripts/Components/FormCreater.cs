using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormCreater : MonoBehaviour
{
    public GameObject consumable;
    public GameObject basicText;
    public void AddText(string text, FontStyle fontStyle)
    {
        GameObject newObject = Instantiate(basicText, GetComponentInChildren<Discription>().transform);
        newObject.GetComponent<Text>().text = text;
        newObject.GetComponent<Text>().fontStyle = fontStyle;
    }

    public void AddText(string text)
    {
        GameObject newObject = Instantiate(basicText, GetComponentInChildren<Discription>().transform);
        newObject.GetComponent<Text>().text = text;
    }

    public void AddConsumables(int amount)
    {
        GameObject newObject = Instantiate(consumable, GetComponentInChildren<Opener>().transform);
        newObject.GetComponent<ConsumablePanel>().SpawnToggles(amount);
    }
}
