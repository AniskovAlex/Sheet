using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributePointBuy : MonoBehaviour
{
    [SerializeField] Text attrObject;
    [SerializeField] Text modifier;
    [SerializeField] Text conPrice;
    [SerializeField] Text plusPrice;
    [SerializeField] Button plusButton;
    [SerializeField] Button minusButton;
    int attr = 8;
    int plus = -1;
    int minus = 1;
    PointBuy pointBuy;

    private void Start()
    {
        pointBuy = GetComponentInParent<PointBuy>();
    }

    public void Plus()
    {
        attr++;
        int buf = plus;
        CheckAttr();
        if (pointBuy != null)
            pointBuy.ChangePoint(buf);
    }

    public void Minus()
    {
        attr--;
        int buf = minus;
        CheckAttr();
        if (pointBuy != null)
            pointBuy.ChangePoint(buf);
    }

    public void SetAttr()
    {
        attrObject.text = attr.ToString();
        Utilities.SetTextSign((attr / 2 - 5), modifier);
    }

    public int GetAttr()
    {
        return attr;
    }

    public void UpdateChanges(int value)
    {
        if (value < Mathf.Abs(plus) || attr >= 15)
            plusButton.interactable = false;
        else
            plusButton.interactable = true;

    }

    void CheckAttr()
    {
        if (attr >= 14)
            minus = 2;
        else
            minus = 1;
        if (attr >= 13)
            plus = -2;
        else
            plus = -1;
        if (attr == 8)
        {
            minusButton.interactable = false;
            minus = 0;
        }
        if (attr < 14)
            plusButton.interactable = true;
        if (attr >= 15)
        {
            plusButton.interactable = false;
            plus = 0;
        }
        if (attr > 8)
            minusButton.interactable = true;
        Utilities.SetTextSign(minus, conPrice);
        Utilities.SetTextSign(plus, plusPrice);
    }
}
