using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFeat : MonoBehaviour
{
    [SerializeField] Dropdown chosenFeat;
    [SerializeField] GameObject basicText;
    [SerializeField] GameObject discription;
    [SerializeField] GameObject dropdown;
    [SerializeField] FormCreater form;
    List<string> attrAdd = new List<string>();
    Feat currentFeat = null;
    public Action check;
    bool bufFlag = false;

    List<Feat> list = null;
    // Start is called before the first frame update
    private void Awake()
    {
        list = new List<Feat>(FileSaverAndLoader.LoadFeats());
        List<Feat> buf = PresavedLists.feats.FindAll(g => !g.mul);
        for (int i = 0; i < list.Count; i++)
            foreach (Feat x in buf)
                if (list[i].id == x.id)
                {
                    list.RemoveAt(i);
                    i--;
                    break;
                }
        bool firtsLevel = false;
        int[] attr = null;
        AttributesCreater attributes;
        ClassesAbilities classesAbilities;
        PlayersClass playersClas = null;
        if (CharacterData.GetLevel() == 0)
        {
            firtsLevel = true;
            attributes = FindObjectOfType<AttributesCreater>();
            classesAbilities = FindObjectOfType<ClassesAbilities>();
            if (attributes != null)
                attr = attributes.GetAttributes();
            if (classesAbilities != null)
                playersClas = classesAbilities.GetClass();
        }
        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                switch (list[i].req)
                {
                    default:
                        break;
                    case 1:
                        bool flag = false;
                        if (!firtsLevel)
                        {
                            foreach ((int, PlayersClass) y in CharacterData.GetClasses())
                                if (y.Item2.magic != 0)
                                    flag = true;
                        }
                        else if (playersClas != null && playersClas.magic != 0)
                        {
                            flag = true;
                        }
                        if (!flag)
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                    case 2:
                        if (!firtsLevel)
                        {
                            if (CharacterData.GetAtribute(0) < 13)
                            {
                                list.Remove(list[i]);
                                i--;
                            }
                        }
                        else if (attr != null && attr[0] < 13)
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                    case 3:
                        if (!firtsLevel)
                        {
                            if (CharacterData.GetAtribute(5) < 13)
                            {
                                list.Remove(list[i]);
                                i--;
                            }
                            break;
                        }
                        else if (attr != null && attr[5] < 13)
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                    case 4:
                        if (!firtsLevel)
                        {
                            if (!CharacterData.GetArmorProficiency().Contains(Armor.ArmorType.Light))
                            {
                                list.Remove(list[i]);
                                i--;
                            }
                        }
                        else if (!PresavedLists.armorTypes.Contains(Armor.ArmorType.Light))
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                    case 5:
                        if (!firtsLevel)
                        {
                            if (!CharacterData.GetArmorProficiency().Contains(Armor.ArmorType.Medium))
                            {
                                list.Remove(list[i]);
                                i--;
                            }
                        }
                        else if (!PresavedLists.armorTypes.Contains(Armor.ArmorType.Medium))
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                    case 6:
                        if (!firtsLevel)
                        {
                            if (!CharacterData.GetArmorProficiency().Contains(Armor.ArmorType.Heavy))
                            {
                                list.Remove(list[i]);
                                i--;
                            }
                        }
                        else if (!PresavedLists.armorTypes.Contains(Armor.ArmorType.Heavy))
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                    case 7:
                        if (!firtsLevel)
                        {
                            if (CharacterData.GetAtribute(1) < 13)
                            {
                                list.Remove(list[i]);
                                i--;
                            }
                        }
                        else if (attr != null && attr[1] < 13)
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                    case 8:
                        if (!firtsLevel)
                        {
                            if (CharacterData.GetAtribute(3) < 13 && CharacterData.GetAtribute(4) < 13)
                            {
                                list.Remove(list[i]);
                                i--;
                            }
                        }
                        else if (attr != null && attr[3] < 13 && attr[4] < 13)
                        {
                            list.Remove(list[i]);
                            i--;
                        }
                        break;
                }
            }
        }
        chosenFeat.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("Пусто"));
        foreach (Feat x in list)
        {
            options.Add(new Dropdown.OptionData(x.ability.head));
        }
        chosenFeat.AddOptions(options);

    }

    public Dropdown GetDropdown()
    {
        return chosenFeat;
    }

    public void FeatDiscription()
    {
        /*Text[] texts = discription.GetComponentsInChildren<Text>();
        foreach (Text x in texts)
            Destroy(x.gameObject);*/
        FormCreater[] abilities = discription.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in abilities)
            DestroyImmediate(x.gameObject);
        Dropdown[] dropdowns = discription.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in dropdowns)
            DestroyImmediate(x.gameObject);
        Text[] texts = discription.GetComponentsInChildren<Text>();
        foreach (Text x in texts)
            DestroyImmediate(x.gameObject);
        foreach (string x in attrAdd)
        {
            PresavedLists.RemoveFromAttrAdd(x);
        }
        attrAdd.Clear();
        foreach (Feat x in list)
        {
            if (x.ability.head == chosenFeat.captionText.text)
            {
                if (x.attr != null && x.attr.Count > 0)
                {
                    foreach (int y in x.attr)
                        switch (y)
                        {
                            case 0:
                                attrAdd.Add("Сила");
                                break;
                            case 1:
                                attrAdd.Add("Ловкость");
                                break;
                            case 2:
                                attrAdd.Add("Телосложение");
                                break;
                            case 3:
                                attrAdd.Add("Интеллект");
                                break;
                            case 4:
                                attrAdd.Add("Мудрость");
                                break;
                            case 5:
                                attrAdd.Add("Харизма");
                                break;
                        }
                    if (x.attr.Count > 1)
                    {
                        SetText((1, "Повысить характеристику"));
                        GameObject newObject = Instantiate(dropdown, discription.transform);
                        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
                        foreach (string y in attrAdd)
                        {
                            list.Add(new Dropdown.OptionData(y));
                        }
                        list.Add(new Dropdown.OptionData("Пусто"));
                        list.Add(new Dropdown.OptionData("Пусто"));
                        newObject.GetComponent<Dropdown>().options = list;
                        newObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { ChangeSelected(newObject.GetComponent<Dropdown>()); });

                    }
                    else
                    {
                        SetText((1, "Повысить характеристику: " + attrAdd[0]));
                        PresavedLists.UpdateAttrAdd(attrAdd[0]);
                    }
                }
                /*foreach ((int, string) y in x.ability.discription)
                    SetText(y);*/
                if (currentFeat != null)
                    PresavedLists.feats.Remove(currentFeat);
                currentFeat = x;
                PresavedLists.feats.Add(x);
                if (currentFeat.ability != null)
                {
                    FormCreater buf = Instantiate(form, discription.transform);
                    buf.CreateAbility(currentFeat.ability);
                    buf.RemoveHead();
                }
                break;
            }
        }
        if (check != null)
            check();
        ContentSizer content;
        if (discription.TryGetComponent(out content))
            content.Resize();
    }


    void SetText((int, string) preText)
    {
        int textSize;
        FontStyle fontStyle;
        Text newObjectText = Instantiate(basicText, discription.transform).GetComponent<Text>();
        switch (preText.Item1)
        {
            default:
            case 0:
                textSize = 40;
                fontStyle = FontStyle.Italic;
                break;
            case 1:
                textSize = 40;
                fontStyle = FontStyle.Normal;
                break;
            case 2:
                textSize = 60;
                fontStyle = FontStyle.Bold;
                break;
        }
        newObjectText.text = preText.Item2;
        newObjectText.fontSize = textSize;
        newObjectText.fontStyle = fontStyle;

    }

    void ChangeSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        attrAdd.Remove(oldValue);
        if (dropdown.captionText.text != "Пусто")
            attrAdd.Add(dropdown.captionText.text);
        PresavedLists.UpdateAttrAdd(oldValue, dropdown.captionText.text);
        if (currentFeat != null && currentFeat.ability.listName == "Resilent")
        {
            int saveRemove = -1;
            if (!bufFlag)
            {
                switch (oldValue)
                {
                    case "Сила":
                        saveRemove = 0;
                        break;
                    case "Ловкость":
                        saveRemove = 1;
                        break;
                    case "Телосложение":
                        saveRemove = 2;
                        break;
                    case "Интеллект":
                        saveRemove = 3;
                        break;
                    case "Мудрость":
                        saveRemove = 4;
                        break;
                    case "Харизма":
                        saveRemove = 5;
                        break;
                }
                PresavedLists.saveThrows.Remove(saveRemove);
            }
            bufFlag = false;
            int saveAdd = -1;
            switch (dropdown.captionText.text)
            {
                case "Сила":
                    saveAdd = 0;
                    break;
                case "Ловкость":
                    saveAdd = 1;
                    break;
                case "Телосложение":
                    saveAdd = 2;
                    break;
                case "Интеллект":
                    saveAdd = 3;
                    break;
                case "Мудрость":
                    saveAdd = 4;
                    break;
                case "Харизма":
                    saveAdd = 5;
                    break;
            }
            if (PresavedLists.saveThrows.Contains(saveAdd))
                bufFlag = true;
            else
                PresavedLists.saveThrows.Add(saveAdd);
        }
    }

    private void OnDestroy()
    {
        PresavedLists.feats.Remove(currentFeat);
        foreach (string x in attrAdd)
        {
            PresavedLists.RemoveFromAttrAdd(x);
            if (currentFeat != null && currentFeat.ability.listName == "Resilent")
            {
                int saveRemove = -1;
                if (!bufFlag)
                {
                    switch (x)
                    {
                        case "Сила":
                            saveRemove = 0;
                            break;
                        case "Ловкость":
                            saveRemove = 1;
                            break;
                        case "Телосложение":
                            saveRemove = 2;
                            break;
                        case "Интеллект":
                            saveRemove = 3;
                            break;
                        case "Мудрость":
                            saveRemove = 4;
                            break;
                        case "Харизма":
                            saveRemove = 5;
                            break;
                    }
                    PresavedLists.saveThrows.Remove(saveRemove);
                }
            }
        }
    }

    private void OnDisable()
    {
        PresavedLists.feats.Remove(currentFeat);
        foreach (string x in attrAdd)
        {
            PresavedLists.RemoveFromAttrAdd(x);
            if (currentFeat != null && currentFeat.ability.listName == "Resilent")
            {
                int saveRemove = -1;
                if (!bufFlag)
                {
                    switch (x)
                    {
                        case "Сила":
                            saveRemove = 0;
                            break;
                        case "Ловкость":
                            saveRemove = 1;
                            break;
                        case "Телосложение":
                            saveRemove = 2;
                            break;
                        case "Интеллект":
                            saveRemove = 3;
                            break;
                        case "Мудрость":
                            saveRemove = 4;
                            break;
                        case "Харизма":
                            saveRemove = 5;
                            break;
                    }
                    PresavedLists.saveThrows.Remove(saveRemove);
                }
            }
        }
    }
}
