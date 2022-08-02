using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterCollection : ScriptableObject
{
    string name = null;
    static CharacterCollection instance;

    public static CharacterCollection GetCollection()
    {
        if (!instance)
        {
            instance = Resources.Load("Character Collection") as CharacterCollection;
        }
        return instance;
    }

    public static string GetName()
    {
        if (!instance)
        {
            instance = Resources.Load("Character Collection") as CharacterCollection;
            instance.name = "";
            return instance.name;
        }
        return instance.name;
    }

    public static void SetName(string text)
    {
        if (!instance)
        {
            instance = Resources.Load("Character Collection") as CharacterCollection;
            instance.name = text;
            return;
        }
        instance.name = text;
    }
}
