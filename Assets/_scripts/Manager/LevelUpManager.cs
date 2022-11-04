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
    int healthDice = 0;

    [SerializeField] ClassesAbilities classes;

    private void Start()
    {
        characterName = CharacterCollection.GetName();
        GetComponent<CharacterDataLoader>().LoadPrelists();
    }

    public void LoadView()
    {
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
}
