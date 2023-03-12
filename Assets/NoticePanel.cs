using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePanel : MonoBehaviour
{
    [SerializeField] NoticeBody body;
    public void SetNotice(FormCreater formCreater)
    {
        Instantiate(body, transform).Set("Не выбрано значение поля \"" + formCreater.GetHead()+"\"");
    }

    public void SetNotice(Text text)
    {
        Instantiate(body, transform).Set("Не выбрано значение поля \"" + text.text + "\"");
    }

    public void SetNotice(string text)
    {
        Instantiate(body, transform).Set("Не выбрано значение поля \"" + text + "\"");
    }

    public void SetNotice(int text)
    {
        switch (text)
        {
            case 0:
                Instantiate(body, transform).Set("Не введено имя персонажа");
                break;
            case 1:
                Instantiate(body, transform).Set("У вас уже существует персонаж с таким именем");
                break;
            case 2:
                Instantiate(body, transform).Set("Оба поля замены должны быть выбраны");
                break;
        }
    }
}
