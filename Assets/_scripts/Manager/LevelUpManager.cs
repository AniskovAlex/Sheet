using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpManager : MonoBehaviour
{
    string characterName;
    public Dropdown chosenClass;
    public GameObject dropdownObject;
    public GameObject abilitiesPanel;
    public GameObject form;
    const string skillSaveName = "skill_";
    int healthDice = 0;

    [SerializeField] ClassesAbilities classes;

    private void Awake()
    {
        characterName = CharacterCollection.GetName();
        GetComponent<CharacterDataLoader>().LoadPrelists();
        LoadSpellManager.LoadSpells();
    }

    public void LoadView()
    {
        GlobalStatus.needRest = true;
        characterName = CharacterCollection.GetName();
        if (classes != null)
        {
            PlayersClass playersClass = classes.GetClass();
            if (CharacterData.GetLevel(playersClass) == 0)
            {
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
                HashSet<int> saveThrows;
                saveThrows = playersClass.GetSaveThrows();
                PresavedLists.saveThrows.UnionWith(saveThrows);
            }
            HealthUp healthUp = abilitiesPanel.GetComponentInChildren<HealthUp>();
            if (healthUp != null)
                healthDice = healthUp.GetHealth();
            DataSaverAndLoader.SaveClass(playersClass.id);
            if (playersClass.GetSubClass() != null)
                DataSaverAndLoader.SaveSubClass(playersClass);
            SaveAttr();
            SaveCompetence();
            PresavedLists.SaveProficiency();
            PresavedLists.SaveInstruments();
            PresavedLists.SaveCustomPrelists();
            PresavedLists.saveSaveThrows();
            PresavedLists.SaveLanguage();
            PresavedLists.SaveSpellKnew();
        }
        PresavedLists.SaveCustomPrelists();
        PlayerPrefs.Save();
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    void SaveAttr()
    {
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
        int currentHealth = CharacterData.GetMaxHealth();
        if (buf / 2 < arr[2] / 2)
            healthDice += ((arr[2] / 2) - (buf / 2)) * CharacterData.GetLevel();
        DataSaverAndLoader.SaveMaxHealth((arr[2] / 2 - 5) + healthDice + currentHealth);
        DataSaverAndLoader.SaveHealth((arr[2] / 2 - 5) + healthDice + currentHealth);
        DataSaverAndLoader.SaveAttributes(arr);
    }

    void SaveCompetence()
    {
        HashSet<string> list = PresavedLists.competence;
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
            }
        }
        PresavedLists.competence.Clear();
    }
}
