using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class PresavedLists
{
    public enum Language
    {
        giant,
        common
    }

    static public HashSet<Language> languages = new HashSet<Language>();

    static public HashSet<Weapon.BladeType> bladeTypes = new HashSet<Weapon.BladeType>();

    static public HashSet<Weapon.WeaponType> weaponTypes = new HashSet<Weapon.WeaponType>();

    static public HashSet<Armor.ArmorType> armorTypes = new HashSet<Armor.ArmorType>();

    static public HashSet<string> skills = new HashSet<string>();
    
    static public HashSet<string> instruments = new HashSet<string>();

    static public HashSet<int> saveThrows = new HashSet<int>();

    static public List<(string, List<string>)> preLists = new List<(string, List<string>)>();

    static public Action<string> ChangePing;
    static public Action<string> ChangeSkillPing;
    static public Action<string> ChangeIntrumentsPing;

    static public void UpdatePrelist(string listName, string oldValue, string newValue)
    {
        foreach ((string, List<string>) x in preLists.FindAll(x => x.Item1 == listName))
        {
            x.Item2.Remove(oldValue);
            x.Item2.Add(newValue);
        }
        ChangePing(listName);
    }

    static public void UpdateSkills(string oldValue, string newValue)
    {
        string forceRemoveSkill = "";
        if (skills.Contains(newValue))
            forceRemoveSkill = newValue;
        skills.Remove(oldValue);
        skills.Add(newValue);
        ChangeSkillPing(forceRemoveSkill);
    }

    static public void UpdateInstruments(string oldValue, string newValue)
    {
        string forceRemoveInstruments = "";
        if (instruments.Contains(newValue))
            forceRemoveInstruments = newValue;
        instruments.Remove(oldValue);
        instruments.Add(newValue);
        ChangeIntrumentsPing(forceRemoveInstruments);
    }

    static public void RemoveFromPrelist(string listName, string value)
    {
        foreach ((string, List<string>) x in preLists.FindAll(x => x.Item1 == listName))
        {
            x.Item2.Remove(value);
            if (x.Item2.Count <= 0)
            {
                preLists.Remove(x);
            }
        }
        if (ChangePing != null)
            ChangePing(listName);
    }
    static public void RemoveFromSkills(string value)
    {
        skills.Remove(value);
        if (ChangeSkillPing != null)
            ChangeSkillPing("");
    }

    static public void RemoveFromInstruments(string value)
    {
        instruments.Remove(value);
        if (ChangeIntrumentsPing != null)
            ChangeIntrumentsPing("");
    }

    static public void SaveProficiency()
    {
        DataSaverAndLoader.SaveBladeProficiency(bladeTypes);
        DataSaverAndLoader.SaveWeaponProficiency(weaponTypes);
        DataSaverAndLoader.SaveArmorProficiency(armorTypes);
    }

    static public void SaveInstruments()
    {
        DataSaverAndLoader.SaveInstruments(instruments);
    }

    static public void SaveCustomPrelists()
    {
        preLists.ForEach(x => DataSaverAndLoader.SaveCustomList(x.Item1, x.Item2));
    }

    static public void saveSaveThrows()
    {
        DataSaverAndLoader.SaveSaveThrows(saveThrows);
    }
}
