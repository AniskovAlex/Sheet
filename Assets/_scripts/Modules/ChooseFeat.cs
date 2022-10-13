using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseFeat : MonoBehaviour
{
    [SerializeField] Dropdown chosenFeat;
    [SerializeField] GameObject basicText;
    [SerializeField] GameObject discription;
    [SerializeField] GameObject dropdown;
    List<string> attrAdd = new List<string>();

    Feat[] feats = null;
    // Start is called before the first frame update
    private void Awake()
    {
        feats = FileSaverAndLoader.LoadFeats();
        chosenFeat.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData("Пусто"));
        foreach (Feat x in feats)
        {
            options.Add(new Dropdown.OptionData(x.head));
        }
        chosenFeat.AddOptions(options);

    }

    public Dropdown GetDropdown()
    {
        return chosenFeat;
    }

    public void FeatDiscription()
    {
        Text[] texts = discription.GetComponentsInChildren<Text>();
        foreach (Text x in texts)
            Destroy(x.gameObject);
        Dropdown[] dropdowns = discription.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in dropdowns)
            Destroy(x.gameObject);
        foreach (Feat x in feats)
        {
            if (x.head == chosenFeat.captionText.text)
            {
                if (x.attr != null)
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
                        SetText((1, "Повысить характеристику: " + attrAdd));
                        PresavedLists.UpdateAttrAdd(attrAdd[0]);
                    }
                }
                foreach ((int, string) y in x.discription)
                    SetText(y);
            }
        }
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
                fontStyle = FontStyle.Normal;
                break;
            case 1:
                textSize = 40;
                fontStyle = FontStyle.Italic;
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
    }

    private void OnDestroy()
    {
        foreach (string x in attrAdd)
            PresavedLists.RemoveFromAttrAdd(x);
    }

    private void OnBecameInvisible()
    {
        foreach (string x in attrAdd)
            PresavedLists.RemoveFromAttrAdd(x);
    }
}
