using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerSubClass : ObjectsBehavior
{
    protected string characterName = CharacterCollection.GetName();
    protected Ability[] abilities;
    protected int level = 0;
    protected int mainState;
    protected int PB;

    protected void LoadAbilities(string pathName)
    {
        abilities = FileSaverAndLoader.LoadAbilities("subClasses/"+ pathName);
    }

    public virtual Ability[] GetAbilities()
    {
        return abilities;
    }
}
