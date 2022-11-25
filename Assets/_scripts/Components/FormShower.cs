using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormShower : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] GameObject headObject;
    [SerializeField] ConsumablePanel consumable;
    [SerializeField] GameObject basicText;
    GameObject discription;
    Ability _ability;
    void Awake()
    {
        discription = GetComponentInChildren<Discription>().gameObject;
    }

    public void CreateAbility(Ability ability, int level)
    {
        _ability = ability;
        head.text = ability.head;
        switch (ability.type)
        {
            case Ability.Type.abilitie:
                foreach ((int, string) x in ability.discription)
                    SetText(x);
                break;
            case Ability.Type.charUp:
            case Ability.Type.instruments:
            case Ability.Type.language:
            case Ability.Type.skills:
            case Ability.Type.spellChoose:
            case Ability.Type.subRace:
            case Ability.Type.subClass:
                Destroy(gameObject);
                break;
            case Ability.Type.withChoose:
                foreach ((int, string) x in ability.discription)
                    SetText(x);
                (string, string)[] list;
                if (ability.isUniq)
                    list = FileSaverAndLoader.LoadList(ability.pathToList).ToArray();
                else
                    list = ability.list;
                List<int> chosen = DataSaverAndLoader.LoadCustom(ability.listName);
                foreach (int x in chosen)
                {
                    SetText((2, list[x].Item1));
                    SetText((0, list[x].Item2));
                }
                break;
            case Ability.Type.consumable:
                foreach ((int, string) x in ability.discription)
                    SetText(x);
                int amount = DataSaverAndLoader.LoadConsumAmount(ability.listName);
                ConsumablePanel buf = Instantiate(consumable, headObject.transform);
                int buf2 = 0;
                if (level > 0)
                {
                    foreach ((int, int) x in ability.consum)
                        if (x.Item1 <= level)
                        {
                            buf2 = x.Item2;
                        }
                    buf.SpawnToggles(buf2, amount);
                }
                else
                    buf.SpawnToggles(ability.consum[0].Item2, amount);
                buf.update += UpdateConsum;
                break;
        }
        if (ability.changeRule)
            RuleChanger();
    }

    void UpdateConsum(int count)
    {
        DataSaverAndLoader.SaveConsumAmount(_ability.listName, count);
    }

    public void SetHead(string text)
    {
        head.text = text;
    }

    public GameObject GetDiscription()
    {
        return discription;
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

    void RuleChanger()
    {
        if(_ability.listName == "BattleStyles")
        {
            List<int> list = DataSaverAndLoader.LoadCustom(_ability.listName);
            foreach(int x in list)
            {
                switch (x)
                {
                    case 0:
                        GlobalStatus.duelist = true;
                        break;
                    case 2:
                        GlobalStatus.defence = true;
                        break;
                    case 5:
                        GlobalStatus.archer = true;
                        break;
                }
            }
        }
    }
}
