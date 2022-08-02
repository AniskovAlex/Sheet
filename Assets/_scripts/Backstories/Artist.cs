using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist : Backstory
{
    public Artist(GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(panel, basicForm, dropdownForm, true)
    {
        PresavedLists.skills.Add("јкробатика");
        PresavedLists.skills.Add("¬ыступление");
        Special();
    }

    public Artist(GameObject panel, GameObject basicForm) : base(panel, basicForm, null, false)
    {
        Special();
    }

    void Special()
    {
        string caption = "”мение: ѕо многочисленным просьбам";
        string discription = "¬ы всегда можете найти место дл€ выступлени€. ќбычно это таверна или посто€лый двор, но это может быть цирк, театр или даже двор знатного господина. ¬ этом месте вы получаете бесплатный постой и еду по скромным или комфортным стандартам (в зависимости от качества заведени€), если вы выступаете каждый вечер.  роме того, ваши выступлени€ делают вас местной знаменитостью.  огда посторонние узнают вас в городе, в котором вы давали представление, они, скорее всего, будут к вам относитьс€ хорошо.";
        CreatAbility(caption, "", discription);
    }

    public override void Save()
    {
        base.Save();
        PlayerPrefs.SetString(characterName + backstorySaveName, "јртист");
    }
}
