using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    Spell[] spells;
    [SerializeField] List<GameObject> spellLevelContainerObjects;
    [SerializeField] List<SpellShower> spellLevelObjects;
    [SerializeField] SpellBody spellBody;
    [SerializeField] GameObject spellPanel;
    [SerializeField] GameObject spellBook;
    [SerializeField] GameObject spellPrepare;
    [SerializeField] Button spellPanelButton;
    public static Action ReloadSpells;
    public static List<(int, List<Spell>)> spellKnew = new List<(int, List<Spell>)>();
    public static List<(int, List<Spell>)> spellPrepared = new List<(int, List<Spell>)>();
    public static List<Spell> spellMaster = new List<Spell>();
    int[] currentCells;
    int[] maxCells;
    int warAbs = 0;
    int warLevel = 0;

    private void Start()
    {
        spellPanel.SetActive(false);
        spellPrepare.SetActive(false);
        spellPanelButton.interactable = false;
        spellBook.SetActive(false);
        foreach ((int, PlayersClass) x in CharacterData.GetClasses())
            if (x.Item2.magic != 0)
            {
                GlobalStatus.magic = true;
                spellPanel.SetActive(true);
                spellPanelButton.interactable = true;
                if (x.Item2.id == 0 || x.Item2.id == 2 || x.Item2.id == 10 || x.Item2.id == 11) continue;
                spellPrepare.SetActive(true);
                if (x.Item2.id == 3)
                {
                    spellBook.SetActive(true);
                    break;
                }
            }
        if (LoadSpellManager.LoadSpells())
            spells = LoadSpellManager.GetSpells();
        else
        {
            spells = null;
            Debug.Log("Список заклинаний не загружен!");
            return;
        }
        currentCells = DataSaverAndLoader.LoadCellsAmount();
        List<(int, HashSet<int>)> spellKnewIdList = DataSaverAndLoader.LoadSpellKnew();
        if (spellKnewIdList.Count > 0)
        {
            GlobalStatus.magic = true;
            spellPanel.SetActive(true);
            spellPanelButton.interactable = true;
        }
        List<(int, HashSet<int>)> spellPreparedIdList = DataSaverAndLoader.LoadSpellPrepared();
        HashSet<int> spellMasterIdList = DataSaverAndLoader.LoadSpellMaster();
        foreach (int x in spellMasterIdList)
        {
            foreach (Spell y in spells)
                if (y.id == x)
                {
                    spellMaster.Add(y);
                    break;
                }
        }
        spellKnew = SetSpell(spellKnewIdList);
        spellPrepared = SetSpell(spellPreparedIdList);
        InitSpells();
        ReloadSpells = RespellKnew;
        ReloadSpells += InitSpells;
        InitSpellCells();
    }

    void RespellKnew()
    {
        List<(int, HashSet<int>)> spellKnewIdList = DataSaverAndLoader.LoadSpellKnew();
        spellKnew = SetSpell(spellKnewIdList);
    }

    List<(int, List<Spell>)> SetSpell(List<(int, HashSet<int>)> list)
    {
        List<(int, List<Spell>)> spellList = new List<(int, List<Spell>)>();
        list.ForEach(g =>
        {
            List<Spell> buf = new List<Spell>();
            foreach (int x in g.Item2)
            {
                foreach (Spell y in spells)
                    if (y.id == x)
                    {
                        buf.Add(y);
                        break;
                    }
            }
            spellList.Add((g.Item1, buf));
        });
        return spellList;
    }

    public static List<(int, HashSet<int>)> GetSpellsId(List<(int, List<Spell>)> list)
    {
        List<(int, HashSet<int>)> spellList = new List<(int, HashSet<int>)>();
        list.ForEach(g =>
        {
            HashSet<int> buf = new HashSet<int>();
            foreach (Spell x in g.Item2)
            {
                buf.Add(x.id);
            }
            spellList.Add((g.Item1, buf));
        });
        return spellList;
    }

    void InitSpells()
    {
        foreach (GameObject x in spellLevelContainerObjects)
            for (int i = 0; i < x.transform.childCount; i++)
                Destroy(x.transform.GetChild(i).gameObject);
        spellKnew.ForEach(g =>
        {
            if (g.Item1 != 3)
            {
                foreach (Spell x in g.Item2)
                    if (x.level >= 0 && x.level <= 9)
                    {
                        Instantiate(spellBody, spellLevelContainerObjects[x.level].transform).SetSpell(x);
                        Debug.Log(spellLevelContainerObjects[x.level].GetComponentsInChildren<SpellBody>().Length);
                    }
            }
            else
                foreach (Spell x in g.Item2)
                    if (x.level == 0)
                        Instantiate(spellBody, spellLevelContainerObjects[x.level].transform).SetSpell(x);
        });
        spellPrepared.ForEach(g =>
        {
            foreach (Spell x in g.Item2)
                if (x.level >= 0 && x.level <= 9)
                    Instantiate(spellBody, spellLevelContainerObjects[x.level].transform).SetSpell(x);
        });
        foreach (Spell x in spellMaster)
            if (x.level == 3)
                Instantiate(spellBody, spellLevelContainerObjects[x.level].transform).SetSpell(x);
    }

    public static void SaveSpellMaster()
    {
        HashSet<int> list = new HashSet<int>();
        foreach (Spell x in spellMaster)
            list.Add(x.id);
        DataSaverAndLoader.SaveSpellMaster(list);
    }

    void InitSpellCells()
    {
        List<(int, PlayersClass)> playerClasses = CharacterData.GetClasses();
        int[] cells = new int[9];
        int levelAbs = 0;
        foreach ((int, PlayersClass) x in playerClasses)
        {
            if (x.Item2.magic > 0 && x.Item1 >= x.Item2.magic)
            {
                levelAbs += (x.Item1 + (x.Item2.magic - 1)) / (x.Item2.magic);
            }
            if (x.Item2.magic == -1)
            {
                if (x.Item1 == 1)
                    warAbs += 1;
                if (x.Item1 >= 2 && x.Item1 < 10)
                    warAbs += 2;
                if (x.Item1 >= 11 && x.Item1 < 16)
                    warAbs += 3;
                if (x.Item1 >= 17)
                    warAbs += 4;
                warLevel = (Mathf.Clamp(x.Item1, 1, 10) + 1) / 2;
                cells[warLevel - 1] += warAbs;
            }
        }
        switch (levelAbs)
        {
            case 1:
                cells[0] += 2;
                break;
            case 2:
                cells[0] += 3;
                break;
            case 3:
                cells[0] += 4;
                cells[1] += 2;
                break;
            case 4:
                cells[0] += 4;
                cells[1] += 3;
                break;
            case 5:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 2;
                break;
            case 6:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                break;
            case 7:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 1;
                break;
            case 8:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 2;
                break;
            case 9:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 1;
                break;
            case 10:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 2;
                break;
            case 11:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 2;
                cells[5] += 1;
                break;
            case 12:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 2;
                cells[5] += 1;
                break;
            case 13:
            case 14:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 2;
                cells[5] += 1;
                cells[6] += 1;
                break;
            case 15:
            case 16:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 2;
                cells[5] += 1;
                cells[6] += 1;
                cells[7] += 1;
                break;
            case 17:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 2;
                cells[5] += 1;
                cells[6] += 1;
                cells[7] += 1;
                cells[8] += 1;
                break;
            case 18:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 3;
                cells[5] += 1;
                cells[6] += 1;
                cells[7] += 1;
                cells[8] += 1;
                break;
            case 19:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 3;
                cells[5] += 2;
                cells[6] += 1;
                cells[7] += 1;
                cells[8] += 1;
                break;
            case 20:
                cells[0] += 4;
                cells[1] += 3;
                cells[2] += 3;
                cells[3] += 3;
                cells[4] += 3;
                cells[5] += 2;
                cells[6] += 2;
                cells[7] += 1;
                cells[8] += 1;
                break;
        }
        maxCells = cells;
        int cellMax = Math.Max(warLevel, Utilities.GetMaxSpellLevel(playerClasses));
        for (int i = 0; i < cells.Length; i++)
        {
            if (i + 1 <= cellMax || (spellLevelContainerObjects[i + 1].GetComponentsInChildren<SpellBody>().Length > 0))
            {
                spellLevelObjects[i + 1].SetCells(cells[i], currentCells[i]);
                int buf = i;
                spellLevelObjects[i + 1].GetComponentInChildren<ConsumablePanel>().GetComponentInChildren<Button>().onClick.AddListener(delegate { updateCurrentCell(buf); });
                if (GlobalStatus.sorcererUnit && i + 1 <= 5)
                    spellLevelObjects[i + 1].GetComponentInChildren<ConsumablePanel>().SpawnResetButton();
            }
            else
                spellLevelObjects[i + 1].gameObject.SetActive(false);
        }
    }

    void updateCurrentCell(int level)
    {
        currentCells[level] = Mathf.Clamp(currentCells[level] - 1, 0, 99);
    }

    public void ResetSpellCells()
    {
        ConsumablePanel[] consumables = GetComponentsInChildren<ConsumablePanel>();
        foreach (ConsumablePanel x in consumables)
        {
            x.ResetToggels();
            currentCells = maxCells;
        }
    }

    public void ResetWarSpellCells()
    {
        ConsumablePanel[] consumables = GetComponentsInChildren<ConsumablePanel>();
        consumables[warLevel - 1].ResetToggels(warAbs);
        currentCells[warLevel - 1] = Mathf.Clamp(warAbs + currentCells[warLevel], 0, maxCells[warLevel]);
    }

    public void ResetSpellCell(int cellid, int add)
    {
        ConsumablePanel[] consumables = GetComponentsInChildren<ConsumablePanel>();
        if (cellid < consumables.Length)
        {
            consumables[cellid].ResetToggels(add);
            currentCells[cellid] += add;
        }
    }

    public int[] GetCellsCurrent()
    {
        return currentCells;
    }

    public int[] GetCellsMax()
    {
        return maxCells;
    }

}
