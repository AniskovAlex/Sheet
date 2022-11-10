using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareButton : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject popupMenu;
    [SerializeField] GameObject preparePopup;

    public void Prepare()
    {
        popupMenu.SetActive(true);
        Instantiate(preparePopup, panel.transform);
    }
}
