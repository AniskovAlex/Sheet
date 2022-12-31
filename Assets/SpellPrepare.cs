using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellPrepare : MonoBehaviour
{
    [SerializeField] SpellBody spellBody;
    [SerializeField] GameObject choose;
    [SerializeField] GameObject chosen;
    [SerializeField] Text head;
    [SerializeField] Text leftText;
    int leftCount = 0;
    public void SetSpells(PlayersClass playerClass, int level)
    {
        head.text = playerClass.name;
        List<Spell> list = new List<Spell>(LoadSpellManager.GetSpells());
        if (list == null) return;
        if (playerClass.id != 3)
            list = list.FindAll(g => (g.level <= Utilities.GetMaxSpellLevel(CharacterData.GetClasses())) && (g.classes.Contains(playerClass.id)) && g.level > 0);
        else
            foreach ((int, List<Spell>) x in SpellController.spellKnew)
            {
                if (x.Item1 == 3)
                {
                    list = x.Item2;
                    break;
                }
            }
        foreach ((int, List<Spell>) x in SpellController.spellKnew)
        {
            if (x.Item1 != 3)
            {
                list = list.Except(x.Item2).ToList();
            }
        }
        SpellController.spellPrepared.ForEach(g => list = list.Except(g.Item2).ToList());
        
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
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, playerClass.id); });
            }
        }
        
        List<Spell> preparedList = SpellController.spellPrepared.Find(g => g.Item1 == playerClass.id).Item2;
        int preparedCount = 0;
        if (preparedList != null)
        {
            foreach (Spell x in preparedList)
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
                        button.onClick.AddListener(delegate { ChangeSection(newSpell, playerClass.id); });
                    }
                }
            }
            preparedCount = preparedList.Count;
        }
        bool flag = false;
        foreach ((int, List<Spell>) x in SpellController.spellPrepared)
            if (x.Item1 == playerClass.id)
                flag = true;
        if (!flag)
            SpellController.spellPrepared.Add((playerClass.id, new List<Spell>()));
        int prepareFromLevel = level / playerClass.magic;
        leftCount = Mathf.Clamp(prepareFromLevel + CharacterData.GetModifier(playerClass.mainState), 1, 100) - preparedCount;
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
            foreach ((int, List<Spell>) x in SpellController.spellPrepared)
            {
                if (x.Item1 == id)
                {
                    SpellController.spellPrepared[i].Item2.Remove(spellBody.GetSpell());
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
            foreach ((int, List<Spell>) x in SpellController.spellPrepared)
            {
                if (x.Item1 == id)
                {
                    SpellController.spellPrepared[i].Item2.Add(spellBody.GetSpell());
                }
                i++;
            }
            leftCount--;
        }
        spellBody.transform.SetAsLastSibling();
        leftText.text = leftCount.ToString();
    }
}
