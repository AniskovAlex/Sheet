using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerSubClass : ObjectsBehavior
{

    protected int level = 0;
    protected int mainState;
    protected int PB;

    protected PlayerSubClass(int level, int mainState, GameObject panel, GameObject basicForm, GameObject dropdownForm, int PB, bool redact) : base(panel, basicForm, dropdownForm, redact)
    {
        this.level = level;
        this.mainState = mainState;
        this.panel = panel;
        this.basicForm = basicForm;
        this.dropdownForm = dropdownForm;
        this.PB = PB;
        this.redact = redact;

        if (redact)
        {
            ShowAbilities(level);
        }
        else
        {
            for (int i = 1; i <= level; i++)
            {
                ShowAbilities(i);
            }
        }
    }

    public virtual void ShowAbilities(int level)
    {

    }

    public virtual void Save()
    {

    }
}
