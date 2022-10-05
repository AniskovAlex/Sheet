using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormCreater : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] GameObject charUp;
    [SerializeField] GameObject dropdown;
    public GameObject consumable;
    public GameObject basicText;
    GameObject discription;
    List<(string, string)> list;
    Ability ability = null;

    private void Awake()
    {
        discription = GetComponentInChildren<Discription>().gameObject;
    }

    public void CreateAbility(Ability ability)
    {
        this.ability = ability;
        head.text = ability.head;
        switch (ability.type)
        {
            case Ability.Type.abilitie:
                foreach ((int, string) x in ability.discription)
                    SetText(x);
                break;
            case Ability.Type.charUp:
                Instantiate(charUp, discription.transform);
                break;
            case Ability.Type.subClass:
                Dropdown subClass = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                subClass.ClearOptions();
                foreach ((int, string) x in ability.discription)
                    subClass.options.Add(new Dropdown.OptionData(x.Item2));
                subClass.options.Add(new Dropdown.OptionData("Пусто"));
                subClass.onValueChanged.AddListener(delegate { GetComponentInParent<ClassesAbilities>().ChosenSubClass(subClass, this); });
                subClass.value = ability.discription.Count;
                break;
            case Ability.Type.withChoose:
                if (ability.isUniq)
                    list = FileSaverAndLoader.LoadList(ability.pathToList);
                else
                    list = new List<(string, string)>(ability.list);
                bool found = false;
                foreach ((string, List<string>) x in PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName))
                {
                    found = true;
                    list.RemoveAll(g => x.Item2.Contains(g.Item1));
                }
                if (!found)
                    PresavedLists.preLists.Add((ability.listName, new List<string>()));
                PresavedLists.ChangePing += UpdateOptions;
                for (int i = 0; i < ability.chooseCount; i++)
                {
                    Dropdown chooseDrop = Instantiate(dropdown, discription.transform).GetComponent<Dropdown>();
                    Text listDis = Instantiate(basicText, discription.transform).GetComponent<Text>();
                    listDis.text = "";
                    chooseDrop.GetComponent<DropdownExtend>().text = listDis;
                    chooseDrop.ClearOptions();
                    foreach ((string, string) x in list)
                        chooseDrop.options.Add(new Dropdown.OptionData(x.Item1));
                    chooseDrop.options.Add(new Dropdown.OptionData("Пусто"));
                    chooseDrop.onValueChanged.AddListener(delegate
                    {
                        ChangeSelected(chooseDrop);
                    });
                    chooseDrop.value = ability.list.Length;
                }
                break;
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



    void UpdateOptions(string listName)
    {
        if (listName == ability.listName)
        {
            Dropdown[] dropdowns = GetComponentsInChildren<Dropdown>();
            if (ability.isUniq)
                list = FileSaverAndLoader.LoadList(ability.pathToList);
            else
                list = new List<(string, string)>(ability.list);
            foreach ((string, List<string>) x in PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName))
                list.RemoveAll(g => x.Item2.Contains(g.Item1));
            foreach (Dropdown x in dropdowns)
            {
                x.options.Clear();
                foreach ((string, string) y in list)
                    x.options.Add(new Dropdown.OptionData(y.Item1));
                x.options.Add(new Dropdown.OptionData("Пусто"));
            }
        }
    }

    void ChangeSelected(Dropdown dropdown)
    {
        string oldValue = dropdown.GetComponent<DropdownExtend>().currentValueText;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        foreach ((string, string) x in list)
            if (x.Item1 == dropdown.captionText.text)
            {
                dropdown.GetComponent<DropdownExtend>().text.text = x.Item2;
                break;
            }
        PresavedLists.UpdatePrelist(ability.listName, oldValue, dropdown.captionText.text);
    }

    private void OnDestroy()
    {
        if (ability.type == Ability.Type.withChoose)
        {
            PresavedLists.ChangePing -= UpdateOptions;
            foreach (Dropdown x in GetComponentsInChildren<Dropdown>())
                PresavedLists.RemoveFromPrelist(ability.listName, x.GetComponent<DropdownExtend>().currentValueText);
        }
    }
}
