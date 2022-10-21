using System.Collections;
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
    }

    public void ShowBuy()
    {
        if (classes.GetClass() != null && backstory.GetBackstory() != null)
        {
            returnButton.SetActive(true);
            buyPanel.SetActive(true);
            choosePanel.SetActive(false);
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
            standartPanel.GetComponent<StandartInvetoryPanel>().SetInventoryChoice(classes.GetClass(), backstory.GetBackstory(), items);
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
}
