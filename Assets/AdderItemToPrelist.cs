using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdderItemToPrelist : MonoBehaviour, IAdder
{
    [SerializeField] GameObject itemsContainer;
    [SerializeField] GameObject itemObject;
    [SerializeField] Text coinsText;
    int[] coins = new int[3] { 0, 0, 0, };

    public void RemoveItem(Item item)
    {
        SetCoins(GetAbsoluteCost(coins) + GetAbsoluteCost(item));
    }

    public void SetCoins(int absolute)
    {
        coins[2] = absolute % 10;
        absolute /= 10;
        coins[1] = absolute % 10;
        absolute /= 10;
        coins[0] = absolute;
        coinsText.text = "гл: " + coins[0] + ", ял: " + coins[1] + ", лл: " + coins[2];
    }

    public void AddItem(Item addItem, int addAmount)
    {
        ItemBox[] items = itemsContainer.GetComponentsInChildren<ItemBox>();
        foreach (ItemBox x in items)
        {
            if (x.GetItem().id > 0 && x.GetItem().id == addItem.id)
            {
                if (!CheckMoney(x.GetItem(), addAmount)) return; 
                x.AddAmount(addAmount);
                return;
            }
            else
            {
                if (x.GetItem().id == -1 && x.GetItem().label == addItem.label)
                {
                    if (!CheckMoney(x.GetItem(), addAmount)) return;
                    x.AddAmount(addAmount);
                    return;
                }
            }
        }

        AddNewItem(addItem, addAmount);
    }

    int GetAbsoluteCost(Item item)
    {
        switch (item.mType)
        {
            case Item.MType.copperCoin:
                return item.cost;
            case Item.MType.silverCoin:
                return item.cost * 10;
            case Item.MType.goldCoin:
                return item.cost * 100;
        }
        return 0;
    }

    int GetAbsoluteCost(int[] coins)
    {
        int sum = 0;
        for (int i = 0; i < coins.Length; i++)
        {
            int exp = coins.Length - i - 1;
            sum += (int)Mathf.Pow(10, exp) * coins[i];
        }
        return sum;
    }

    bool CheckMoney(Item item,int amount)
    {
        int itemCost = GetAbsoluteCost(item);
        int absoluteCoins = GetAbsoluteCost(coins);
        if (itemCost * amount > absoluteCoins) return false;
        absoluteCoins -= itemCost * amount;
        SetCoins(absoluteCoins);
        return true;
    }

    public void AddNewItem(Item item, int amount)
    {
        if (!CheckMoney(item, amount)) return;
        ItemBox newItemBox = Instantiate(itemObject, itemsContainer.transform).GetComponent<ItemBox>();
        newItemBox.SetItem(item, amount, false);
    }
}
