using System.Collections;
using System.Diagnostics;
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
            cellMax = Mathf.Max(cellMax, ((x.Item1 + buf) -1) / (2 * x.Item2.magic) + 1);
        }
        return Mathf.Clamp(cellMax, 0, 9);
    }

}
