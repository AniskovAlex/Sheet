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
        List<List<List<(int, Item)>>> list = playersClass.GetItems();
        foreach (List<List<(int, Item)>> x in list)
        {
            GameObject parent = Instantiate(ObjectPanel, transform);
            int i = 1;
            foreach (List<(int, Item)> y in x)
            {
                if (x.Count > 1)
                {
                    ShowList(y, parent);
                    if (i != x.Count)
                        Instantiate(text, parent.transform).text = "или";
                }
                else
                    ShowItems(y, parent);
                i++;
            }
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
                if (itemList.IndexOf(g) <= itemList.IndexOf(y)) ;
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
                case -3:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Простое оружие";
                    else
                        Instantiate(text, itemsParent.transform).text = "Простое оружие" + " x" + x.Item1;
                    break;
                case -4:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Музыкальные инструменты";
                    else
                        Instantiate(text, itemsParent.transform).text = "Музыкальные инструменты" + " x" + x.Item1;
                    break;
                case -5:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Воинское рукопашное оружие";
                    else
                        Instantiate(text, itemsParent.transform).text = "Воинское рукопашное оружие" + " x" + x.Item1;
                    break;
                case -6:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Магическая фокусировка";
                    else
                        Instantiate(text, itemsParent.transform).text = "Магическая фокусировка" + " x" + x.Item1;
                    break;
                case -7:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Простое рукопашное оружие";
                    else
                        Instantiate(text, itemsParent.transform).text = "Простое рукопашное оружие" + " x" + x.Item1;
                    break;
                case -8:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Фокусировка друида";
                    else
                        Instantiate(text, itemsParent.transform).text = "Фокусировка друида" + " x" + x.Item1;
                    break;
                case -9:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = "Священный символ";
                    else
                        Instantiate(text, itemsParent.transform).text = "Священный символ" + " x" + x.Item1;
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
                            itemList.Add((x.Item1, y));
                            break;
                        }
                    break;
                case -1:
                    if (x.Item1 <= 1)
                        Instantiate(text, itemsParent.transform).text = x.Item2.label;
                    else
                        Instantiate(text, itemsParent.transform).text = x.Item2.label + " x" + x.Item1;
                    itemList.Add(x);
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
                case -3:
                    Instantiate(text, itemsParent.transform).text = "Простое оружие";
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
                                if (weapon.weaponType == Weapon.WeaponType.CommonDist || weapon.weaponType == Weapon.WeaponType.CommonMelee)
                                    options.Add(new Dropdown.OptionData(y.label));

                            }
                        }
                        options.Add(new Dropdown.OptionData("Пусто"));
                        newItem.options = options;
                    }
                    break;
                case -4:
                    Instantiate(text, itemsParent.transform).text = "Музыкальный инструмент";
                    for (int i = 0; i < x.Item1; i++)
                    {
                        Dropdown newItem = Instantiate(dropdown, itemsParent.transform);
                        dropdowns.Add(newItem);
                        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
                        foreach (Item y in items)
                        {
                            if (y.id >= 122 && y.id <= 131)
                                options.Add(new Dropdown.OptionData(y.label));
                        }
                        options.Add(new Dropdown.OptionData("Пусто"));
                        newItem.options = options;
                    }
                    break;
                case -5:
                    Instantiate(text, itemsParent.transform).text = "Воинское рукопашное оружие";
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
                                if (weapon.weaponType == Weapon.WeaponType.WarMelee)
                                    options.Add(new Dropdown.OptionData(y.label));

                            }
                        }
                        options.Add(new Dropdown.OptionData("Пусто"));
                        newItem.options = options;
                    }
                    break;
                case -6:
                    Instantiate(text, itemsParent.transform).text = "Магическая фокусировка";
                    for (int i = 0; i < x.Item1; i++)
                    {
                        Dropdown newItem = Instantiate(dropdown, itemsParent.transform);
                        dropdowns.Add(newItem);
                        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
                        foreach (Item y in items)
                        {

                            if (y.id >= 44 && y.id <= 47)
                                options.Add(new Dropdown.OptionData(y.label));


                        }
                        options.Add(new Dropdown.OptionData("Пусто"));
                        newItem.options = options;
                    }
                    break;
                case -7:
                    Instantiate(text, itemsParent.transform).text = "Простое рукопашное оружие";
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
                                if (weapon.weaponType == Weapon.WeaponType.CommonMelee)
                                    options.Add(new Dropdown.OptionData(y.label));

                            }
                        }
                        options.Add(new Dropdown.OptionData("Пусто"));
                        newItem.options = options;
                    }
                    break;
                case -8:
                    Instantiate(text, itemsParent.transform).text = "Фокусировка друида";
                    for (int i = 0; i < x.Item1; i++)
                    {
                        Dropdown newItem = Instantiate(dropdown, itemsParent.transform);
                        dropdowns.Add(newItem);
                        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
                        foreach (Item y in items)
                        {

                            if (y.id >= 86 && y.id <= 89)
                                options.Add(new Dropdown.OptionData(y.label));


                        }
                        options.Add(new Dropdown.OptionData("Пусто"));
                        newItem.options = options;
                    }
                    break;
                case -9:
                    Instantiate(text, itemsParent.transform).text = "Священный символ";
                    for (int i = 0; i < x.Item1; i++)
                    {
                        Dropdown newItem = Instantiate(dropdown, itemsParent.transform);
                        dropdowns.Add(newItem);
                        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
                        foreach (Item y in items)
                        {

                            if (y.id >= 72 && y.id <= 74)
                                options.Add(new Dropdown.OptionData(y.label));


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
