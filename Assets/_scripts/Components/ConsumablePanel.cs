using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablePanel : MonoBehaviour
{
    public GameObject toggleObject;
    
    public void SpawnToggles(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(toggleObject, transform);
        }
    }

    public void Decrease()
    {
        Toggle[] list = GetComponentsInChildren<Toggle>();
        for(int i = list.Length-1;i>= 0; i--)
        {
            if(list[i].isOn)
            {
                list[i].isOn = false;
                break;
            }
        }
    }

    public void Reset()
    {
        Toggle[] list = GetComponentsInChildren<Toggle>();
        foreach(Toggle x in list)
        {
            x.isOn = true;
        }
    }
}
