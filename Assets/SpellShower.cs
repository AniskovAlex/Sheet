using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellShower : MonoBehaviour
{
    [SerializeField] ConsumablePanel consumable;
    [SerializeField] int level;
    public void SetCells(int amount,int currentAmount)
    {
        ConsumablePanel buf = Instantiate(consumable, GetComponentInChildren<Opener>().transform);
        buf.SpawnToggles(amount, currentAmount);
        buf.update += UpdateConsum;
    }

    void UpdateConsum(int amount)
    {
        DataSaverAndLoader.SaveCellAmount(level, amount);
    }
}
