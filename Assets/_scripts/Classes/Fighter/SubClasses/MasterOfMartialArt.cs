using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterOfMartialArt : PlayerSubClass
{
    const string MOMABattleSupriorityCountSaveName = "MOMABattleSupriorityCount_";
    const string MOMABattleSuprioritySaveName = "MOMABattleSupriority_";
    bool flagBatSup= false;

    public MasterOfMartialArt()
    {
        name = "Мастер боевых искусств";
        LoadAbilities("masterOfMartialArt");
    }
}
