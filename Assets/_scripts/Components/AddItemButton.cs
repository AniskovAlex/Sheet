using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemButton : MonoBehaviour
{
    ItemsRedactor itemsRedactor;
    AdderItemsToInventory adderItemsToInventory;
    // Start is called before the first frame update
    void Start()
    {
        itemsRedactor = gameObject.GetComponentInParent<ItemsRedactor>();
        adderItemsToInventory = gameObject.GetComponentInParent<AdderItemsToInventory>();
    }

    public void AddItem()
    {
        (Item, int) result = itemsRedactor.Itempackaging();
        if (result.Item1 != null)
            adderItemsToInventory.AddItem(result.Item1, result.Item2);
    }
}
