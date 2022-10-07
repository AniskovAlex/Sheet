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

    static public List<Language> languages = new List<Language>();

    static public List<string> skills = new List<string>();

    static public List<(string, List<string>)> preLists = new List<(string, List<string>)>();

    static public Action<string> ChangePing;

    static public void UpdatePrelist(string listName, string oldValue, string newValue)
    {
        foreach ((string, List<string>) x in preLists.FindAll(x => x.Item1 == listName))
        {
            x.Item2.Remove(oldValue);
            x.Item2.Add(newValue);
        }
        ChangePing(listName);
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

    static public void SavePrelists()
    {
        preLists.ForEach(x => DataSaverAndLoader.SaveCustomList(x.Item1, x.Item2));
    }
}
