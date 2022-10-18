using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesCreater : MonoBehaviour
{
    [SerializeField] GameObject choosePanel, pointBuyPanel, randomPanel, returnButton;
    int position = 0;

    public void ShowPointBuy()
    {
        returnButton.SetActive(true);
        pointBuyPanel.SetActive(true);
        choosePanel.SetActive(false);
        position = 1;
    }

    public void ShowRandom()
    {
        returnButton.SetActive(true);
        randomPanel.SetActive(true);
        choosePanel.SetActive(false);
        position = 2;
    }

    public void ReturnToChoose()
    {
        returnButton.SetActive(false);
        pointBuyPanel.SetActive(false);
        randomPanel.SetActive(false);
        choosePanel.SetActive(true);
        position = 0;
    }

    public int[] GetAttributes()
    {
        int[] arr = new int[6];
        switch (position)
        {
            case 1:
                AttributePointBuy[] attrsPB = pointBuyPanel.GetComponentsInChildren<AttributePointBuy>();
                for (int i = 0; i < arr.Length; i++)
                    arr[i] = attrsPB[i].GetAttr();
                break;
            case 2:
                SlotAttr[] attrsR = randomPanel.GetComponentsInChildren<SlotAttr>();
                foreach (SlotAttr x in attrsR)
                    if (x.attr != -1)
                        arr[x.attr] = x.GetAttr();
                break;
        }
        return arr;
    }
}
