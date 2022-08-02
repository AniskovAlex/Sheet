using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Race : ObjectsBehavior
{
    protected string characterName = CharacterCollection.GetName();
    const string sizeSaveName = "size_";
    const string speedSaveName = "spd_";
    protected const string raceSaveName = "race_";



    public enum Size
    {
        little,
        medium,
        large
    }

    protected Size size;
    int speed;
    protected Race(GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact, int speed) : base(panel, basicForm, dropdownForm, redact)
    {
        this.speed = speed;
        RaceDiscription();
    }



    public virtual void RaceDiscription()
    {

    }

    public virtual void Erase()
    {

    }

    public virtual void Save()
    {
        characterName = CharacterCollection.GetName();
        AllClassesAbilities.SaveLanguage();
        int buf1 = size - Size.little;
        PlayerPrefs.SetInt(characterName + sizeSaveName, buf1);
        PlayerPrefs.SetInt(characterName + speedSaveName, speed);
    }
}
