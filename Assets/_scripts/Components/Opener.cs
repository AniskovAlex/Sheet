using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void click()
    {
        Debug.Log(gameObject.name);
        panel.SetActive(!panel.active);
    }
}
