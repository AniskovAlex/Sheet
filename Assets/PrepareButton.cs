using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareButton : MonoBehaviour
{
    [SerializeField] PopoutController popupMenu;
    [SerializeField] SpellMasterAdd masterPopup;
    [SerializeField] GameObject preparePopup;

    public void Prepare()
    {
        List<GameObject> list = new List<GameObject>();
        if (GlobalStatus.spellMaster)
            list.Add(masterPopup.gameObject);
        list.Add(preparePopup);
        popupMenu.SetPopout(list);
    }
}
