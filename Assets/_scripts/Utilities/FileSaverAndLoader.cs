using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class FileSaverAndLoader
{
    public static Ability[] LoadAbilities(string pathName)
    {
        List<Ability> listAbilities = new List<Ability>();
        string JSONAbilities = File.ReadAllText("Assets/Resources/"+pathName+".txt");
        listAbilities = JsonConvert.DeserializeObject<List<Ability>>(JSONAbilities);
        return listAbilities.ToArray();
    }

    public static Feat[] LoadFeats()
    {
        List<Feat> listAbilities = new List<Feat>();
        string JSONAbilities = File.ReadAllText("Assets/Resources/feats.txt");
        listAbilities = JsonConvert.DeserializeObject<List<Feat>>(JSONAbilities);
        return listAbilities.ToArray();
    }
}
