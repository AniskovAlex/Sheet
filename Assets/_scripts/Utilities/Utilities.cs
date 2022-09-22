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

}
