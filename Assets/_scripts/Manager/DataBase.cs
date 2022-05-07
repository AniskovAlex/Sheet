using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    List<Item> list;
    float timeWait = 0;
    bool deleted = false;
    // Start is called before the first frame update
    void Start()
    {
        list = ItemCollection.GetCollection().GetList();
        ItemCollection.GetCollection().ShowCollection();
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

    public GameObject item;
    public GameObject panel;
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
            foreach (Item x in list)
            {
                if (x.label.StartsWith(s))
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

    public void DestroyInChil(float t)
    {
        timeWait = t;
        deleted = true;
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
        PlayerPrefs.Save();
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
            PlayerPrefs.DeleteKey(itemSaveName + (itemsCount-1));
            itemsCount--;
            PlayerPrefs.SetInt(itemsCountSaveName, itemsCount);
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
                moneyType = "ÇÌ:";
                break;
            case Item.MType.silverCoin:
                moneyType = "ÑÌ:";
                break;
            case Item.MType.copperCoin:
                moneyType = "ÌÌ:";
                break;
        }
        newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = moneyType + " " + x.cost;
        switch (x.weight)
        {
            case -1:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "1/2 ôíò";
                break;
            case -2:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = "1/4 ôíò";
                break;
            default:
                newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = x.weight + " ôíò";
                break;
        }
        string itemType = "";
        switch (x.type)
        {
            case Item.Type.item:
                itemType = "Ï";
                break;
            case Item.Type.armor:
                itemType = "Á";
                break;
            case Item.Type.weapon:
                itemType = "Î";
                break;
        }
        newItem.GetComponentInChildren<Type>().GetComponentInChildren<Text>().text = itemType;
        newItem.GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(itemAmountSaveName + x.label).ToString();
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
                moneyType = "ÇÌ:";
                break;
            case 1:
                moneyType = "ÑÌ:";
                break;
            case 2:
                moneyType = "ÌÌ:";
                break;
        }
        newItem.GetComponentInChildren<Money>().GetComponentInChildren<Text>().text = moneyType + " " + PlayerPrefs.GetInt(itemCostSaveName + label);
        newItem.GetComponentInChildren<Weight>().GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(itemWeightSaveName + label).ToString() + " ôíò";
        string type = "";
        int typeInt = PlayerPrefs.GetInt(itemTypeSaveName + label);
        switch (typeInt)
        {
            case 0:
                type = "Ï";
                break;
            case 1:
                type = "Á";
                break;
            case 2:
                type = "Î";
                break;
        }
        newItem.GetComponentInChildren<Type>().GetComponentInChildren<Text>().text = type;
        newItem.GetComponentInChildren<Amount>().GetComponentInChildren<Text>().text = PlayerPrefs.GetInt(itemAmountSaveName + label).ToString();
        newItem.GetComponent<Box>().db = this;
        newItem.GetComponent<Box>().label = label;
    }

    void LoadItems()
    {
        if (PlayerPrefs.HasKey(itemsCountSaveName))
        {
            itemsCount = PlayerPrefs.GetInt(itemsCountSaveName);
            Debug.Log(itemsCount);
            for (int i = 0; i < itemsCount; i++)
            {

                bool found = false;
                string label = PlayerPrefs.GetString(itemSaveName + i);
                if (PlayerPrefs.HasKey(label))
                    continue;
                foreach (Item x in list)
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
        LoadItems();
    }
}
