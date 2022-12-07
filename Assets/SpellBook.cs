using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    [SerializeField] PopoutController popupMenu;
    [SerializeField] GameObject preparePopup;

    public void Add()
    {
        popupMenu.SetPopout(new List<GameObject> { preparePopup });
    }
}
