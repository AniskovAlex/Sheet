using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectretsBookknow : MonoBehaviour
{
    [SerializeField] SpellBody spellBody;
    [SerializeField] GameObject choose;
    [SerializeField] GameObject chosen;
    [SerializeField] Text head;
    List<Spell> spellKnew = new List<Spell>();
    private void Start()
    {

        head.text = "Ритуальная книга";
        bool flag = false;

        foreach ((int, List<Spell>) x in SpellController.spellKnew)
        {
            if (x.Item1 == -3)
            {
                flag = true;
            }
        }
        if (!flag)
            SpellController.spellKnew.Add((-3, new List<Spell>()));

        List<Spell> list = new List<Spell>(LoadSpellManager.GetSpells());
        if (list == null) return;

        list = list.FindAll(g => g.level <= (CharacterData.GetLevel() + 1) / 2 && g.level > 0 && g.ritual);
        foreach ((int, List<Spell>) x in SpellController.spellKnew)
            if (x.Item1 == -3)
            {
                list = list.Except(x.Item2).ToList();
                spellKnew = x.Item2;
            }
        int ID = 0;
        ID = -3;
        foreach (Spell x in list)
        {
            if (x.level == 0) continue;
            SpellBody newSpell = Instantiate(spellBody, choose.transform);
            newSpell.SetSpell(x);
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, ID); });
            }
        }
        if (spellKnew != null)
        {
            StartCoroutine(InstSpellsKnewAsync(spellKnew));
        }
    }

    IEnumerator InstSpellsKnewAsync(List<Spell> knewList)
    {
        foreach (Spell x in knewList)
        {
            if (x.level == 0) continue;
            SpellBody newSpell = Instantiate(spellBody, chosen.transform);
            newSpell.SetSpell(x);
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                {
                    button.GetComponentInChildren<Text>().text = "-";
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, -3); });
                }
            }
        }
        yield return null;
    }

    void ChangeSection(SpellBody spellBody, int id)
    {
        Amount buf = spellBody.GetComponentInChildren<Amount>();
        if (buf == null) return;
        Button button = buf.GetComponent<Button>();
        if (button == null) return;

        if (spellBody.transform.parent == chosen.transform)
        {
            if (button != null)
                button.GetComponentInChildren<Text>().text = "+";
            spellBody.transform.parent = choose.transform;
            int i = 0;
            foreach ((int, List<Spell>) x in SpellController.spellKnew)
            {
                if (x.Item1 == id)
                {
                    spellKnew.Remove(spellBody.GetSpell());
                }
                i++;
            }
        }
        else
        {
            if (button != null)
                button.GetComponentInChildren<Text>().text = "-";
            spellBody.transform.parent = chosen.transform;
            int i = 0;
            foreach ((int, List<Spell>) x in SpellController.spellKnew)
            {
                if (x.Item1 == id)
                    spellKnew.Add(spellBody.GetSpell());
                i++;
            }
        }
        spellBody.transform.SetAsLastSibling();
        Resize();
    }

    void Resize()
    {
        Opener opener;
        opener = choose.transform.parent.parent.GetComponentInChildren<Opener>();
        if (opener != null)
        {
            opener.HieghtSizeInit();
        }
        opener = chosen.transform.parent.parent.GetComponentInChildren<Opener>();
        if (opener != null)
        {
            opener.HieghtSizeInit();
        }

        opener = GetComponentInChildren<Opener>();
        opener.HieghtSizeInit();
        Transform obj = this.transform;
        for (obj = transform.parent; obj != null; obj = obj.parent)
        {
            ContentSizer contentSizer;
            if (obj.TryGetComponent<ContentSizer>(out contentSizer))
            {
                contentSizer.HieghtSizeInit();
            }
            Opener opener1;
            if (obj.parent != null && obj.parent.GetChild(0).TryGetComponent<Opener>(out opener1))
            {
                opener1.HieghtSizeInit();
            }
        }
    }

    private void OnDestroy()
    {
        HashSet<int> buf = new HashSet<int>();
        foreach (Spell x in spellKnew)
            buf.Add(x.id);
        DataSaverAndLoader.SaveSpellKnewOverride(new List<(int, HashSet<int>)>() { (-3, buf) });
        SpellController.ReloadSpells();
    }
}
