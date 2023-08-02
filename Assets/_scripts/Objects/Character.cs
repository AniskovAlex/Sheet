using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting;

public class Character
{
    [Preserve] public int _id;
    [Preserve] public string _name;
    [Preserve] public int _level;

    [Preserve] public List<(int, int, int, int)> _classes = new List<(int, int, int, int)>(); // 0 - id класса, 1 - уровень, 2 - id подкласс, 3 - текущие кости хитов
    [Preserve] List<(int, PlayersClass)> classes = new List<(int, PlayersClass)>();

    [Preserve] public List<(string, string)> _notes = new List<(string, string)>();

    [Preserve] public List<Item> _items = new List<Item>();
    [Preserve] public List<Weapon> _weapon = new List<Weapon>();
    [Preserve] public List<Armor> _armor = new List<Armor>();

    [Preserve] public List<(int, string)> _weaponEquip = new List<(int, string)>();
    [Preserve] public List<(int, string)> _armorEquip = new List<(int, string)>();

    [Preserve] public int[] _raceId = new int[2]; // 0 - id расы, 1 - id подрасы
    [Preserve] Race _race;

    [Preserve] public int _backstoryId;
    [Preserve] Backstory _backstory;

    [Preserve] public int _maxHP;
    [Preserve] public int _currentHP;
    [Preserve] public int _tempHP;

    [Preserve] public int[] _charAtr = new int[6];
    [Preserve] public int[] _saves = new int[6];
    [Preserve] public int[] _charModifier = new int[6];

    [Preserve] public int[] _money = new int[3];
    [Preserve] public int[] _skills = new int[18];

    [Preserve] public int[] _spellCells = new int[9];

    [Preserve] public int profMod;


    //public List<(int, PlayersClass)> _classes;
    [Preserve] public List<Weapon.BladeType> _bladeProficiency = new List<Weapon.BladeType>();
    [Preserve] public List<Weapon.WeaponType> _weaponProficiency = new List<Weapon.WeaponType>();
    [Preserve] public List<Armor.ArmorType> _armorProficiency = new List<Armor.ArmorType>();
    [Preserve] public HashSet<string> _language = new HashSet<string>();
    [Preserve] public HashSet<string> _instruments = new HashSet<string>();
    [Preserve] public HashSet<string> _instrumentsComp = new HashSet<string>();
    [Preserve] public List<int> _feats = new List<int>();

    [Preserve] public List<(int, List<int>)> _spellKnew = new List<(int, List<int>)>();
    [Preserve] public List<(int, List<int>)> _spellPrepared = new List<(int, List<int>)>();
    [Preserve] public List<int> _spellMaster = new List<int>();
    
    [Preserve] public int _alignment = 0;
    [Preserve] public string _nature = "";
    [Preserve] public string _ideal = "";
    [Preserve] public string _attachment = "";
    [Preserve] public string _weakness = "";
    [Preserve] public string _backstoryExtend = "";
    
    [Preserve] public List<(string, List<int>)> _customLists = new List<(string, List<int>)>();
    [Preserve] public Dictionary<string, int> _consum = new Dictionary<string, int>();

    public void Init()
    {
        _level = 0;
        for (int i = 0; i < _classes.Count; i++)
        {
            PlayersClass playersClass = null;
            switch (_classes[i].Item1)
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
            classes.Add((_classes[i].Item2, playersClass));
            playersClass.ChooseSubClass(_classes[i].Item3);
            _level += _classes[i].Item2;
        }
        switch (_raceId[0])
        {
            case 0:
                _race = new Gnome();
                break;
            case 1:
                _race = new Dwarf();
                break;
            case 2:
                _race = new Dragonborn();
                break;
            case 3:
                _race = new HalfOrc();
                break;
            case 4:
                _race = new Halfling();
                break;
            case 5:
                _race = new HalfElf();
                break;
            case 6:
                _race = new Tiefling();
                break;
            case 7:
                _race = new Human();
                break;
            case 8:
                _race = new Elf();
                break;
            default:
                break;
        }
        if (_race != null) _race.ChooseSubRace(_raceId[1]);
        switch (_backstoryId)
        {
            case 0:
                _backstory = new Artist();
                break;
            case 1:
                _backstory = new Waif();
                break;
            case 2:
                _backstory = new Noble();
                break;
            case 3:
                _backstory = new GuildArtiser();
                break;
            case 4:
                _backstory = new Sailor();
                break;
            case 5:
                _backstory = new Sage();
                break;
            case 6:
                _backstory = new PeoplesHero();
                break;
            case 7:
                _backstory = new Hermit();
                break;
            case 8:
                _backstory = new Criminal();
                break;
            case 9:
                _backstory = new Acolyte();
                break;
            case 10:
                _backstory = new Soldier();
                break;
            case 11:
                _backstory = new Foreigner();
                break;
            case 12:
                _backstory = new Charlatan();
                break;
        }
        profMod = (_level - 1) / 4 + 2;
        for (int i = 0; i < _charAtr.Length; i++)
            _charModifier[i] = _charAtr[i] / 2 - 5;

        foreach (int x in _feats)
            foreach (Feat y in FileSaverAndLoader.LoadFeats())
            {
                if (y.id == x)
                {
                    PresavedLists.feats.Add(y);
                    break;
                }
            }
    }
    [Preserve]
    public Race GetRace()
    {
        return _race;
    }
    [Preserve]
    public Backstory GetBackstory()
    {
        return _backstory;
    }
    [Preserve]
    public List<(int, PlayersClass)> GetClasses()
    {
        return classes;
    }

    /*public Character(int[] charAtr, int[] saves, int[] money, int[] skills, List<(int, PlayersClass)> classes, HashSet<string> language, HashSet<string> instruments, HashSet<Weapon.BladeType> bladeProf, HashSet<Weapon.WeaponType> weaponProf, HashSet<Armor.ArmorType> armorProf,
        int level, Race race, Backstory backstory, int maxHP, int currentHP, int tempHP)
    {
        _charAtr = charAtr;
        _saves = saves;
        _skills = skills;
        _money = money;
        _level = level;
        .._classes = new List<(int, int)>();
        foreach ((int, PlayersClass) x in classes)
            _classes.Add((5, 5));
        _raceId = 5;
        _backstoryId = 5;
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
}
