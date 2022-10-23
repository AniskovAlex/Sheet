using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAdder 
{
    public void AddItem(Item addItem, int addAmount);
    public void AddNewItem(Item item, int amount);
}
