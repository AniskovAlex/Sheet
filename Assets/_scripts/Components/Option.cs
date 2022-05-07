using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public InputField input;
    public DataBase db;
    public Item item;
    public void Touch()
    {
        db.flag = false;
        db.addItem = item;
        db.flag1 = true;
        db.showFoundedItem();
        //db.DestroyInChil(0); 
    }

    public void DestroyMyself()
    {
        DestroyImmediate(gameObject);
        //Destroy(gameObject,t);
    }
}
