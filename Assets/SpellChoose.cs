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
    public bool mult = false;
    public bool changeable = false;
    public bool multAdd = false;
    public bool blocked = false;
    bool block = false;
    int _level = 0;
    private void Start()
    {
        backup = new List<(int, HashSet<int>)>(PresavedLists.spellKnew);
        changeText.gameObject.SetActive(false);
    }

    public void SetSpells(int spellClassId, int count, int level, int classId)
    {
        _level = level;
        ChosenSpells(spellClassId, count, level, classId);
    }

    public void SetSpells(int classId, List<Spell> listKnew, int changeCount)
    {
        ChangeSpells(classId, listKnew, changeCount);
    }

    void ChosenSpells(int spellClassId, int count, int level, int classId)
    {
        List<Spell> list = new List<Spell>(LoadSpellManager.GetSpells());
        if (list == null) return;
        int levelCul = 0;

        List<(int, PlayersClass)> listBuf = new List<(int, PlayersClass)>(CharacterData.GetClasses());
        bool flag = false;
        for (int i = 0; i < listBuf.Count; i++)
            if (listBuf[i].Item2.id == classId)
            {
                if (classId == 2 || classId == 10)
                    listBuf[i].Item2.SetMagic(3);
                listBuf.Add((listBuf[i].Item1 + 1, listBuf[i].Item2));
                listBuf.Remove(listBuf[i]);
                flag = true;
            }
        if (!flag)
            listBuf.Add((1, PlayersClass.GetPlayersClassByID(classId)));
        levelCul = Utilities.GetMaxSpellLevel(listBuf);

        if (spellClassId == 7)
        {
            foreach ((int, PlayersClass) x in listBuf)
                if (x.Item2.magic == -1)
                {
                    levelCul = (Mathf.Clamp(x.Item1, 1, 10) + 1) / 2;
                    break;
                }
        }
        switch (spellClassId)
        {
            case -1:
                list = list.FindAll(g => (g.level <= levelCul));
                break;
            case -2:
                list = list.FindAll(g => (g.level == levelCul && g.spellType == Spell.Type.Necromancy));
                break;
            default:
                switch (level)
                {

                    case -1:
                        if (classId == 7)
                        {
                            List<(int, PlayersClass)> listBuf1 = CharacterData.GetClasses();
                            int classLevel = 0;
                            HashSet<int> bufSet = new HashSet<int>();
                            foreach ((int, PlayersClass) x in listBuf1)
                            {
                                if (x.Item2.id != spellClassId) continue;
                                classLevel = (Mathf.Clamp(x.Item1 + 1, 1, 10) + 1) / 2;
                                PlayerSubClass bufSub = x.Item2.GetSubClass();
                                if (bufSub == null) break;
                                bufSet = bufSub.GetSpells();
                            }
                            list = list.FindAll(g => (g.level <= classLevel) && (g.level > 0) && ((g.classes.Contains(spellClassId)) || (bufSet != null && bufSet.Contains(g.id))));
                        }
                        else
                            list = list.FindAll(g => (g.level <= levelCul) && (g.level > 0) && (g.classes.Contains(spellClassId)));
                        break;
                    case -2:
                        list = list.FindAll(g => (g.level <= levelCul && g.level > 0 && g.spellType == Spell.Type.Abjuration) && (g.classes.Contains(spellClassId)));
                        break;
                    case -3:
                        list = list.FindAll(g => (g.level <= levelCul && g.level > 0 && g.spellType == Spell.Type.Evocation) && (g.classes.Contains(spellClassId)));
                        break;
                    case -4:
                        list = list.FindAll(g => (g.level <= levelCul && g.level > 0 && (g.spellType == Spell.Type.Abjuration || g.spellType == Spell.Type.Evocation)) && (g.classes.Contains(spellClassId)));
                        break;
                    case -5:
                        HashSet<int> buf = null;
                        foreach ((int, HashSet<int>) x in DataSaverAndLoader.LoadSpellKnew())
                            if (x.Item1 == 3)
                            {
                                buf = x.Item2;
                                break;
                            }
                        if (buf != null)
                            list = list.FindAll(g => g.level == 3 && buf.Contains(g.id));
                        break;
                    case -6:
                        list = list.FindAll(g => (g.level <= levelCul && g.level > 0 && g.spellType == Spell.Type.Illusion) && (g.classes.Contains(spellClassId)));
                        break;
                    case -7:
                        list = list.FindAll(g => (g.level <= levelCul && g.level > 0 && g.spellType == Spell.Type.Enchantment) && (g.classes.Contains(spellClassId)));
                        break;
                    case -8:
                        list = list.FindAll(g => (g.level <= levelCul && g.level > 0 && (g.spellType == Spell.Type.Illusion || g.spellType == Spell.Type.Enchantment)) && (g.classes.Contains(spellClassId)));
                        break;
                    case -9:
                        list = list.FindAll(g => (g.level == 1 && g.ritual == true && (g.classes.Contains(spellClassId))));
                        break;
                    default:
                        list = list.FindAll(g => (g.level == level) && (g.classes.Contains(spellClassId)));
                        break;
                }
                break;
        }
        if (level != -5)
            for (int i = 0; i < list.Count; i++)
            {
                bool flag1 = false;
                foreach ((int, HashSet<int>) x in DataSaverAndLoader.LoadSpellKnew())
                {
                    if (x.Item2.Contains(list[i].id))
                    {
                        list.RemoveAt(i);
                        i--;
                        flag1 = true;
                        break;
                    }
                }
                if (flag1) continue;
                foreach ((int, HashSet<int>) x in PresavedLists.spellKnew) 
                {
                    if (x.Item2.Contains(list[i].id))
                    {
                        list.RemoveAt(i);
                        i--;
                        break;
                    }
                }
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
                    button.onClick.AddListener(delegate
                    {
                        ChangeSection(newSpell, classId);
                    });
            }
        }
        leftCount = count;
        leftText.text = leftCount.ToString();
    }

    void ChangeSpells(int classId, List<Spell> listKnew, int changeCount)
    {
        changeLeft = changeCount;
        if (changeLeft > 0)
            changeText.gameObject.SetActive(true);
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

    public void RechangeLeft(int change)
    {
        changeLeft = change;
        changeText.text = changeLeft.ToString();
        if (changeLeft > 0)
            changeText.gameObject.SetActive(true);
        else
            changeText.gameObject.SetActive(false);
    }

    public void SetCount(int add)
    {
        leftCount = Mathf.Clamp(leftCount + add, 0, 999);
        leftText.text = leftCount.ToString();
    }

    void Blocker(SpellBody spellBody)
    {
        if (spellBody.transform.parent == chosen.transform)
        {
            if (changeLeft <= 0)
            {
                block = true;
                changeText.gameObject.SetActive(false);
                return;
            }
            changeLeft--;
            if (mult)
            {
                SpellChoose[] chosenSpells = transform.parent.parent.parent.GetComponentsInChildren<SpellChoose>();
                foreach (SpellChoose x in chosenSpells)
                {
                    if (x.GetLevel() != 0)
                        x.RechangeLeft(changeLeft);
                }
            }
        }
        else
        {
            if (leftCount <= 0) return;
            changeLeft++;
        }
        if(changeLeft>0)
            changeText.gameObject.SetActive(true);
        else
            changeText.gameObject.SetActive(false);
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
            if (_level != -5)
            {
                bool flag = false;
                int i = 0;
                foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                {
                    if (x.Item1 == id)
                    {
                        PresavedLists.spellKnew[i].Item2.Remove(spellBody.GetSpell().id);
                        flag = true;
                        break;
                    }
                    i++;
                }
                if (!flag)
                    DataSaverAndLoader.DeleteSpellKnew(id, spellBody.GetSpell().id);
            }
            else
            {
                PresavedLists.spellMaster.Remove(spellBody.GetSpell().id);
            }
            leftCount++;
        }
        else
        {
            if (leftCount == 0) return;
            if (button != null)
                button.GetComponentInChildren<Text>().text = "-";
            spellBody.transform.parent = chosen.transform;
            if (_level != -5)
            {
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
            }
            else
            {
                PresavedLists.spellMaster.Add(spellBody.GetSpell().id);
            }
            leftCount--;
            if (mult)
            {
                SpellChoose[] chosenSpells = transform.parent.GetComponentsInChildren<SpellChoose>();
                foreach (SpellChoose x in chosenSpells)
                    if (x.multAdd && x != this)
                        x.SetCount(-1);
            }
        }
        spellBody.transform.SetAsLastSibling();
        leftText.text = leftCount.ToString();
    }

    public bool isSetted()
    {
        if (leftCount > 0)
            return true;
        else return false;
    }

    public int GetLevel()
    {
        return _level;
    }

    public SpellBody[] GetSpellsChosen()
    {
        return chosen.GetComponentsInChildren<SpellBody>();
    }

    private void OnDestroy()
    {
        PresavedLists.spellKnew = backup;
        PresavedLists.spellMaster = new HashSet<int>();
    }
}
