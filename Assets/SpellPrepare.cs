using System.Collections;
using System.Linq;
using System;
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
    [SerializeField] SheetControler sheetsController;
    [SerializeField] int spellsPerSheet;
    SheetControler sheetControlerChoose;
    List<(int, string, List<Spell>)> spellSheetsChoose;
    int leftCount = 0;
    int id;
    public void SetSpells(PlayersClass playerClass, int level)
    {
        head.text = playerClass.name;
        id = playerClass.id;
        List<Spell> list = new List<Spell>(LoadSpellManager.GetSpells());
        if (list == null) return;
        if (playerClass.id != 3)
            list = list.FindAll(g => (g.level <= Utilities.GetMaxSpellLevel(CharacterData.GetClasses())) && (g.classes.Contains(playerClass.id)) && g.level > 0);
        else
        {
            bool flagWiz = false;
            foreach ((int, List<Spell>) x in SpellController.spellKnew)
            {
                if (x.Item1 == 3)
                {
                    list = x.Item2;
                    flagWiz = true;
                    break;
                }
            }
            if (!flagWiz) return;
        }
        foreach ((int, List<Spell>) x in SpellController.spellKnew)
        {
            if (x.Item1 != 3)
            {
                list = list.Except(x.Item2).ToList();
            }
        }
        SpellController.spellPrepared.ForEach(g => list = list.Except(g.Item2).ToList());

        if (list.Count > spellsPerSheet)
        {
            spellSheetsChoose = Utilities.SplitSpellList(list, spellsPerSheet);
            if (spellSheetsChoose.Count > 0)
                list = spellSheetsChoose[0].Item3;
            sheetControlerChoose = Instantiate(sheetsController, choose.transform);
            sheetControlerChoose.changeSpells += ChangeSpells;
            sheetControlerChoose.SetButtons(spellSheetsChoose, true, choose);
        }
        else
            ChangeSpells(list, true, choose);

        List<Spell> preparedList = SpellController.spellPrepared.Find(g => g.Item1 == playerClass.id).Item2;
        int preparedCount = 0;
        if (preparedList != null)
        {
            ChangeSpells(preparedList, false, chosen);
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

    void ChangeSpells(List<Spell> spells, bool add, GameObject panel)
    {
        SpellBody[] spellChooses = panel.GetComponentsInChildren<SpellBody>();
        foreach (SpellBody x in spellChooses)
            DestroyImmediate(x.gameObject);
        foreach (Spell x in spells)
        {
            if (x.level == 0) continue;
            SpellBody newSpell = Instantiate(spellBody, panel.transform);
            newSpell.SetSpell(x);
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                {
                    if (!add)
                        button.GetComponentInChildren<Text>().text = "-";
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, id); });
                }
            }
        }
        Resize();
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
            if (sheetControlerChoose != null)
            {
                spellSheetsChoose[0].Item3.Add(spellBody.GetSpell());
                spellSheetsChoose = Utilities.SortSpellList(spellSheetsChoose, spellsPerSheet);
                sheetControlerChoose.SetButtons(spellSheetsChoose, true, choose);
            }
            else
                spellBody.transform.SetAsLastSibling();
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
            if (spellSheetsChoose != null)
                foreach ((int, string, List<Spell>) y in spellSheetsChoose)
                y.Item3.Remove(spellBody.GetSpell());
            spellBody.transform.SetAsLastSibling();
        }
        leftText.text = leftCount.ToString();
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
}
