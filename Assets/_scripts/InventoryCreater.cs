using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCreater : MonoBehaviour
{
    [SerializeField] GameObject choosePanel, buyPanel, standartPanel, returnButton;
    [SerializeField] ClassesAbilities classes;
    [SerializeField] BackstoryAbilities backstory;
    Item[] items;
    int position = 0;

    private void Start()
    {
        items = GetComponent<LoadInventoryManager>().GetItems();
        classes.chosen += ReturnToChoose;
        backstory.chosen += ReturnToChooseWithCheck;
    }

    public void ShowBuy()
    {
        if (classes.GetClass() != null)
        {
            returnButton.SetActive(true);
            buyPanel.SetActive(true);
            choosePanel.SetActive(false);
            buyPanel.GetComponent<BuyInventoryPanel>().SetAdder(classes.GetClass());
            position = 1;
        }
    }

    public void ShowStandart()
    {
        if (classes.GetClass() != null && backstory.GetBackstory() != null)
        {
            returnButton.SetActive(true);
            standartPanel.SetActive(true);
            choosePanel.SetActive(false);
            standartPanel.GetComponentInChildren<StandartInvetoryPanel>().SetInventoryChoice(classes.GetClass(), backstory.GetBackstory(), items);
            position = 2;
        }
    }

    public void ReturnToChoose()
    {
        returnButton.SetActive(false);
        buyPanel.SetActive(false);
        standartPanel.SetActive(false);
        choosePanel.SetActive(true);
        position = 0;
    }

    public void ReturnToChooseWithCheck()
    {
        if (position == 1) return;
        returnButton.SetActive(false);
        buyPanel.SetActive(false);
        standartPanel.SetActive(false);
        choosePanel.SetActive(true);
        position = 0;
    }

    public List<(int, Item)> GetItems()
    {
        switch (position)
        {
            case 1:
                return buyPanel.GetComponent<BuyInventoryPanel>().GetItems();
            case 2:
                return standartPanel.GetComponentInChildren<StandartInvetoryPanel>().GetItems();
        }
        return null;
    }

    public Item[] GetItemsList()
    {
        return items;
    }

    public bool isStandart()
    {
        if (position == 1) return false;
        return true;
    }

    public int[] GetMoney()
    {
        switch (position)
        {
            case 1:
                return buyPanel.GetComponent<BuyInventoryPanel>().GetMoney();
            case 2:
                return backstory.GetBackstory().GetMoney();
        }
        return null;
    }
}
