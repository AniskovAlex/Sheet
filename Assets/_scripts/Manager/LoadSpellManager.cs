using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LoadSpellManager 
{
    static Spell[] spells;

    static public bool LoadSpells()
    {
        try
        {
            string JSONSpell = File.ReadAllText("Assets/Resources/spells.json");
            spells = JsonConvert.DeserializeObject<Spell[]>(JSONSpell);
        }
        catch (IOException e)
        {
            Debug.Log(e);
            return false;
        }
        return true;
    }

    static public Spell[] GetSpells()
    {
        return spells;
    }
}
