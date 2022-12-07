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
    [SerializeField] Text changeText;
    List<(int, HashSet<int>)> backup;
    int leftCount = 0;
    int changeLeft = 0;
    public bool changeable = false;
    bool block = false;
    private void Start()
    {
        backup = new List<(int, HashSet<int>)>(PresavedLists.spellKnew);
    }

    public void SetSpells(int classId, int count, int level)
    {
        ChosenSpells(classId, count, level);
    }

    public void SetSpells(int classId, List<Spell> listKnew, int changeCount)
    {
        ChangeSpells(classId, listKnew, changeCount);
    }

    void ChosenSpells(int classId, int count, int level)
    {
        List<Spell> list = new List<Spell>(LoadSpellManager.GetSpells());
        if (list == null) return;
        int levelCul = 0;
        if (level == -1)
        {
            List<(int, PlayersClass)> listBuf = CharacterData.GetClasses();

            for (int i = 0; i < listBuf.Count; i++)
                if (listBuf[i].Item2.magic == 1)
                {
                    listBuf.Add((listBuf[i].Item1 + 1, listBuf[i].Item2));
                    listBuf.Remove(listBuf[i]);
                }
            levelCul = Utilities.GetMaxSpellLevel(listBuf);
        }
        if (classId == -1)
            list = list.FindAll(g => (g.level <= levelCul));
        else
        {
            if (level != -1)
                list = list.FindAll(g => (g.level == level) && (g.classes.Contains(classId)));
            else
                list = list.FindAll(g => (g.level <= levelCul) && (g.level > 0) && (g.classes.Contains(classId)));
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
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, classId); });
            }
        }
        leftCount = count;
        leftText.text = leftCount.ToString();
    }

    void ChangeSpells(int classId, List<Spell> listKnew, int changeCount)
    {
        changeLeft = changeCount;
        changeText.text = changeCount.ToString();
        changeable = true;
        foreach (Spell x in listKnew)
        {
            SpellBody newSpell = Instantiate(spellBody, chosen.transform);
            newSpell.SetSpell(x);
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(delegate { Blocker(newSpell); ChangeSection(newSpell, classId); });
            }
        }
    }

    void Blocker(SpellBody spellBody)
    {
        if (spellBody.transform.parent == chosen.transform)
        {
            if (changeLeft <= 0)
            {
                block = true;
                return;
            }
            changeLeft--;
        }
        else
        {
            if (leftCount <= 0) return;
            changeLeft++;
        }
        changeText.text = changeLeft.ToString();
    }
    void ChangeSection(SpellBody spellBody, int id)
    {
        if (block)
        {
            block = false;
            return;
        }
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

    private void OnDestroy()
    {
        PresavedLists.spellKnew = backup;
    }
}
