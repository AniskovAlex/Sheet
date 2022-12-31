using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterDataLoader : MonoBehaviour
{
    const string attrSaveName = "@atr_";
    const string moneySaveName = "@mon_";
    const string skillSaveName = "@skill_";
    const string levelCountSaveName = "@lvlCount_";
    const string levelSaveName = "@lvl_";
    const string levelLabelSaveName = "@lvlLabel_";
    const string saveSaveName = "@save_";
    const string maxHealthSaveName = "@maxHP_";
    const string healthSaveName = "@HP_";
    const string tempHealthSaveName = "@THP_";
    const string raceSaveName = "@race_";
    const string backstorySaveName = "@backstory_";

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
        PresavedLists.ResetAll();
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
        LoadFeats();
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
                    case 0:
                        playersClass = new Bard();
                        break;
                    case 1:
                        playersClass = new Barbarian();
                        break;
                    case 2:
                        playersClass = new Fighter();
                        break;
                    case 3:
                        playersClass = new Wizard();
                        break;
                    case 4:
                        playersClass = new Druid();
                        break;
                    case 5:
                        playersClass = new Cleric();
                        break;
                    case 7:
                        playersClass = new Warlock();
                        break;
                    case 8:
                        playersClass = new Monk();
                        break;
                    case 9:
                        playersClass = new Paladin();
                        break;
                    case 10:
                        playersClass = new Rogue();
                        break;
                    case 11:
                        playersClass = new Ranger();
                        break;
                    case 12:
                        playersClass = new Sorcerer();
                        break;
                }
                level += classLevel;
                playersClass.ChooseSubClass(DataSaverAndLoader.LoadSubClass(playersClass));
                _classes.Add((classLevel, playersClass));
            }
        }
    }

    void LoadRace()
    {
        switch (PlayerPrefs.GetInt(characterName + raceSaveName))
        {
            case 0:
                race = new Gnome();
                break;
            case 1:
                race = new Dwarf();
                break;
            case 2:
                race = new Dragonborn();
                break;
            case 3:
                race = new HalfOrc();
                break;
            case 4:
                race = new Halfling();
                break;
            case 5:
                race = new HalfElf();
                break;
            case 6:
                race = new Tiefling();
                break;
            case 7:
                race = new Human();
                break;
            case 8:
                race = new Elf();
                break;
        }
    }

    void LoadBackstory()
    {
        switch (PlayerPrefs.GetInt(characterName + backstorySaveName))
        {
            case 0:
                backstory = new Artist();
                break;
            case 1:
                backstory = new Waif();
                break;
            case 2:
                backstory = new Noble();
                break;
            case 3:
                backstory = new GuildArtiser();
                break;
            case 4:
                backstory = new Sailor();
                break;
            case 5:
                backstory = new Sage();
                break;
            case 6:
                backstory = new PeoplesHero();
                break;
            case 7:
                backstory = new Hermit();
                break;
            case 8:
                backstory = new Criminal();
                break;
            case 9:
                backstory = new Acolyte();
                break;
            case 10:
                backstory = new Soldier();
                break;
            case 11:
                backstory = new Foreigner();
                break;
            case 12:
                backstory = new Charlatan();
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

    void LoadFeats()
    {
        List<int> list = DataSaverAndLoader.LoadFeats();
        foreach (int x in list)
            foreach (Feat y in FileSaverAndLoader.LoadFeats())
            {
                if (y.id == x)
                {
                    PresavedLists.feats.Add(y);
                    break;
                }
            }
    } 

    public void LoadPrelists()
    {
        string name = "";
        for (int i = 0; i < 18; i++)
            if (_skills[i] != 0)
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
                if (_skills[i] == 1)
                    PresavedLists.skills.Add(name);
                else
                    PresavedLists.competence.Add(name);
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
        PresavedLists.compInstruments = DataSaverAndLoader.LoadInstrumentsComp();
    }
}
