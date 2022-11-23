using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchField : MonoBehaviour
{
    bool flag = false;
    public int searchCount = 5;

    [SerializeField] GameObject itemCollection;
    [SerializeField] GameObject searchObject;
    [SerializeField] GameObject searchedItems;
    ItemsRedactor redactor;
    Item[] items;

    private void Start()
    {
        items = itemCollection.GetComponent<LoadInventoryManager>().GetItems();
        redactor = GetComponentInParent<ItemsRedactor>();
    }

    public void ShowSearch()
    {
        string s = GetComponent<InputField>().text;
        s = s.ToLower();
        ClearSearchedItems();
        if (s != "" && flag)
        {
            List<Item> searchList = new List<Item>();
            foreach (Item x in items)
            {
                string buf = x.label.ToLower();
                if (buf.Contains(s))
                    searchList.Add(x);
            }
            Sorts sorts = new Sorts(s);
            searchList.Sort(sorts.Compare);
            for (int i = 0; i < searchList.Count && i < searchCount; i++)
            {
                GameObject b = Instantiate(searchObject, searchedItems.transform);
                b.GetComponentInChildren<Text>().text = searchList[i].label[0].ToString().ToUpper() + searchList[i].label.Remove(0, 1);
                Option option = b.GetComponent<Option>();
                option.item = searchList[i];
                option.Chosen = ShowFoundedItem;
            }
        }
        else
            flag = true;

    }



    public void ShowFoundedItem(Item item)
    {
        redactor.SetItem(item);
        ClearSearchedItems();
    }

    void ClearSearchedItems()
    {
        Option[] options = GetComponentsInChildren<Option>();
        foreach (Option x in options)
            x.DestroyMyself();
    }
}

