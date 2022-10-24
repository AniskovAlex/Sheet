using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandartInvetoryPanel : MonoBehaviour
{
    [SerializeField] GameObject ObjectPanel;
    [SerializeField] GameObject ObjectList;
    [SerializeField] Dropdown dropdown;
    [SerializeField] Text text;
    [SerializeField] Button button;
    List<(int, Item)> itemList = new List<(int, Item)>();
    List<Dropdown> dropdowns = new List<Dropdown>();
    Item[] items = null;

    public void SetInventoryChoice(PlayersClass playersClass, Backstory backstory, Item[] items)
    {
        this.items = items;
        List<(List<(int, Item)>, List<(int, Item)>)> list = playersClass.GetItems();
        foreach ((List<(int, Item)>, List<(int, Item)>) x in list)
        {
            GameObject parent = Instantiate(ObjectPanel, transform);
            if (x.Item2 != null)
            {
                ShowList(x.Item1, parent);
                Instantiate(text, parent.transform).text = "или";
                ShowList(x.Item2, parent);
            }
            else
                ShowItems(x.Item1, parent);
        }
    }

    public List<(int, Item)> GetItems()
    {
        foreach (Dropdown x in dropdowns)
            foreach (Item y in items)
                if (x.captionText.text == y.label)
                {
                    itemList.Add((1, y));
                    break;
                }
        itemList.ForEach(g =>
        {
            itemList.ForEach(y =>
            {
                if (itemList.IndexOf(g) <= itemList.IndexOf(y));
                else
                {
                    if (g.Item2 == y.Item2)
                    {
                        g.Item1 += y.Item1;
                        itemList.Remove(y);

                    }
                }
            });
        });
        return itemList;
    }

    void ShowList(List<(int, Item)> list, GameObject parent)
    {
        GameObject itemsParent = Instantiate(ObjectList, parent.transform);
        foreach ((int, Item) x in list)
        {
            switch (x.Item2.id)
            {
                default:
                    foreach (Item y in items)
                        if (x.Item2.id == y.id)
                        {
                            if (x.Item1 <= 1)
                                Instantiate(text, itemsParent.transform).text = y.label;
                            else
                                Instantiate(text, itemsParent.transform).text = y.label + " x" + x.Item1;
                            break;
                        }
                    break;
                case -1:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = x.Item2.label;
                    else
                        Instantiate(text, itemsParent.transform).text = x.Item2.label + " x" + x.Item1;
                    break;
                case -2:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Воинское оружие";
                    else
                        Instantiate(text, itemsParent.transform).text = "Воинское оружие" + " x" + x.Item1;
                    break;
            }
        }
        Instantiate(button, itemsParent.transform).onClick.AddListener(delegate { SelectedList(parent, list); });
    }

    void ShowItems(List<(int, Item)> list, GameObject parent)
    {
        ;
        GameObject itemsParent = Instantiate(ObjectList, parent.transform);
        foreach ((int, Item) x in list)
        {
            switch (x.Item2.id)
            {
                default:
                    foreach (Item y in items)
                        if (y.id == x.Item2.id)
                        {
                            if (x.Item1 <= 1)
                                Instantiate(text, itemsParent.transform).text = y.label;
                            else
                                Instantiate(text, itemsParent.transform).text = y.label + " x" + x.Item1;
                            list.Add((x.Item1, y));
                            break;
                        }
                    break;
                case -1:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = x.Item2.label;
                    else
                        Instantiate(text, itemsParent.transform).text = x.Item2.label + " x" + x.Item1;
                    list.Add(x);
                    break;
                case -2:
                    Instantiate(text, itemsParent.transform).text = "Воинское оружие";
                    for (int i = 0; i < x.Item1; i++)
                    {
                        Dropdown newItem = Instantiate(dropdown, itemsParent.transform);
                        dropdowns.Add(newItem);
                        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
                        foreach (Item y in items)
                        {
                            if (y is Weapon)
                            {
                                Weapon weapon = y as Weapon;
                                if (weapon.weaponType == Weapon.WeaponType.WarDist || weapon.weaponType == Weapon.WeaponType.WarMelee)
                                    options.Add(new Dropdown.OptionData(y.label));

                            }
                        }
                        options.Add(new Dropdown.OptionData("Пусто"));
                        newItem.options = options;
                    }
                    break;
            }
        }
    }

    void SelectedList(GameObject parent, List<(int, Item)> list)
    {
        foreach (Transform x in parent.transform)
            Destroy(x.gameObject);
        ShowItems(list, parent);
    }
}
