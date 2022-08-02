using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public SelecterManager manager = null;
    string characterName;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<SelecterManager>();
    }
    public void Select()
    {
        if (manager != null)
            manager.LoadCharacter(characterName);
    }

    public void SetName(string text)
    {
        characterName = text;
        this.gameObject.GetComponentInChildren<Text>().text = text;
    }

    public string GetName()
    {
        return characterName;
    }
}
