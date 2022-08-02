using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstory : ObjectsBehavior
{
    protected string characterName = CharacterCollection.GetName();
    protected const string backstorySaveName = "backstory_";

    protected Backstory (GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact) : base(panel, basicForm, dropdownForm, redact)
    {

    }

    public virtual void Save()
    {
        characterName = CharacterCollection.GetName();
    }
}
