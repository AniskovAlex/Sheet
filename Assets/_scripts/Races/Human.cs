using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Race
{
    public Human(GameObject panel, GameObject basicForm, GameObject dropdownForm, bool redact) : base(panel, basicForm, dropdownForm, redact, 30)
    {
        if (redact)
        {
            AllClassesAbilities.AddFeat(panel, basicForm, dropdownForm, redact, false);
            AllClassesAbilities.AttributiesUp(panel, basicForm, dropdownForm, 2, true);
            languages.Add(Race.Language.common);
            ChooseLanguage(1);
        }
    }
}
