using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WeaponCollection : ScriptableObject
{
    List<Weapon> list = new List<Weapon>();
    static WeaponCollection instance;

    void LoadCollection()
    {
        list.Add(new Weapon("���"));
    }

    public static WeaponCollection GetCollection()
    {
        if (!instance)
        {
            instance = Resources.Load("Weapon Collection") as WeaponCollection;
            instance.LoadCollection();
        }
        return instance;
    }

    public void ShowCollection()
    {
        foreach (Weapon x in list)
        {
            Debug.Log(x.label);
        }
    }
    public List<Weapon> GetList()
    {
        return list;
    }
}
