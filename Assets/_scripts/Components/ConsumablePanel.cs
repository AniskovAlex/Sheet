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

    private void Awake()
    {
        GetComponentInChildren<Button>().onClick.AddListener(delegate { Decrease(); });
    }

    public void SpawnToggles(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(toggleObject, transform);
        }
    }

    public void SpawnResetButton()
    {
        Button button = Instantiate(reset, transform);
        button.GetComponentInChildren<Text>().text = "+";
        button.onClick.AddListener(delegate { ResetToggels(1); });
    }

    public void SpawnResetWarCellsButton()
    {
        Button button = Instantiate(reset, transform);
        button.GetComponentInChildren<Text>().text = "+";
        button.onClick.AddListener(delegate { ResetWarCells(); });
    }

    public void SpawnToggles(int amount, int currentAmount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject gameObject = Instantiate(toggleObject, transform);
            if (currentAmount > 0)
                currentAmount--;
            else
                gameObject.GetComponent<Toggle>().isOn = false;
        }
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
