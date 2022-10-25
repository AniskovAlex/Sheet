using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyInventoryPanel : MonoBehaviour
{
    [SerializeField] AdderItemToPrelist adder;
    public void SetAdder(PlayersClass playersClass)
    {
        adder.SetCoins(playersClass.GetMoney());
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
