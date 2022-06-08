using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    const string FighterBattleStyleCountSaveName = "FghterBattleStyleCount_";
    const string FighterBattleStyleSaveName = "FghterBattleStyle_";

    int mainState;
    GameObject panel;
    public GameObject basicForm;
    public GameObject dropdownForm;
    int PB;
    bool redact = false;

    public Fighter(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { })
    {
        this.mainState = mainState;
        this.panel = panel;
        this.PB = PB;
        this.basicForm = basicForm;
        redact = false;

        for (int i = 1; i <= level; i++)
        {
            ShowAbilities(i);
        }
    }

    public Fighter(GameObject panel, GameObject basicForm, GameObject dropdownForm, int mainState) : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 },
        new List<int> { 0, 1 })
    {
        PB = 2;
        this.mainState = mainState;
        this.panel = panel;
        this.basicForm = basicForm;
        this.dropdownForm = dropdownForm;
        redact = true;

        ShowAbilities(1);
        
    }

    void ShowAbilities(int level)
    {
        switch (level)
        {
            case 1:
                BattleStyle();
                SecondBreath();
                break;
            case 2:
                if (level < 17)
                    ActionSurge(1);
                else
                    ActionSurge(2);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
        }
    }

    void SecondBreath()
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "Второе дыхание";
        form.AddText("1-й уровень, умение воина", FontStyle.Italic);
        form.AddText("Вы обладаете ограниченным источником выносливости, которым можете воспользоваться, чтобы уберечь себя. В свой ход вы можете бонусным действием восстановить хиты в размере 1к10 + ваш уровень воина.\n\nИспользовав это умение, вы должны завершить короткий либо продолжительный отдых, чтобы получить возможность использовать его снова.");
        if (!redact)
            form.AddConsumables(1);
    }

    void ActionSurge(int i)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "Всплеск действий";
        form.AddText("2-й уровень, умение воина", FontStyle.Italic);
        form.AddText("Вы получаете возможность на мгновение преодолеть обычные возможности. В свой ход вы можете совершить одно дополнительное действие помимо обычного и бонусного действий. Использовав это умение, вы должны завершить короткий или продолжительный отдых, чтобы получить возможность использовать его снова. Начиная с 17-го уровня вы можете использовать это умение дважды, прежде чем вам понадобится отдых, но в течение одного хода его всё равно можно использовать лишь один раз.");
        if (!redact)
            form.AddConsumables(i);
    }

    void BattleStyle()
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "Боевой стиль";
        if (redact)
        {
            GameObject newBattleStyle = GameObject.Instantiate(dropdownForm, newObject.GetComponentInChildren<Discription>().transform);
            Dropdown buf = newBattleStyle.GetComponent<Dropdown>();
            Text styleDiscriptionText = form.AddText("");
            buf.onValueChanged.AddListener(delegate { BattleStyleDiscription(buf, styleDiscriptionText); }); 
            List<string> battleStyleList = new List<string>{ "Дуэлянт", "Защита" , "Оборона" , "Сражение большим оружием" , "Сражение двумя оружиями", "Стрельба" };
            List<string> battleStyleExcludedList = new List<string>();
            if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
            {
                int styles = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
                for (int i = 0; i < styles; i++)
                {
                    int style = PlayerPrefs.GetInt(FighterBattleStyleSaveName + i);
                    switch (style)
                    {
                        case 0:
                            battleStyleExcludedList.Add("Дуэлянт");
                            break;
                        case 1:
                            battleStyleExcludedList.Add("Защита"); 
                            break;
                        case 2:
                            battleStyleExcludedList.Add("Оборона"); 
                            break;
                        case 3:
                            battleStyleExcludedList.Add("Сражение большим оружием"); 
                            break;
                        case 4:
                            battleStyleExcludedList.Add("Сражение двумя оружиями"); 
                            break;
                        case 5:
                            battleStyleExcludedList.Add("Стрельба"); 
                            break;
                    }
                }
            }
            newBattleStyle.GetComponent<SkillsDropdown>().list = battleStyleList;
            newBattleStyle.GetComponent<SkillsDropdown>().excludedList = battleStyleExcludedList;
            List<string> buf1 = new List<string>();
            foreach(string x in battleStyleList)
            {
                if (!battleStyleExcludedList.Contains(x))
                    buf1.Add(x);
            }
            buf.options.Add(new Dropdown.OptionData("Пусто"));
            for (int j = 0; j < buf1.Count; j++)
            {
                buf.options.Add(new Dropdown.OptionData(buf1[j].ToString()));
            }
        }
        else
        {
            if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
            {
                int styles = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
                for (int i = 0; i < styles; i++)
                {
                    int style = PlayerPrefs.GetInt(FighterBattleStyleSaveName + i);
                    switch (style)
                    {
                        case 0:
                            form.AddText("Дуэлянт", 30, FontStyle.Bold);
                            form.AddText("Пока вы держите рукопашное оружие в одной руке, и не используете другого оружия, вы получаете бонус +2 к броскам урона этим оружием.");
                            break;
                        case 1:
                            form.AddText("Защита", 30, FontStyle.Bold);
                            form.AddText("Если существо, которое вы видите, атакует не вас, а другое существо, находящееся в пределах 5 футов от вас, вы можете реакцией создать помеху его броску атаки. Для этого вы должны использовать щит.");
                            break;
                        case 2:
                            form.AddText("Оборона", 30, FontStyle.Bold);
                            form.AddText("Пока вы носите доспехи, вы получаете бонус +1 к КД.");
                            break;
                        case 3:
                            form.AddText("Сражение большим оружием", 30, FontStyle.Bold);
                            form.AddText("Если у вас выпало «1» или «2» на кости урона оружия при атаке, которую вы совершали рукопашным оружием, удерживая его двумя руками, то вы можете перебросить эту кость, и должны использовать новый результат, даже если снова выпало «1» или «2». Чтобы воспользоваться этим преимуществом, ваше оружие должно иметь свойство «двуручное» или «универсальное».");
                            break;
                        case 4:
                            form.AddText("Сражение двумя оружиями", 30, FontStyle.Bold);
                            form.AddText("Если вы сражаетесь двумя оружиями, вы можете добавить модификатор характеристики к урону от второй атаки.");
                            break;
                        case 5:
                            form.AddText("Стрельба", 30, FontStyle.Bold);
                            form.AddText("Вы получаете бонус +2 к броску атаки, когда атакуете дальнобойным оружием.");
                            break;
                    }
                }
            }
        }
    }

    void BattleStyleDiscription(Dropdown style, Text textField)
    {
        switch (style.captionText.text)
        {
            default:
                textField.text = " ";
                break;
            case "Дуэлянт":
                textField.text = "Пока вы держите рукопашное оружие в одной руке, и не используете другого оружия, вы получаете бонус +2 к броскам урона этим оружием.";
                break;
            case "Защита":
                textField.text = "Если существо, которое вы видите, атакует не вас, а другое существо, находящееся в пределах 5 футов от вас, вы можете реакцией создать помеху его броску атаки. Для этого вы должны использовать щит.";
                break;
            case "Оборона":
                textField.text = "Пока вы носите доспехи, вы получаете бонус +1 к КД.";
                break;
            case "Сражение большим оружием":
                textField.text = "Если у вас выпало «1» или «2» на кости урона оружия при атаке, которую вы совершали рукопашным оружием, удерживая его двумя руками, то вы можете перебросить эту кость, и должны использовать новый результат, даже если снова выпало «1» или «2». Чтобы воспользоваться этим преимуществом, ваше оружие должно иметь свойство «двуручное» или «универсальное».";
                break;
            case "Сражение двумя оружиями":
                textField.text = "Если вы сражаетесь двумя оружиями, вы можете добавить модификатор характеристики к урону от второй атаки.";
                break;
            case "Стрельба":
                textField.text = "Вы получаете бонус +2 к броску атаки, когда атакуете дальнобойным оружием.";
                break;
        }
    }

    public override void Save()
    {
        base.Save();
        Dropdown style = panel.GetComponentInChildren<Dropdown>();
        PlayerPrefs.SetInt(FighterBattleStyleCountSaveName, 1);

        switch (style.captionText.text)
        {
            case "Дуэлянт":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 0);
                break;
            case "Защита":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 1);
                break;
            case "Оборона":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 2);
                break;
            case "Сражение большим оружием":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 3);
                break;
            case "Сражение двумя оружиями":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 4);
                break;
            case "Стрельба":
                PlayerPrefs.SetInt(FighterBattleStyleSaveName + 0, 5); 
                break;
        }

    }
}
