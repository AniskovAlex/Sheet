using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemCollection : ScriptableObject
{
    List<Item> list = new List<Item>();
    static ItemCollection instance;

    void LoadCollection()
    {
        list.Add(new Item("����", 2, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����. �����", 50, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 1, -1, Item.MType.copperCoin, Item.Type.item));
        Debug.Log(list.Count);
    } 

    public static ItemCollection GetCollection()
    {
        if (!instance)
        {
            instance = Resources.Load("Item Collection") as ItemCollection;
            instance.LoadCollection();
        }
        return instance;
    }

    public void ShowCollection()
    {
        foreach(Item x in list)
        {
            Debug.Log(x.label + ", " + x.cost+" ��, " + x.weight+" ���.");
        }
    }
    public List<Item> GetList()
    {
        return list;
    }
}
