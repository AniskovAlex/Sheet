using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Threading.Tasks;

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
    List<(int,int)> healthDices = new List<(int,int)>();
    List<int> feats = new List<int>();
    int maxHP;
    int currentHP;
    int tempHP;
    bool init = false;

    private void Start()
    {
        //LoadSceneManager.Instance.GetReady();
        PresavedLists.ResetAll();
        characterName = CharacterCollection.GetName();
        GlobalStatus.ResetRuleChanger();
        //CharacterData.SetCharacter(DataCloudeSave.Load<Character>(characterName).Result);
        StartCoroutine(Auth());
        //while (!init);
    }

    IEnumerator Auth()
    {
        Task task = DataCloudeSave.Auth();
        while (!task.IsCompleted) yield return null;
        task = SetCharaterFromCloud();
        while (!task.IsCompleted) yield return null;
        CharacterData.loadIsDone();
        LoadSceneManager.Instance.HideLoadScreen();
    }

    async Task SetCharaterFromCloud()
    {
        if (characterName == "")
        {
            CharacterData.SetCharacter(new Character());
            return;
        }
        Character character = await DataCloudeSave.Load<Character>("char_Id_" + CharacterCollection.GetId().ToString());
        Debug.Log(character);
        if (character == null)
        {
            
            //character = new Character(_attributesArr, _saves, _money, _skills, _classes, language, instruments, bladeProficiency, weaponProficiency, armorProficiency, level, race, backstory, maxHP, currentHP, tempHP);
            character = new Character();
            LoadOldSave();
            initChar(character);
            //await DataCloudeSave.Save("char_Id_" + character._id.ToString(), character);
        }

        character.Init();
        CharacterData.SetCharacter(character);
        init = true;
        Debug.Log(characterName + "is Loaded!");
    }

    void LoadOldSave()
    {
        
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
        LoadHealthDices();
        language = DataSaverAndLoader.LoadLanguage();
        instruments = DataSaverAndLoader.LoadInstruments();


    }

    void initChar(Character character)
    {
        character._name = characterName;
        for (int i=0; i<_classes.Count;i++)
        {
            int subId = 0;
            if (_classes[i].Item2.GetSubClass() == null)
                subId = -1;
            else
                subId = _classes[i].Item2.GetSubClass().id;
            int curHealthDices = 0;
            foreach ((int, int) x in healthDices)
                if (x.Item2 == _classes[i].Item2.id)
                    curHealthDices = x.Item1;
            character._classes.Add((_classes[i].Item1, _classes[i].Item2.id, subId, curHealthDices));
        }

        character._notes = DataSaverAndLoader.LoadNotes();
        character._feats = feats;
        character._charAtr = _attributesArr;
        character._saves = _saves;
        character._money = _money;
        character._skills = _skills;
        character._language = language;
        character._instruments = instruments;
        character._bladeProficiency = bladeProficiency.ToList();
        character._weaponProficiency = weaponProficiency.ToList();
        character._armorProficiency = armorProficiency.ToList();
        character._level = level;
        character._backstoryId = backstory.id;
        character._raceId[0] = race.id;
        if (race.GetSubRace() == null)
            character._raceId[1] = -1;
        else
            character._raceId[1] = race.GetSubRace().id; 
        character._maxHP = maxHP;
        character._tempHP = tempHP;
        character._currentHP = currentHP;
        character._spellCells = DataSaverAndLoader.LoadCellsAmount();
        character._instrumentsComp = DataSaverAndLoader.LoadInstrumentsComp();
        character._spellPrepared = DataSaverAndLoader.LoadSpellPrepared();
        character._spellMaster= DataSaverAndLoader.LoadSpellMaster().ToList();
        character._spellKnew = DataSaverAndLoader.LoadSpellKnew();
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
        race.ChooseSubRace(DataSaverAndLoader.LoadSubRace(race));
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
                    feats.Add(y.id);
                    break;
                }
            }
    }

    void LoadHealthDices()
    {
        foreach((int,PlayersClass) x in _classes)
        {
            healthDices.Add((DataSaverAndLoader.LoadHealthDice(x.Item2.id),x.Item2.id));
        }
    }

    private void OnDestroy()
    {
        CharacterData.load = null;
    }


}
