using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void Refresh()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SaveData()
    {
        
    }
}
