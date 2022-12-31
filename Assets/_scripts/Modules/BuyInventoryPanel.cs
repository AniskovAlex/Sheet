using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyInventoryPanel : MonoBehaviour
{
    [SerializeField] AdderItemToPrelist adder;
    public void SetAdder(PlayersClass playersClass)
    {
        adder.SetCoins(playersClass.GetMoney()*100);
    }

    public List<(int, Item)> GetItems()
    {
        return adder.GetItems();
    }

    public int[] GetMoney()
    {
        return adder.GetCoins();
    }
}
