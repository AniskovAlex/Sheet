using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class SelecterManager : MonoBehaviour
{

    const string charactersCountSaveName = "@charactersCount_";
    const string charactersSaveName = "@characters_";
    const string levelCountSaveName = "@lvlCount_";
    const string levelSaveName = "@lvl_";
    const string levelLabelSaveName = "@lvlLabel_";

    public GameObject panel;
    public GameObject prefab;

    List<(int, string)> characters = new List<(int, string)>();
    CloudSaveObj saveObj = new CloudSaveObj();

    [SerializeField] PopoutConfirm confirm;

    void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        LoadSceneManager.Instance.SetLoadScreen();
        CharacterCollection.SetId(0);
        CharacterCollection.SetName("");
        StartCoroutine(Auth());
    }

    async Task DeleteData()
    {
        characters = await DataCloudeSave.Load<List<(int, string)>>("_characters_");
        foreach ((int, string) x in new List<(int, string)>(characters))
        {
            DataCloudeSave.Delete("char_Id_" + x.Item1);
            characters.Remove(x);
        }
        await DataCloudeSave.Save("_characters_", characters);
    }


    async Task Load()
    {
        Debug.Log("Begging load!!!");
        characters = await DataCloudeSave.Load<List<(int, string)>>("_characters_");
        Debug.Log("End load!!!");
    }

    public IEnumerator Auth()
    {
        Task task = DataCloudeSave.Auth();
        while (!task.IsCompleted) yield return null;

        /*task = DeleteData();
        while (!task.IsCompleted) yield return null;*/

        task = Load();
        while (!task.IsCompleted) yield return null;

        CloudAutoSaveManager.GetInstance().init = true;

        //task = CloudAutoSaveManager.GetInstance().SyncSaves(characters);
        while (!task.IsCompleted) yield return null;

        task = Init();
        while (!task.IsCompleted) yield return null;

        panel.GetComponent<ContentSizer>().HieghtSizeInit();
        LoadSceneManager.Instance.HideLoadScreen();
    }


    async Task Init()
    {
        if (characters != null && characters.Count > 0)
            foreach ((int, string) x in characters)
            {
                GameObject newObject = GameObject.Instantiate(prefab, panel.transform);
                Character character = await DataCloudeSave.Load<Character>("char_Id_" + x.Item1.ToString());
                Debug.Log(character._id + " " + character._name);
                character.Init();
                Debug.Log(character._id + " " + character._name + "aaaaaaaaaaaaaa");
                //int countClasses = PlayerPrefs.GetInt(charName + levelCountSaveName);
                int level = character._level;
                List<(int, PlayersClass)> classes = character.GetClasses();

                string className = "";
                if (classes.Count > 0)
                    className = classes[0].Item2.name;
                if (classes.Count > 1)
                    className += " +" + (classes.Count - 1 - 1);
                Debug.Log(x.Item2 + " " + x.Item1);
                newObject.GetComponent<CharacterTab>().SetCharacter(x.Item2, className, level, x.Item1);
            }
        else
            await OldInit();
    }
    async Task OldInit()
    {
        List<(int, string)> newCharacters = new List<(int, string)>();
        int count = PlayerPrefs.GetInt(charactersCountSaveName);
        Debug.Log("Begging InitOld" + count);
        for (int i = count; i > 0; i--)
        {
            string charName = PlayerPrefs.GetString(charactersSaveName + i);
            Debug.Log(charName);
            GameObject newObject = GameObject.Instantiate(prefab, panel.transform);
            int countClasses = PlayerPrefs.GetInt(charName + levelCountSaveName);
            int level = 0;
            string className = "";
            Debug.Log("Init " + charName);
            for (int j = 0; j < countClasses; j++)
            {
                if (PlayerPrefs.HasKey(charName + levelSaveName + j))
                {
                    int classLevel = PlayerPrefs.GetInt(charName + levelSaveName + j);
                    if (j == 0)
                        switch (PlayerPrefs.GetInt(charName + levelLabelSaveName + j))
                        {
                            case 0:
                                className = new Bard().name;
                                break;
                            case 1:
                                className = new Barbarian().name;
                                break;
                            case 2:
                                className = new Fighter().name;
                                break;
                            case 3:
                                className = new Wizard().name;
                                break;
                            case 4:
                                className = new Druid().name;
                                break;
                            case 5:
                                className = new Cleric().name;
                                break;
                            case 7:
                                className = new Warlock().name;
                                break;
                            case 8:
                                className = new Monk().name;
                                break;
                            case 9:
                                className = new Paladin().name;
                                break;
                            case 10:
                                className = new Rogue().name;
                                break;
                            case 11:
                                className = new Ranger().name;
                                break;
                            case 12:
                                className = new Sorcerer().name;
                                break;
                        }
                    level += classLevel;
                }
            }
            if (countClasses > 1)
                className += " +" + (countClasses - 1);
            await LoadOldChar(charName);
            newObject.GetComponent<CharacterTab>().SetCharacter(charName, className, level, lastId);

            newCharacters.Add((lastId, charName));
        }
        DataCloudeSave.Save("_characters_", newCharacters);
    }

    async Task LoadOldChar(string characterName)
    {
        Debug.Log("Load " + characterName);
        this.characterName = characterName;
        //Task<Character> task = DataCloudeSave.Load<Character>(characterName);
        //await task;
        Character character = null;
        /*if (task.IsCompletedSuccessfully)
            character = task.Result;*/
        if (character == null)
        {

            //character = new Character(_attributesArr, _saves, _money, _skills, _classes, language, instruments, bladeProficiency, weaponProficiency, armorProficiency, level, race, backstory, maxHP, currentHP, tempHP);
            character = new Character();
            CharacterCollection.SetName(characterName);
            LoadOldSave();
            lastId = await DataCloudeSave.Load<int>("char_last_id");
            initChar(character);
            await DataCloudeSave.Save("char_Id_" + character._id.ToString(), character);
            await DataCloudeSave.Save("char_last_id", character._id);
            lastId++;
        }
        character.Init();
        CharacterData.SetCharacter(character);
        Debug.Log(characterName + "is Loaded!");
    }

    void LoadOldSave()
    {
        ResetAll();
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

    public async void DeleteAllCloudSaves()
    {
        await DataCloudeSave.DeleteAll();
        LoadSceneManager.Instance.LoadScene("CharacterSelecter");
    }

    void ResetAll()
    {
        //characterName = "";
        _classes.Clear();
        feats.Clear();
        _attributesArr = new int[6];
        _saves = new int[6];
        _money = new int[3];
        _skills = new int[18];
        language.Clear();
        instruments.Clear();
        bladeProficiency.Clear();
        weaponProficiency.Clear();
        armorProficiency.Clear();
        level = 0;
        backstory = null;
        race = null;
        maxHP = 0;
        tempHP = 0;
        currentHP = 0;
    }

    void initChar(Character character)
    {
        character._name = characterName;
        for (int i = 0; i < _classes.Count; i++)
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
            character._classes.Add((_classes[i].Item2.id, _classes[i].Item1, subId, curHealthDices));
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
        character._spellMaster = DataSaverAndLoader.LoadSpellMaster().ToList();
        character._spellKnew = DataSaverAndLoader.LoadSpellKnew();
        character._alignment = DataSaverAndLoader.LoadAlignment();
        character._nature = DataSaverAndLoader.LoadNature();
        character._ideal = DataSaverAndLoader.LoadIdeal();
        character._attachment = DataSaverAndLoader.LoadAttachment();
        character._weakness = DataSaverAndLoader.LoadWeakness();
        character._backstoryExtend = DataSaverAndLoader.LoadBackstoryExtend();
        character._id = lastId + 1;
    }

    public void ConfiermDelete(CharacterTab character)
    {
        confirm.Show(character);
    }

    public void DeleteCharacter(CharacterTab character)
    {
        //DataSaverAndLoader.DeleteCharacter(character.GetName());
        foreach ((int, string) x in new List<(int, string)>(characters))
        {
            if (x.Item1 == character.GetId())
            {
                DataCloudeSave.Delete("char_Id_" + x.Item1);
                characters.Remove(x);
                DataCloudeSave.Save("_characters_", characters);
            }
        }
        Destroy(character.gameObject);
    }

    public void LoadCharacter(string name, int id)
    {
        CharacterCollection.SetId(id);
        CharacterCollection.SetName(name);
        LoadSceneManager.Instance.LoadScene("view");
        //SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    public void LoadBuilder()
    {
        LoadSceneManager.Instance.LoadScene("CharacterBuilder");
        //SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Single);
    }

    const string attrSaveName = "@atr_";
    const string moneySaveName = "@mon_";
    const string skillSaveName = "@skill_";
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
    List<(int, int)> healthDices = new List<(int, int)>();
    List<int> feats = new List<int>();
    int maxHP;
    int currentHP;
    int tempHP;
    int lastId = 0;

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
        foreach ((int, PlayersClass) x in _classes)
        {
            healthDices.Add((DataSaverAndLoader.LoadHealthDice(x.Item2.id), x.Item2.id));
        }
    }
}
