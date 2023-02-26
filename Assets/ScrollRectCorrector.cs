using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectCorrector : MonoBehaviour
{
    RectTransform scrollRect;
    [SerializeField] RectTransform view;
    [SerializeField] RectTransform content;
    float height;
    float bufCont;

    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<RectTransform>();
        bufCont = content.rect.height;
        height = view.rect.height;
        content.sizeDelta = new Vector2(view.sizeDelta.x - 20, 0);
        scrollRect.sizeDelta = new Vector2(0, height);
    }

    public void Checked()
    {
        if (bufCont == content.rect.height) return;
        bufCont = content.rect.height;
        height = Mathf.Clamp(content.rect.height, 0, view.rect.height);
        scrollRect.sizeDelta = new Vector2(0, height);
        Debug.Log(scrollRect.rect.height);
    }

}
