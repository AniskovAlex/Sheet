using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUp : MonoBehaviour
{
    [SerializeField] Dropdown featDropdown;
    [SerializeField] List<Dropdown> attrDropdowns;
    [SerializeField] GameObject feat;
    [SerializeField] GameObject attr;
    [SerializeField] GameObject text;


    public void ChoosedFeat()
    {
        if (featDropdown.captionText.text == "Пусто")
        {
            attr.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }
        else
        {
            attr.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }

    public void ChoosedAttr()
    {
        bool flag = true;
        foreach(Dropdown x in attrDropdowns)
        {
            if (x.captionText.text != "Пусто")
                flag = false;
        }
        if (flag)
        {
            feat.SetActive(true);
            text.SetActive(true);
        }
        else
        {
            feat.SetActive(false);
            text.SetActive(false);
        }
    }
}
