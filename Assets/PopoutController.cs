using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopoutController : MonoBehaviour
{
    [SerializeField] GameObject panel;

    private void OnEnable()
    {
        GlobalStatus.popoutMenu = true;
    }

    private void OnDisable()
    {
        GlobalStatus.popoutMenu = false;
        for (int i = 0; i < panel.transform.childCount; i++)
            Destroy(panel.transform.GetChild(i).gameObject);
    }

    public void Cancle()
    {
        gameObject.SetActive(false);
    }
}
