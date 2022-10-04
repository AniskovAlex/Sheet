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
                    List<string> buf = new List<string>();
                    foreach (int y in x.attr)
                        switch (y)
                        {
                            case 0:
                                buf.Add("Сила");
                                break;
                            case 1:
                                buf.Add("Ловкость");
                                break;
                            case 2:
                                buf.Add("Телосложение");
                                break;
                            case 3:
                                buf.Add("Мудрость");
                                break;
                            case 4:
                                buf.Add("Интеллект");
                                break;
                            case 5:
                                buf.Add("Харизма");
                                break;
                        }
                    if (x.attr.Count > 1)
                    {
                        SetText((1, "Повысить характеристику"));
                        GameObject newObject = Instantiate(dropdown, discription.transform);
                        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
                        foreach (string y in buf)
                        {
                            list.Add(new Dropdown.OptionData(y));
                        }
                        list.Add(new Dropdown.OptionData("Пусто"));
                        newObject.GetComponent<Dropdown>().options = list;
                    }
                    else
                    {
                        SetText((1, "Повысить характеристику: "+ buf));
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
}
