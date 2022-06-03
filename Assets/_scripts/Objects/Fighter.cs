using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fighter : PlayersClass
{
    int mainState;
    GameObject panel;
    public GameObject basicForm;
    int PB;

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

        for (int i = 1; i <= level; i++)
        {
            switch (i)
            {
                case 1:
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
    }

    public Fighter() : base(10,
        new List<Armor.Type> { Armor.Type.Heavy, Armor.Type.Light, Armor.Type.Medium, Armor.Type.Shield },
        new List<Weapon.Type> { },
        0,
        new List<string> { },
        2,
        new List<int> { 0, 1},
        new List<int> {0,1 })
    {
        PB = 2;
    }

    void SecondBreath()
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "Второе дыхание";
        form.AddText("1-й уровень, умение воина", FontStyle.Italic);
        form.AddText("Вы обладаете ограниченным источником выносливости, которым можете воспользоваться, чтобы уберечь себя. В свой ход вы можете бонусным действием восстановить хиты в размере 1к10 + ваш уровень воина.\n\nИспользовав это умение, вы должны завершить короткий либо продолжительный отдых, чтобы получить возможность использовать его снова.");
        form.AddConsumables(1);
    }

    void ActionSurge(int i)
    {
        GameObject newObject = GameObject.Instantiate(basicForm, panel.transform);
        FormCreater form = newObject.GetComponent<FormCreater>();
        newObject.GetComponentInChildren<Text>().text = "Всплеск действий";
        form.AddText("2-й уровень, умение воина", FontStyle.Italic);
        form.AddText("Вы получаете возможность на мгновение преодолеть обычные возможности. В свой ход вы можете совершить одно дополнительное действие помимо обычного и бонусного действий. Использовав это умение, вы должны завершить короткий или продолжительный отдых, чтобы получить возможность использовать его снова. Начиная с 17-го уровня вы можете использовать это умение дважды, прежде чем вам понадобится отдых, но в течение одного хода его всё равно можно использовать лишь один раз.");
        form.AddConsumables(i);
    }
}
