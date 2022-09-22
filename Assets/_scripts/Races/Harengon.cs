using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harengon : Race
{
    public Harengon(GameObject panel, GameObject basicForm, GameObject dropdownForm) : base(panel, basicForm, dropdownForm, true, 30)
    {
        AllClassesAbilities.AttributiesUp(panel, basicForm, dropdownForm, 3, false);
        PresavedLists.languages.Add(PresavedLists.Language.common);
        AllClassesAbilities.ChooseLanguage(panel, basicForm, dropdownForm, 1);
        PresavedLists.skills.Add("Внимательность");
        Trigger();
        Dodge();
        Jump(2);
    }

    public Harengon(GameObject panel, GameObject basicForm, int PB) : base(panel, basicForm, null, false, 30)
    {
        Trigger();
        Dodge();
        Jump(PB);
    }

    public Harengon()
    {

    }

    public override void RaceDiscription()
    {
        FormCreater form = panel.GetComponentInParent<FormCreater>();
        if (form != null)
        {
            form.GetComponentInChildren<Text>().text = "Харенгон";
            GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
            FormCreater formInForm = newObject.GetComponent<FormCreater>();
            newObject.GetComponentInChildren<Text>().text = "Описание";
            formInForm.AddText("Харенгоны родом из Страны Фей, где они общаются на Сильване и воплощают дух свободы и путешествий. Со временем эти кролелюды перескочили в другие миры, принеся с собой богатство Страны Фей и по ходу дела изучая новые языки.\n\nХаренгоны двуногие, с характерными кроличьими длинными ногами, на которых они и похожи, а так же мехом различных цветов.Они обладают острыми чувствами и мощными ногами зайцевых существ и наполнены энергией, как заведенная пружина.Харенгоны наделены небольшой удачей, и им часто везёт оказаться в нескольких счастливых футах от опасностей во время приключений.");
        }
    }

    void Trigger()
    {

        string caption = "Заячий триггер";
        string abilityLevel = "";
        string discription = "Вы можете добавить свой бонус мастерства к своему броску инициативы.";
        CreatAbility(caption, abilityLevel, discription);
    }
    void Dodge()
    {

        string caption = "Удачный манёвр";
        string abilityLevel = "";
        string discription = "Если вы проваливаете спасбросок Ловкости, вы можете реакцией бросить к4 и добавить его к результату, потенциально превращая провал в успех. Вы не можете использовать эту реакцию, если вы сбиты с ног или ваша скорость равна 0.";
        CreatAbility(caption, abilityLevel, discription);
    }
    void Jump(int i)
    {

        string caption = "Кроличий прыжок";
        string abilityLevel = "";
        string discription = "Бонусным действием вы можете прыгнуть на количество футов равное вашему пятикратному бонусу мастерства, не вызывая провоцированных атак. Вы можете использовать эту особенность если ваша скорость больше 0. Вы можете использовать эту особенность количество раз, равное " + i + " (бонус мастерства). Вы восстанавливаете все израсходованные применения после окончания продолжительного отдыха.";
        CreatAbility(caption, abilityLevel, discription, i);
    }

    public override void Erase()
    {
        PresavedLists.languages.Remove(PresavedLists.Language.common);
        PresavedLists.skills.Remove("Внимательность");
    }
    public override void Save()
    {
        base.Save();
        AllClassesAbilities.SaveAttributies();
        PlayerPrefs.SetString(characterName + raceSaveName, "Харенгон");
    }

}
