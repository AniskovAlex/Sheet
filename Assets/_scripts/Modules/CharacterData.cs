using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    static CharacterData instance;
    static int[] _charAtr = new int[6];
    static int[] _saves = new int[6];
    static int[] _charModifier = new int[6];
    static int[] _money = new int[3];
    static int[] _skills = new int[18];
    static List<(int, PlayersClass)> _classes = new List<(int, PlayersClass)>();
    static int _level = 0;
    static Race _race = null;
    static Backstory _backstory = null;
    static int _maxHP;
    static int _currentHP;
    static int _tempHP;
    static int profMod;

    CharacterData(int[] charAtr, int[] saves, int[] money, int[] skills, List<(int, PlayersClass)> classes, int level, Race race, Backstory backstory, int maxHP, int currentHP, int tempHP)
    {
        _charAtr = charAtr;
        _skills = skills;
        _money = money;
        _classes = classes;
        _level = level;
        _race = race;
        _backstory = backstory;
        _maxHP = maxHP;
        _currentHP = currentHP;
        _tempHP = tempHP;
        profMod = (level - 1) / 4 + 2;
    }

    public static int GetAtribute(int index)
    {
        if (index > 0 && index < _charAtr.Length)
            return _charAtr[index];
        return 0;
    }

    public static int GetModifier(int index)
    {
        if (index > 0 && index < _charModifier.Length)
            return _charModifier[index];
        return 0;
    }

    public static int GetSave(int index)
    {
        if (index > 0 && index < _saves.Length)
            return _saves[index];
        return 0;
    }

    public static int GetMoney(int index)
    {
        if (index > 0 && index < _money.Length)
            return _money[index];
        return 0;
    }

    public static int GetSkill(int index)
    {
        if (index > 0 && index < _skills.Length)
            return _skills[index];
        return 0;
    }

    public static int GetProficiencyBonus()
    {
        return profMod;
    }
    public static List<(int, PlayersClass)> GetClasses()
    {
        return _classes;
    }

    public static int GetLevel()
    {
        return _level;
    }

    public static Race GetRace()
    {
        return _race;
    }

    public static Backstory GetBackstory()
    {
        return _backstory;
    }

    public static void GetHealth(out int maxHP, out int currentHP, out int tempHP)
    {
        maxHP = _maxHP;
        currentHP = _currentHP;
        tempHP = _tempHP;
    }

    public static void SetCharacterData(int[] charAtr, int[] saves, int[] money, int[] skills, List<(int, PlayersClass)> classes, int level,
        Race race, Backstory backstory, int maxHP, int currentHP, int tempHP)
    {
        if (instance == null)
            instance = new CharacterData(charAtr, saves, money, skills, classes, level, race, backstory, maxHP, currentHP, tempHP);
    }
}
