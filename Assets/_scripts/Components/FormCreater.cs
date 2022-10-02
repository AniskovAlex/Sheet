using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormCreater : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] GameObject charUp;
    public GameObject consumable;
    public GameObject basicText;
    GameObject discription;

    private void Awake()
    {
        discription = GetComponentInChildren<Discription>().gameObject;
    }

    public void CreateAbility(Ability ability)
    {
        head.text = ability.head;
        switch (ability.type)
        {
            case Ability.Type.abilitie:
                foreach ((int, string) x in ability.discription)
                    SetText(x);
                break;
            case Ability.Type.charUp:
                Instantiate(charUp, discription.transform);
                break;
        }
    }

    void SetText((int, string) preText)
    {
        int textSize;
        FontStyle fontStyle;
        Text newObjectText = Instantiate(basicText, discription.transform).GetComponent<Text>();
        switch (preText.Item1)
        {
            default:
            case 0:
                textSize = 40;
                fontStyle = FontStyle.Normal;
                break;
            case 1:
                textSize = 40;
                fontStyle = FontStyle.Italic;
                break;
            case 2:
                textSize = 60;
                fontStyle = FontStyle.Bold;
                break;
        }
        newObjectText.text = preText.Item2;
        newObjectText.fontSize = textSize;
        newObjectText.fontStyle = fontStyle;

    }
    /*public Text AddText(string text)
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
    }*/
}
