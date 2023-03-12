using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Validater : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] Text characterName;
    [SerializeField] NoticePanel notice;
    const string charactersCountSaveName = "@charactersCount_";
    const string charactersSaveName = "@characters_";

    public bool Validate()
    {
        Dropdown[] dropdowns = canvas.GetComponentsInChildren<Dropdown>();
        bool flag = true;
        foreach (Dropdown x in dropdowns)
            if (x.captionText.text == "" || x.captionText.text == "Пусто")
            {
                bool bufFlag = flag;
                flag = false;
                FormCreater formCreater;
                if (x.transform.parent.parent.TryGetComponent(out formCreater))
                    notice.SetNotice(formCreater);
                else
                {
                    ChangeChosen changeChosen;
                    if (x.transform.parent.parent.TryGetComponent(out changeChosen))
                    {
                        if (!changeChosen.Validate())
                            notice.SetNotice(2);
                        else
                        {
                            flag = bufFlag;
                            continue;
                        }
                    }
                    Discription discription;
                    if (x.transform.parent.TryGetComponent(out discription))
                        notice.SetNotice(discription.transform.parent.GetComponentInChildren<Text>());
                    else
                    {
                        if (x.transform.parent == null) continue;
                        RaceAbilities raceAbilities = x.transform.parent.GetComponentInChildren<RaceAbilities>();
                        if (raceAbilities != null)
                        {
                            notice.SetNotice("Раса");
                            continue;
                        }
                        ClassesAbilities classesAbilities = x.transform.parent.GetComponentInChildren<ClassesAbilities>();
                        if (classesAbilities != null)
                        {
                            notice.SetNotice("Класс");
                            continue;
                        }
                        Text text = x.transform.parent.GetComponentInChildren<Text>();
                        if (text != null)
                            notice.SetNotice(text);
                    }
                }
            }
        if (characterName == null)
            return flag;
        if (characterName.text == "")
        {
            notice.SetNotice(0);
            flag = false;
        }
        int count = PlayerPrefs.GetInt(charactersCountSaveName);
        for (int i = count; i > 0; i--)
        {
            string charName = PlayerPrefs.GetString(charactersSaveName + i);
            if (charName == characterName.text)
            {
                notice.SetNotice(1);
                flag = false;
            }
        }
        return flag;
    }

}
