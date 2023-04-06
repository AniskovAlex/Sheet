using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentLocater : MonoBehaviour
{
    [SerializeField] float verticalPadding;
    [SerializeField] float horizontalPadding;
    [SerializeField] float space;
    RectTransform rectTransform;
    public int preferedChild = -1;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        float hieght =  Size();
        int active = 0;
        for (int i = 0; i < transform.childCount && i != preferedChild; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy) continue;
            active++;
        }
        if (preferedChild>0 && hieght + verticalPadding< rectTransform.sizeDelta.y)
        {
            float bufSapce = 0;
            if (active != 0)
                bufSapce = space;
            RectTransform preferedChildRectTransform = transform.GetChild(preferedChild).GetComponent<RectTransform>();
            preferedChildRectTransform.sizeDelta = new Vector2(preferedChildRectTransform.sizeDelta.x, rectTransform.sizeDelta.y - (hieght - preferedChildRectTransform.sizeDelta.y) - verticalPadding - bufSapce);
            Size();
        }

    }

    float Size()
    {
        float hieght = 0f;
        int childCount = transform.childCount;
        int active = 0;
        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.activeInHierarchy) continue;
            RectTransform childRectTransform = transform.GetChild(i).GetComponent<RectTransform>();
            childRectTransform.anchoredPosition = new Vector2(0, -(hieght + verticalPadding + (active * space)));
            hieght += childRectTransform.rect.height;
            active++;
        }
        return hieght;
    }

}
