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
            if(CharacterData.GetLevel(classes.GetClass()) == 0)
            {
                HashSet<Weapon.BladeType> bladeProf;
                HashSet<Weapon.WeaponType> weaponProf;
                HashSet<Armor.ArmorType> armorProf;
                bladeProf = classes.GetClass().GetBladeProficiency();
                if (bladeProf != null)
                    PresavedLists.bladeTypes.UnionWith(bladeProf);

                weaponProf = classes.GetClass().GetWeaponProficiency();
                if (weaponProf != null)
                    PresavedLists.weaponTypes.UnionWith(weaponProf);

                armorProf = classes.GetClass().GetArmorProficiency();
                if (armorProf != null)
                    PresavedLists.armorTypes.UnionWith(armorProf);
                HashSet<int> saveThrows;
                saveThrows = classes.GetClass().GetSaveThrows();
                PresavedLists.saveThrows.UnionWith(saveThrows);
            }
            DataSaverAndLoader.SaveClass(classes.GetClass().name);
            if (classes.GetClass().GetSubClass() != null)
                DataSaverAndLoader.SaveSubClass(classes.GetClass());
        }
        PresavedLists.SaveCustomPrelists();
        PlayerPrefs.Save();
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }
}
