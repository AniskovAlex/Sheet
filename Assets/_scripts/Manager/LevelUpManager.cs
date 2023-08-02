using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LevelUpManager : MonoBehaviour
{
    string characterName;
    public Dropdown chosenClass;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;
    const string skillSaveName = "@skill_";
    int healthDice = 0;

    [SerializeField] ClassesAbilities classes;

    private void Awake()
    {
        characterName = CharacterCollection.GetName();
        CharacterData.InitPrelist();
        //GetComponent<CharacterDataLoader>().LoadPrelists();
        LoadSpellManager.LoadSpells();
    }

    public void LoadView()
    {
        if (!GetComponent<Validater>().Validate()) return;
        GlobalStatus.needRest = true;
        GlobalStatus.load = true;
        characterName = CharacterCollection.GetName();

        if (classes != null)
        {
            PlayersClass playersClass = classes.GetClass();

            bool flag = false;
            if (playersClass != null)
            {
                int subId = -1;
                if (playersClass.GetSubClass() != null)
                {
                    subId = playersClass.GetSubClass().id;
                    //DataSaverAndLoader.SaveSubClass(playersClass);
                }

                List<(int, int, int, int)> updClasses = CharacterData.GetRawClasses();
                foreach ((int, int, int, int) x in updClasses)
                {
                    if (x.Item1 == playersClass.id)
                    {
                        int index = updClasses.IndexOf(x);
                        updClasses.Remove(x);
                        updClasses.Insert(index, (x.Item1, x.Item2 + 1, subId, x.Item4 + 1));
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                    updClasses.Add((playersClass.id, 1, subId, 1));
            }
            flag = false;
            //DataSaverAndLoader.SaveClass(playersClass.id);

            HashSet<Weapon.BladeType> bladeProf;
            HashSet<Weapon.WeaponType> weaponProf;
            HashSet<Armor.ArmorType> armorProf;
            bladeProf = playersClass.GetBladeProficiency();
            if (bladeProf != null)
                PresavedLists.bladeTypes.UnionWith(bladeProf);

            weaponProf = playersClass.GetWeaponProficiency();
            if (weaponProf != null)
                PresavedLists.weaponTypes.UnionWith(weaponProf);

            armorProf = playersClass.GetArmorProficiency();
            if (armorProf != null)
                PresavedLists.armorTypes.UnionWith(armorProf);
            HealthUp healthUp = abilitiesPanel.GetComponentInChildren<HealthUp>();
            if (healthUp != null)
                healthDice = healthUp.GetHealth();
            //DataSaverAndLoader.SaveHealthDice(playersClass.id, CharacterData.GetLevel(playersClass.id) + 1);
            ChangeChosen[] changeChosens = classes.GetComponentsInChildren<ChangeChosen>();
            foreach (ChangeChosen x in changeChosens)
            {
                PresavedLists.RemoveFromPrelist(x.GetAbility().listName, x.GetRemoveID());
            }
            SaveAttr();
            SaveSkills();
            SaveCompetence();
            //SaveSpacialSpells();
            CharacterData.GetArmorProficiency().UnionWith(PresavedLists.armorTypes);
            CharacterData.GetWeaponProficiency().UnionWith(PresavedLists.weaponTypes);
            CharacterData.GetBladeProficiency().UnionWith(PresavedLists.bladeTypes);
            //PresavedLists.SaveProficiency();

            CharacterData.GetInstruments().UnionWith(PresavedLists.instruments);
            //PresavedLists.SaveInstruments();
            CharacterData.GetInstrumentsComp().UnionWith(PresavedLists.compInstruments);
            //PresavedLists.SaveInstrumentsComp();
            int[] saves = CharacterData.GetSave();
            foreach (int x in PresavedLists.saveThrows)
                saves[x] = 1;
            //PresavedLists.saveSaveThrows();
            CharacterData.GetLanguage().UnionWith(PresavedLists.languages);
            //PresavedLists.SaveLanguage();
            foreach ((int, List<int>) x in new List<(int, List<int>)>(CharacterData.GetSpellsKnew()))
            {
                PresavedLists.spellKnew.ForEach(g =>
                {
                    if (g.Item1 == x.Item1)
                        CharacterData.SetSpellKnew(x.Item1, x.Item2.Union(g.Item2.ToList()).ToList());
                });
            }
            //PresavedLists.SaveSpellKnew();

            PresavedLists.SaveCustomPrelists();
            List<int> list = new List<int>();
            foreach (Feat x in PresavedLists.feats)
                list.Add(x.id);
            CharacterData.SetFeats(CharacterData.GetFeats().Union<int>(list).ToList());
            //PresavedLists.SaveFeats();

            CharacterData.GetSpellMaster().UnionWith(PresavedLists.spellMaster);

            /*if (PresavedLists.spellMaster != null || PresavedLists.spellMaster.Count > 0)
            {
                HashSet<int> buf = DataSaverAndLoader.LoadSpellMaster();
                if (buf != null)
                    DataSaverAndLoader.SaveSpellMaster(new HashSet<int>(buf.Concat(PresavedLists.spellMaster).ToList()));
                else
                    DataSaverAndLoader.SaveSpellMaster(PresavedLists.spellMaster);
            }*/

            StartCoroutine(SaveChar());

        }
        //PlayerPrefs.Save();
        LoadSceneManager.Instance.LoadScene("view");
        //SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    IEnumerator SaveChar()
    {
        Task task = CharacterData.SaveCharacter();
        while (!task.IsCompleted) yield return null;
        CloudAutoSaveManager.GetInstance().AutoSave();
    }

    private void SaveSpacialSpells()
    {

    }

    public void LoadForceView()
    {
        LoadSceneManager.Instance.LoadScene("view");
        //SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    void SaveAttr()
    {
        /*int[] arr = CharacterData.GetAtribute();
        int buf = arr[2];
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
        int currentHealth = CharacterData.GetMaxHealth();
        int addHealth = DataSaverAndLoader.LoadAddHealth();
        if (buf / 2 < arr[2] / 2)
            healthDice += ((arr[2] / 2) - (buf / 2)) * CharacterData.GetLevel();
        DataSaverAndLoader.SaveMaxHealth((arr[2] / 2 - 5) + healthDice + currentHealth + addHealth);
        DataSaverAndLoader.SaveHealth((arr[2] / 2 - 5) + healthDice + currentHealth + addHealth);
        DataSaverAndLoader.SaveAttributes(arr);*/
        int[] arr = CharacterData.GetAtribute();
        int buf = arr[2];
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
        if (buf / 2 < arr[2] / 2)
            healthDice += ((arr[2] / 2) - (buf / 2)) * CharacterData.GetLevel();
        CharacterData.SetMaxHealth((arr[2] / 2 - 5) + healthDice + addHealth + PresavedLists.addMaxHealth + CharacterData.GetMaxHealth());
        //DataSaverAndLoader.SaveMaxHealth((arr[2] / 2 - 5) + healthDice + addHealth + PresavedLists.addMaxHealth);
        CharacterData.SetCurrentHealthSilent(CharacterData.GetMaxHealth());
        //DataSaverAndLoader.SaveHealth((arr[2] / 2 - 5) + healthDice + addHealth + PresavedLists.addMaxHealth);
        /*if (PresavedLists.addHealth > 0)
            DataSaverAndLoader.SaveAddHealth(PresavedLists.addHealth + addHealth);*/
        //DataSaverAndLoader.SaveAttributes(arr);
    }
    void SaveSkills()
    {
        /*HashSet<string> list = PresavedLists.skills;
        foreach (string x in list)
        {
            switch (x)
            {
                case "Атлетика":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 0, 1);
                    break;
                case "Акробатика":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 1, 1);
                    break;
                case "Ловкость рук":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 2, 1);
                    break;
                case "Скрытность":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 3, 1);
                    break;
                case "Анализ":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 4, 1);
                    break;
                case "История":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 5, 1);
                    break;
                case "Магия":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 6, 1);
                    break;
                case "Природа":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 7, 1);
                    break;
                case "Религия":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 8, 1);
                    break;
                case "Внимательность":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 9, 1);
                    break;
                case "Выживание":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 10, 1);
                    break;
                case "Медицина":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 11, 1);
                    break;
                case "Проницательность":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 12, 1);
                    break;
                case "Уход за животными":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 13, 1);
                    break;
                case "Выступление":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 14, 1);
                    break;
                case "Запугивание":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 15, 1);
                    break;
                case "Обман":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 16, 1);
                    break;
                case "Убеждение":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 17, 1);
                    break;
            }
        }
        PresavedLists.skills.Clear();*/
        HashSet<string> list = PresavedLists.skills;
        int[] skills = CharacterData.GetSkill();
        foreach (string x in list)
        {
            switch (x)
            {
                case "Атлетика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 0, 1);
                    skills[0] = 1;
                    break;
                case "Акробатика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 1, 1);
                    skills[1] = 1;
                    break;
                case "Ловкость рук":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 2, 1);
                    skills[2] = 1;
                    break;
                case "Скрытность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 3, 1);
                    skills[3] = 1;
                    break;
                case "Анализ":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 4, 1);
                    skills[4] = 1;
                    break;
                case "История":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 5, 1);
                    skills[5] = 1;
                    break;
                case "Магия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 6, 1);
                    skills[6] = 1;
                    break;
                case "Природа":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 7, 1);
                    skills[7] = 1;
                    break;
                case "Религия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 8, 1);
                    skills[8] = 1;
                    break;
                case "Внимательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 9, 1);
                    skills[9] = 1;
                    break;
                case "Выживание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 10, 1);
                    skills[10] = 1;
                    break;
                case "Медицина":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 11, 1);
                    skills[11] = 1;
                    break;
                case "Проницательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 12, 1);
                    skills[12] = 1;
                    break;
                case "Уход за животными":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 13, 1);
                    skills[13] = 1;
                    break;
                case "Выступление":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 14, 1);
                    skills[14] = 1;
                    break;
                case "Запугивание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 15, 1);
                    skills[15] = 1;
                    break;
                case "Обман":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 16, 1);
                    skills[16] = 1;
                    break;
                case "Убеждение":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 17, 1);
                    skills[17] = 1;
                    break;
            }
        }
        PresavedLists.skills.Clear();
    }

    void SaveCompetence()
    {
        /*HashSet<string> list = PresavedLists.competence;
        foreach (string x in list)
        {
            switch (x)
            {
                case "Атлетика":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 0, 2);
                    break;
                case "Акробатика":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 1, 2);
                    break;
                case "Ловкость рук":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 2, 2);
                    break;
                case "Скрытность":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 3, 2);
                    break;
                case "Анализ":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 4, 2);
                    break;
                case "История":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 5, 2);
                    break;
                case "Магия":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 6, 2);
                    break;
                case "Природа":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 7, 2);
                    break;
                case "Религия":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 8, 2);
                    break;
                case "Внимательность":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 9, 2);
                    break;
                case "Выживание":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 10, 2);
                    break;
                case "Медицина":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 11, 2);
                    break;
                case "Проницательность":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 12, 2);
                    break;
                case "Уход за животными":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 13, 2);
                    break;
                case "Выступление":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 14, 2);
                    break;
                case "Запугивание":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 15, 2);
                    break;
                case "Обман":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 16, 2);
                    break;
                case "Убеждение":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 17, 2);
                    break;
                case "Воровские инстурменты":
                    PresavedLists.compInstruments.Add(x);
                    break;
            }
        }
        PresavedLists.competence.Clear();*/
        HashSet<string> list = PresavedLists.competence;
        int[] skills = CharacterData.GetSkill();
        foreach (string x in list)
        {
            switch (x)
            {
                case "Атлетика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 0, 1);
                    skills[0] = 2;
                    break;
                case "Акробатика":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 1, 1);
                    skills[1] = 2;
                    break;
                case "Ловкость рук":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 2, 1);
                    skills[2] = 2;
                    break;
                case "Скрытность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 3, 1);
                    skills[3] = 2;
                    break;
                case "Анализ":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 4, 1);
                    skills[4] = 2;
                    break;
                case "История":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 5, 1);
                    skills[5] = 2;
                    break;
                case "Магия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 6, 1);
                    skills[6] = 2;
                    break;
                case "Природа":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 7, 2);
                    skills[7] = 2;
                    break;
                case "Религия":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 8, 2);
                    skills[8] = 2;
                    break;
                case "Внимательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 9, 2);
                    skills[9] = 2;
                    break;
                case "Выживание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 20, 2);
                    skills[10] = 2;
                    break;
                case "Медицина":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 11, 1);
                    skills[11] = 2;
                    break;
                case "Проницательность":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 12, 1);
                    skills[12] = 2;
                    break;
                case "Уход за животными":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 13, 1);
                    skills[13] = 2;
                    break;
                case "Выступление":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 14, 1);
                    skills[14] = 2;
                    break;
                case "Запугивание":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 15, 1);
                    skills[15] = 2;
                    break;
                case "Обман":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 16, 1);
                    skills[16] = 2;
                    break;
                case "Убеждение":
                    //PlayerPrefs.SetInt(characterName + skillSaveName + 17, 1);
                    skills[17] = 2;
                    break;
            }
        }
        PresavedLists.competence.Clear();
    }
}
