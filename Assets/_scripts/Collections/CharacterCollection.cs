using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterCollection : ScriptableObject
{
    int id = 0;
    string name = "";
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
    public static int GetId()
    {
        if (!instance)
        {
            instance = Resources.Load("Character Collection") as CharacterCollection;
            instance.id = 0;
            return instance.id;
        }
        return instance.id;
    }

    public static void SetName(string name)
    {
        if (!instance)
        {
            instance = Resources.Load("Character Collection") as CharacterCollection;
            instance.name = name;
            return;
        }
        instance.name = name;
    }

    public static void SetId(int id)
    {
        if (!instance)
        {
            instance = Resources.Load("Character Collection") as CharacterCollection;
            instance.id = id;
            return;
        }
        instance.id = id;
    }
}
