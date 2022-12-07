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
    [SerializeField] InventoryCreater inventory;
    [SerializeField] Dropdown alignment;
    [SerializeField] InputField nature;
    [SerializeField] InputField ideal;
    [SerializeField] InputField attachment;
    [SerializeField] InputField weakness;
    [SerializeField] InputField backstoryExtend;
    int healthDice = 0;

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
        GlobalStatus.needRest = true;
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

                DataSaverAndLoader.SaveClass(player.id);
                if (player.GetSubClass() != null)
                    DataSaverAndLoader.SaveSubClass(classes.GetClass());
                saveThrows = player.GetSaveThrows();
                PresavedLists.saveThrows.UnionWith(saveThrows);
                healthDice = player.healthDice;
            }
            if (race.GetRace() != null)
            {
                DataSaverAndLoader.SaveRace(race.GetRace().id);
                if (race.GetRace().GetSubRace() != null)
                    DataSaverAndLoader.SaveSubRace(race.GetRace());
            }
            if (backstory.GetBackstory() != null)
            {
                DataSaverAndLoader.SaveBackstory(backstory.GetBackstory().id);
            }
            SaveSkills();
            SaveCompetence();
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
            PresavedLists.SaveSpellKnew();
            List<(int, Item)> itemList = inventory.GetItems();
            if (itemList != null)
                foreach ((int, Item) x in itemList)
                    DataSaverAndLoader.SaveNewItem(x.Item2, itemList.IndexOf(x), x.Item1);
            int[] money = inventory.GetMoney();
            if (money != null)
                DataSaverAndLoader.SaveMoney(new List<int>(money));
            PlayerPrefs.Save();
        }
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    void SaveAttr()
    {
        int[] arr = attr.GetAttributes();
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
        DataSaverAndLoader.SaveMaxHealth((arr[2] / 2 - 5) + healthDice);
        DataSaverAndLoader.SaveHealth((arr[2] / 2 - 5) + healthDice);
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
            }
        }
        PresavedLists.competence.Clear();
    }
}
