using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class CharacterData
{
    static Character instance;
    public static Action load;
    /*static int[] _charAtr = new int[6];
    static int[] _saves = new int[6];
    static int[] _charModifier = new int[6];
    static int[] _money = new int[3];
    static int[] _skills = new int[18];
    static List<(int, PlayersClass)> _classes = new List<(int, PlayersClass)>();
    static HashSet<Weapon.BladeType> _bladeProficiency = new HashSet<Weapon.BladeType>();
    static HashSet<Weapon.WeaponType> _weaponProficiency = new HashSet<Weapon.WeaponType>();
    static HashSet<Armor.ArmorType> _armorProficiency = new HashSet<Armor.ArmorType>();
    static HashSet<string> _language = new HashSet<string>();
    static HashSet<string> _instruments = new HashSet<string>();
    static int _level = 1;
    static Race _race = null;
    static Backstory _backstory = null;
    static int _maxHP;
    static int _currentHP;
    static int _tempHP;
    static int profMod;*/

    /* CharacterData(int[] charAtr, int[] saves, int[] money, int[] skills, List<(int, PlayersClass)> classes, HashSet<string> language, HashSet<string> instruments, HashSet<Weapon.BladeType> bladeProf, HashSet<Weapon.WeaponType> weaponProf, HashSet<Armor.ArmorType> armorProf,
         int level, Race race, Backstory backstory, int maxHP, int currentHP, int tempHP)
     {
         _charAtr = charAtr;
         _saves = saves;
         _skills = skills;
         _money = money;
         _classes = classes;
         _level = level;
         _race = race;
         _backstory = backstory;
         _bladeProficiency = bladeProf;
         _weaponProficiency = weaponProf;
         _armorProficiency = armorProf;
         _maxHP = maxHP;
         _currentHP = currentHP;
         _tempHP = tempHP;
         _language = language;
         _instruments = instruments;
         profMod = (level - 1) / 4 + 2;
         for (int i = 0; i < _charAtr.Length; i++)
             _charModifier[i] = _charAtr[i] / 2 - 5;
     }*/

    public static void loadIsDone()
    {
        if (load != null)
            load();
    }

    public static int[] GetAtribute()
    {
        return instance._charAtr;
    }

    public static int GetAtribute(int index)
    {
        //if (instance._charAtr == null) return 0;
        if (index >= 0 && index < instance._charAtr.Length)
            return instance._charAtr[index];
        return 0;
    }

    public static void AddAtribute(int index, int add)
    {
        //if (instance._charAtr == null) return;
        if (index >= 0 && index < instance._charAtr.Length)
            instance._charAtr[index] += add;
    }

    public static int GetModifier(int index)
    {
        //if (instance._charModifier == null) return 0;
        if (index >= 0 && index < instance._charModifier.Length)
            return instance._charModifier[index];
        return 0;
    }

    public static int GetSave(int index)
    {
        //if (instance._saves == null) return 0;
        if (index >= 0 && index < instance._saves.Length)
            return instance._saves[index];
        return 0;
    }

    public static int[] GetSave()
    {
        return instance._saves;
    }
    public static void AddSave(int index)
    {
        //if (instance._saves == null) return;
        if (index >= 0 && index < instance._saves.Length)
            instance._saves[index] = 1;
    }

    public static int GetMoney(int index)
    {
        //if (instance._money == null) return 0;
        if (index >= 0 && index < instance._money.Length)
            return instance._money[index];
        return 0;
    }

    public static int GetSkill(int index)
    {
        //if (instance._skills == null) return 0;
        if (index >= 0 && index < instance._skills.Length)
            return instance._skills[index];
        return 0;
    }

    public static int[] GetSkill()
    {
        return instance._skills;
    }

    public static int GetSpeed()
    {
        if (instance.GetRace() != null)
            return instance.GetRace().GetSpeed();
        else return 0;
    }

    public static int GetProficiencyBonus()
    {
        return instance.profMod;
    }

    public static HashSet<Weapon.BladeType> GetBladeProficiency()
    {
        //if (instance._bladeProficiency != null)
        return new HashSet<Weapon.BladeType>(instance._bladeProficiency);
        //else
        //  return new HashSet<Weapon.BladeType>();
    }

    public static HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        //if (instance._weaponProficiency != null)
        return new HashSet<Weapon.WeaponType>(instance._weaponProficiency);
        //else
        //  return new HashSet<Weapon.WeaponType>();
    }

    public static HashSet<Armor.ArmorType> GetArmorProficiency()
    {
        //if (instance._armorProficiency != null)
        return new HashSet<Armor.ArmorType>(instance._armorProficiency);
        //else
        //  return new HashSet<Armor.ArmorType>();
    }

    public static List<(int, PlayersClass)> GetClasses()
    {
        return instance.GetClasses();
    }

    public static List<(int, int, int, int)> GetRawClasses()
    {
        return instance._classes;
    }

    public static HashSet<string> GetLanguage()
    {
        //if (instance._language == null) return new HashSet<string>();
        return instance._language;
    }

    public static HashSet<string> GetInstruments()
    {
        //if (instance._instruments == null) return new HashSet<string>();
        return instance._instruments;
    }

    public static HashSet<string> GetInstrumentsComp()
    {
        return instance._instrumentsComp;
    }

    public static int GetLevel()
    {
        return instance._level;
    }

    public static int GetLevel(PlayersClass playerClass)
    {
        //if (playerClass == null) return 0;
        foreach ((int, PlayersClass) x in instance.GetClasses())
            if (x.Item2.id == playerClass.id)
                return x.Item1;
        return 0;
    }

    public static int GetLevel(int id)
    {
        foreach ((int, PlayersClass) x in instance.GetClasses())
            if (x.Item2.id == id)
                return x.Item1;
        return 0;
    }

    public static int GetMagic()
    {
        int magic = 0;
        foreach ((int, PlayersClass) x in instance.GetClasses())
            if (x.Item2.magic > 0)
                magic += 1 / x.Item2.magic;
        return magic;
    }

    public static Race GetRace()
    {
        return instance.GetRace();
    }

    public static Backstory GetBackstory()
    {
        return instance.GetBackstory();
    }

    public static void GetHealth(out int maxHP, out int currentHP, out int tempHP)
    {
        maxHP = instance._maxHP;
        currentHP = instance._currentHP;
        tempHP = instance._tempHP;
    }

    public static int GetCurrentHealth()
    {
        return instance._currentHP;
    }

    public static void SetCurrentHealth(int currentHP, int tempHP)
    {
        instance._currentHP = currentHP;
        instance._tempHP = tempHP;
        SaveCharacter();
        /*DataSaverAndLoader.SaveHealth(currentHP);
        DataSaverAndLoader.SaveTempHealth(tempHP);*/
    }

    public static void SetCurrentHealthSilent(int currentHP)
    {
        instance._currentHP = currentHP;
    }

    public static void SetCurrentHealth(int currentHP)
    {
        instance._currentHP = currentHP;
        SaveCharacter();
        //DataSaverAndLoader.SaveHealth(currentHP);
    }

    public static int GetMaxHealth()
    {
        return instance._maxHP;
    }

    public static void SetMaxHealth(int maxHP)
    {
        instance._maxHP = maxHP;
    }

    public static void SetCharacter(Character character)
    {
        instance = character;
    }

    public static List<(int, List<int>)> GetSpellsKnew()
    {
        if (instance == null) return null;
        return instance._spellKnew;
    }

    public static void SetSpellKnew(List<(int, List<int>)> spells)
    {
        instance._spellKnew = spells;
        SaveCharacter();
    }

    public static void SetSpellKnew(int classID, List<int> spells)
    {
        bool flag = false;
        foreach ((int, List<int>) x in instance._spellKnew)
        {
            if (x.Item1 == classID)
            {
                int index = instance._spellKnew.IndexOf(x);
                instance._spellKnew.Remove(x);
                instance._spellKnew.Insert(index, (classID, spells));
                flag = true;
                break;
            }
        }
        if (!flag)
            instance._spellKnew.Add((classID, spells));
        SaveCharacter();
    }

    public static List<(int, List<int>)> GetSpellPrepared()
    {
        return instance._spellPrepared;
    }

    public static void SetSpellPrepared(List<(int, List<int>)> spells)
    {
        instance._spellPrepared = spells;
        CharacterData.SaveCharacter();
    }
    public static HashSet<int> GetSpellMaster()
    {
        return new HashSet<int>(instance._spellMaster);
    }

    public static void SetSpellMaster(HashSet<int> spells)
    {
        instance._spellMaster = spells.ToList();
    }

    public static void InitPrelist()
    {
        string name = "";
        for (int i = 0; i < 18; i++)
            if (instance._skills[i] > 0)
            {
                switch (i)
                {
                    case 0:
                        name = "Атлетика";
                        break;
                    case 1:
                        name = "Акробатика";
                        break;
                    case 2:
                        name = "Ловкость рук";
                        break;
                    case 3:
                        name = "Скрытность";
                        break;
                    case 4:
                        name = "Анализ";
                        break;
                    case 5:
                        name = "История";
                        break;
                    case 6:
                        name = "Магия";
                        break;
                    case 7:
                        name = "Природа";
                        break;
                    case 8:
                        name = "Религия";
                        break;
                    case 9:
                        name = "Внимательность";
                        break;
                    case 10:
                        name = "Выживание";
                        break;
                    case 11:
                        name = "Медицина";
                        break;
                    case 12:
                        name = "Проницательность";
                        break;
                    case 13:
                        name = "Уход за животными";
                        break;
                    case 14:
                        name = "Выступление";
                        break;
                    case 15:
                        name = "Запугивание";
                        break;
                    case 16:
                        name = "Обман";
                        break;
                    case 17:
                        name = "Убеждение";
                        break;
                }
                if (instance._skills[i] == 1)
                    PresavedLists.skills.Add(name);
                else
                    PresavedLists.competence.Add(name);
            }
        foreach ((int, PlayersClass) x in instance.GetClasses())
        {
            Ability[] abilities = x.Item2.GetAbilities();
            if (x.Item2.GetSubClass() != null)
                abilities.Concat(x.Item2.GetSubClass().GetAbilities());
            foreach (Ability y in abilities)
                if (y.listName != null && y.level <= x.Item1)
                    PresavedLists.preLists.Add((y.listName, GetCustomList(y.listName)));
        }
        PresavedLists.armorTypes = new HashSet<Armor.ArmorType>(instance._armorProficiency);
        PresavedLists.bladeTypes = new HashSet<Weapon.BladeType>(instance._bladeProficiency);
        PresavedLists.weaponTypes = new HashSet<Weapon.WeaponType>(instance._weaponProficiency);
        PresavedLists.instruments = instance._instruments;
        PresavedLists.languages = instance._language;
        for (int i = 0; i < instance._saves.Length; i++)
            if (instance._saves[i] == 1)
                PresavedLists.saveThrows.Add(i);
        //PresavedLists.saveThrows.Concat(instance._saves);
        PresavedLists.compInstruments = instance._instrumentsComp;
    }

    public static void AddCustomList(string listName, List<int> list)
    {
        foreach ((string, List<int>) x in instance._customLists)
            if (x.Item1 == listName)
            {
                instance._customLists.Remove(x);
                break;
            }
        instance._customLists.Add((listName, list));
        //DataCloudeSave.Save(instance._name, instance);
    }

    public static List<int> GetCustomList(string listName)
    {
        foreach ((string, List<int>) x in instance._customLists)
        {
            if (x.Item1 == listName)
                return x.Item2;
        }
        List<int> old = DataSaverAndLoader.LoadCustom(listName);
        if (old.Count > 0)
        {
            AddCustomList(listName, old);
            SaveCharacter();
            return old;
        }
        else
            return new List<int>();
    }

    public static List<int> GetFeats()
    {
        return instance._feats;
    }

    public static void SetFeats(List<int> list)
    {
        instance._feats = list;
    }


    public async static Task SaveCharacter()
    {
        await DataCloudeSave.Save("char_Id_" + instance._id.ToString(), instance);
    }

    public static void SetHealthDice(int classId, int count)
    {
        for (int i = 0; i < instance._classes.Count; i++)
        {
            if (instance._classes[i].Item1 == classId)
            {
                (int, int, int, int) buf = (instance._classes[i].Item1, instance._classes[i].Item2, instance._classes[i].Item3, count);
                instance._classes.RemoveAt(i);
                instance._classes.Insert(i, buf);
            }
        }
    }

    public static int GetHealthDice(int classId)
    {
        for (int i = 0; i < instance._classes.Count; i++)
            if (instance._classes[i].Item1 == classId)
                return instance._classes[i].Item4;
        return 0;
    }

    public static int GetSubClass(int classId)
    {
        for (int i = 0; i < instance._classes.Count; i++)
            if (instance._classes[i].Item1 == classId)
                return instance._classes[i].Item3;
        return -1;
    }

    public static SubRace GetSubRace()
    {
        return instance.GetRace().GetSubRace();
    }

    public static void SetMoney(int[] cur)
    {
        instance._money = cur;
        SaveCharacter();
    }

    public static void SetAlignments(int str)
    {
        instance._alignment = str;
        SaveCharacter();
    }

    public static int GetAlignments()
    {
        return instance._alignment;
    }

    public static void SetNature(string str)
    {
        instance._nature = str;
        SaveCharacter();
    }

    public static string GetNature()
    {
        return instance._nature;
    }

    public static void SetIdeal(string str)
    {
        instance._ideal = str;
        SaveCharacter();
    }
    public static string GetIdeal()
    {
        return instance._ideal;
    }
    public static void SetAttachment(string str)
    {
        instance._attachment = str;
        SaveCharacter();
    }
    public static string GetAttachment()
    {
        return instance._attachment;
    }
    public static void SetWeakness(string str)
    {
        instance._weakness = str;
        SaveCharacter();
    }
    public static string GetWeakness()
    {
        return instance._weakness;
    }
    public static void SetBackstoryExtend(string str)
    {
        instance._backstoryExtend = str;
        SaveCharacter();
    }
    public static string GetBackstoryExtend()
    {
        return instance._backstoryExtend;
    }

    public static List<(string, string)> GetNotes()
    {
        return instance._notes;
    }

    public static void SetNote(string noteName, string noteEnt)
    {
        for (int i = 0; i < instance._notes.Count; i++)
            if (instance._notes[i].Item1.Equals(noteName))
            {
                instance._notes.RemoveAt(i);
                instance._notes.Insert(i, (noteName, noteEnt));
                break;
            }
        SaveCharacter();
    }

    public static void AddNote(string noteName, string noteEnt)
    {
        instance._notes.Add((noteName, noteEnt));
        SaveCharacter();
    }

    public static void DeleteNote(int index)
    {
        instance._notes.RemoveAt(index);
        SaveCharacter();
    }

    public static int GetConsum(string name)
    {
        int con = 0;
        if (!instance._consum.TryGetValue(name, out con))
        {
            con = DataSaverAndLoader.LoadConsumAmount(name);
            SetConsum(name, con);
        }
        return con;
    }

    public static void SetConsum(string name, int amount)
    {
        if (instance._consum.ContainsKey(name))
            instance._consum[name] = amount;
        else
            instance._consum.Add(name, amount);
        SaveCharacter();
    }

    public static int[] GetSpellCells()
    {
        return instance._spellCells;
    }

    public static void SetSpellCell(int level, int count)
    {
        if (level > 0 && level <= 9)
            instance._spellCells[level - 1] = count;
        SaveCharacter();
    }

    public static void SetItemSilent(Item item)
    {
        if (item is Weapon)
        {
            instance._weapon.Add(item as Weapon);
            return;
        }
        if (item is Armor)
        {
            instance._armor.Add(item as Armor);
            return;
        }
        instance._items.Add(item);
        //SaveCharacter();
    }

    public static List<Item> GetItems()
    {
        return instance._items.Concat(instance._weapon).Concat(instance._armor).ToList();
        return new List<Item>();
    }

    public static void EditItem(string name, int amount)
    {
        bool found = false;
        for (int i = 0; i < instance._items.Count; i++)
        {
            if (instance._items[i].label == name)
            {
                if (amount <= 0)
                    instance._items.RemoveAt(i);
                else
                    instance._items[i].amount = amount;
                found = true;
                break;
            }
        }
        if (!found)
            for (int i = 0; i < instance._weapon.Count; i++)
            {
                if (instance._weapon[i].label == name)
                {
                    if (amount <= 0)
                        instance._weapon.RemoveAt(i);
                    else
                        instance._weapon[i].amount = amount;
                    found = true;
                    break;
                }
            }
        if (!found)
            for (int i = 0; i < instance._armor.Count; i++)
            {
                if (instance._armor[i].label == name)
                {
                    if (amount <= 0)
                        instance._armor.RemoveAt(i);
                    else
                        instance._armor[i].amount = amount;
                    break;
                }
            }
        SaveCharacter();
    }

    public static void EditItemAmountAdd(string name, int amountAdd)
    {
        bool found = false;
        for (int i = 0; i < instance._items.Count; i++)
        {
            if (instance._items[i].label == name)
            {
                instance._items[i].amount += amountAdd;
                found = true;
                break;
            }
        }
        if (!found)
            for (int i = 0; i < instance._weapon.Count; i++)
            {
                if (instance._weapon[i].label == name)
                {
                    instance._weapon[i].amount += amountAdd;
                    found = true;
                    break;
                }
            }
        if (!found)
            for (int i = 0; i < instance._armor.Count; i++)
            {
                if (instance._armor[i].label == name)
                {
                    instance._armor[i].amount += amountAdd;
                    break;
                }
            }
        SaveCharacter();
    }

    public static void EditItem(int id, int amount)
    {
        bool found = false;
        for (int i = 0; i < instance._items.Count; i++)
        {
            if (instance._items[i].id == id)
            {
                if (amount <= 0)
                    instance._items.RemoveAt(i);
                else
                    instance._items[i].amount = amount;
                found = true;
                break;
            }
        }
        if (!found)
            for (int i = 0; i < instance._weapon.Count; i++)
            {
                if (instance._weapon[i].id == id)
                {
                    if (amount <= 0)
                        instance._weapon.RemoveAt(i);
                    else
                        instance._weapon[i].amount = amount;
                    found = true;
                    break;
                }
            }
        if (!found)
            for (int i = 0; i < instance._armor.Count; i++)
            {
                if (instance._armor[i].id == id)
                {
                    if (amount <= 0)
                        instance._armor.RemoveAt(i);
                    else
                        instance._armor[i].amount = amount;
                    break;
                }
            }
        SaveCharacter();
    }

    public static List<(int, string)> GetWeaponEquip()
    {
        return instance._weaponEquip;
    }

    public static List<(int, string)> GetArmorEquip()
    {
        return instance._armorEquip;
    }

    public static void EditWeaponEquip(Weapon weapon, bool isOn)
    {
        if (isOn)
        {
            instance._weaponEquip.Add((weapon.id, weapon.label));
            return;
        }
        foreach ((int, string) x in instance._weaponEquip)
        {
            if (weapon.id == -1 && weapon.label == x.Item2)
            {
                instance._weaponEquip.Remove(x);
                return;
            }
            else if (weapon.id != -1 && weapon.id == x.Item1)
            {
                instance._weaponEquip.Remove(x);
                return;
            }
        }
    }

    public static void EditArmorEquip(Armor armor, bool isOn)
    {
        if (isOn)
        {
            instance._armorEquip.Add((armor.id, armor.label));
            return;
        }
        foreach ((int, string) x in instance._armorEquip)
        {
            if (armor.id == -1 && armor.label == x.Item2)
            {
                instance._armorEquip.Remove(x);
                return;
            }
            else if (armor.id != -1 && armor.id == x.Item1)
            {
                instance._armorEquip.Remove(x);
                return;
            }
        }
    }
}
