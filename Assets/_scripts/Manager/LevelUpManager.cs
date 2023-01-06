using System.Collections;
using System.Linq;
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
    const string skillSaveName = "@skill_";
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
            SaveSkills();
            SaveCompetence();
            PresavedLists.SaveProficiency();
            PresavedLists.SaveInstruments();
            PresavedLists.SaveInstrumentsComp();
            PresavedLists.saveSaveThrows();
            PresavedLists.SaveLanguage();
            PresavedLists.SaveSpellKnew();
            PresavedLists.SaveCustomPrelists();
            PresavedLists.SaveFeats();
        }
        if (PresavedLists.spellMaster != null || PresavedLists.spellMaster.Count > 0)
        {
            HashSet<int> buf = DataSaverAndLoader.LoadSpellMaster();
            if (buf != null)
                DataSaverAndLoader.SaveSpellMaster(new HashSet<int>(buf.Concat(PresavedLists.spellMaster).ToList()));
            else
                DataSaverAndLoader.SaveSpellMaster(PresavedLists.spellMaster);
        }
        PlayerPrefs.Save();

        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    public void LoadForceView()
    {
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    void SaveAttr()
    {
        int[] arr = CharacterData.GetAtribute();
        int buf = arr[2];
        foreach (string x in PresavedLists.attrAdd)
            switch (x)
            {
                case "����":
                    arr[0]++;
                    break;
                case "��������":
                    arr[1]++;
                    break;
                case "������������":
                    arr[2]++;
                    break;
                case "���������":
                    arr[3]++;
                    break;
                case "��������":
                    arr[4]++;
                    break;
                case "�������":
                    arr[5]++;
                    break;
            }
        int currentHealth = CharacterData.GetMaxHealth();
        int addHealth = DataSaverAndLoader.LoadAddHealth();
        if (buf / 2 < arr[2] / 2)
            healthDice += ((arr[2] / 2) - (buf / 2)) * CharacterData.GetLevel();
        DataSaverAndLoader.SaveMaxHealth((arr[2] / 2 - 5) + healthDice + currentHealth + addHealth);
        DataSaverAndLoader.SaveHealth((arr[2] / 2 - 5) + healthDice + currentHealth + addHealth);
        DataSaverAndLoader.SaveAttributes(arr);
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

    void SaveCompetence()
    {
        HashSet<string> list = PresavedLists.competence;
        foreach (string x in list)
        {
            switch (x)
            {
                case "��������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 0, 2);
                    break;
                case "����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 1, 2);
                    break;
                case "�������� ���":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 2, 2);
                    break;
                case "����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 3, 2);
                    break;
                case "������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 4, 2);
                    break;
                case "�������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 5, 2);
                    break;
                case "�����":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 6, 2);
                    break;
                case "�������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 7, 2);
                    break;
                case "�������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 8, 2);
                    break;
                case "��������������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 9, 2);
                    break;
                case "���������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 10, 2);
                    break;
                case "��������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 11, 2);
                    break;
                case "����������������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 12, 2);
                    break;
                case "���� �� ���������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 13, 2);
                    break;
                case "�����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 14, 2);
                    break;
                case "�����������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 15, 2);
                    break;
                case "�����":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 16, 2);
                    break;
                case "���������":
                    PlayerPrefs.SetInt(characterName + skillSaveName + 17, 2);
                    break;
                case "��������� �����������":
                    PresavedLists.compInstruments.Add(x);
                    break;
            }
        }
        PresavedLists.competence.Clear();
    }
}
