using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestController : MonoBehaviour
{
    [SerializeField] SpellPreparePopup preparePanel;
    [SerializeField] RestoreHealthPanel restore;
    [SerializeField] PopoutController popout;
    [SerializeField] HealthController health;
    [SerializeField] SpellController spell;
    [SerializeField] GameObject person;
    List<(int, ConsumablePanel)> shortRestRestore = new List<(int, ConsumablePanel)>();

    private void Start()
    {
        if (GlobalStatus.needRest)
        {
            health.ResetHealth();
            spell.ResetSpellCells();
            ConsumablePanel[] consumables = person.GetComponentsInChildren<ConsumablePanel>(true);
            foreach (ConsumablePanel x in consumables)
                x.Reset();
        }
    }

    public void AddShortRest(int count, ConsumablePanel consumablePanel)
    {
        shortRestRestore.Add((count, consumablePanel));
    }

    public void ShortRest()
    {
        foreach ((int, ConsumablePanel) x in shortRestRestore)
            x.Item2.Reset(x.Item1);
        List<GameObject> list = new List<GameObject> { restore.gameObject };
        popout.SetPopout(list);
    }

    public void LongRest()
    {
        popout.SetPopout(new List<GameObject> { preparePanel.gameObject });
        health.ResetHealth();
        spell.ResetSpellCells();
        ConsumablePanel[] consumables = person.GetComponentsInChildren<ConsumablePanel>(true);
        foreach (ConsumablePanel x in consumables)
            x.Reset();
    }


}
