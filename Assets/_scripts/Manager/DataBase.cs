using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Newtonsoft.Json;
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
    public AdderItemsToInventory inventory;

    Item[] items;
    int itemsCount;


    // Start is called before the first frame update
    void Start()
    {
        characterName = CharacterCollection.GetName();

        items = GetComponent<LoadInventoryManager>().GetItems();
        CharacterData.load += LoadItems;
    }

    void LoadItems()
    {
        //PlayerPrefs.DeleteAll();
        foreach (Item x in CharacterData.GetItems())
        {
            if (x.id == -1)
                AddItemBox(x, x.amount);
            else
                foreach (Item y in items)
                    if (y.id == x.id)
                        AddItemBox(y, x.amount);
        }
        if (PlayerPrefs.HasKey(characterName + itemsCountSaveName) && CharacterData.GetItems().Count == 0) // надо удалять предметы, когда загрузил их из реестра
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
                    Item custom = DataSaverAndLoader.LoadSavedItem(label);
                    AddItemBox(custom, amount);
                    custom.amount = amount;
                    CharacterData.SetItemSilent(custom);
                }
                else
                {

                    amount = PlayerPrefs.GetInt(characterName + itemAmountSaveName + id);
                    foreach (Item x in items)
                    {
                        if (x.id == id)
                        {
                            AddItemBox(x, amount);
                            x.amount = amount;
                            CharacterData.SetItemSilent(x);
                            break;
                        }
                    }
                }
            }
            CharacterData.SaveCharacter();
        }
        WeaponInventory weaponInventory = FindObjectOfType<WeaponInventory>();
        weaponInventory.LoadEquited();
        ArmorInventory armorInventory = FindObjectOfType<ArmorInventory>();
        armorInventory.LoadEquited();
        inventory.updatePanel();
    }


    void AddItemBox(Item x, int amount)
    {

        GameObject newItem = Instantiate(item, panel.transform);
        newItem.GetComponent<ItemBox>().SetItem(x, amount, true);

    }

}
