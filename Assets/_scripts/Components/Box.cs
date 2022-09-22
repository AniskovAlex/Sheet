using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    public int index;
    public DataBase db;
    public string label;

    public void touch()
    {
        //db.DeleteItem(index);
    }

    public void DestroyMyself()
    {
        DestroyImmediate(gameObject);
    }
}
