using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] Text characterNameText;
    [SerializeField] Text characterClassText;
    [SerializeField] Text characterLevelText;
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

    public void SetCharacter(string name, string className, int level)
    {
        characterName = name;
        characterNameText.text = name;
        characterClassText.text = className;
        characterLevelText.text = "" + level;
    }

    public string GetName()
    {
        return characterName;
    }

    public void DeleteCharacter()
    {
        manager.ConfiermDelete(this);
    }
}
