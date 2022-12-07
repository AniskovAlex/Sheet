using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellBookKnow : MonoBehaviour
{
    [SerializeField] SpellBody spellBody;
    [SerializeField] GameObject choose;
    [SerializeField] GameObject chosen;
    [SerializeField] Text head;
    private void Start()
    {
        head.text = "����� ����������";
        bool flag = false;
        foreach ((int, List<Spell>) x in SpellController.spellKnew)
        {
            if (x.Item1 == 3)
            {
                flag = true;
            }
        }
        if (!flag)
            SpellController.spellKnew.Add((3, new List<Spell>()));
        List<Spell> list = new List<Spell>(LoadSpellManager.GetSpells());
        if (list == null) return;
        list = list.FindAll(g => (g.level <= Utilities.GetMaxSpellLevel(CharacterData.GetClasses())) && (g.classes.Contains(3)) && g.level > 0);
        List<Spell> knewList = new List<Spell>();
        foreach ((int, List<Spell>) x in SpellController.spellKnew)
            if (x.Item1 == 3)
            {
                list = list.Except(x.Item2).ToList();
                knewList = x.Item2;
            }
        foreach (Spell x in list)
        {
            SpellBody newSpell = Instantiate(spellBody, choose.transform);
            newSpell.SetSpell(x);
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, 3); });
            }
        }
        if (knewList != null)
        {
            foreach (Spell x in knewList)
            {
                SpellBody newSpell = Instantiate(spellBody, chosen.transform);
                newSpell.SetSpell(x);
                Amount buf = newSpell.GetComponentInChildren<Amount>();
                if (buf != null)
                {
                    Button button = buf.GetComponent<Button>();
                    if (button != null)
                    {
                        button.GetComponentInChildren<Text>().text = "-";
                        button.onClick.AddListener(delegate { ChangeSection(newSpell, 3); });
                    }
                }
            }
        }
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
                    SpellController.spellKnew[i].Item2.Remove(spellBody.GetSpell());
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
                SpellController.spellKnew[i].Item2.Add(spellBody.GetSpell());
                i++;
            }
        }
        spellBody.transform.SetAsLastSibling();
    }

    private void OnDestroy()
    {
        DataSaverAndLoader.SaveAddSpellKnew(SpellController.GetSpellsId(SpellController.spellKnew));
        SpellController.ReloadSpells();
    }
}
