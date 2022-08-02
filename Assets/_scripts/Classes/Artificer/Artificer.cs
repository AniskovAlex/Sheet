using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artificer : PlayersClass
{

    const string ArtificerSubClassSaveName = "ArtificerSubClass_";

    PlayerSubClass subClass = null;
    bool upFlag = false;


    public Artificer(int level, GameObject panel, GameObject basicForm, int mainState, int PB) : base(8,
        new List<Armor.Type> { },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { },
        new List<int> { },
        level, mainState, panel, basicForm, null, PB, false)
    {
        if (PlayerPrefs.HasKey(characterName + ArtificerSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(characterName + ArtificerSubClassSaveName))
            {
                case 0:
                    subClass = new Alchemist(level, panel, basicForm, dropdownForm, mainState, PB);
                    break;
            }
        }
    }

    public Artificer(int level, GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(8,
        new List<Armor.Type> { Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 2, 4, 5, 6, 7, 9, 11 },
        new List<int> { 2, 3 },
        level, 0, panel, basicForm, dropdownForm, 2, true)
    {
        if (PlayerPrefs.HasKey(characterName + ArtificerSubClassSaveName))
        {
            switch (PlayerPrefs.GetInt(characterName + ArtificerSubClassSaveName))
            {
                case 0:
                    subClass = new MasterOfMartialArt(level, panel, basicForm, dropdownForm);
                    break;
            }
        }
    }

    public override void ClassDiscription()
    {
        FormCreater form = panel.GetComponentInParent<FormCreater>();
        if (form != null)
        {
            form.GetComponentInChildren<Text>().text = "Изобретатель";
            GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
            FormCreater formInForm = newObject.GetComponent<FormCreater>();
            newObject.GetComponentInChildren<Text>().text = "Описание";
            formInForm.AddText("Изобретатели — величайшие мастера пробуждать магию в обычных предметах. Они рассматривают магию как сложную систему, которую следует расшифровать и применять в заклинаниях и изобретениях. В следующих нескольких разделах вы найдёте всё необходимое для игры за одного из изобретателей.\n\nДля направления своей магической силы они используют различные инструменты.Накладывая заклинание, изобретатель может использовать принадлежности алхимика для создания мощного эликсира, набор каллиграфа, чтобы нарисовать знак силы на доспехах союзника или инструменты ремонтника, чтобы создать временный амулет.Магия изобретателей связана с их инструментами и способностями, и мало кто, кроме них, сможет создать правильный рабочий инструмент.");
                }
    }

    public override void ShowAbilities(int level)
    {
        switch (level)
        {
            case 1:
                if (redact)
                    AllClassesAbilities.SetSkills(panel, basicForm, dropdownForm, "Навыки", GetSkillProfs(), 2);
                MagickCrafter();
                break;
            case 2:
                Infusion();
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

    void MagickCrafter()
    {
        string caption = "Магический мастеровой";
        string abilityLevel = "1-й уровень, умение изобретателя";
        string discription = "Вы научились вкладывать искру магии в обычные предметы. Чтобы использовать это умение, вы должны держать в руках воровские инструменты или инструменты ремесленника. Затем действием вы касаетесь крошечного немагического объекта и наделяете его одним из следующих магических свойств на ваш выбор:\n\nЗачарованный объект излучает яркий свет в радиусе 5 футов и тусклый свет в радиусе еще 5 футов\nОбъект проигрывает записанное сообщение, которое можно услышать в пределах 10 футов каждый раз, когда до него дотрагивается существо.Вы произносите это сообщение, когда наделяете объект данным свойством, а сама запись не может быть длиннее 6 секунд.\nОбъект непрерывно испускает запах или издаёт звук на ваш выбор(ветер, волны, стрекотание и прочее). Выбранное явление можно ощутить на расстоянии 10 футов\nСтатичный визуальный эффект появляется на одной из поверхностей объекта. Этот эффект может быть изображением, текстом до 25 слов, линиями и формами или совмещением этих элементов по вашему выбору.\nВыбранное свойство навсегда остается присущим объекту. Действием вы можете коснуться объекта и лишить его этого свойства.\n\nТаким образом можно наделить магическими свойствами несколько предметов, но не больше, чем одно свойство на один предмет.Максимальное количество объектов, которые вы можете наделить магией за один раз, равно " + mainState + " - модификатор Интеллекта(минимум один объект).Если вы пытаетесь превысить свой максимум, самое старое свойство немедленно заканчивается, а затем начинает действовать новое свойство.";
        CreatAbility(caption, abilityLevel, discription);
    }

    void Infusion()
    {
        string caption = "Инфузия";
        string abilityLevel = "2-й уровень, умение изобретателя";
        string discription = "Заканчивая продолжительный отдых, вы можете дотронуться до немагического объекта и наполнить его магией с помощью инфузии. Инфузия действует только на те виды объектов, которые указаны в её описании. Если созданный предмет требует настройки, вы можете настроиться на него сразу же. Если вы решили настроиться на предмет позже, то должны будете сделать это, используя обычный процесс настройки (см. раздел «Настройка» в «Руководстве Мастера»).\n\nВаша инфузия остается в предмете бесконечно долго, но когда вы умираете, она исчезает через количество дней, равное вашему модификатору Интеллекта(минимум 1 день).Исчезает она также, если вы отказываетесь от знания этой инфузии, чтобы изучить другую.\n\nЗаканчивая продолжительный отдых, вы можете наполнить магией более одного немагического объекта. Максимальное их количество отображено в столбце «Инфузии предметов» таблицы «Изобретатель». Вы должны касаться предмета, и каждая из ваших инфузий может быть применена только к одному объекту единовременно. Более того, ни один предмет не выдержит попытки наполнения его более чем одной инфузией.Если вы попытаетесь превысить максимальное количество инфузий, самая старая из них немедленно заканчивается, а затем применяется новая.\n\nЕсли на предмете, в котором содержатся другие вещи, например, сумке хранения[bag of holding], заканчивается действие инфузии, его содержимое просто появляется вокруг него.";
        CreatAbility(caption, abilityLevel, discription);
    }

    void SubClass()
    {
        string caption = "Специализации изобретателя";
        string abilityLevel = "Изобретатели могут достигать успеха в различных областях. Специализация отражает их подход.";
        List<(string, string)> subClassList = new List<(string, string)>();
        subClassList.Add(("Алхимик", "Алхимик — эксперт по созданию магических эффектов путём комбинирования экзотических реагентов. Алхимики используют свои творения, чтобы дарить и отбирать жизнь. Алхимия — древнейшая из изобретательских традиций, и её многофункциональность издавна ценилась и во время войн, и в годы мира."));
        CreatAbility(caption, abilityLevel, subClassList);
    }

    public override void ChooseSubClass(Dropdown mySelf)
    {
        base.ChooseSubClass(mySelf);
        switch (mySelf.captionText.text)
        {
            case "Алхимик":
                subClass = new Alchemist(level, panel.GetComponentInChildren<FormCreater>().GetComponentInChildren<Discription>().gameObject, basicForm, dropdownForm);
                break;
        }
    }

    public override void Save()
    {
        Debug.Log(level);
        int count = PlayerPrefs.GetInt(characterName + levelCountSaveName);
        bool flag = false;
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.GetString(characterName + levelLabelSaveName + i) == "Изобретатель")
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
                    PlayerPrefs.SetString(characterName + levelLabelSaveName + count, "Изобретатель");
                    PlayerPrefs.SetInt(characterName + levelSaveName + count, level);
                    PlayerPrefs.SetInt(characterName + levelCountSaveName, count + 1);
                }
                break;
            case 3:

                // это заглушка
                PlayerPrefs.SetInt(characterName + ArtificerSubClassSaveName, 0);
                break;
            case 4:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
            case 8:
                AllClassesAbilities.SaveFeat();
                AllClassesAbilities.SaveAttributies();
                break;
        }
    }
}
