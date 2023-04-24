using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablePanel : MonoBehaviour
{
    public GameObject toggleObject;
    public Action<int> update;
    public Button reset;
    [SerializeField] GameObject horizontalePanel;
    [SerializeField] GameObject current;
    float width = 0f;
    public float widthMax = 540f;//560
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Button buf = GetComponentInChildren<Button>();
        buf.onClick.AddListener(delegate { Decrease(); });
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 80 + 20);
    }

    public void SpawnToggles(int amount)
    {
        Opener opener;
        if (transform.parent.TryGetComponent<Opener>(out opener))
        {
            opener.AddConsum(this);
        }
        Selectable[] toggles = current.GetComponentsInChildren<Selectable>();
        foreach (Selectable x in toggles)
            width += 90;
        for (int i = 0; i < amount; i++)
        {
            width += 90;
            if (width >= widthMax)
            {
                width = 90;
                current = Instantiate(horizontalePanel, transform);
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + 80 + 10);
                if (opener != null)
                    opener.ResizeHead(80 + 10);
            }
            Instantiate(toggleObject, current.transform);
        }
    }

    public void SpawnResetButton()
    {
        Selectable[] toggles = current.GetComponentsInChildren<Selectable>();
        float width = 0f;
        foreach (Selectable x in toggles)
            width += x.GetComponent<RectTransform>().rect.width;
        width += 90;
        if (width >= widthMax)
        {
            width = reset.GetComponent<RectTransform>().rect.width;
            current = Instantiate(horizontalePanel, transform);
        }
        Button button = Instantiate(reset, current.transform);
        button.GetComponentInChildren<Text>().text = "+";
        button.onClick.AddListener(delegate { ResetToggels(1); });
    }

    public void SpawnResetWarCellsButton()
    {
        Selectable[] toggles = current.GetComponentsInChildren<Selectable>();
        float width = 0f;
        foreach (Selectable x in toggles)
            width += x.GetComponent<RectTransform>().rect.width;
        width += 90;
        if (width >= widthMax)
        {
            width = reset.GetComponent<RectTransform>().rect.width;
            current = Instantiate(horizontalePanel, transform);
        }
        Button button = Instantiate(reset, current.transform);
        button.GetComponentInChildren<Text>().text = "+";
        button.onClick.AddListener(delegate { ResetWarCells(); });
    }

    public void SpawnToggles(int amount, int currentAmount)
    {
        Opener opener;
        if (transform.parent.TryGetComponent<Opener>(out opener))
        {
            opener.AddConsum(this);
        }
        //[] toggles = current.GetComponentsInChildren<Selectable>();
        float width = 0f;
        for (int i = 0; i < current.transform.childCount; i++)
            width += 90;
        for (int i = 0; i < amount; i++)
        {
            width += 90;
            if (width >= widthMax)
            {
                width = 90;
                current = Instantiate(horizontalePanel, transform);
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + 80 + 10);
                if (opener != null)
                    opener.ResizeHead(80 + 10);
            }
            GameObject gameObject = Instantiate(toggleObject, current.transform);
            if (currentAmount > 0)
                currentAmount--;
            else
                gameObject.GetComponent<Toggle>().isOn = false;
        }
        if (opener != null)
            opener.ResizeHead(0);
    }

    public void Decrease()
    {
        Toggle[] list = GetComponentsInChildren<Toggle>();
        for (int i = list.Length - 1; i >= 0; i--)
        {
            if (list[i].isOn)
            {
                list[i].isOn = false;
                if (update != null)
                    update(i);
                break;
            }
        }
    }

    public void ResetToggels()
    {
        Toggle[] list = GetComponentsInChildren<Toggle>();
        if (list == null) return;
        foreach (Toggle x in list)
        {
            x.isOn = true;
        }
        if (update != null)
            update(list.Length);
    }

    public void ResetWarCells()
    {
        SpellController spellController = FindObjectOfType<SpellController>();
        if (spellController != null)
            spellController.ResetWarSpellCells();
    }

    public void ResetToggels(int add)
    {
        Toggle[] list = GetComponentsInChildren<Toggle>();
        bool flag = false;
        if (add == 0)
            flag = true;
        int count = 0;
        foreach (Toggle x in list)
        {
            if (flag)
            {
                x.isOn = true;
                continue;
            }
            if (!x.isOn)
            {
                if (add > 0)
                {
                    add--;
                    x.isOn = true;
                    count++;
                }
                else
                {
                    if (add == -1)
                    {
                        x.isOn = true;
                        count++;
                    }
                }
            }
            else
                count++;
        }
        if (update != null)
            update(count);
    }
}
