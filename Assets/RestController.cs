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

    public void ShortRest()
    {
        popout.SetPopout(new List<GameObject> { restore.gameObject });
    }

    public void LongRest()
    {
        popout.SetPopout(new List<GameObject> { preparePanel.gameObject });
        health.ResetHealth();
        spell.ResetSpellCells();
    }


}
