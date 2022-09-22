using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Option : MonoBehaviour
{
    public Item item;
    public Action<Item> Chosen;

    public void Touch()
    {
        Chosen(item);
    }

    public void DestroyMyself()
    {
        DestroyImmediate(gameObject);
    }
}
