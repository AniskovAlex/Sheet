using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemButton : MonoBehaviour
{
    [SerializeField] ItemsRedactor itemsRedactor;
    [SerializeField] GameObject adder;
    [SerializeField] ContentSizer content;
    IAdder adderItemsToInventory = null;
    // Start is called before the first frame update

    private void Start()
    {
        adderItemsToInventory = adder.GetComponent<IAdder>();
    }

    public void AddItem()
    {
        (Item, int) result = itemsRedactor.Itempackaging();
        if (result.Item1 != null)
            adderItemsToInventory.AddItem(result.Item1, result.Item2);
        content.HieghtSizeInit();
    }
}
