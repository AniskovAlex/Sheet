using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdderItemsToInventory : MonoBehaviour, IAdder
{
    [SerializeField] GameObject itemsContainer;
    [SerializeField] GameObject itemObject;

    private void Start()
    {
    }

    public void updatePanel()
    {
        itemsContainer.GetComponent<ContentSizer>().HieghtSizeInit();
    }

    public void AddItem(Item addItem, int addAmount)
    {
        ItemBox[] items = itemsContainer.GetComponentsInChildren<ItemBox>();
        int itemsCount = items.Length;
        foreach (ItemBox x in items)
        {
            if (x.GetItem().id == addItem.id && x.GetItem().label == addItem.label)
            {
                CharacterData.EditItemAmountAdd(addItem.label, addAmount);
                return;
            }
            /*if (x.GetItem().id > 0 && x.GetItem().id == addItem.id)
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
            }*/
        }

        /*if (DataSaverAndLoader.SaveNewItem(addItem, itemsCount, addAmount))
        {*/
        AddNewItem(addItem, addAmount);
        //}

    }

    public void AddNewItem(Item item, int amount)
    {
        item.amount = amount;
        CharacterData.SetItemSilent(item);
        CharacterData.SaveCharacter();
        ItemBox newItemBox = Instantiate(itemObject, itemsContainer.transform).GetComponent<ItemBox>();
        newItemBox.SetItem(item, amount, true);
    }
}
