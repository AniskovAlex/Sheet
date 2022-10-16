using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointBuy : MonoBehaviour
{
    public int points = 27;
    [SerializeField] Text pointObj;
    AttributePointBuy[] attributes;

    private void Start()
    {
        pointObj.text = points.ToString();
        attributes = GetComponentsInChildren<AttributePointBuy>();
    }

    public void ChangePoint(int value)
    {
        points += value;
        foreach(AttributePointBuy x in attributes)
        {
            x.UpdateChanges(points);
        }
        pointObj.text = points.ToString();
    }
}
