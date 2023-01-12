using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharUp : MonoBehaviour
{
    [SerializeField] ChooseFeat feat;
    [SerializeField] ChooseAttr attr;
    [SerializeField] GameObject text;

    private void Start()
    {
        attr.maxValue = 2;
        attr.SetDropdowns(2);
        attr.check += ChoosedAttr;
    }
    
    public void ChoosedFeat()
    {
        if (feat.GetDropdown().captionText.text == "Пусто")
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
        foreach(Dropdown x in attr.GetDropdowns())
        {
            if (x.captionText.text != "Пусто")
                flag = false;
        }
        if (flag)
        {
            feat.gameObject.SetActive(true);
            text.SetActive(true);
        }
        else
        {
            feat.gameObject.SetActive(false);
            text.SetActive(false);
        }
    }
}
