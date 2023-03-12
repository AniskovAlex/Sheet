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
    public static bool monkDefence = false;
    public static bool monkSpeed = false;
    public static bool monkWeapon = false;
    public static bool dimondSoul = false;
    public static bool spellMaster = false;
    public static bool sorcererUnit = false;
    public static bool fastFeet = false;
    public static bool alert = false;
    public static bool observant = false;
    public static bool dealWielder = false;
    public static bool mediumArmorMaster = false;
    public static bool durable = false;
    public static bool mobile = false;
    public static bool magic = false;
    public static bool load = false;
    public static bool ritualCaster = false;
    public static bool secreatsBook = false;

    public static void ResetRuleChanger()
    {
        dragging = false;
        popoutMenu = false;
        duelist = false;
        defence = false;
        archer = false;
        allHandy = false;
        barbarianDefence = false;
        barbatianFastMove = false;
        wildChampion = false;
        monkDefence = false;
        monkSpeed = false;
        monkWeapon = false;
        dimondSoul = false;
        spellMaster = false;
        sorcererUnit = false;
        fastFeet = false;
        alert = false;
        observant = false;
        dealWielder = false;
        mediumArmorMaster = false;
        durable = false;
        mobile = false;
        ritualCaster = false;
        secreatsBook = false;
    }
}
