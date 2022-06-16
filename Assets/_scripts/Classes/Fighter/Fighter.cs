using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    const string FighterBattleStyleCountSaveName = "FghterBattleStyleCount_";
    const string FighterBattleStyleSaveName = "FghterBattleStyle_";
    const string FighterSubClassSaveName = "FighterSubClass_";
    const string levelSaveName = "lvl_";

    PlayerSubClass subClass = null;

    public Fighter(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(10,
        new List<Armor.Type> { },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { },
        level, mainState, panel, basicForm, null, PB, false)
    {

    }

    public Fighter(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 },
        new List<int> { 0, 1 },
        level, 0, panel, basicForm, dropdownForm, 2, true)
    {

    }

    public override void ShowAbilities(int level)
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
                SubClass();
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                SubClass();
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                SubClass();
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
                SubClass();
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
        string caption = "Второе дыхание";
        string abilityLevel = "1-й уровень, умение воина";
        string discription = "Вы обладаете ограниченным источником выносливости, которым можете воспользоваться, чтобы уберечь себя. В свой ход вы можете бонусным действием восстановить хиты в размере 1к10 + " + level + "(ваш уровень воина).\n\nИспользовав это умение, вы должны завершить короткий либо продолжительный отдых, чтобы получить возможность использовать его снова.";

        CreatAbility(caption, abilityLevel, discription, 1);
    }

    void ActionSurge(int i)
    {

        string caption = "Всплеск действий";
        string abilityLevel = "2-й уровень, умение воина";
        string discription = "Вы получаете возможность на мгновение преодолеть обычные возможности. В свой ход вы можете совершить одно дополнительное действие помимо обычного и бонусного действий. Использовав это умение, вы должны завершить короткий или продолжительный отдых, чтобы получить возможность использовать его снова. Начиная с 17-го уровня вы можете использовать это умение дважды, прежде чем вам понадобится отдых, но в течение одного хода его всё равно можно использовать лишь один раз.";

        CreatAbility(caption, abilityLevel, discription, i);
    }

    void BattleStyle()
    {
        string caption = "Боевой стиль";
        string abilityLevel = "1-й уровень, умение воина";
        List<string> battleStyleExcludedList = new List<string>();
        if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
        {
            int styles = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
            for (int i = 0; i < styles; i++)
            {
                battleStyleExcludedList.Add(PlayerPrefs.GetString(FighterBattleStyleSaveName + i));
            }
        }
        List<string> battleStyleList;
        List<string> discriptionList = new List<string>();
        discriptionList.Add("Пока вы держите рукопашное оружие в одной руке, и не используете другого оружия, вы получаете бонус +2 к броскам урона этим оружием.");
        discriptionList.Add("Если существо, которое вы видите, атакует не вас, а другое существо, находящееся в пределах 5 футов от вас, вы можете реакцией создать помеху его броску атаки. Для этого вы должны использовать щит.");
        discriptionList.Add("Пока вы носите доспехи, вы получаете бонус +1 к КД.");
        discriptionList.Add("Если у вас выпало «1» или «2» на кости урона оружия при атаке, которую вы совершали рукопашным оружием, удерживая его двумя руками, то вы можете перебросить эту кость, и должны использовать новый результат, даже если снова выпало «1» или «2». Чтобы воспользоваться этим преимуществом, ваше оружие должно иметь свойство «двуручное» или «универсальное».");
        discriptionList.Add("Если вы сражаетесь двумя оружиями, вы можете добавить модификатор характеристики к урону от второй атаки.");
        discriptionList.Add("Вы получаете бонус +2 к броску атаки, когда атакуете дальнобойным оружием.");
        battleStyleList = new List<string> { "Дуэлянт", "Защита", "Оборона", "Сражение большим оружием", "Сражение двумя оружиями", "Стрельба" };
        if (redact)
        {  
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList, discriptionList);
        }
        else
        {
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList, discriptionList);
        }
    }

    void SubClass()
    {
        if(redact)
            subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm);
        else
            subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm, mainState, PB);
        Debug.Log(subClass);
    }

    public override void Save()
    {
        Debug.Log(level);
        PlayerPrefs.SetInt(levelSaveName, level);
        if (subClass != null)
        {
            subClass.Save();

        }
        switch (level)
        {
            case 1:
                base.Save();
                StylesSave();
                break;
        }
    }

    void StylesSave()
    {
        Dropdown style = panel.GetComponentInChildren<Dropdown>();
        int count;
        if (PlayerPrefs.HasKey(FighterBattleStyleCountSaveName))
            count = PlayerPrefs.GetInt(FighterBattleStyleCountSaveName);
        else
            count = 0;
        PlayerPrefs.SetInt(FighterBattleStyleCountSaveName, count + 1);
        PlayerPrefs.SetString(FighterBattleStyleSaveName + count, style.captionText.text);
    }
}
