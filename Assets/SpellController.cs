using System.Collections;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    Spell[] spells;
    [SerializeField] List<GameObject> spellLevelContainerObjects;
    [SerializeField] List<SpellShower> spellLevelObjects;
    [SerializeField] SpellBody spellBody;
    public static Action ReloadSpells;
    public static List<(int, List<Spell>)> spellKnew = new List<(int, List<Spell>)>();
    public static List<(int, List<Spell>)> spellPrepared = new List<(int, List<Spell>)>();
    int[] currentCells;

    private void Start()
    {
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
        List<(int, HashSet<int>)> spellPreparedIdList = DataSaverAndLoader.LoadSpellPrepared();
        spellKnew = SetSpell(spellKnewIdList);
        spellPrepared = SetSpell(spellPreparedIdList);
        InitSpells();
        ReloadSpells = InitSpells;
        InitSpellCells();
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
                spellList.Add((g.Item1, buf));
            }
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
            foreach (Spell x in g.Item2)
                if (x.level >= 0 && x.level <= 9)
                    Instantiate(spellBody, spellLevelContainerObjects[x.level].transform).SetSpell(x);
        });
        spellPrepared.ForEach(g =>
        {
            foreach (Spell x in g.Item2)
                if (x.level >= 0 && x.level <= 9)
                    Instantiate(spellBody, spellLevelContainerObjects[x.level].transform).SetSpell(x);
        });
    }

    void InitSpellCells()
    {
        List<(int, PlayersClass)> playerClasses = CharacterData.GetClasses();
        int[] cells = new int[9];
        int levelAbs = 0;
        foreach ((int, PlayersClass) x in playerClasses)
        {
            if (x.Item2.magic != 0)
            {
                levelAbs += (x.Item1 + (x.Item2.magic - 1)) / (x.Item2.magic);
            }

        }
        switch (levelAbs)
        {
            case 1:
                cells[0] = 2;
                break;
            case 2:
                cells[0] = 3;
                break;
            case 3:
                cells[0] = 4;
                cells[1] = 2;
                break;
            case 4:
                cells[0] = 4;
                cells[1] = 3;
                break;
            case 5:
                cells[0] = 4;
                cells[1] = 3;
                cells[3] = 2;
                break;
            case 6:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                break;
            case 7:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 1;
                break;
            case 8:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 2;
                break;
            case 9:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 1;
                break;
            case 10:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 2;
                break;
            case 11:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 2;
                cells[5] = 1;
                break;
            case 12:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 2;
                cells[5] = 1;
                break;
            case 13:
            case 14:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 2;
                cells[5] = 1;
                cells[6] = 1;
                break;
            case 15:
            case 16:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 2;
                cells[5] = 1;
                cells[6] = 1;
                cells[7] = 1;
                break;
            case 17:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 2;
                cells[5] = 1;
                cells[6] = 1;
                cells[7] = 1;
                cells[8] = 1;
                break;
            case 18:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 3;
                cells[5] = 1;
                cells[6] = 1;
                cells[7] = 1;
                cells[8] = 1;
                break;
            case 19:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 3;
                cells[5] = 2;
                cells[6] = 1;
                cells[7] = 1;
                cells[8] = 1;
                break;
            case 20:
                cells[0] = 4;
                cells[1] = 3;
                cells[2] = 3;
                cells[3] = 3;
                cells[4] = 3;
                cells[5] = 2;
                cells[6] = 2;
                cells[7] = 1;
                cells[8] = 1;
                break;
        }
        for (int i = 0; i < cells.Length; i++)
            if (cells[i] != 0)
            {
                spellLevelObjects[i + 1].SetCells(cells[i], currentCells[i]);
            }
            else
                spellLevelObjects[i + 1].gameObject.SetActive(false);
    }

    public void ResetSpellCells()
    {
        ConsumablePanel[] consumables = GetComponentsInChildren<ConsumablePanel>();
        foreach (ConsumablePanel x in consumables)
            x.Reset();
    }
}
