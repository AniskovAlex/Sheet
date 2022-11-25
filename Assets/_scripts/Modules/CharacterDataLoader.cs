using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterDataLoader : MonoBehaviour
{
    const string attrSaveName = "atr_";
    const string moneySaveName = "mon_";
    const string skillSaveName = "skill_";
    const string levelCountSaveName = "lvlCount_";
    const string levelSaveName = "lvl_";
    const string levelLabelSaveName = "lvlLabel_";
    const string saveSaveName = "save_";
    const string maxHealthSaveName = "maxHP_";
    const string healthSaveName = "HP_";
    const string tempHealthSaveName = "THP_";
    const string raceSaveName = "race_";
    const string backstorySaveName = "backstory_";

    string characterName;
    int[] _attributesArr = new int[6];
    int[] _saves = new int[6];
    int[] _money = new int[3];
    int[] _skills = new int[18];
    List<(int, PlayersClass)> _classes = new List<(int, PlayersClass)>();
    HashSet<Weapon.BladeType> bladeProficiency = new HashSet<Weapon.BladeType>();
    HashSet<Weapon.WeaponType> weaponProficiency = new HashSet<Weapon.WeaponType>();
    HashSet<Armor.ArmorType> armorProficiency = new HashSet<Armor.ArmorType>();
    HashSet<string> language = new HashSet<string>();
    HashSet<string> instruments = new HashSet<string>();
    int level = 0;
    PlayersClass playersClass = null;
    Race race = null;
    Backstory backstory = null;
    int maxHP;
    int currentHP;
    int tempHP;

    private void Awake()
    {
        characterName = CharacterCollection.GetName();
        GlobalStatus.ResetRuleChanger();
        LoadAttributes();
        LoadMoney();
        LoadSkills();
        LoadClasses();
        LoadRace();
        LoadBackstory();
        LoadSaves();
        LoadProficiency();
        LoadHP();
        language = DataSaverAndLoader.LoadLanguage();
        instruments = DataSaverAndLoader.LoadInstruments();
        CharacterData.SetCharacterData(_attributesArr, _saves, _money, _skills, _classes, language, instruments, bladeProficiency, weaponProficiency, armorProficiency, level, race, backstory, maxHP, currentHP, tempHP);
    }

    void LoadAttributes()
    {
        for (int i = 0; i < 6; i++)
        {
            string saveName = characterName + attrSaveName + i;
            if (PlayerPrefs.HasKey(saveName))
            {
                _attributesArr[i] = PlayerPrefs.GetInt(saveName);
            }
            else
            {
                _attributesArr[i] = 10;
            }
        }
    }

    void LoadMoney()
    {
        for (int i = 0; i < 3; i++)
            _money[i] = PlayerPrefs.GetInt(characterName + moneySaveName + i);
    }

    void LoadSkills()
    {
        for (int i = 0; i < 18; i++)
            _skills[i] = PlayerPrefs.GetInt(characterName + skillSaveName + i);
    }

    void LoadClasses()
    {
        int count = PlayerPrefs.GetInt(characterName + levelCountSaveName);
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.HasKey(characterName + levelSaveName + i))
            {
                int classLevel = PlayerPrefs.GetInt(characterName + levelSaveName + i);
                switch (PlayerPrefs.GetInt(characterName + levelLabelSaveName + i))
                {
                    case 2:
                        playersClass = new Fighter();
                        break;
                    case 10:
                        playersClass = new Rogue();
                        break;
                    case 6:
                        playersClass = new Artificer();
                        break;
                    default:
                        return;
                }
                level += classLevel;
                _classes.Add((classLevel, playersClass));
            }
        }
    }

    void LoadRace()
    {
        switch (PlayerPrefs.GetString(characterName + raceSaveName))
        {
            case "Человек":
                race = new Human();
                break;
            case "Харенгон":
                race = new Harengon();
                break;
            case "Гном":
                race = new Gnome();
                break;
        }
    }

    void LoadBackstory()
    {
        switch (PlayerPrefs.GetString(characterName + backstorySaveName))
        {
            case "Артист":
                backstory = new Artist();
                break;
        }
    }

    void LoadSaves()
    {
        for (int i = 0; i < 6; i++)
        {
            _saves[i] = PlayerPrefs.GetInt(characterName + saveSaveName + i);
        }
    }

    void LoadProficiency()
    {
        bladeProficiency = DataSaverAndLoader.LoadBladeProfiency();
        weaponProficiency = DataSaverAndLoader.LoadWeaponProfiency();
        armorProficiency = DataSaverAndLoader.LoadArmorProfiency();
    }

    void LoadHP()
    {
        maxHP = PlayerPrefs.GetInt(characterName + maxHealthSaveName);
        currentHP = PlayerPrefs.GetInt(characterName + healthSaveName);
        tempHP = PlayerPrefs.GetInt(characterName + tempHealthSaveName);
    }

    public void LoadPrelists()
    {
        for (int i = 0; i < 18; i++)
            if (_skills[i] != 0)
                switch (i)
                {
                    case 0:
                        PresavedLists.skills.Add("Атлетика");
                        break;
                    case 1:
                        PresavedLists.skills.Add("Акробатика");
                        break;
                    case 2:
                        PresavedLists.skills.Add("Ловкость рук");
                        break;
                    case 3:
                        PresavedLists.skills.Add("Скрытность");
                        break;
                    case 4:
                        PresavedLists.skills.Add("Анализ");
                        break;
                    case 5:
                        PresavedLists.skills.Add("История");
                        break;
                    case 6:
                        PresavedLists.skills.Add("Магия");
                        break;
                    case 7:
                        PresavedLists.skills.Add("Природа");
                        break;
                    case 8:
                        PresavedLists.skills.Add("Религия");
                        break;
                    case 9:
                        PresavedLists.skills.Add("Внимательность");
                        break;
                    case 10:
                        PresavedLists.skills.Add("Выживание");
                        break;
                    case 11:
                        PresavedLists.skills.Add("Медицина");
                        break;
                    case 12:
                        PresavedLists.skills.Add("Проницательность");
                        break;
                    case 13:
                        PresavedLists.skills.Add("Уход за животными");
                        break;
                    case 14:
                        PresavedLists.skills.Add("Выступление");
                        break;
                    case 15:
                        PresavedLists.skills.Add("Запугивание");
                        break;
                    case 16:
                        PresavedLists.skills.Add("Обман");
                        break;
                    case 17:
                        PresavedLists.skills.Add("Убеждение");
                        break;
                }
        foreach ((int, PlayersClass) x in _classes)
        {
            Ability[] abilities = x.Item2.GetAbilities();
            if (x.Item2.GetSubClass() != null)
                abilities.Concat(x.Item2.GetSubClass().GetAbilities());
            foreach (Ability y in abilities)
                if (y.listName != null && y.level <= x.Item1)
                    PresavedLists.preLists.Add((y.listName, DataSaverAndLoader.LoadCustom(y.listName)));
        }
        PresavedLists.armorTypes = armorProficiency;
        PresavedLists.bladeTypes = bladeProficiency;
        PresavedLists.weaponTypes = weaponProficiency;
        PresavedLists.instruments = instruments;
        PresavedLists.languages = language;
        PresavedLists.saveThrows.Concat(_saves);
    }
}
