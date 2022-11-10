using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utilities
{
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
        int spellCellCount = 0;
        foreach ((int, PlayersClass) x in list)
            switch (x.Item2.magic)
            {
                case 1:
                    spellCellCount += x.Item1;
                    break;
                case 2:
                    spellCellCount += x.Item1 / 2;
                    break;
            }
        spellCellCount = (spellCellCount + 1) / 2;
        return Mathf.Clamp(spellCellCount, 0, 9);
    }

}
