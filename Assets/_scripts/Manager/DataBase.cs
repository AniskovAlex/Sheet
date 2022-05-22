using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    List<Item> itemColletion;
    List<Armor> armorCollection;
    List<Weapon> weaponCollection;
    List<Item> itemList = new List<Item>();
    List<Armor> armorList = new List<Armor>();
    List<Weapon> weaponList = new List<Weapon>();
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
                        itemList.Add(x);
                        addExistGameObject(x);
                        break;
                    }
                }
                if (!found)
                {
                    if (PlayerPrefs.HasKey(itemCostSaveName + label))
                    {
                        int cost = PlayerPrefs.GetInt(itemCostSaveName + label);
                        int weight = PlayerPrefs.GetInt(itemWeightSaveName + label);
                        Item.MType mType = Item.MType.goldCoin + PlayerPrefs.GetInt(itemMTypeSaveName + label);
                        Item.Type type = Item.Type.item + PlayerPrefs.GetInt(itemTypeSaveName + label);
                        Item newItem = new Item(label, cost, weight, mType, type);
                        addExistGameObject(newItem);
                    }
                }
            }
            //LoadInventoryArmorItems();
            //LoadInventoryWeaponItems();
        }
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
                int buf = PlayerPrefs.GetInt(itemAmountSaveName + label) + amount;
                PlayerPrefs.SetInt(itemAmountSaveName + label, buf);
                panel.GetComponentsInChildren<Box>()[i].GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = buf.ToString();
                //��������!!!
                foreach (Armor y in armorCollection)
                    if (label == y.label)
                    {
                        armorList.Add(y);
                        AddExistArmorObject(y, armorList.Count - 1);
                        break;
                    }
                foreach (Weapon y in weaponCollection)
                    if (label == y.label)
                    {
                        weaponList.Add(y);
                        AddExistWeaponObject(y, weaponList.Count - 1);
                        break;
                    }
                //ReloadItems();
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
            itemList.Add((Item)addItem);
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
                Item.MType buf1 = Item.MType.goldCoin + mType.value;
                Item.Type buf2 = Item.Type.item + type.value;
                Item newItem = new Item(itemLabel, cost, weight, buf1, buf2);
                itemList.Add(newItem);
                addExistGameObject(newItem);
            }
        }
        ClearField();
        PlayerPrefs.Save();
        //ReloadItems();
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

    public void DeleteItem(int itemIndex)
    {
        string label = PlayerPrefs.GetString(itemSaveName + itemIndex);
        if (PlayerPrefs.GetInt(itemAmountSaveName + label) > 1)
        {
            int amount = PlayerPrefs.GetInt(itemAmountSaveName + label) - 1;
            PlayerPrefs.SetInt(itemAmountSaveName + label, amount);
            panel.GetComponentsInChildren<Box>()[itemIndex].GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = amount.ToString();
            if (itemList[itemIndex].type == Item.Type.armor)
                if (armorEquipmented >= 0)
                    if (label == armorList[armorEquipmented].label)
                    {
                        int index = 0;
                        bool found = false;
                        foreach (Armor x in armorList)
                        {

                            if (x.label == label && index != armorEquipmented)
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
                            Box[] armors = armorInventoryPanel.GetComponentsInChildren<Box>();
                            armors[index].DestroyMyself();
                        }
                        else
                        {
                            armorList.RemoveAt(armorEquipmented);
                            PlayerPrefs.DeleteKey(armorEquipSaveName + armorEquipmented);
                            Box[] armors = armorInventoryPanel.GetComponentsInChildren<Box>();
                            armors[armorEquipmented].DestroyMyself();
                            armorEquipmented = -1;
                        }

                    }
            if (itemList[itemIndex].type == Item.Type.weapon)
                if (weaponEquipmented.Count >= 0)
                {
                    int index = 0;
                    foreach (Weapon x in weaponList)
                    {

                        int indexEquipted = 0;
                        bool foundEquipted = false;
                        bool found = false;
                        bool itemDeleted = false;
                        if (x.label == label && !found)
                        {
                            if (weaponEquipmented.Contains(index))
                            {
                                foundEquipted = true;
                                indexEquipted = index;
                            }
                            else
                            {
                                found = true;
                            }
                        }
                        if (found)
                        {
                            Box[] weapons = weaponInventoryPanel.GetComponentsInChildren<Box>();
                            weapons[index].DestroyMyself();
                            weaponList.RemoveAt(index);
                            PlayerPrefs.DeleteKey(weaponEquipSaveName + index);
                            itemDeleted = true;
                        }
                        else
                        {
                            if (foundEquipted)
                            {
                                Box[] weapons = weaponInventoryPanel.GetComponentsInChildren<Box>();
                                weapons[index].DestroyMyself();
                                weaponEquipmented.Remove(indexEquipted);
                                PlayerPrefs.DeleteKey(weaponEquipSaveName + indexEquipted);
                                weaponList.RemoveAt(indexEquipted);
                                itemDeleted = true;
                            }
                        }
                        if (itemDeleted)
                        {
                            ListShift(index, weaponEquipmented, weaponEquipSaveName);
                            break;
                        }
                        index++;
                    }
                    UpdateWeapon();
                }
        }
        else
        {
            if (PlayerPrefs.HasKey(itemCostSaveName + label))
            {
                PlayerPrefs.DeleteKey(itemCostSaveName + label);
                PlayerPrefs.DeleteKey(itemMTypeSaveName + label);
                PlayerPrefs.DeleteKey(itemWeightSaveName + label);
                PlayerPrefs.DeleteKey(itemTypeSaveName + label);
            }
            Box[] boxes = panel.GetComponentsInChildren<Box>();
            boxes[itemIndex].DestroyMyself();
            for (int i = itemIndex; i < itemsCount - 1; i++)
            {
                string labelNext = PlayerPrefs.GetString(itemSaveName + (i + 1));
                PlayerPrefs.SetString(itemSaveName + i, labelNext);
                boxes[i + 1].index = i;
            }
            PlayerPrefs.DeleteKey(itemSaveName + (itemsCount - 1));
            itemsCount--;
            PlayerPrefs.SetInt(itemsCountSaveName, itemsCount);
            if (itemList[itemIndex].type == Item.Type.armor)
            {
                int index = 0;
                bool found = false;
                foreach (Armor x in armorList)
                {
                    if (label == x.label)
                    {
                        armorList.RemoveAt(index);
                        if (index == armorEquipmented)
                        {

                            PlayerPrefs.DeleteKey(armorEquipSaveName + armorEquipmented);
                            armorEquipmented = -1;
                            found = true;
                        }
                        break;
                    }
                    index++;
                }
                if (!found)
                {
                    if (index < armorEquipmented)
                    {
                        PlayerPrefs.DeleteKey(armorEquipSaveName + armorEquipmented);
                        armorEquipmented--;
                        PlayerPrefs.SetInt(armorEquipSaveName + armorEquipmented, 1);
                    }
                }
                Box[] armors = armorInventoryPanel.GetComponentsInChildren<Box>();
                armors[index].DestroyMyself();
                for (int i = index; i < armors.Length; i++)
                {
                    armors[i].index--;
                }
            }
            if (itemList[itemIndex].type == Item.Type.weapon)
            {
                int index = 0;
                foreach (Weapon x in weaponList)
                {
                    if (label == x.label)
                        break;
                    index++;
                }
                if (weaponEquipmented.Contains(index))
                {
                    weaponEquipmented.Remove(index);
                    PlayerPrefs.DeleteKey(weaponEquipSaveName + index);
                    ListShift(index, weaponEquipmented, weaponEquipSaveName);
                }
                Box[] weapons = weaponInventoryPanel.GetComponentsInChildren<Box>();
                weapons[index].DestroyMyself();
                weaponList.RemoveAt(index);
                for (int i = index; i < weapons.Length; i++)
                {
                    weapons[i].index--;
                }
                UpdateWeapon();
            }


        }
        PlayerPrefs.Save();
        //ReloadItems();
    }

    void ListShift(int index, List<int> list, string saveName)
    {
        for (int y = index; y < list.Count; y++)
        {
            PlayerPrefs.DeleteKey(saveName + list[y]);
            list[y]--;
            PlayerPrefs.SetInt(saveName + list[y], 1);
        }
    }

    void addExistGameObject(Item x)
    {
        GameObject newItem = Instantiate(item, panel.transform);
        newItem.GetComponentInChildren<Label>().GetComponentInChildren<Text>().text = x.label[0].ToString().ToUpper() + x.label.Remove(0, 1);
        string moneyType = "";
        switch (x.mType)
        {
            case Item.MType.goldCoin:
                moneyType = "��:";
                break;
            case Item.MType.silverCoin:
                moneyType = "��:";
                break;
            case Item.MType.copperCoin:
                moneyType = "��:";
                break;
        }
        newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = moneyType + " " + x.cost;
        switch (x.weight)
        {
            case -1:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "1/2 ���";
                break;
            case -2:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "1/4 ���";
                break;
            default:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = x.weight + " ���";
                break;
        }
        int amount = PlayerPrefs.GetInt(itemAmountSaveName + x.label);
        string itemType = "";
        switch (x.type)
        {
            case Item.Type.item:
                itemType = "�";
                break;
            case Item.Type.armor:
                foreach (Armor y in armorCollection)
                    if (x.label == y.label)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            armorList.Add(y);
                            AddExistArmorObject(y, armorList.Count - 1);
                        }
                        break;
                    }
                itemType = "�";
                break;
            case Item.Type.weapon:
                foreach (Weapon y in weaponCollection)
                    if (x.label == y.label)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            weaponList.Add(y);
                            AddExistWeaponObject(y, weaponList.Count - 1);
                        }
                        break;
                    }
                itemType = "�";
                break;
        }
        newItem.GetComponentInChildren<Type>().GetComponentInChildren<Text>().text = itemType;
        newItem.GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = amount.ToString();
        newItem.GetComponent<Box>().db = this;
        newItem.GetComponent<Box>().index = itemList.IndexOf(x);
    }
    /*void addNewGameObject(string label)
    {
        GameObject newItem = Instantiate(item, panel.transform);
        newItem.GetComponentInChildren<Label>().GetComponentInChildren<Text>().text = label[0].ToString().ToUpper() + label.Remove(0, 1);
        string moneyType = "";
        int moneyTypeInt = PlayerPrefs.GetInt(itemMTypeSaveName + label);
        switch (moneyTypeInt)
        {
            case 0:
                moneyType = "��:";
                break;
            case 1:
                moneyType = "��:";
                break;
            case 2:
                moneyType = "��:";
                break;
        }
        newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = moneyType + " " + PlayerPrefs.GetInt(itemCostSaveName + label);
        newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(itemWeightSaveName + label).ToString() + " ���";
        int amount = PlayerPrefs.GetInt(itemAmountSaveName + label);
        string type = "";
        int typeInt = PlayerPrefs.GetInt(itemTypeSaveName + label);
        switch (typeInt)
        {
            case 0:
                type = "�";
                break;
            case 1:
                for (int i = 0; i < amount; i++)
                    armorList.Add(label);
                type = "�";
                break;
            case 2:
                for (int i = 0; i < amount; i++)
                    weaponList.Add(label);
                type = "�";
                break;
        }
        newItem.GetComponentInChildren<Type>().GetComponentInChildren<Text>().text = type;
        newItem.GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = amount.ToString();
        newItem.GetComponent<Box>().db = this;
        newItem.GetComponent<Box>().label = label;
    }*/



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
        foreach (Armor x in armorList)
        {
            index++;
            if (PlayerPrefs.HasKey(itemCostSaveName + x))
            {
                //AddNewArmorObject(x);
                continue;
            }

            foreach (Armor y in armorCollection)
            {
                if (x.label == y.label)
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
        foreach (Weapon x in weaponList)
        {
            index++;
            if (PlayerPrefs.HasKey(itemCostSaveName + x))
            {
                //AddNewWeaponObject(x);
                continue;
            }

            foreach (Weapon y in weaponCollection)
            {
                if (x.label == y.label)
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
        string type = "";
        switch (x.type)
        {
            case Armor.Type.Light:
                type = "˸����";
                break;
            case Armor.Type.Medium:
                type = "�������";
                break;
            case Armor.Type.Heavy:
                type = "������";
                break;
        };
        newItem.GetComponentInChildren<MType>().GetComponentInChildren<Text>().text = type;
        newItem.GetComponentInChildren<Attribute>().GetComponentInChildren<Text>().text = x.AC.ToString();
        switch (x.ACCap)
        {
            case 0:
                newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = "+ ��� ���" + x.ACCap.ToString();
                break;
            case -1:
                newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = "-";
                break;
            default:
                newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = "+ ��� ���(����. " + x.ACCap.ToString() + ")";
                break;
        }
        newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "���. " + x.strReq;
        newItem.GetComponentInChildren<Amount>().GetComponent<Toggle>().isOn = x.stealthDis;
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
        newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = x.dices + "�" + x.hitDice;
        if (x.dist == x.maxDist)
            newItem.GetComponentInChildren<Attribute>().GetComponentInChildren<Text>().text = x.dist + " ��.";
        else
            newItem.GetComponentInChildren<Attribute>().GetComponentInChildren<Text>().text = x.dist + "/" + x.maxDist;
        newItem.GetComponentInChildren<MType>().GetComponent<Toggle>().isOn = x.magic;
        switch (x.damageType)
        {
            case Weapon.DamageType.Slashing:
                newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = "�������";
                break;
            case Weapon.DamageType.Piercing:
                newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = "�������";
                break;
            case Weapon.DamageType.Crushing:
                newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = "��������";
                break;
        }
        Text buf = newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>();
        foreach (Weapon.Properties y in x.properties)
        {
            switch (y)
            {
                case Weapon.Properties.Ammo:
                    buf.text += "���������";
                    break;
                case Weapon.Properties.Distance:
                    buf.text += "���.";
                    break;
                case Weapon.Properties.Fencing:
                    buf.text += "������������";
                    break;
                case Weapon.Properties.Heavy:
                    buf.text += "������";
                    break;
                case Weapon.Properties.Light:
                    buf.text += "˸����";
                    break;
                case Weapon.Properties.Reach:
                    buf.text += "������������";
                    break;
                case Weapon.Properties.Reload:
                    buf.text += "�����������";
                    break;
                case Weapon.Properties.Special:
                    buf.text += "������";
                    break;
                case Weapon.Properties.Throwing:
                    buf.text += "�����������";
                    break;
                case Weapon.Properties.TwoHanded:
                    buf.text += "���������";
                    break;
                case Weapon.Properties.Universal:
                    buf.text += "������������� " + x.dices + "�" + (x.hitDice + 2);
                    break;
            }
            if (y != x.properties[x.properties.Length - 1])
            {
                buf.text += ", ";
            }
        }

        int equip = PlayerPrefs.GetInt(weaponEquipSaveName + index);
        if (equip == 1)
        {
            if (weaponEquipmented.Count < maxWeaponInHand)
            {
                newItem.GetComponentInChildren<Type>().gameObject.GetComponent<Toggle>().isOn = true;
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
        //��������!!!
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
            return;//��������
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
            return;//��������
        }
        bool found = false;
        foreach (Weapon.Properties x in weaponList[index].properties)
        {
            if (x == Weapon.Properties.TwoHanded)
            {
                found = true;
                maxWeaponInHand = 1;
                break;
            }
        }
        if (!found)
        {
            if (maxWeaponInHand == 1 && weaponEquipmented.Count > 0)
            {
                PlayerPrefs.DeleteKey(weaponEquipSaveName + weaponEquipmented[0]);
                weaponInventoryPanel.GetComponentsInChildren<Box>()[weaponEquipmented[0]].GetComponentInChildren<Toggle>().isOn = false;
                
            }
            maxWeaponInHand = 2;
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
        weaponEquipmented.Add(index);
        foreach (Box x in list)
        {
            for (int j = 0; j < weaponEquipmented.Count - maxWeaponInHand; j++)
                if (x.index == weaponEquipmented[j])
                {
                    x.GetComponentInChildren<Toggle>().isOn = false;
                    PlayerPrefs.DeleteKey(weaponEquipSaveName + x.index);
                    break;
                }
        }
        PlayerPrefs.SetInt(weaponEquipSaveName + index, 1);
        PlayerPrefs.Save();
    }

    void UpdateWeapon()
    {
        List<Weapon> list = new List<Weapon>();
        foreach (int x in weaponEquipmented)
        {
            list.Add(weaponList[x]);
        }
        manager.UpdateEquipment(list);
    }

    void ArmorClassChanged()
    {
        Armor buf;
        if (armorEquipmented != -1)
        {
            buf = armorList[armorEquipmented];
            manager.UpdateArmorClass(buf.AC, buf.ACCap);
            return;
        }
        manager.UpdateArmorClass(10, 100);
    }

    Armor? GetItemInArmorList(string label, List<Armor> list)
    {
        foreach (Armor x in list)
        {
            if (x.label == label) return x;
        }
        return null;
    }
}
