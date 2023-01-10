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
    [SerializeField] ScreensControler cam;

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
                    ShowList(y, parent, x.Count);
                    if (i != x.Count)
                    {
                        Text OR = Instantiate(text, parent.transform);
                        OR.text = "или";
                        OR.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 80);
                    }
                }
                else
                    ShowItems(y, parent, 1);
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
        for (int i = 0; i < itemList.Count; i++)

            for (int j = i + 1; j < itemList.Count; j++)
                if (itemList[j].Item2.id != -1 && itemList[i].Item2 == itemList[j].Item2)
                {
                    (int, Item) buf = (itemList[i].Item1 + itemList[j].Item1, itemList[i].Item2);
                    itemList.RemoveAt(i);
                    itemList.Insert(i, buf);
                    itemList.RemoveAt(j);
                    j--;
                }
        return itemList;
    }

    void ShowList(List<(int, Item)> list, GameObject parent, int listCount)
    {
        GameObject itemsParent = Instantiate(ObjectList, parent.transform);
        Vector2 vector2 = new Vector2(cam.GetPanelWidth() / listCount - 120, 0);
        itemsParent.GetComponent<RectTransform>().sizeDelta = vector2;
        Text listText = Instantiate(text, itemsParent.transform);
        listText.text = "";
        foreach ((int, Item) x in list)
        {
            switch (x.Item2.id)
            {
                default:
                    foreach (Item y in items)
                        if (x.Item2.id == y.id)
                        {
                            if (x.Item1 <= 1)
                                listText.text += y.label;
                            else
                                listText.text += y.label + " x" + x.Item1;
                            break;
                        }
                    break;
                case -1:
                    if (x.Item1 <= 1)
                        listText.text += x.Item2.label;
                    else
                        listText.text += x.Item2.label + " x" + x.Item1;
                    break;
                case -2:
                    if (x.Item1 <= 1)
                        listText.text += "Воинское оружие";
                    else
                        listText.text += "Воинское оружие" + " x" + x.Item1;
                    break;
                case -3:
                    if (x.Item1 <= 1)
                        listText.text += "Простое оружие";
                    else
                        listText.text += "Простое оружие" + " x" + x.Item1;
                    break;
                case -4:
                    if (x.Item1 <= 1)
                        listText.text += "Музыкальные инструменты";
                    else
                        listText.text += "Музыкальные инструменты" + " x" + x.Item1;
                    break;
                case -5:
                    if (x.Item1 <= 1)
                        listText.text += "Воинское рукопашное оружие";
                    else
                        listText.text += "Воинское рукопашное оружие" + " x" + x.Item1;
                    break;
                case -6:
                    if (x.Item1 <= 1)
                        listText.text += "Магическая фокусировка";
                    else
                        listText.text += "Магическая фокусировка" + " x" + x.Item1;
                    break;
                case -7:
                    if (x.Item1 <= 1)
                        listText.text += "Простое рукопашное оружие";
                    else
                        listText.text += "Простое рукопашное оружие" + " x" + x.Item1;
                    break;
                case -8:
                    if (x.Item1 <= 1)
                        listText.text += "Фокусировка друида";
                    else
                        listText.text += "Фокусировка друида" + " x" + x.Item1;
                    break;
                case -9:
                    if (x.Item1 <= 1)
                        listText.text += "Священный символ";
                    else
                        listText.text += "Священный символ" + " x" + x.Item1;
                    break;
            }
            listText.text += "\n";
        }
        listText.text = listText.text.Remove(listText.text.Length - 1);
        Instantiate(button, itemsParent.transform).onClick.AddListener(delegate { SelectedList(parent, list); });
    }

    void ShowItems(List<(int, Item)> list, GameObject parent, int listCount)
    {
        GameObject itemsParent = Instantiate(ObjectList, parent.transform);
        Vector2 vector2 = new Vector2(cam.GetPanelWidth() / listCount - 120, 0);
        itemsParent.GetComponent<RectTransform>().sizeDelta = vector2;
        Text listText = Instantiate(text, itemsParent.transform);
        listText.text = "";
        foreach ((int, Item) x in list)
        {
            switch (x.Item2.id)
            {
                default:
                    foreach (Item y in items)
                        if (y.id == x.Item2.id)
                        {
                            if (x.Item1 <= 1)
                                listText.text += y.label;
                            else
                                listText.text += y.label + " x" + x.Item1;
                            itemList.Add((x.Item1, y));
                            break;
                        }
                    break;
                case -1:
                    if (x.Item1 <= 1)
                        listText.text += x.Item2.label;
                    else
                        listText.text += x.Item2.label + " x" + x.Item1;
                    itemList.Add(x);
                    break;
                case -2:
                    listText.text += "Воинское оружие";
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
                    listText.text += "Простое оружие";
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
                    listText.text += "Музыкальный инструмент";
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
                    listText.text += "Воинское рукопашное оружие";
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
                    listText.text += "Магическая фокусировка";
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
                    listText.text += "Простое рукопашное оружие";
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
                    listText.text += "Фокусировка друида";
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
                    listText.text += "Священный символ";
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
            listText.text += "\n";
        }
        listText.text = listText.text.Remove(listText.text.Length - 1);
    }

    void SelectedList(GameObject parent, List<(int, Item)> list)
    {
        foreach (Transform x in parent.transform)
            Destroy(x.gameObject);
        ShowItems(list, parent, 1);
    }

    private void OnDisable()
    {
        foreach (Transform x in gameObject.GetComponentsInChildren<Transform>())
            if (x.gameObject != gameObject)
                Destroy(x.gameObject);
        itemList.Clear();
    }
}
