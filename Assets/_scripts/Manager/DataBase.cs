using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class DataBase : MonoBehaviour
{
    string characterName;
    const string itemsCountSaveName = "@itemsCount_";
    const string itemSaveName = "@itemN_";
    const string itemSaveID = "@itemID_";
    const string itemCostSaveName = "@itemCost_";
    const string itemAmountSaveName = "@itemA_";
    
    public GameObject item;
    public GameObject panel;

    Item[] items;
    int itemsCount;


    // Start is called before the first frame update
    void Start()
    {
        characterName = CharacterCollection.GetName();

        items = GetComponent<LoadInventoryManager>().GetItems();
        LoadItems();
    }

    void LoadItems()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey(characterName + itemsCountSaveName))
        {
            itemsCount = PlayerPrefs.GetInt(characterName + itemsCountSaveName);
            for (int i = 0; i < itemsCount; i++)
            {
                int id = PlayerPrefs.GetInt(characterName + itemSaveID + i);
                string label = PlayerPrefs.GetString(characterName + itemSaveName + i);
                int amount = 1;

                if ((id == -1) && PlayerPrefs.HasKey(characterName + itemCostSaveName + label))
                {
                    amount = PlayerPrefs.GetInt(characterName + itemAmountSaveName + label);
                    AddItemBox(DataSaverAndLoader.LoadSavedItem(label), amount);
                }
                else
                {
                    amount = PlayerPrefs.GetInt(characterName + itemAmountSaveName + id);
                    foreach (Item x in items)
                    {
                        if (x.id == id)
                        {
                            AddItemBox(x, amount);
                            break;
                        }
                    }
                }
            }
        }
        WeaponInventory weaponInventory = FindObjectOfType<WeaponInventory>();
        weaponInventory.LoadEquited();
        ArmorInventory armorInventory = FindObjectOfType<ArmorInventory>();
        armorInventory.LoadEquited();
    }

   
    void AddItemBox(Item x, int amount)
    {

        GameObject newItem = Instantiate(item, panel.transform);
        newItem.GetComponent<ItemBox>().SetItem(x, amount, true);
        
    }
   
}
