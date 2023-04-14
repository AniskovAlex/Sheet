using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utilities
{
    static Stopwatch stopwatch = new Stopwatch();

    public static void StartTimer()
    {
        stopwatch.Start();
    }

    public static void StopTimer()
    {
        stopwatch.Stop();
        System.TimeSpan time = stopwatch.Elapsed;
        UnityEngine.Debug.Log("Время кода: " + time.Milliseconds + "mc");
        stopwatch.Reset();
    }

    public static int GetSkillProfModifier(int skillProf, int PB)
    {
        int modifier = 0;
        switch (skillProf)
        {
            case -1:
                modifier += PB / 2;
                break;
            case 0:
                break;
            case 1:
                modifier += PB;
                break;
            case 2:
                modifier += PB * 2;
                break;
        }
        return modifier;
    }

    public static void SetTextSign(int num, Text text)
    {
        if (num >= 0)
            text.text = "+" + num;
        else
            text.text = num.ToString();
    }

    public static int GetMaxSpellLevel(List<(int, PlayersClass)> list)
    {
        int cellPool = 0;
        int cellMax = 0;
        foreach ((int, PlayersClass) x in list)
        {
            if (x.Item2 == null || x.Item1 < x.Item2.magic || x.Item2.magic <= 0) continue;
            cellPool += x.Item1 / x.Item2.magic;
        }
        foreach ((int, PlayersClass) x in list)
        {
            if (x.Item2 == null || x.Item1 < x.Item2.magic || x.Item2.magic <= 0) continue;
            int buf = cellPool - (x.Item1 / x.Item2.magic);
            cellMax = Mathf.Max(cellMax, ((x.Item1 + buf) - 1) / (2 * x.Item2.magic) + 1);
        }
        return Mathf.Clamp(cellMax, 0, 9);
    }

    public static List<(int, string, List<Spell>)> SplitSpellList(List<Spell> spells, int spellsPerSheet)
    {
        if (spells.Count > 0)
            spells.Sort((x, y) => x.name.CompareTo(y.name));
        char nameFirst = ' ';
        char nameSecond = ' ';
        List<(int, string, List<Spell>)> returnList = new List<(int, string, List<Spell>)>();
        Spell[] sheet = new Spell[spellsPerSheet];
        int i;
        for (i = 0; (i + 1) * spellsPerSheet <= spells.Count; i++)
        {
            spells.CopyTo(i * spellsPerSheet, sheet, 0, spellsPerSheet);
            nameFirst = sheet[0].name[0];
            nameSecond = sheet[spellsPerSheet - 1].name[0];
            returnList.Add((0, nameFirst + "-" + nameSecond, sheet.ToList()));
        }
        int left = spells.Count - (i * spellsPerSheet);
        Spell[] sheetLeft = new Spell[spells.Count - (i * spellsPerSheet)];
        spells.CopyTo(i * spellsPerSheet, sheetLeft, 0, left);
        nameFirst = sheetLeft[0].name[0];
        nameSecond = sheetLeft[left - 1].name[0];
        returnList.Add((0, nameFirst + "-" + nameSecond, sheetLeft.ToList()));
        for (int j = 0; j < 10; j++)
        {
            List<Spell> spellsLevel = spells.FindAll(g => g.level == j);
            if (spellsLevel.Count > 0)
                returnList.Add((1, j.ToString(), spellsLevel));
        }

        return returnList;
    }

    public static List<(int, string, List<Spell>)> SortSpellList(List<(int, string, List<Spell>)> list, int spellsPerSheet)
    {
        List<Spell> allList = new List<Spell>();
        foreach ((int, string, List<Spell>) x in list)
        {
            if (x.Item1 != 0) continue;
            allList = allList.Concat(x.Item3).ToList();
        }
        if (allList.Count > 0)
            allList.Sort((x,y) => x.name.CompareTo(y.name));
        return SplitSpellList(allList, spellsPerSheet);
    }

}
