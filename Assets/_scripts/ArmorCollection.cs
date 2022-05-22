using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmorCollection : ScriptableObject
{
    List<Armor> list = new List<Armor>();
    static ArmorCollection instance;

    void LoadCollection()
    {
        list.Add(new Armor("кираса", 14, 2, 0, false, Armor.Type.Medium));
    }

    public static ArmorCollection GetCollection()
    {
        if (!instance)
        {
            instance = Resources.Load("Armor Collection") as ArmorCollection;
            instance.LoadCollection();
        }
        return instance;
    }

    public void ShowCollection()
    {
        foreach (Armor x in list)
        {
            Debug.Log(x.label);
        }
    }
    public List<Armor> GetList()
    {
        return list;
    }
}
