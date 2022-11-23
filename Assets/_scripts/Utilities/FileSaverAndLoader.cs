using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;

public static class FileSaverAndLoader
{
    public static Ability[] LoadAbilities(string pathName)
    {
        List<Ability> listAbilities = new List<Ability>();
        string path = Path.Combine(Application.streamingAssetsPath, pathName + ".json");
        string JSONAbilities = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            www.SendWebRequest();
            while (!www.isDone) ;
            JSONAbilities = www.downloadHandler.text;
        }
        else
            JSONAbilities = File.ReadAllText(path);

        listAbilities = JsonConvert.DeserializeObject<List<Ability>>(JSONAbilities);
        return listAbilities.ToArray();
    }

    public static string LoadFile(string pathname)
    {
        string JSON = "";
        string path = Path.Combine(Application.streamingAssetsPath, pathname + ".json");
        Debug.Log(path);
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            www.SendWebRequest();
            while (!www.isDone) ;
            JSON = www.downloadHandler.text;
            Debug.Log(JSON);
        }
        else
            JSON = File.ReadAllText(path);
        return JSON;
    }

    public static Feat[] LoadFeats()
    {
        List<Feat> listAbilities = new List<Feat>();
        string JSONAbilities = "";
        string path = Path.Combine(Application.streamingAssetsPath, "Feats.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            www.SendWebRequest();
            while (!www.isDone) ;
            JSONAbilities = www.downloadHandler.text;
        }
        else
            JSONAbilities = File.ReadAllText(path);

        listAbilities = JsonConvert.DeserializeObject<List<Feat>>(JSONAbilities);
        return listAbilities.ToArray();
    }

    public static List<(string, string)> LoadList(string pathName)
    {
        pathName = pathName[0].ToString().ToUpper() + pathName.Remove(0,1);
        List<(string, string)> list = new List<(string, string)>();
        string path = Path.Combine(Application.streamingAssetsPath, pathName + ".json");
        string JSONAbilities = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            www.SendWebRequest();
            while (!www.isDone) ;
            JSONAbilities = www.downloadHandler.text;
        }
        else
            JSONAbilities = File.ReadAllText(path);

        list = JsonConvert.DeserializeObject<List<(string, string)>>(JSONAbilities);
        return list;
    }

    public static Spell[] LoadSpells()
    {
        Spell[] spells;
        string JSONSpell = "";
        string path = Path.Combine(Application.streamingAssetsPath, "Spells.json");
        if (Application.platform == RuntimePlatform.Android)
        {
            UnityWebRequest www = UnityWebRequest.Get(path);
            www.SendWebRequest();
            while (!www.isDone) ;
            JSONSpell = www.downloadHandler.text;
        }
        else
            JSONSpell = File.ReadAllText(path);
        spells = JsonConvert.DeserializeObject<Spell[]>(JSONSpell);
        return spells;

    }
}
