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
                        itemList.Add(newItem);
                        addExistGameObject(newItem);
                    }
                }
            }
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

    public Manager manager;
    public GameObject item;
    public GameObject armor;
    public GameObject weapon;
    public GameObject panel;
    public GameObject armorInventoryPanel;
    public GameObject weaponInventoryPanel;
    public GameObject input;
    public GameObject search;
    public GameObject weaponSet;
    public GameObject armorSet;
    public GameObject propertiesPanel;
    public GameObject propertie;
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
            switch (type.value)
            {
                case 0:
                    armorSet.SetActive(false);
                    weaponSet.SetActive(false);
                    break;
                case 1:
                    armorSet.SetActive(true);
                    weaponSet.SetActive(false);
                    foreach (Armor x in armorCollection)
                    {
                        if (x.label == addItem.Value.label)
                        {
                            armorSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value = (int)x.type;
                            armorSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().interactable = false;
                            armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text = x.AC.ToString();
                            armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().interactable = false;
                            armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().text = x.ACCap.ToString();
                            armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().interactable = false;
                            armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().text = x.strReq.ToString();
                            armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().interactable = false;
                            weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn = x.stealthDis;
                            weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().interactable = false;
                        }
                    }
                    break;
                case 2:
                    armorSet.SetActive(false);
                    weaponSet.SetActive(true);
                    foreach (Weapon x in weaponCollection)
                    {
                        if (x.label == addItem.Value.label)
                        {
                            weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value = (int)x.damageType;
                            weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().interactable = false;
                            weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().text = x.dices.ToString();
                            weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().interactable = false;
                            weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text = x.hitDice.ToString();
                            weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().interactable = false;
                            weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().text = x.dist.ToString();
                            weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().interactable = false;
                            weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().text = x.maxDist.ToString();
                            weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().interactable = false;
                            weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn = x.magic;
                            weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().interactable = false;
                            Skill[] buf = weaponSet.GetComponentsInChildren<Skill>();
                            for (int i = 0; i < buf.Length; i++)
                            {
                                Destroy(buf[i].gameObject);
                            }
                            int j = 0;
                            foreach (Weapon.Properties y in x.properties)
                            {
                                GameObject newGameObject = Instantiate(propertie, propertiesPanel.transform);
                                newGameObject.GetComponent<Dropdown>().value = (int)x.properties[j];
                                newGameObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { AddProperties(newGameObject.GetComponent<Dropdown>()); });
                                newGameObject.GetComponent<Dropdown>().interactable = false;
                                j++;
                            }
                            weaponSet.GetComponentInChildren<Amount>().GetComponent<Dropdown>().value = (int)x.type;
                            weaponSet.GetComponentInChildren<Amount>().GetComponent<Dropdown>().interactable = false;
                            break;
                        }
                    }
                    break;
            }
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
        for (int i = 0; i < itemList.Count; i++)
        {
            string label = itemList[i].label;
            if (label == itemLabel)
            {
                int amount;
                int.TryParse(amountField.text, out amount);
                int buf = PlayerPrefs.GetInt(itemAmountSaveName + label) + amount;
                PlayerPrefs.SetInt(itemAmountSaveName + label, buf);
                panel.GetComponentsInChildren<Box>()[i].GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = buf.ToString();
                //временно!!!
                switch (itemList[i].type)
                {
                    case Item.Type.armor:
                        Armor? newArmor = null;// = Armor.LoadArmor(label);
                        if (newArmor == null)
                        {
                            foreach (Armor y in armorList)
                                if (label == y.label)
                                {
                                    if (y.type == Armor.Type.Shield)
                                    {
                                        Weapon shield = new Weapon("щит", 0, 2, 0, 0, false, Weapon.DamageType.Crushing, new Weapon.Properties[] { }, Weapon.Type.Shield);
                                        for (int j = 0; j < amount; j++)
                                        {
                                            weaponList.Add(shield);
                                            AddExistWeaponObject(shield, weaponList.Count - 1);
                                        }
                                        break;
                                    }
                                    for (int j = 0; j < amount; j++)
                                    {
                                        armorList.Add(y);
                                        AddExistArmorObject(y, armorList.Count - 1);
                                    }
                                    break;
                                }
                        }
                        else
                        {
                            for (int j = 0; j < amount; j++)
                            {
                                armorList.Add((Armor)newArmor);
                                AddExistArmorObject((Armor)newArmor, armorList.Count - 1);
                            }
                        }
                        break;
                    case Item.Type.weapon:
                        Weapon? newWeapon = Weapon.LoadWeapon(label);
                        if (newWeapon == null)
                        {
                            foreach (Weapon y in weaponList)
                                if (label == y.label)
                                {
                                    for (int j = 0; j < amount; j++)
                                    {
                                        weaponList.Add(y);
                                        AddExistWeaponObject(y, weaponList.Count - 1);
                                    }
                                    break;
                                }
                        }
                        else
                        {
                            for (int j = 0; j < amount; j++)
                            {
                                weaponList.Add((Weapon)newWeapon);
                                AddExistWeaponObject((Weapon)newWeapon, weaponList.Count - 1);
                            }
                        }
                        break;
                }
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
        if (addItem != null && itemLabel == addItem.Value.label)
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
            if (nameField.text != "" && costField.text != "" && weightField.text != "") // убрать !=
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
                switch (newItem.type)
                {
                    case Item.Type.armor:
                        break;
                    case Item.Type.weapon:
                        CreatNewWeapon(itemLabel);
                        break;
                }
                addExistGameObject(newItem);
            }
        }
        ClearField();
        PlayerPrefs.Save();
    }

    void CreatNewWeapon(string label)
    {
        int dices, hitDices, dist, maxDist;
        bool magic;
        Weapon.DamageType damageType = Weapon.DamageType.Slashing + weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value;
        int.TryParse(weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().text, out dices);
        int.TryParse(weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text, out hitDices);
        int.TryParse(weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().text, out dist);
        int.TryParse(weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().text, out maxDist);
        magic = weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn;
        Skill[] buf = weaponSet.GetComponentsInChildren<Skill>();
        List<Weapon.Properties> properties = new List<Weapon.Properties>();
        for (int i = 0; i < buf.Length; i++)
        {
            if(buf[i].GetComponent<Dropdown>().value<11)
            properties.Add(buf[i].GetComponent<Dropdown>().value + Weapon.Properties.Ammo);
        }
        Weapon.Type type = weaponSet.GetComponentInChildren<Amount>().GetComponent<Dropdown>().value + Weapon.Type.CommonMelee;
        Weapon newWeapon = new Weapon(label, dices, hitDices, dist, maxDist, magic, damageType, properties.ToArray(), type);
        Weapon.SaveWeapon(newWeapon);
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
            if (itemList[itemIndex].type == Item.Type.armor && itemList[itemIndex].label != "щит")
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
            if (itemList[itemIndex].type == Item.Type.weapon || itemList[itemIndex].label == "щит")
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
            if (itemList[itemIndex].type == Item.Type.armor && itemList[itemIndex].label != "щит")
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
            if (itemList[itemIndex].type == Item.Type.weapon || itemList[itemIndex].label == "щит")
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
                Weapon.DeleteWeapon(label);
                UpdateWeapon();
            }


        }
        PlayerPrefs.Save();
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
        bool exception = false;
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
        bool found = false;
        switch (x.type)
        {
            case Item.Type.item:
                itemType = "П";
                break;
            case Item.Type.armor:
                foreach (Armor y in armorCollection)
                    if (x.label == y.label)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            if (y.type == Armor.Type.Shield)
                            {
                                Weapon shield = new Weapon("щит", 0, 2, 0, 0, false, Weapon.DamageType.Crushing, new Weapon.Properties[] { }, Weapon.Type.Shield);
                                weaponList.Add(shield);
                                AddExistWeaponObject(shield, weaponList.Count - 1);
                                continue;
                            }
                            armorList.Add(y);
                            AddExistArmorObject(y, armorList.Count - 1);
                        }
                        found = true;
                        break;
                    }
                itemType = "Б";
                if (!found)
                {
                    Armor? newArmor = null; //= Armor.LoadArmor(x.label);
                    if (newArmor != null)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            armorList.Add((Armor)newArmor);
                            AddExistArmorObject((Armor)newArmor, armorList.Count - 1);
                        }
                    }
                    else
                    {
                        exception = true;
                    }
                }

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
                        found = true;
                        break;
                    }
                itemType = "О";
                if (!found)
                {
                    Weapon? newWeapon = Weapon.LoadWeapon(x.label);
                    if (newWeapon != null)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            weaponList.Add((Weapon)newWeapon);
                            AddExistWeaponObject((Weapon)newWeapon, weaponList.Count - 1);
                        }
                    }
                    else
                    {
                        exception = true;
                    }
                }
                break;

        }
        newItem.GetComponentInChildren<Type>().GetComponentInChildren<Text>().text = itemType;
        newItem.GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = amount.ToString();
        newItem.GetComponent<Box>().db = this;
        newItem.GetComponent<Box>().index = itemList.IndexOf(x);
        if (exception)
        {
            newItem.GetComponent<Box>().DestroyMyself();
            PlayerPrefs.DeleteKey(itemSaveName + itemList.IndexOf(x));
            PlayerPrefs.DeleteKey(itemCostSaveName + x.label);
            PlayerPrefs.DeleteKey(itemMTypeSaveName + x.label);
            PlayerPrefs.DeleteKey(itemWeightSaveName + x.label);
            PlayerPrefs.DeleteKey(itemTypeSaveName + x.label);
            Weapon.DeleteWeapon(x.label);
            itemList.Remove(x);
            PlayerPrefs.SetInt(itemCostSaveName, itemList.Count);
            PlayerPrefs.Save();
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
                type = "Лёгкий";
                break;
            case Armor.Type.Medium:
                type = "Средний";
                break;
            case Armor.Type.Heavy:
                type = "Тяжёлый";
                break;
        };
        newItem.GetComponentInChildren<MType>().GetComponentInChildren<Text>().text = type;
        newItem.GetComponentInChildren<Attribute>().GetComponentInChildren<Text>().text = x.AC.ToString();
        switch (x.ACCap)
        {
            case 0:
                newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = "+ мод ЛОВ" + x.ACCap.ToString();
                break;
            case -1:
                newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = "-";
                break;
            default:
                newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = "+ мод ЛОВ(Макс. " + x.ACCap.ToString() + ")";
                break;
        }
        newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "Сил. " + x.strReq;
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
        if (x.type != Weapon.Type.Shield)
        {
            newItem.GetComponentInChildren<Modifier>().GetComponentInChildren<Text>().text = x.dices + "к" + x.hitDice;
            if (x.dist == x.maxDist)
                newItem.GetComponentInChildren<Attribute>().GetComponentInChildren<Text>().text = x.dist + " фт.";
            else
                newItem.GetComponentInChildren<Attribute>().GetComponentInChildren<Text>().text = x.dist + "/" + x.maxDist;
            newItem.GetComponentInChildren<MType>().GetComponent<Toggle>().isOn = x.magic;
            switch (x.damageType)
            {
                case Weapon.DamageType.Slashing:
                    newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = "Рубящий";
                    break;
                case Weapon.DamageType.Piercing:
                    newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = "Колющий";
                    break;
                case Weapon.DamageType.Crushing:
                    newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = "Дробящий";
                    break;
            }
            Text buf = newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>();
            foreach (Weapon.Properties y in x.properties)
            {
                switch (y)
                {
                    case Weapon.Properties.Ammo:
                        buf.text += "Боеприпас";
                        break;
                    case Weapon.Properties.Distance:
                        buf.text += "Дис.";
                        break;
                    case Weapon.Properties.Fencing:
                        buf.text += "Фехтовальное";
                        break;
                    case Weapon.Properties.Heavy:
                        buf.text += "Тяжёлое";
                        break;
                    case Weapon.Properties.Light:
                        buf.text += "Лёгкое";
                        break;
                    case Weapon.Properties.Reach:
                        buf.text += "Досягаемость";
                        break;
                    case Weapon.Properties.Reload:
                        buf.text += "Перезарядка";
                        break;
                    case Weapon.Properties.Special:
                        buf.text += "Особое";
                        break;
                    case Weapon.Properties.Throwing:
                        buf.text += "Метательное";
                        break;
                    case Weapon.Properties.TwoHanded:
                        buf.text += "Двуручное";
                        break;
                    case Weapon.Properties.Universal:
                        buf.text += "Универсальное " + x.dices + "к" + (x.hitDice + 2);
                        break;
                }
                if (y != x.properties[x.properties.Length - 1])
                {
                    buf.text += ", ";
                }
            }
        }
        else
        {
            DestroyImmediate(newItem.GetComponentInChildren<Skill>().gameObject);
            newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "КД +" + x.hitDice;
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
            if (buf.ACCap == 0)
                manager.UpdateArmorClass(buf.AC, 100);
            else
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

    public void ShowAdditionalSet(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                armorSet.SetActive(false);
                weaponSet.SetActive(false);
                break;
            case 1:
                armorSet.SetActive(true);
                weaponSet.SetActive(false);
                break;
            case 2:
                armorSet.SetActive(false);
                weaponSet.SetActive(true);
                break;
        }
    }

    public void AddProperties(Dropdown obj)
    {
        if (obj.value >= 11)
        {
            Destroy(obj.gameObject);
            //Skill[] buf = propertiesPanel.GetComponentsInChildren<Skill>();
            //buf[buf.Length - 1].GetComponent<Dropdown>().interactable = true;
        }
        else
        {
            //obj.interactable = false;
            if (propertiesPanel.GetComponentsInChildren<Skill>()[propertiesPanel.GetComponentsInChildren<Skill>().Length - 1].GetComponent<Dropdown>().value < 11)
            {
                GameObject newGameObject = Instantiate(propertie, propertiesPanel.transform);
                newGameObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { AddProperties(newGameObject.GetComponent<Dropdown>()); });
            }
        }
    }
}
