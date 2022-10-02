using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    const string FighterBattleStyleCountSaveName = "FghterBattleStyleCount_";
    const string FighterBattleStyleSaveName = "FghterBattleStyle_";
    const string FighterSubClassSaveName = "FighterSubClass_";
    


    PlayerSubClass subClass = null;
    bool upFlag = false;


    public Fighter(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(10,
        new List<Armor.ArmorType> { },
        new List<Weapon.WeaponType> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { },
        level, mainState, panel, basicForm, null, PB, false)
    {
        if (PlayerPrefs.HasKey(characterName + FighterSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(characterName + FighterSubClassSaveName))
            {
                case 0:
                    subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm, mainState, PB);
                    break;
            }
        }
    }

    public Fighter(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(10,
        new List<Armor.ArmorType> { },
        new List<Weapon.WeaponType> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1, 5, 9, 10, 12, 13, 15 },
        new List<int> { 0, 1 },
        level, 0, panel, basicForm, dropdownForm, 2, true)
    {
        if (PlayerPrefs.HasKey(characterName + FighterSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(characterName + FighterSubClassSaveName))
            {
                case 0:
                    subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm);
                    break;
            }
        }
    }

    public Fighter() : base()
    {
        LoadAbilities("fighter");
    }


    /*public override void ClassDiscription()
    {
        FormCreater form = panel.GetComponentInParent<FormCreater>();
        if (form != null)
        {
            form.GetComponentInChildren<Text>().text = "Воин";
            GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
            FormCreater formInForm = newObject.GetComponent<FormCreater>();
            newObject.GetComponentInChildren<Text>().text = "Описание";
            formInForm.AddText("Женщина, прикрываясь щитом и лязгая доспехами, устремляется в толпу гоблинов. Из-за её спины одетый в клёпанную кожу эльф осыпает врагов стрелами, выпуская их из своего изящного лука. Невдалеке от них полуорк выкрикивает приказы, указывая лучшее направление для атаки. Дварф в кольчуге выставил свой щит перед компаньоном, отражая смертельный удар дубины огра. Его напарник, полуэльф в чешуйчатом доспехе, закрутил два своих скимитара в сверкающем вихре и двинулся в обход огра, отыскивая брешь в его защите.\n\nОпытный гладиатор сражается на арене и хорошо знает, как использовать свои трезубец и сеть, чтобы опрокинуть противника и обойти его, вызывая ликование публики и получая тактическое преимущество.Меч его противника вспыхивает голубым светом и испускает сверкающую молнию.Все эти герои — воины.Представители, возможно, самого разнообразного класса в мире D&D. Странствующие рыцари, военачальники - завоеватели, королевские чемпионы, элитная пехота, бронированные наёмники и короли разбоя — будучи воинами, все они мастерски владеют оружием, доспехами, и приёмами ведения боя.А еще они хорошо знакомы со смертью — они несут её сами, и часто смотрят в её холодные глаза.", FontStyle.Italic);
            formInForm.AddText("Разностороние специалисты", 40, FontStyle.Bold);
            formInForm.AddText("Воины владеют основами всех боевых стилей. Каждый воин может рубить топором, фехтовать рапирой, владеет длинным и двуручным мечом, может стрелять из лука и даже при некоторой сноровке способен поймать противника сетью. Помимо этого, воины хорошо знакомы с использованием щита и любых доспехов. Помимо общих знаний, каждый воин специализируется на определённом стиле боя. Некоторые концентрируются на стрельбе из лука, другие на сражении с оружием в каждой руке, а есть те, кто свои воинские способности усиливает заклинаниями. Сочетание широких общих навыков и углублённой специализации делает воинов непревзойдёнными на поле боя.");
            formInForm.AddText("Готовые к опасности", 40, FontStyle.Bold);
            formInForm.AddText("Не все члены городской стражи, деревенского ополчения или королевской армии являются воинами. Большинство из них это просто обученные солдаты, обладающие лишь основными воинскими навыками. Солдаты-ветераны, офицеры, обученные телохранители, посвящённые рыцари и похожие персоны, как правило, являются воинами. Некоторые воины чувствуют потребность использовать свою подготовку в качестве искателей приключений. Исследование подземелий, убийство чудовищ, и другая опасная работа, обыденная для искателей приключений, является второй натурой воина, и не так сильно отличается от жизни, оставленной в прошлом. Риск здесь, возможно, и выше, но и награда значительно больше — например, воины в городском дозоре вряд ли могут найти меч язык пламени.");
        }
    }*/

    /*public override void ShowAbilities(int level)
    {
        switch (level)
        {
            case 1:
                if (redact)
                    AllClassesAbilities.SetSkills(panel, basicForm, dropdownForm, "Навыки", GetSkillProfs(), 2);
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
                AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
                upFlag = true;
                break;
            case 5:
                break;
            case 6:
                if (!upFlag)
                    AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
                break;
            case 7:
                break;
            case 8:
                if (!upFlag)
                    AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
                break;
            case 9:
                break;
            case 10:
                if (!upFlag)
                    AllClassesAbilities.AbilitiesUp(panel, basicForm, dropdownForm, redact);
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
    }*/

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
        if (PlayerPrefs.HasKey(characterName + FighterBattleStyleCountSaveName))
        {
            int styles = PlayerPrefs.GetInt(characterName + FighterBattleStyleCountSaveName);
            for (int i = 0; i < styles; i++)
            {
                battleStyleExcludedList.Add(PlayerPrefs.GetString(characterName + FighterBattleStyleSaveName + i));
            }
        }
        List<(string, string)> battleStyleList = new List<(string, string)>();
        battleStyleList.Add(("Дуэлянт", "Пока вы держите рукопашное оружие в одной руке, и не используете другого оружия, вы получаете бонус +2 к броскам урона этим оружием."));
        battleStyleList.Add(("Защита", "Если существо, которое вы видите, атакует не вас, а другое существо, находящееся в пределах 5 футов от вас, вы можете реакцией создать помеху его броску атаки. Для этого вы должны использовать щит."));
        battleStyleList.Add(("Оборона", "Пока вы носите доспехи, вы получаете бонус +1 к КД."));
        battleStyleList.Add(("Сражение большим оружием", "Если у вас выпало «1» или «2» на кости урона оружия при атаке, которую вы совершали рукопашным оружием, удерживая его двумя руками, то вы можете перебросить эту кость, и должны использовать новый результат, даже если снова выпало «1» или «2». Чтобы воспользоваться этим преимуществом, ваше оружие должно иметь свойство «двуручное» или «универсальное»."));
        battleStyleList.Add(("Сражение двумя оружиями", "Если вы сражаетесь двумя оружиями, вы можете добавить модификатор характеристики к урону от второй атаки."));
        battleStyleList.Add(("Стрельба", "Вы получаете бонус +2 к броску атаки, когда атакуете дальнобойным оружием."));
        if (redact)
        {
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList);
        }
        else
        {
            CreatAbility(caption, abilityLevel, battleStyleList, battleStyleExcludedList);
        }
    }

    /*void SubClass()
    {
        string caption = "Военский архетип";
        string abilityLevel = "Разные воины используют разные подходы для совершенствования своих воинских способностей. Воинский архетип отражает выбранный вами подход.";
        List<(string, string)> subClassList = new List<(string, string)>();
        subClassList.Add(("Мастер боевых искусств", "Тот, кто выбрал архетип мастера боевых искусств, полагается на техники, выработанные поколениями бойцов. Для такого воина сражение сродни академической задаче, и часто включает вещи, далёкие от боя, вроде кузнечного мастерства или каллиграфии. Не все воины способны впитать уроки истории, теорию и артистизм, отражённые в архетипе мастера боевых искусств, но те, кто смог сделать это, являются отлично подготовленными воинами, обладающими прекрасными навыками и знаниями."));
        CreatAbility(caption, abilityLevel, subClassList);
    }

    public override void ChooseSubClass(Dropdown mySelf)
    {
        base.ChooseSubClass(mySelf);
        switch (mySelf.captionText.text)
        {
            case "Мастер боевых искусств":
                subClass = new MasterOfMartialArt(level, panel.GetComponentInChildren<FormCreater>().GetComponentInChildren<Discription>().gameObject, basicForm, dropdownForm);
                break;
        }
    }*/

    /*public override void Save()
    {
        base.Save();
        Debug.Log(level);
        int count = PlayerPrefs.GetInt(characterName + levelCountSaveName);
        bool flag = false;
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.GetString(characterName + levelLabelSaveName + i) == "Воин")
            {
                PlayerPrefs.SetInt(characterName + levelSaveName + i, level);
                flag = true;
                break;
            }
        }

        if (subClass != null)
        {
            subClass.Save();

        }
        switch (level)
        {
            case 1:
                base.Save();
                if (!flag)
                {
                    PlayerPrefs.SetString(characterName + levelLabelSaveName + count, "Воин");
                    PlayerPrefs.SetInt(characterName + levelSaveName + count, level);
                    PlayerPrefs.SetInt(characterName + levelCountSaveName, count + 1);
                }
                StylesSave();
                break;
            case 3:
                PlayerPrefs.SetInt(characterName + FighterSubClassSaveName, 0);
                break;
            case 4:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
            case 6:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
            case 8:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
            case 10:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
        }
    }*/

    void StylesSave()
    {
        Dropdown style = panel.GetComponentInChildren<Dropdown>();
        int count;
        if (PlayerPrefs.HasKey(characterName + FighterBattleStyleCountSaveName))
            count = PlayerPrefs.GetInt(characterName + FighterBattleStyleCountSaveName);
        else
            count = 0;
        PlayerPrefs.SetInt(characterName + FighterBattleStyleCountSaveName, count + 1);
        PlayerPrefs.SetString(characterName + FighterBattleStyleSaveName + count, style.captionText.text);
    }
}
