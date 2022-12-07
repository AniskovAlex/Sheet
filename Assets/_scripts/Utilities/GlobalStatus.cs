using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalStatus 
{
    public static bool dragging = false;
    public static bool popoutMenu = false;
    public static bool needRest = false;
    public static bool duelist = false;
    public static bool defence = false;
    public static bool archer = false;
    public static bool allHandy = false;
    public static bool barbarianDefence = false;
    public static bool barbatianFastMove = false;
    public static bool wildChampion = false;
    public static void ResetRuleChanger()
    {
        duelist = false;
        defence= false;
    }
}
