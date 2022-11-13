using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareButton : MonoBehaviour
{
    [SerializeField] PopoutController popupMenu;
    [SerializeField] GameObject preparePopup;

    public void Prepare()
    {
        popupMenu.SetPopout(new List<GameObject> { preparePopup });
    }
}
