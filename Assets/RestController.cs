using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestController : MonoBehaviour
{
    [SerializeField] SpellPreparePopup preparePanel;
    [SerializeField] RestoreHealthPanel restore;
    [SerializeField] RestSpellCellRestore spellCellRestore;
    [SerializeField] PopoutController popout;
    [SerializeField] HealthController health;
    [SerializeField] SpellController spell;
    [SerializeField] GameObject person;
    List<(int, ConsumablePanel)> shortRestRestore = new List<(int, ConsumablePanel)>();

    private void Start()
    {
        if (GlobalStatus.needRest)
        {
            GlobalStatus.needRest = false;
            health.ResetHealth();
            spell.ResetSpellCells();
            ConsumablePanel[] consumables = person.GetComponentsInChildren<ConsumablePanel>(true);
            foreach (ConsumablePanel x in consumables)
                x.ResetToggels();
        }
    }

    public void AddShortRest(int count, ConsumablePanel consumablePanel)
    {
        shortRestRestore.Add((count, consumablePanel));
    }

    public void ShortRest()
    {
        foreach ((int, ConsumablePanel) x in shortRestRestore)
            x.Item2.ResetToggels(x.Item1);
        List<GameObject> list = new List<GameObject> { restore.gameObject };
        foreach ((int, PlayersClass) x in CharacterData.GetClasses())
        {
            if (x.Item2.id == 3 || (x.Item2.id == 4 && x.Item1 >= 2 && x.Item2.GetSubClass() != null && x.Item2.GetSubClass().id == 1))
                list.Add(spellCellRestore.gameObject);
            if (x.Item2.id == 7)
                spell.ResetWarSpellCells();
        }
        popout.SetPopout(list);
    }

    public void LongRest()
    {
        popout.SetPopout(new List<GameObject> { preparePanel.gameObject });
        health.ResetHealth();
        spell.ResetSpellCells();
        ConsumablePanel[] consumables = person.GetComponentsInChildren<ConsumablePanel>(true);
        foreach (ConsumablePanel x in consumables)
            x.ResetToggels();
    }


}
