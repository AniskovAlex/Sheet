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
    [SerializeField] BackstoryAbilities backstory;

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
            if (classes != null)
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
            if (race != null)
            {
                DataSaverAndLoader.SaveRace(race.GetRace().name);
                if (race.GetRace().GetSubRace() != null)
                    DataSaverAndLoader.SaveSubRace(race.GetRace());
            }
            if (backstory != null)
            {
                DataSaverAndLoader.SaveBackstory(backstory.GetBackstory().name);
            }
            SaveSkills();
            int buf;
            int.TryParse(maxHealth.text, out buf);
            PlayerPrefs.SetInt(characterName + maxHealthSaveName, buf);
            PresavedLists.SaveProficiency();
            PresavedLists.SaveInstruments();
            PresavedLists.SaveCustomPrelists();
            PresavedLists.saveSaveThrows();
            PresavedLists.SaveLanguage();
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    void SaveSkills()
    {
        HashSet<string> list = PresavedLists.skills;
        foreach (string x in list)
        {
            switch (x)
            {
                case "��������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 0, 1);
                    break;
                case "����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 1, 1);
                    break;
                case "�������� ���":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 2, 1);
                    break;
                case "����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 3, 1);
                    break;
                case "������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 4, 1);
                    break;
                case "�������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 5, 1);
                    break;
                case "�����":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 6, 1);
                    break;
                case "�������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 7, 1);
                    break;
                case "�������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 8, 1);
                    break;
                case "��������������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 9, 1);
                    break;
                case "���������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 10, 1);
                    break;
                case "��������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 11, 1);
                    break;
                case "����������������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 12, 1);
                    break;
                case "���� �� ���������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 13, 1);
                    break;
                case "�����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 14, 1);
                    break;
                case "�����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 15, 1);
                    break;
                case "�����":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 16, 1);
                    break;
                case "���������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 17, 1);
                    break;
            }
        }
        PresavedLists.skills.Clear();
    }
}
