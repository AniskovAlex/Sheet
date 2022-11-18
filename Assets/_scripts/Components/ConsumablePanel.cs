using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablePanel : MonoBehaviour
{
    public GameObject toggleObject;
    public Action<int> update;

    public void SpawnToggles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(toggleObject, transform);
        }
    }
    public void SpawnToggles(int amount, int currentAmount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject gameObject = Instantiate(toggleObject, transform);
            if (currentAmount > 0)
                currentAmount--;
            else
                gameObject.GetComponent<Toggle>().isOn = false;
        }
    }

    public void Decrease()
    {
        Toggle[] list = GetComponentsInChildren<Toggle>();
        for (int i = list.Length - 1; i >= 0; i--)
        {
            if (list[i].isOn)
            {
                list[i].isOn = false;
                update(i);
                break;
            }
        }
    }

    public void Reset()
    {
        Toggle[] list = GetComponentsInChildren<Toggle>();
        foreach (Toggle x in list)
        {
            x.isOn = true;
        }
        update(list.Length);
    }
}
