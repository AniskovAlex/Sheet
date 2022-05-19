using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    List<Item> itemColletion;
    List<Armor> armorCollection;
    List<Weapon> weaponCollection;
    List<string> armorList = new List<string>();
    List<string> weaponList = new List<string>();
    int armorEquipmented = -1;
    List<int> weaponEquipmented = new List<int>();
    int maxWeaponInHand = 2;
    float timeWait = 0;
    bool deleted = false;
    // Start is called before the first frame update
    void Start()
    {
        itemColletion = ItemCollection.GetCollection().GetList();
        armorCollection = ArmorCollection.GetCollection().GetList();
        weaponCollection = WeaponCollection.GetCollection().GetList();
        //ItemCollection.GetCollection().ShowCollection();
        //ArmorCollection.GetCollection().ShowCollection();
        nameField.onValueChanged.AddListener(delegate { ShowSearch(); });
        LoadItems();
    }

    private void Update()
    {
        if (deleted)
            if (timeWait <= 0)
            {
                int j = 0;
                while (input.GetComponentInChildren<Option>() && j < 5)
                {
                    input.GetComponentInChildren<Option>().DestroyMyself();
                    j++;
                }
                deleted = false;
            }
            else
                timeWait -= Time.deltaTime;
    }

    const string itemsCountSaveName = "itemsCount_";
    const string itemSaveName = "itemN_";
    const string itemCostSaveName = "itemCost_";
    const string itemWeightSaveName = "itemW_";
    const string itemMTypeSaveName = "itemMT_";
    const string itemTypeSaveName = "itemT_";
    const string itemAmountSaveName = "itemA_";
    const string armorEquipSaveName = "armorE_";
    const string weaponEquipSaveName = "weaponE_";
    const string armorClassSaveName = "ac_";

    public Manager manager;
    public GameObject item;
    public GameObject armor;
    public GameObject weapon;
    public GameObject panel;
    public GameObject armorInventoryPanel;
    public GameObject weaponInventoryPanel;
    public GameObject input;
    public GameObject search;
    public InputField nameField;
    public InputField costField;
    public InputField weightField;
    public InputField amountField;
    public Dropdown mType;
    public Dropdown type;
    public Item? addItem = null;
    public bool flag = true;
    public bool flag1 = false;
    int itemsCount;
    public void ShowSearch()
    {
        string s = nameField.text;
        s = s.ToLower();
        int j = 0;
        while (input.GetComponentInChildren<Option>() && j < 5)
        {
            input.GetComponentInChildren<Option>().DestroyMyself();
            j++;
        }
        if (s != "" && flag)
        {
            List<Item> searchList = new List<Item>();
            foreach (Item x in itemColletion)
            {
                if (x.label.Contains(s))
                    searchList.Add(x);
            }
            for (int i = 0; i < searchList.Count && i < 5; i++)
            {
                GameObject b = Instantiate(search, input.transform);
                b.GetComponent<Option>().input = nameField;
                b.GetComponentInChildren<Text>().text = searchList[i].label[0].ToString().ToUpper() + searchList[i].label.Remove(0, 1);
                b.GetComponent<Option>().db = this;
                b.GetComponent<Option>().item = searchList[i];
            }
        }
        else
            flag = true;

    }

    public void showFoundedItem()
    {
        if (addItem != null && flag1 == true)
        {
            nameField.text = addItem.Value.label[0].ToString().ToUpper() + addItem.Value.label.Remove(0, 1);
            costField.text = addItem.Value.cost.ToString();
            weightField.text = addItem.Value.weight.ToString();
            mType.value = (int)addItem.Value.mType;
            type.value = (int)addItem.Value.type;
            costField.interactable = false;
            weightField.interactable = false;
            mType.interactable = false;
            type.interactable = false;
            flag1 = false;
        }
        else
        {
            costField.interactable = true;
            weightField.interactable = true;
            mType.interactable = true;
            type.interactable = true;
        }
    }

    public void AddItem()
    {
        bool found = false;
        string itemLabel = nameField.text.ToLower();
        for (int i = 0; i < itemsCount; i++)
        {
            string label = PlayerPrefs.GetString(itemSaveName + i);
            if (label == itemLabel)
            {
                int amount;
                int.TryParse(amountField.text, out amount);
                PlayerPrefs.SetInt(itemAmountSaveName + label, PlayerPrefs.GetInt(itemAmountSaveName + label) + amount);
                ReloadItems();
                found = true;
                break;
            }
        }
        if (found)
        {
            PlayerPrefs.Save();
            ClearField();
            return;
        }
        if (addItem != null)
        {

            PlayerPrefs.SetString(itemSaveName + itemsCount, addItem.Value.label);
            int amount;
            int.TryParse(amountField.text, out amount);
            PlayerPrefs.SetInt(itemAmountSaveName + addItem.Value.label, amount);
            itemsCount++;
            PlayerPrefs.SetInt(itemsCountSaveName, itemsCount);
            addExistGameObject((Item)addItem);
            addItem = null;
        }
        else
        {
            if (nameField.text != "" && costField.text != "" && weightField.text != "")
            {
                int cost;
                int weight;
                int.TryParse(costField.text, out cost);
                int.TryParse(weightField.text, out weight);
                PlayerPrefs.SetString(itemSaveName + itemsCount, itemLabel);
                PlayerPrefs.SetInt(itemCostSaveName + itemLabel, cost);
                PlayerPrefs.SetInt(itemWeightSaveName + itemLabel, weight);
                PlayerPrefs.SetInt(itemMTypeSaveName + itemLabel, mType.value);
                PlayerPrefs.SetInt(itemTypeSaveName + itemLabel, type.value);
                int amount;
                int.TryParse(amountField.text, out amount);
                PlayerPrefs.SetInt(itemAmountSaveName + itemLabel, amount);
                itemsCount++;
                PlayerPrefs.SetInt(itemsCountSaveName, itemsCount);
                addNewGameObject(itemLabel);
            }
        }
        ClearField();
        PlayerPrefs.Save();
    }

    void ClearField()
    {
        nameField.text = "";
        mType.value = 0;
        mType.interactable = false;
        costField.text = "0";
        costField.interactable = false;
        weightField.text = "0";
        weightField.interactable = false;
        type.value = 0;
        type.interactable = false;
        amountField.text = "1";
    }

    public void clear()
    {
        PlayerPrefs.DeleteAll();
    }

    public void DeleteItem(string label)
    {
        int itemIndex = 0;
        for (int i = 0; i < itemsCount; i++)
            if (PlayerPrefs.GetString(itemSaveName + i) == label)
            {
                itemIndex = i;
                break;
            }
        string labelLast = PlayerPrefs.GetString(itemSaveName + itemIndex);
        if (PlayerPrefs.GetInt(itemAmountSaveName + labelLast) > 1)
        {
            int amount = PlayerPrefs.GetInt(itemAmountSaveName + labelLast) - 1;
            PlayerPrefs.SetInt(itemAmountSaveName + labelLast, amount);
            if (armorEquipmented >= 0)
                if (labelLast == armorList[armorEquipmented])
                {
                    int index = 0;
                    bool found = false;
                    foreach (string x in armorList)
                    {

                        if (x == labelLast && index != armorEquipmented)
                        {
                            found = true;
                            break;
                        }
                        index++;
                    }
                    if (found)
                    {
                        armorList.RemoveAt(index);
                        PlayerPrefs.DeleteKey(armorEquipSaveName + index);
                        if (index < armorEquipmented)
                        {
                            PlayerPrefs.DeleteKey(armorEquipSaveName + armorEquipmented);
                            armorEquipmented--;
                            PlayerPrefs.SetInt(armorEquipSaveName + armorEquipmented, 1);
                        }
                    }
                    else
                    {
                        armorList.Remove(labelLast);
                        armorEquipmented = -1;
                        PlayerPrefs.DeleteKey(armorEquipSaveName + armorEquipmented);
                    }

                }
            if (weaponEquipmented.Count > 0)
            {
                if (weaponList.Contains(labelLast))
                {
                    int index = 0;
                    bool found = false;
                    foreach (string x in weaponList)
                    {
                        if (x == labelLast && !weaponEquipmented.Contains(index))
                        {
                            found = true;
                            break;
                        }
                        index++;
                    }
                    if (found)
                    {
                        weaponList.RemoveAt(index);
                        PlayerPrefs.DeleteKey(weaponEquipSaveName + index);
                    }
                    else
                    {
                        index = weaponList.IndexOf(labelLast);
                        weaponEquipmented.Remove(index);
                        PlayerPrefs.DeleteKey(weaponEquipSaveName + index);
                        weaponList.Remove(labelLast);
                    }
                    for (int x = 0; x < weaponEquipmented.Count; x++)
                    {
                        if (index < weaponEquipmented[x])
                        {
                            PlayerPrefs.DeleteKey(weaponEquipSaveName + weaponEquipmented[x]);
                            weaponEquipmented[x]--;
                            PlayerPrefs.SetInt(weaponEquipSaveName + weaponEquipmented[x], 1);
                        }
                    }
                }
            }
        }
        else
        {
            if (PlayerPrefs.HasKey(itemCostSaveName + labelLast))
            {
                PlayerPrefs.DeleteKey(itemCostSaveName + labelLast);
                PlayerPrefs.DeleteKey(itemMTypeSaveName + labelLast);
                PlayerPrefs.DeleteKey(itemWeightSaveName + labelLast);
                PlayerPrefs.DeleteKey(itemTypeSaveName + labelLast);
            }
            for (int i = itemIndex; i < itemsCount - 1; i++)
            {
                string labelNext = PlayerPrefs.GetString(itemSaveName + (i + 1));
                PlayerPrefs.SetString(itemSaveName + i, labelNext);
            }
            PlayerPrefs.DeleteKey(itemSaveName + (itemsCount - 1));
            itemsCount--;
            PlayerPrefs.SetInt(itemsCountSaveName, itemsCount);
            if (itemIndex == armorEquipmented)
            {
                armorEquipmented = -1;
            }
            PlayerPrefs.DeleteKey(armorEquipSaveName + armorList.FindIndex(labelLast.StartsWith));
            armorList.Remove(labelLast);

            if (weaponEquipmented.Contains(itemIndex))
            {
                weaponEquipmented.Remove(itemIndex);
            }
            PlayerPrefs.DeleteKey(weaponEquipSaveName + weaponList.FindIndex(labelLast.StartsWith));
            weaponList.Remove(labelLast);

        }
        PlayerPrefs.Save();
        ReloadItems();
    }

    void addExistGameObject(Item x)
    {
        GameObject newItem = Instantiate(item, panel.transform);
        newItem.GetComponentInChildren<Label>().GetComponentInChildren<Text>().text = x.label[0].ToString().ToUpper() + x.label.Remove(0, 1);
        string moneyType = "";
        switch (x.mType)
        {
            case Item.MType.goldCoin:
                moneyType = "ЗМ:";
                break;
            case Item.MType.silverCoin:
                moneyType = "СМ:";
                break;
            case Item.MType.copperCoin:
                moneyType = "ММ:";
                break;
        }
        newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = moneyType + " " + x.cost;
        switch (x.weight)
        {
            case -1:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "1/2 фнт";
                break;
            case -2:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "1/4 фнт";
                break;
            default:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = x.weight + " фнт";
                break;
        }
        int amount = PlayerPrefs.GetInt(itemAmountSaveName + x.label);
        string itemType = "";
        switch (x.type)
        {
            case Item.Type.item:
                itemType = "П";
                break;
            case Item.Type.armor:
                for (int i = 0; i < amount; i++)
                    armorList.Add(x.label);
                itemType = "Б";
                break;
            case Item.Type.weapon:
                for (int i = 0; i < amount; i++)
                    weaponList.Add(x.label);
                itemType = "О";
                break;
        }
        newItem.GetComponentInChildren<Type>().GetComponentInChildren<Text>().text = itemType;
        newItem.GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = amount.ToString();
        newItem.GetComponent<Box>().db = this;
        newItem.GetComponent<Box>().label = x.label;
    }
    void addNewGameObject(string label)
    {
        GameObject newItem = Instantiate(item, panel.transform);
        newItem.GetComponentInChildren<Label>().GetComponentInChildren<Text>().text = label[0].ToString().ToUpper() + label.Remove(0, 1);
        string moneyType = "";
        int moneyTypeInt = PlayerPrefs.GetInt(itemMTypeSaveName + label);
        switch (moneyTypeInt)
        {
            case 0:
                moneyType = "ЗМ:";
                break;
            case 1:
                moneyType = "СМ:";
                break;
            case 2:
                moneyType = "ММ:";
                break;
        }
        newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = moneyType + " " + PlayerPrefs.GetInt(itemCostSaveName + label);
        newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(itemWeightSaveName + label).ToString() + " фнт";
        int amount = PlayerPrefs.GetInt(itemAmountSaveName + label);
        string type = "";
        int typeInt = PlayerPrefs.GetInt(itemTypeSaveName + label);
        switch (typeInt)
        {
            case 0:
                type = "П";
                break;
            case 1:
                for (int i = 0; i < amount; i++)
                    armorList.Add(label);
                type = "Б";
                break;
            case 2:
                for (int i = 0; i < amount; i++)
                    weaponList.Add(label);
                type = "О";
                break;
        }
        newItem.GetComponentInChildren<Type>().GetComponentInChildren<Text>().text = type;
        newItem.GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = amount.ToString();
        newItem.GetComponent<Box>().db = this;
        newItem.GetComponent<Box>().label = label;
    }

    void LoadItems()
    {
        if (PlayerPrefs.HasKey(itemsCountSaveName))
        {
            itemsCount = PlayerPrefs.GetInt(itemsCountSaveName);
            for (int i = 0; i < itemsCount; i++)
            {

                bool found = false;
                string label = PlayerPrefs.GetString(itemSaveName + i);

                foreach (Item x in itemColletion)
                {
                    if (x.label == label)
                    {
                        found = true;
                        addExistGameObject(x);
                        break;
                    }
                }
                if (!found)
                {
                    addNewGameObject(label);
                }
            }
            LoadInventoryArmorItems();
            LoadInventoryWeaponItems();
        }
    }

    void ReloadItems()
    {
        int j = 0;
        while (panel.GetComponentInChildren<Box>())
        {
            panel.GetComponentInChildren<Box>().DestroyMyself();
            j++;
        }
        armorList.Clear();
        armorEquipmented = -1;
        while (armorInventoryPanel.GetComponentInChildren<Box>())
        {
            armorInventoryPanel.GetComponentInChildren<Box>().DestroyMyself();
        }
        weaponList.Clear();
        weaponEquipmented.Clear();
        while (weaponInventoryPanel.GetComponentInChildren<Box>())
        {
            weaponInventoryPanel.GetComponentInChildren<Box>().DestroyMyself();
        }
        LoadItems();
    }

    void LoadInventoryArmorItems()
    {
        int index = -1;
        foreach (string x in armorList)
        {
            index++;
            if (PlayerPrefs.HasKey(itemCostSaveName + x))
            {
                AddNewArmorObject(x);
                continue;
            }

            foreach (Armor y in armorCollection)
            {
                if (x == y.label)
                {
                    AddExistArmorObject(y, index);
                    break;
                }

            }
        }
    }

    void LoadInventoryWeaponItems()
    {
        int index = -1;
        foreach (string x in weaponList)
        {
            index++;
            if (PlayerPrefs.HasKey(itemCostSaveName + x))
            {
                //AddNewWeaponObject(x);
                continue;
            }

            foreach (Weapon y in weaponCollection)
            {
                if (x == y.label)
                {
                    AddExistWeaponObject(y, index);
                    break;
                }

            }
        }
    }

    void AddExistArmorObject(Armor x, int index)
    {
        GameObject newItem = Instantiate(armor, armorInventoryPanel.transform);
        newItem.GetComponent<Box>().index = index;
        newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { ArmorEquipmentChanged(newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>()); });
        newItem.GetComponentInChildren<Label>().GetComponentInChildren<Text>().text = x.label[0].ToString().ToUpper() + x.label.Remove(0, 1);
        int equip = PlayerPrefs.GetInt(armorEquipSaveName + index);
        if (equip == 1)
        {
            if (armorEquipmented == -1)
            {
                newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().isOn = true;
                armorEquipmented = index;
            }
            else
            {
                PlayerPrefs.DeleteKey(armorEquipSaveName + index);
                PlayerPrefs.Save();
                newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().isOn = false;
        }
    }

    void AddExistWeaponObject(Weapon x, int index)
    {
        GameObject newItem = Instantiate(weapon, weaponInventoryPanel.transform);
        newItem.GetComponent<Box>().index = index;
        newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { WeaponEquipmentChanged(newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>()); });
        newItem.GetComponentInChildren<Label>().GetComponentInChildren<Text>().text = x.label[0].ToString().ToUpper() + x.label.Remove(0, 1);
        int equip = PlayerPrefs.GetInt(weaponEquipSaveName + index);
        if (equip == 1)
        {
            if (weaponEquipmented.Count < maxWeaponInHand)
            {
                newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().isOn = true;
                weaponEquipmented.Add(index);
            }
            else
            {
                PlayerPrefs.DeleteKey(weaponEquipSaveName + index);
                PlayerPrefs.Save();
                newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().isOn = false;
            }
        }
        else
        {
            newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().isOn = false;
        }
    }

    void AddNewArmorObject(string x)
    {
        //Дописать!!!
    }

    void ArmorEquipmentChanged(Toggle toggle)
    {
        int index = toggle.GetComponentInParent<Box>().index;
        if (!toggle.isOn)
        {
            PlayerPrefs.DeleteKey(armorEquipSaveName + index);
            PlayerPrefs.Save();
            if (index == armorEquipmented)
            {
                armorEquipmented = -1;
                ArmorClassChanged();
            }
            return;//временно
        }
        if (armorEquipmented == -1)
        {
            PlayerPrefs.SetInt(armorEquipSaveName + index, 1);
            PlayerPrefs.Save();
            armorEquipmented = index;
            ArmorClassChanged();
            return;
        }
        Box[] list = armorInventoryPanel.GetComponentsInChildren<Box>();
        foreach (Box x in list)
        {
            if (x.index == armorEquipmented)
            {
                x.GetComponentInChildren<Toggle>().isOn = false;
                PlayerPrefs.DeleteKey(armorEquipSaveName + x.index);
                break;
            }
        }
        PlayerPrefs.SetInt(armorEquipSaveName + index, 1);
        PlayerPrefs.Save();
        armorEquipmented = index;
        ArmorClassChanged();
    }

    void WeaponEquipmentChanged(Toggle toggle)
    {
        int index = toggle.GetComponentInParent<Box>().index;
        if (!toggle.isOn)
        {
            PlayerPrefs.DeleteKey(weaponEquipSaveName + index);
            PlayerPrefs.Save();
            if (weaponEquipmented.Contains(index))
            {
                weaponEquipmented.Remove(index);
                UpdateWeapon();
            }
            return;//временно
        }
        if (weaponEquipmented.Count < maxWeaponInHand)
        {
            PlayerPrefs.SetInt(weaponEquipSaveName + index, 1);
            PlayerPrefs.Save();
            weaponEquipmented.Add(index);
            UpdateWeapon();
            return;
        }
        Box[] list = weaponInventoryPanel.GetComponentsInChildren<Box>();
        foreach (Box x in list)
        {
            if (x.index == weaponEquipmented[0])
            {
                x.GetComponentInChildren<Toggle>().isOn = false;
                PlayerPrefs.DeleteKey(weaponEquipSaveName + x.index);
                break;
            }
        }
        PlayerPrefs.SetInt(weaponEquipSaveName + index, 1);
        weaponEquipmented.Add(index);
        UpdateWeapon();
        PlayerPrefs.Save();
    }

    void UpdateWeapon()
    {
        List<Weapon> list = new List<Weapon>();
        foreach(int x in weaponEquipmented)
        {
            foreach(Weapon y in weaponCollection)
            {
                if (y.label == weaponList[x])
                {
                    list.Add(y);
                    break;
                }
            }
        }
        manager.UpdateEquipment(list);
    }

    void ArmorClassChanged()
    {
        string buf = "";
        if (armorEquipmented != -1)
        {
            buf = armorList[armorEquipmented];
            foreach (Armor x in armorCollection)
            {
                if (x.label == buf)
                {
                    manager.UpdateArmorClass(x.AC, x.ACCap);
                    return;
                }
            }
        }
        manager.UpdateArmorClass(10, 100);
    }
}
