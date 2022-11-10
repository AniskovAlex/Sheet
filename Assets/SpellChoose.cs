using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellChoose : MonoBehaviour
{
    [SerializeField] SpellBody spellBody;
    [SerializeField] GameObject choose;
    [SerializeField] GameObject chosen;
    [SerializeField] Text leftText;
    int leftCount = 0;
    public void SetSpells(int classId, int count, int level)
    {
        List<Spell> list = new List<Spell>(LoadSpellManager.GetSpells());
        if (list == null) return;
        list = list.FindAll(g => (g.level == level) && (g.classes.Contains(classId)));
        foreach (Spell x in list)
        {
            SpellBody newSpell = Instantiate(spellBody, choose.transform);
            newSpell.SetSpell(x);
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, classId); });
            }
        }
        leftCount = count;
        leftText.text = leftCount.ToString();
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
            foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
            {
                if (x.Item1 == id)
                {
                    PresavedLists.spellKnew[i].Item2.Remove(spellBody.GetSpell().id);
                }
                i++;
            }
            leftCount++;
        }
        else
        {
            if (leftCount == 0) return;
            if (button != null)
                button.GetComponentInChildren<Text>().text = "-";
            spellBody.transform.parent = chosen.transform;
            int i = 0;
            bool flag = false;
            foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
            {
                if (x.Item1 == id)
                {
                    PresavedLists.spellKnew[i].Item2.Add(spellBody.GetSpell().id);
                    flag = true;
                }
                i++;
            }
            if (!flag)
                PresavedLists.spellKnew.Add((id, new HashSet<int>() { spellBody.GetSpell().id }));
            leftCount--;
        }
        spellBody.transform.SetAsLastSibling();
        leftText.text = leftCount.ToString();
    }
}
