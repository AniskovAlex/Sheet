using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayersClass : ObjectsBehavior
{
    protected string characterName = CharacterCollection.GetName();
    const string armorProfSaveName = "armorProf_";
    const string weaponProfSaveName = "weaponProf_";
    const string saveThrowProfSaveName = "save_";
    protected const string levelCountSaveName = "lvlCount_";
    protected const string levelSaveName = "lvl_";
    protected const string levelLabelSaveName = "lvlLabel_";

    int healthDice;
    List<Armor.ArmorType> armorProfs;
    List<Weapon.WeaponType> weaponProfs;
    int instrumentsAmount;
    List<string> instrumentProfs;
    int skillsAmount;
    List<int> skillProfs;
    List<int> savethrowProfs;
    string name = "Класс";

    protected int level = 0;
    protected int mainState;
    
    protected int PB;
    

    protected PlayersClass(int healthDice, List<Armor.ArmorType> armorProfs, List<Weapon.WeaponType> weaponProfs, int instrumentsAmount, List<string> instrumentProfs, int skillsAmount, List<int> skillProfs, List<int> savethrowProfs,
        int level, int mainState, GameObject panel, GameObject basicForm, GameObject dropdownForm, int PB, bool redact) : base(panel, basicForm, dropdownForm, redact)
    {
        this.healthDice = healthDice;
        this.armorProfs = armorProfs;
        this.weaponProfs = weaponProfs;
        this.instrumentsAmount = instrumentsAmount;
        this.instrumentProfs = instrumentProfs;
        this.skillsAmount = skillsAmount;
        this.skillProfs = skillProfs;
        this.savethrowProfs = savethrowProfs;
        this.level = level;
        this.mainState = mainState;
        this.PB = PB;

        if (redact)
        {
            ClassDiscription();
            ShowAbilities(level);
        }
        else
        {
            ClassDiscription();
            for (int i = 1; i <= level; i++)
            {
                ShowAbilities(i);
            }
        }
    }

    public PlayersClass()
    {

    }

    public virtual void ShowAbilities(int level)
    {

    }

    public List<Armor.ArmorType> GetArmorProfs()
    {
        return armorProfs;
    }

    public List<Weapon.WeaponType> GetWeaponProfs()
    {
        return weaponProfs;
    }
    public List<string> GetInstrumentProfs()
    {
        return instrumentProfs;
    }
    public List<int> GetSkillProfs()
    {
        return skillProfs;
    }

    public List<int> GetSavethrowProfs()
    {
        return savethrowProfs;
    }

    public int GetInstrumentsAmount()
    {
        return instrumentsAmount;
    }

    public int GetSkillsAmount()
    {
        return skillsAmount;
    }

    public string GetName()
    {
        return name;
    }

    public void CreatAbility(string caption, string level, List<(string,string)> includedList)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = caption;
        form.AddText(level, FontStyle.Italic);
        if (redact)
        {
            GameObject newDropDownChoose = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
            Dropdown buf = newDropDownChoose.GetComponent<Dropdown>();
            Text styleDiscriptionText = form.AddText("");
            buf.onValueChanged.AddListener(delegate { Discription(buf, styleDiscriptionText, includedList); });
            buf.onValueChanged.AddListener(delegate { ChooseSubClass(buf); });
            List<string> captionList = new List<string>();
            foreach ((string, string) x in includedList)
                captionList.Add(x.Item1);
            newDropDownChoose.GetComponent<SkillsDropdown>().list = captionList;
            newDropDownChoose.GetComponent<SkillsDropdown>().excludedList = new List<string>();
            List<string> buf1 = new List<string>();
            foreach ((string, string) x in includedList)
            {
                buf1.Add(x.Item1);
            }
            buf.options.Add(new Dropdown.OptionData("Пусто"));
            for (int j = 0; j < buf1.Count; j++)
            {
                buf.options.Add(new Dropdown.OptionData(buf1[j].ToString()));
            }
        }
    }

    void Discription(Dropdown style, Text textField, List<(string, string)> inclcudedList)
    {
        foreach((string,string) x in inclcudedList)
        {
            if(x.Item1 == style.captionText.text)
            {
                textField.text = x.Item2;
                return;
            }
        }
        textField.text = " ";
    }

    public virtual void Save()
    {
        characterName = CharacterCollection.GetName();
        foreach (Weapon.WeaponType x in weaponProfs)
        {
            PlayerPrefs.SetInt(characterName + weaponProfSaveName + x, 1);
        }
        foreach (Armor.ArmorType x in armorProfs)
        {
            PlayerPrefs.SetInt(characterName+armorProfSaveName + x, 1);
        }
        foreach (int x in savethrowProfs)
        {
            PlayerPrefs.SetInt(characterName + saveThrowProfSaveName + x, 1);
        }
    }

    public virtual void ChooseSubClass(Dropdown mySelf)
    {
        FormCreater[] abilitieForms = panel.GetComponentInChildren<FormCreater>().GetComponentInChildren<Discription>().gameObject.GetComponentsInChildren<FormCreater>();
        foreach (FormCreater x in abilitieForms)
        {
            MonoBehaviour.Destroy(x.gameObject);
        }
    }

    public virtual void ClassDiscription()
    {

    }

}
