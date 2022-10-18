using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuilderManager : MonoBehaviour
{
    string characterName;
    const string skillSaveName = "skill_";
    const string maxHealthSaveName = "maxHP_";


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
    [SerializeField] Dropdown alignment;
    [SerializeField] InputField nature;
    [SerializeField] InputField ideal;
    [SerializeField] InputField attachment;
    [SerializeField] InputField weakness;
    [SerializeField] InputField backstoryExtend;

    public void Del()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadView()
    {
        if (playerName.text != "")
        {
            DataSaverAndLoader.SaveCharacter(playerName.text);
            CharacterCollection.SetName(playerName.text);
            characterName = CharacterCollection.GetName();
            HashSet<Weapon.BladeType> bladeProf;
            HashSet<Weapon.WeaponType> weaponProf;
            HashSet<Armor.ArmorType> armorProf;
            HashSet<int> saveThrows;
            if (classes.GetClass() != null)
            {
                bladeProf = classes.GetClass().GetBladeProficiency();
                if (bladeProf != null)
                    PresavedLists.bladeTypes.UnionWith(bladeProf);

                weaponProf = classes.GetClass().GetWeaponProficiency();
                if (weaponProf != null)
                    PresavedLists.weaponTypes.UnionWith(weaponProf);

                armorProf = classes.GetClass().GetArmorProficiency();
                if (armorProf != null)
                    PresavedLists.armorTypes.UnionWith(armorProf);

                DataSaverAndLoader.SaveClass(classes.GetClass().name);
                if (classes.GetClass().GetSubClass() != null)
                    DataSaverAndLoader.SaveSubClass(classes.GetClass());
                saveThrows = classes.GetClass().GetSaveThrows();
                PresavedLists.saveThrows.UnionWith(saveThrows);
            }
            if (race.GetRace() != null)
            {
                DataSaverAndLoader.SaveRace(race.GetRace().name);
                if (race.GetRace().GetSubRace() != null)
                    DataSaverAndLoader.SaveSubRace(race.GetRace());
            }
            if (backstory.GetBackstory() != null)
            {
                DataSaverAndLoader.SaveBackstory(backstory.GetBackstory().name);
            }
            SaveSkills();
            int buf;
            int.TryParse(maxHealth.text, out buf);
            PlayerPrefs.SetInt(characterName + maxHealthSaveName, buf);
            DataSaverAndLoader.SaveAlignment(alignment.value);
            DataSaverAndLoader.SaveNature(nature.text);
            DataSaverAndLoader.SaveIdeal(ideal.text);
            DataSaverAndLoader.SaveAttachment(attachment.text);
            DataSaverAndLoader.SaveWeakness(weakness.text);
            DataSaverAndLoader.SaveBackstoryExtend(backstoryExtend.text);
            SaveAttr();
            PresavedLists.SaveProficiency();
            PresavedLists.SaveInstruments();
            PresavedLists.SaveCustomPrelists();
            PresavedLists.saveSaveThrows();
            PresavedLists.SaveLanguage();
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    void SaveAttr()
    {
        int[] arr= attr.GetAttributes();
        foreach(string x in PresavedLists.attrAdd)
            switch (x){
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
        DataSaverAndLoader.SaveAttributes(arr);
    }

    void SaveSkills()
    {
        HashSet<string> list = PresavedLists.skills;
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
        PresavedLists.skills.Clear();
    }
}
