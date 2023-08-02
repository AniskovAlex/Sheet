using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public static class PresavedLists
{
    static public int addHealth = 0;
    static public int addMaxHealth = 0;

    static public List<Feat> feats = new List<Feat>();

    static public HashSet<string> languages = new HashSet<string>();

    static public HashSet<Weapon.BladeType> bladeTypes = new HashSet<Weapon.BladeType>();

    static public HashSet<Weapon.WeaponType> weaponTypes = new HashSet<Weapon.WeaponType>();

    static public HashSet<Armor.ArmorType> armorTypes = new HashSet<Armor.ArmorType>();

    static string net = null;

    static public HashSet<string> skills = new HashSet<string>();
    static public List<string> skillsAbove = new List<string>();
    static public HashSet<string> competence = new HashSet<string>();

    static public HashSet<string> instruments = new HashSet<string>();
    static public HashSet<string> compInstruments = new HashSet<string>();

    static public List<(int, HashSet<int>)> spellKnew = new List<(int, HashSet<int>)>();
    static public HashSet<int> spellMaster = new HashSet<int>();
    static public HashSet<int> saveThrows = new HashSet<int>();

    static public List<string> attrAdd = new List<string>();

    static public List<string> items = new List<string>();

    static public List<(string, List<int>)> preLists = new List<(string, List<int>)>();

    static public Action<string> ChangePing;
    static public Action<string> ChangeSkillPing;
    static public Action<string> ChangeCompetencePing;
    static public Action<string> ChangeIntrumentsPing;
    static public Action<string> ChangeLanguagePing;

    static public void Init(Character character)
    {

    }

    static public void UpdatePrelist(string listName, int oldValue, int newValue)
    {
        foreach ((string, List<int>) x in preLists.FindAll(x => x.Item1 == listName))
        {
            x.Item2.Remove(oldValue);
            if (newValue >= 0)
                x.Item2.Add(newValue);
        }
        if (ChangePing != null)
            ChangePing(listName);
    }

    static public void UpdateSkills(string oldValue, string newValue)
    {
        string forceRemoveSkill = "";
        if (net == oldValue)
        {
            net = null;
            return;
        }
        if (skills.Contains(newValue))
        {
            forceRemoveSkill = newValue;
            net = newValue;
        }
        skills.Remove(oldValue);
        if (newValue != "Пусто")
            skills.Add(newValue);
        if (ChangeSkillPing != null)
            ChangeSkillPing(forceRemoveSkill);
    }

    static public void UpdateCompentence(string oldValue, string newValue)
    {
        string forceRemoveSkill = "";
        if (competence.Contains(newValue))
            forceRemoveSkill = newValue;
        competence.Remove(oldValue);
        competence.Add(newValue);
        if (ChangeCompetencePing != null)
            ChangeCompetencePing(forceRemoveSkill);
    }

    static public void UpdateInstruments(string oldValue, string newValue)
    {
        string forceRemoveInstruments = "";
        if (net == oldValue)
        {
            net = null;
            return;
        }
        if (instruments.Contains(newValue))
        {
            forceRemoveInstruments = newValue;
            net = newValue;
        }
        instruments.Remove(oldValue);
        if (newValue != "Пусто")
            instruments.Add(newValue);
        if (ChangeIntrumentsPing != null)
            ChangeIntrumentsPing(forceRemoveInstruments);
    }

    static public void UpdateLanguage(string oldValue, string newValue)
    {
        string forceRemoveLangugae = "";
        if (net == oldValue)
        {
            net = null;
            return;
        }
        if (languages.Contains(newValue))
        {
            forceRemoveLangugae = newValue;
            net = newValue;
        }
        languages.Remove(oldValue);
        if (newValue != "Пусто")
            languages.Add(newValue);
        if (ChangeLanguagePing != null)
            ChangeLanguagePing(forceRemoveLangugae);
    }

    static public void UpdateAttrAdd(string newValue)
    {
        if (newValue != "Пусто")
            attrAdd.Add(newValue);
    }

    static public void UpdateAttrAdd(string oldValue, string newValue)
    {
        attrAdd.Remove(oldValue);
        if (newValue != "Пусто")
            attrAdd.Add(newValue);
    }

    static public void RemoveFromPrelist(string listName, int value)
    {
        foreach ((string, List<int>) x in preLists.FindAll(x => x.Item1 == listName))
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

    static public void RemoveFromCompetence(string value)
    {
        competence.Remove(value);
        if (ChangeCompetencePing != null)
            ChangeCompetencePing("");
    }

    static public void RemoveFromInstruments(string value)
    {
        instruments.Remove(value);
        if (ChangeIntrumentsPing != null)
            ChangeIntrumentsPing("");
    }
    static public void RemoveFromLanguage(string value)
    {
        languages.Remove(value);
        if (ChangeLanguagePing != null)
            ChangeLanguagePing("");
    }

    static public void RemoveFromAttrAdd(string value)
    {
        attrAdd.Remove(value);
    }

    static public void SaveProficiency()
    {
        //DataSaverAndLoader.SaveBladeProficiency(bladeTypes);
        //DataSaverAndLoader.SaveWeaponProficiency(weaponTypes);
        //DataSaverAndLoader.SaveArmorProficiency(armorTypes);
    }

    static public void SaveInstruments()
    {
        //DataSaverAndLoader.SaveInstruments(instruments);
    }

    static public void SaveInstrumentsComp()
    {
        //DataSaverAndLoader.SaveInstrumentsComp(compInstruments);
    }
    static public void SaveLanguage()
    {
        //DataSaverAndLoader.SaveLanguage(languages);
    }

    static public void SaveSpellKnew()
    {

        //DataSaverAndLoader.SaveAddSpellKnew(spellKnew);
    }

    static public void SaveFeats()
    {
        //DataSaverAndLoader.SaveFeats(feats);

    }

    static public void SaveCustomPrelists()
    {
        preLists.ForEach(x => CharacterData.AddCustomList(x.Item1, x.Item2) /*DataSaverAndLoader.SaveCustomList(x.Item1, x.Item2)*/);
    }

    static public void saveSaveThrows()
    {
        //DataSaverAndLoader.SaveSaveThrows(saveThrows);
    }

    static public void ResetAll()
    {
        addHealth = 0;
        addMaxHealth = 0;

        feats = new List<Feat>();

        languages = new HashSet<string>();

        bladeTypes = new HashSet<Weapon.BladeType>();

        weaponTypes = new HashSet<Weapon.WeaponType>();

        armorTypes = new HashSet<Armor.ArmorType>();

        skills = new HashSet<string>();
        competence = new HashSet<string>();

        instruments = new HashSet<string>();
        compInstruments = new HashSet<string>();

        spellKnew = new List<(int, HashSet<int>)>();
        spellMaster = new HashSet<int>();
        saveThrows = new HashSet<int>();

        attrAdd = new List<string>();

        items = new List<string>();

        preLists = new List<(string, List<int>)>();

        ChangePing = null;
        ChangeSkillPing = null;
        ChangeCompetencePing = null;
        ChangeIntrumentsPing = null;
        ChangeLanguagePing = null;
    }
}
