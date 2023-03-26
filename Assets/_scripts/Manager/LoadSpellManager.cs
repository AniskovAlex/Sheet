using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadSpellManager 
{
    static Spell[] spells;

    static public bool LoadSpells()
    {
        spells = FileSaverAndLoader.LoadSpells();
        return true;
    }

    static public Spell[] GetSpells()
    {
        if (spells == null) LoadSpells();
        return spells;
    }
}
