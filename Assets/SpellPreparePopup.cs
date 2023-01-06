using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPreparePopup : MonoBehaviour
{
    [SerializeField] SpellPrepare spellPrepare;
    List<SpellPrepare> objList = new List<SpellPrepare>();

    // Start is called before the first frame update
    void Start()
    {
        List<(int, PlayersClass)> list = CharacterData.GetClasses();
        foreach ((int, PlayersClass) x in list)
            if (x.Item2.magic > 0)
            {
                if (x.Item2.id == 0 || x.Item2.id == 2 || x.Item2.id == 10 || x.Item2.id == 11) continue;
                SpellPrepare buf = Instantiate(spellPrepare, transform);
                objList.Add(buf);

                buf.SetSpells(x.Item2, x.Item1);

            }
    }

    private void OnDestroy()
    {
        DataSaverAndLoader.SaveSpellPrepared(SpellController.GetSpellsId(SpellController.spellPrepared));
        SpellController.ReloadSpells();
    }
}
