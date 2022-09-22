using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdderItemsToInventory : MonoBehaviour
{
    [SerializeField] GameObject itemsContainer;
    [SerializeField] GameObject itemObject;
    public void AddItem(Item addItem, int addAmount)
    {
        ItemBox[] items = itemsContainer.GetComponentsInChildren<ItemBox>();
        int itemsCount = items.Length;
        foreach (ItemBox x in items)
        {
            if (x.GetItem().id > 0 && x.GetItem().id == addItem.id)
            {
                x.AddAmount(addAmount);
                DataSaverAndLoader.SaveAmountItem(x.GetItem().id, x.GetAmount());
                return;
            }
            else
            {
                if (x.GetItem().id == -1 && x.GetItem().label == addItem.label)
                {
                    x.AddAmount(addAmount);
                    DataSaverAndLoader.SaveAmountItem(x.GetItem().label, x.GetAmount());
                    return;
                }
            }
        }

        if (DataSaverAndLoader.SaveNewItem(addItem, itemsCount, addAmount))
        {
            AddNewItem(addItem, addAmount);
        }

    }

    void AddNewItem(Item item, int amount)
    {
        ItemBox newItemBox = Instantiate(itemObject, itemsContainer.transform).GetComponent<ItemBox>();
        newItemBox.SetItem(item, amount);
    }
}
