using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmorCollection : ScriptableObject
{
    List<Armor> list = new List<Armor>();
    static ArmorCollection instance;

    /*void LoadCollection()
    {
        list.Add(new Armor("������", 14, 2, 0, false, Armor.Type.Medium));
        list.Add(new Armor("�������", 11, 0, 0, true, Armor.Type.Light));
        list.Add(new Armor("�������", 11, 0, 0, false, Armor.Type.Light));
        list.Add(new Armor("����������� �������", 12, 0, 0, false, Armor.Type.Light));
        list.Add(new Armor("�������", 12, 2, 0, false, Armor.Type.Medium));
        list.Add(new Armor("���������� ������", 13, 2, 0, false, Armor.Type.Medium));
        list.Add(new Armor("����������", 14, 2, 0, true, Armor.Type.Medium));
        list.Add(new Armor("��������", 15, 2, 0, true, Armor.Type.Medium));
        list.Add(new Armor("��������", 14, -1, 0, true, Armor.Type.Heavy));
        list.Add(new Armor("��������", 16, -1, 13, true, Armor.Type.Heavy));
        list.Add(new Armor("��������", 17, -1, 15, true, Armor.Type.Heavy));
        list.Add(new Armor("����", 18, -1, 15, true, Armor.Type.Heavy));
        list.Add(new Armor("���", 2, -1, 0, false, Armor.Type.Shield));
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
    }*/
}
