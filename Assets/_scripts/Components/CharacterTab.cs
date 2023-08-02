using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTab : MonoBehaviour
{
    [SerializeField] Text characterNameText;
    [SerializeField] Text characterClassText;
    [SerializeField] Text characterLevelText;
    public SelecterManager manager = null;
    string characterName;
    int id;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<SelecterManager>();
    }
    public void Select()
    {
        if (manager != null)
            manager.LoadCharacter(characterName, id);
    }

    public void SetCharacter(string name, string className, int level, int id)
    {
        characterName = name;
        characterNameText.text = name;
        characterClassText.text = className;
        characterLevelText.text = "" + level;
        this.id = id;
    }

    public string GetName()
    {
        return characterName;
    }

    public int GetId()
    {
        return id;
    }

    public void DeleteCharacter()
    {
        manager.ConfiermDelete(this);
    }
}
