using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestSpellCellRestoreRow : MonoBehaviour
{
    [SerializeField] ConsumablePanel consumable;
    [SerializeField] Text lvl;
    public Action<int, int> update;
    int _level;
    int _current;

    public void SetRow(int level, int current, int max)
    {
        _level = level;
        _current = current;
        lvl.text = level + "-é Óð.";
        consumable.SpawnToggles(max, current);
        consumable.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        consumable.GetComponentInChildren<Button>().onClick.AddListener(delegate { consumable.ResetToggels(1); });
        consumable.update += UpdateCell;

    }

    void UpdateCell(int count)
    {
        update(_level, count - _current);
        _current = count;
    }

    public void UpdateRow(int left)
    {
        if (left < _level)
            GetComponentInChildren<Button>().interactable = false;
    }
}
