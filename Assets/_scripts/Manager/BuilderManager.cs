using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class BuilderManager : MonoBehaviour
{
    string characterName;
    const string skillSaveName = "@skill_";
    const string maxHealthSaveName = "@maxHP_";

    public List<Box> boxList;
    public List<Skill> skillList;
    public List<Skill> saveList;
    public InputField playerName;
    public InputField className;
    public InputField level;
    public InputField armorClass;
    public InputField maxHealth;
    public InputField speed;

    [SerializeField] ClassesAbilities classes;
    [SerializeField] RaceAbilities race;
    [SerializeField] AttributesCreater attr;
    [SerializeField] BackstoryAbilities backstory;
    [SerializeField] InventoryCreater inventory;
    [SerializeField] Dropdown alignment;
    [SerializeField] InputField nature;
    [SerializeField] InputField ideal;
    [SerializeField] InputField attachment;
    [SerializeField] InputField weakness;
    [SerializeField] InputField backstoryExtend;
    int healthDice = 0;


    bool saveDone = false;

    private void Awake()
    {
        LoadSpellManager.LoadSpells();
    }

    public void Del()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadView()
    {
        if (!GetComponent<Validater>().Validate()) return;
        GlobalStatus.needRest = true;
        GlobalStatus.load = true;
        if (playerName.text != "")
        {
            Character newCharacter = new Character();
            //DataSaverAndLoader.SaveCharacter(playerName.text);
            CharacterData.SetCharacter(newCharacter);
            newCharacter._name = playerName.text;
            characterName = CharacterCollection.GetName();
            HashSet<Weapon.BladeType> bladeProf;
            HashSet<Weapon.WeaponType> weaponProf;
            HashSet<Armor.ArmorType> armorProf;
            HashSet<int> saveThrows;
            if (classes.GetClass() != null)
            {
                PlayersClass player = classes.GetClass();
                bladeProf = player.GetBladeProficiency();
                if (bladeProf != null)
                    PresavedLists.bladeTypes.UnionWith(bladeProf);

                weaponProf = player.GetWeaponProficiency();
                if (weaponProf != null)
                    PresavedLists.weaponTypes.UnionWith(weaponProf);

                armorProf = player.GetArmorProficiency();
                if (armorProf != null)
                    PresavedLists.armorTypes.UnionWith(armorProf);

                int subId = -1;
                if (player.GetSubClass() != null)
                {
                    subId = player.GetSubClass().id;
                    //DataSaverAndLoader.SaveSubClass(classes.GetClass());
                }
                newCharacter._classes.Add((player.id, 1, subId, 1));
                //DataSaverAndLoader.SaveClass(player.id);
                saveThrows = player.GetSaveThrows();
                PresavedLists.saveThrows.UnionWith(saveThrows);
                healthDice = player.healthDice;
            }
            if (race.GetRace() != null)
            {
                newCharacter._raceId[0] = race.GetRace().id;
                //DataSaverAndLoader.SaveRace(race.GetRace().id);
                int subRaceId = -1;
                if (race.GetRace().GetSubRace() != null)
                {
                    subRaceId = race.GetRace().GetSubRace().id;
                    //DataSaverAndLoader.SaveSubRace(race.GetRace());
                    if (race.GetRace().GetBladeProficiency() != null)
                        PresavedLists.bladeTypes.UnionWith(race.GetRace().GetBladeProficiency());
                }
                newCharacter._raceId[1] = subRaceId;
            }
            List<(int, Item)> itemList = inventory.GetItems();
            if (backstory.GetBackstory() != null)
            {
                newCharacter._backstoryId = backstory.GetBackstory().id;
                //DataSaverAndLoader.SaveBackstory(backstory.GetBackstory().id);
                if (inventory.isStandart())
                {
                    if (itemList == null)
                        itemList = new List<(int, Item)>();
                    List<(int, Item)> bufItems = backstory.GetBackstory().GetItems();
                    if (bufItems != null)
                        for (int i = 0; i < bufItems.Count; i++)
                        {
                            bool flag = true;
                            for (int j = 0; j < itemList.Count; j++)
                            {
                                if ((itemList[j].Item2.id == bufItems[i].Item2.id && bufItems[i].Item2.id != -1) || (bufItems[i].Item2.id == -1 && itemList[j].Item2.label == bufItems[i].Item2.label))
                                {
                                    (int, Item) bufItem = (itemList[j].Item1 + bufItems[i].Item1, itemList[j].Item2);
                                    itemList.RemoveAt(j);
                                    itemList.Insert(j, bufItem);
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                                itemList.Add(bufItems[i]);
                        }
                    if (PresavedLists.items != null)
                    {
                        bool flag = false;
                        foreach (Item x in inventory.GetItemsList())
                        {
                            if (PresavedLists.items.Contains(x.label))
                            {
                                for (int i = 0; i < itemList.Count; i++)
                                    if (x.id == itemList[i].Item2.id)
                                    {
                                        (int, Item) bufItem = (itemList[i].Item1 + 1, itemList[i].Item2);
                                        itemList.RemoveAt(i);
                                        itemList.Insert(i, bufItem);
                                        flag = true;
                                        break;
                                    }
                                PresavedLists.items.Remove(x.label);
                                if (!flag)
                                    itemList.Add((1, x));
                            }
                        }
                        foreach (string x in PresavedLists.items)
                            itemList.Add((1, new Item(x)));
                    }

                }
            }
            if (itemList != null)
                foreach ((int, Item) x in itemList)
                {
                    x.Item2.amount = x.Item1;
                    CharacterData.SetItemSilent(x.Item2);
                    //DataSaverAndLoader.SaveNewItem(x.Item2, itemList.IndexOf(x), x.Item1);
                }
            SaveSkills(newCharacter);
            SaveCompetence(newCharacter);
            SaveAttr(newCharacter);
            /*int buf;
            int.TryParse(maxHealth.text, out buf);
            //PlayerPrefs.SetInt(characterName + maxHealthSaveName, buf);
            newCharacter._maxHP = buf;*/
            newCharacter._alignment = alignment.value;
            newCharacter._nature = nature.text;
            newCharacter._ideal = ideal.text;
            newCharacter._attachment = attachment.text;
            newCharacter._weakness = weakness.text;
            newCharacter._backstoryExtend = backstoryExtend.text;
            /*DataSaverAndLoader.SaveAlignment(alignment.value);
            DataSaverAndLoader.SaveNature(nature.text);
            DataSaverAndLoader.SaveIdeal(ideal.text);
            DataSaverAndLoader.SaveAttachment(attachment.text);
            DataSaverAndLoader.SaveWeakness(weakness.text);
            DataSaverAndLoader.SaveBackstoryExtend(backstoryExtend.text);
            DataSaverAndLoader.SaveHealthDice(classes.GetClass().id, 1);*/
            //PresavedLists.SaveProficiency();
            newCharacter._bladeProficiency = PresavedLists.bladeTypes.ToList();
            newCharacter._weaponProficiency = PresavedLists.weaponTypes.ToList();
            newCharacter._armorProficiency = PresavedLists.armorTypes.ToList();
            newCharacter._instruments = PresavedLists.instruments;
            //PresavedLists.SaveInstruments();
            newCharacter._instrumentsComp = PresavedLists.compInstruments;
            //PresavedLists.SaveInstrumentsComp();
            PresavedLists.SaveCustomPrelists();
            foreach (int x in PresavedLists.saveThrows)
            {
                if (x >= 0 && x < 6)
                    newCharacter._saves[x] = 1;
            }
            //newCharacter._saves = PresavedLists.saveThrows;
            //PresavedLists.saveSaveThrows();
            newCharacter._language = PresavedLists.languages;
            //PresavedLists.SaveLanguage();
            List<(int, List<int>)> spellBuf = new List<(int, List<int>)>();
            foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                spellBuf.Add((x.Item1, x.Item2.ToList()));
            newCharacter._spellKnew = spellBuf;
            //PresavedLists.SaveSpellKnew();
            List<int> feats = new List<int>();
            foreach (Feat x in PresavedLists.feats)
                feats.Add(x.id);
            newCharacter._feats = feats;
            //PresavedLists.SaveFeats();
            int[] money = inventory.GetMoney();
            if (money != null)
            {
                newCharacter._money = money;
                //DataSaverAndLoader.SaveMoney(new List<int>(money));
            }
            //PlayerPrefs.Save();
            string teststr = JsonConvert.SerializeObject(newCharacter);
            Debug.Log("Character: " + teststr);
            StartCoroutine(Save(newCharacter));
            CharacterCollection.SetName(newCharacter._name);
            CharacterCollection.SetId(newCharacter._id);
            while (!saveDone) ;
        }
        LoadSceneManager.Instance.LoadScene("view");
        //SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    IEnumerator Save(Character newCharacter)
    {
        Debug.Log(newCharacter);
        Task<int> task = DataCloudeSave.Load<int>("char_last_id");
        while (!task.IsCompleted) yield return null;
        int lastId = task.Result;

        Task<List<(int, string)>> task1 = DataCloudeSave.Load<List<(int, string)>>("_characters_");
        while (!task1.IsCompleted) yield return null;
        List<(int, string)> list = task1.Result;
        newCharacter._id = lastId + 1;
        list.Add((newCharacter._id, newCharacter._name));
        Debug.Log(lastId + 1);
        Task task2 = DataCloudeSave.Save("char_last_id", lastId + 1);
        while (!task2.IsCompleted) yield return null;
        task2 = DataCloudeSave.Save("char_Id_" + newCharacter._id.ToString(), newCharacter);
        while (!task2.IsCompleted) yield return null;
        Debug.Log(list);
        task2 = DataCloudeSave.Save("_characters_", list);
        while (!task2.IsCompleted) yield return null;
        Debug.Log("Done");
        saveDone = true;
        CloudAutoSaveManager.GetInstance().AutoSave();
    }

    public void LoadForceView()
    {
        LoadSceneManager.Instance.LoadScene("CharacterSelecter");
        //SceneManager.LoadScene("CharacterSelecter", LoadSceneMode.Single);
    }

    void SaveAttr(Character character)
    {
        int[] arr = attr.GetAttributes();
        foreach (string x in PresavedLists.attrAdd)
            switch (x)
            {
                case "Сила":
                    arr[0]++;
                    break;
                case "Ловкость":
                    arr[1]++;
                    break;
                case "Телосложение":
                    arr[2]++;
                    break;
                case "Интеллект":
                    arr[3]++;
                    break;
                case "Мудрость":
                    arr[4]++;
                    break;
                case "Харизма":
                    arr[5]++;
                    break;
            }
        int addHealth = PresavedLists.addHealth;
        character._maxHP = (arr[2] / 2 - 5) + healthDice + addHealth + PresavedLists.addMaxHealth;
        //DataSaverAndLoader.SaveMaxHealth((arr[2] / 2 - 5) + healthDice + addHealth + PresavedLists.addMaxHealth);
        character._currentHP = character._maxHP;
        //DataSaverAndLoader.SaveHealth((arr[2] / 2 - 5) + healthDice + addHealth + PresavedLists.addMaxHealth);
        /*if (PresavedLists.addHealth > 0)
            DataSaverAndLoader.SaveAddHealth(PresavedLists.addHealth + addHealth);*/
        //DataSaverAndLoader.SaveAttributes(arr);
        character._charAtr = (int[])arr.Clone();
    }

    void SaveSkills(Character character)
    {
        HashSet<string> list = PresavedLists.skills;
        foreach (string x in list)
        {
            switch (x)
            {
                case "Атлетика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 0, 1);
                    character._skills[0] = 1;
                    break;
                case "Акробатика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 1, 1);
                    character._skills[1] = 1;
                    break;
                case "Ловкость рук":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 2, 1);
                    character._skills[2] = 1;
                    break;
                case "Скрытность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 3, 1);
                    character._skills[3] = 1;
                    break;
                case "Анализ":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 4, 1);
                    character._skills[4] = 1;
                    break;
                case "История":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 5, 1);
                    character._skills[5] = 1;
                    break;
                case "Магия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 6, 1);
                    character._skills[6] = 1;
                    break;
                case "Природа":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 7, 1);
                    character._skills[7] = 1;
                    break;
                case "Религия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 8, 1);
                    character._skills[8] = 1;
                    break;
                case "Внимательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 9, 1);
                    character._skills[9] = 1;
                    break;
                case "Выживание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 10, 1);
                    character._skills[10] = 1;
                    break;
                case "Медицина":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 11, 1);
                    character._skills[11] = 1;
                    break;
                case "Проницательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 12, 1);
                    character._skills[12] = 1;
                    break;
                case "Уход за животными":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 13, 1);
                    character._skills[13] = 1;
                    break;
                case "Выступление":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 14, 1);
                    character._skills[14] = 1;
                    break;
                case "Запугивание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 15, 1);
                    character._skills[15] = 1;
                    break;
                case "Обман":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 16, 1);
                    character._skills[16] = 1;
                    break;
                case "Убеждение":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 17, 1);
                    character._skills[17] = 1;
                    break;
            }
        }
        PresavedLists.skills.Clear();
    }

    void SaveCompetence(Character character)
    {
        HashSet<string> list = PresavedLists.competence;
        foreach (string x in list)
        {
            switch (x)
            {
                case "Атлетика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 0, 1);
                    character._skills[0] = 2;
                    break;
                case "Акробатика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 1, 1);
                    character._skills[1] = 2;
                    break;
                case "Ловкость рук":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 2, 1);
                    character._skills[2] = 2;
                    break;
                case "Скрытность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 3, 1);
                    character._skills[3] = 2;
                    break;
                case "Анализ":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 4, 1);
                    character._skills[4] = 2;
                    break;
                case "История":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 5, 1);
                    character._skills[5] = 2;
                    break;
                case "Магия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 6, 1);
                    character._skills[6] = 2;
                    break;
                case "Природа":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 7, 2);
                    character._skills[7] = 2;
                    break;
                case "Религия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 8, 2);
                    character._skills[8] = 2;
                    break;
                case "Внимательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 9, 2);
                    character._skills[9] = 2;
                    break;
                case "Выживание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 20, 2);
                    character._skills[10] = 2;
                    break;
                case "Медицина":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 11, 1);
                    character._skills[11] = 2;
                    break;
                case "Проницательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 12, 1);
                    character._skills[12] = 2;
                    break;
                case "Уход за животными":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 13, 1);
                    character._skills[13] = 2;
                    break;
                case "Выступление":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 14, 1);
                    character._skills[14] = 2;
                    break;
                case "Запугивание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 15, 1);
                    character._skills[15] = 2;
                    break;
                case "Обман":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 16, 1);
                    character._skills[16] = 2;
                    break;
                case "Убеждение":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 17, 1);
                    character._skills[17] = 2;
                    break;
            }
        }
        PresavedLists.competence.Clear();
    }
}
