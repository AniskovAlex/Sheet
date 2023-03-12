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
    [SerializeField] SpellBody spellBody;
    GameObject discription;
    Ability _ability;
    ConsumablePanel consum;
    void Awake()
    {
        discription = GetComponentInChildren<Discription>().gameObject;
    }

    public void CreateAbility(Ability ability, int level, PlayersClass playersClass)
    {
        _ability = ability;
        head.text = ability.head;
        foreach ((int, string) x in ability.discription)
            SetText(x);
        switch (ability.type)
        {
            case Ability.Type.abilitie:
                switch (ability.bufInt)
                {
                    case 1:
                        FormShower[] forms = transform.parent.GetComponentsInChildren<FormShower>();
                        foreach (FormShower x in forms)
                        {
                            if (x.GetAbility() != null && x.GetAbility().listName == _ability.listName)
                                if (x.GetConsumablePanel() != null)
                                    x.GetConsumablePanel().SpawnResetButton();
                        }
                        break;
                }
                if (ability.listName == "RitualCaster" || ability.listName == "AimCaster")
                {
                    List<int> listClass = DataSaverAndLoader.LoadCustom(ability.listName);
                    if (listClass.Count <= 0) break;
                    string buf = "Класс: ";
                    switch (listClass[0])
                    {
                        default:
                        case 0:
                            buf += "Бард (Харизма)";
                            break;
                        case 3:
                            buf += "Волшебник (Интеллект)";
                            break;
                        case 4:
                            buf += "Друид (Мудрость)";
                            break;
                        case 5:
                            buf += "Жрец (Мудрость)";
                            break;
                        case 7:
                            buf += "Колдун (Харизма)";
                            break;
                        case 12:
                            buf += "Чародей (Харизма)";
                            break;
                    }
                    SetText((1, buf));
                }
                break;
            case Ability.Type.language:
            case Ability.Type.skills:
            case Ability.Type.spellChoose:
                if (ability.spellShow == null || (ability.spellShow != null && ability.spellShow.Count <= 0))
                    Destroy(gameObject);
                break;
            case Ability.Type.instruments:
            case Ability.Type.attr:
            case Ability.Type.charUp:
            case Ability.Type.subRace:
            case Ability.Type.subClass:
            case Ability.Type.feat:
                Destroy(gameObject);
                break;
            case Ability.Type.withChoose:
                (int, string, string, int)[] list;
                if (ability.isUniq)
                    list = FileSaverAndLoader.LoadList(ability.pathToList).ToArray();
                else
                    list = ability.list;
                List<int> chosen = DataSaverAndLoader.LoadCustom(ability.listName);
                foreach (int x in chosen)
                {
                    foreach ((int, string, string, int) y in list)
                    {
                        if (y.Item1 != x) continue;
                        SetText((2, list[x].Item2));
                        SetText((0, list[x].Item3));
                        if (ability.listName == "Appeals" && y.Item1 == 15)
                            GlobalStatus.secreatsBook = true;
                        if (ability.consum != null)
                            foreach ((int, int) k in ability.consum)
                                if (k.Item1 == y.Item1)
                                {
                                    if (k.Item2 < 0) break;
                                    Spell[] spells;
                                    spells = LoadSpellManager.GetSpells();
                                    for (int i = 0; i < spells.Length; i++)
                                        if (spells[i].id == k.Item2)
                                            Instantiate(spellBody, discription.transform).SetSpell(spells[i]);
                                }
                        break;
                    }
                }
                break;
            case Ability.Type.consumable:
                int amount = DataSaverAndLoader.LoadConsumAmount(ability.listName);
                consum = Instantiate(consumable, headObject.transform);
                int buf2 = 0;
                if (level > 0)
                {
                    foreach ((int, int) x in ability.consum)
                        if (x.Item1 <= level)
                        {
                            buf2 = x.Item2;
                            switch (x.Item2)
                            {
                                default:
                                    buf2 = x.Item2;
                                    break;
                                case -1:
                                    if (playersClass != null)
                                        buf2 = Mathf.Clamp(CharacterData.GetModifier(playersClass.mainState), 1, 10);
                                    break;
                                case -2:
                                    if (playersClass != null)
                                        buf2 = Mathf.Clamp(CharacterData.GetModifier(playersClass.mainState), 0, 10) + 1;
                                    break;

                            }
                        }
                    consum.SpawnToggles(buf2, amount);
                }
                else return;
                if (ability.isUniq)
                {
                    RestController restController = FindObjectOfType<RestController>();
                    if (restController != null)
                        restController.AddShortRest(ability.bufInt, consum);
                }
                consum.update += UpdateConsum;
                if (ability.changeRule)
                    switch (ability.listName)
                    {
                        case "SaveMovement":
                            consum.SpawnResetButton();
                            break;
                        case "MysteryMaster":
                            consum.SpawnResetWarCellsButton();
                            break;
                    }
                switch (ability.listName)
                {
                    case "Arcanum6":
                    case "Arcanum7":
                    case "Arcanum8":
                    case "Arcanum9":
                        List<int> spellsId = DataSaverAndLoader.LoadCustom(ability.listName);
                        ability.spellShow = spellsId;
                        break;
                }
                break;
        }

        if (ability.spellShow.Count > 0)
        {
            Spell[] spells;
            spells = LoadSpellManager.GetSpells();
            foreach (int x in ability.spellShow)
            {
                for (int i = 0; i < spells.Length; i++)
                    if (spells[i].id == x)
                        Instantiate(spellBody, discription.transform).SetSpell(spells[i]);
            }
        }
        if (ability.changeRule)
            RuleChanger();
        if (ability.hide)
            Destroy(gameObject);
    }

    public Ability GetAbility()
    {
        return _ability;
    }

    public ConsumablePanel GetConsumablePanel()
    {
        return consum;
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
            case 1:
                textSize = 40;
                fontStyle = FontStyle.Normal;
                break;
            case 3:
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
        FormShower[] forms;
        switch (_ability.listName)
        {
            case "BattleStyles":
                List<int> list = DataSaverAndLoader.LoadCustom(_ability.listName);
                foreach (int x in list)
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
                break;
            case "AllHandy":
                GlobalStatus.allHandy = true;
                break;
            case "BarbarianDefence":
                GlobalStatus.barbarianDefence = true;
                break;
            case "BarbatianFastMove":
                GlobalStatus.barbatianFastMove = true;
                break;
            case "WildChampion":
                GlobalStatus.wildChampion = true;
                CharacterData.AddAtribute(0, 4);
                CharacterData.AddAtribute(2, 4);
                break;
            case "MonkDefence":
                GlobalStatus.monkDefence = true;
                break;
            case "MonkWeapon":
                GlobalStatus.monkWeapon = true;
                break;
            case "MonkSpeed":
                GlobalStatus.monkSpeed = true;
                break;
            case "DiamondSoul":
                GlobalStatus.dimondSoul = true;
                break;
            case "SpellMaster":
                GlobalStatus.spellMaster = true;
                break;
            case "sourceOfInpiration":
                forms = transform.parent.GetComponentsInChildren<FormShower>();
                foreach (FormShower x in forms)
                {
                    if (x.GetAbility() != null && x.GetAbility().listName == "bardInsiper")
                        if (x.GetConsumablePanel() != null)
                        {
                            RestController restController = FindObjectOfType<RestController>();
                            if (restController != null)
                                restController.AddShortRest(1, x.GetConsumablePanel());
                        }
                }
                break;
            case "SlipperyMind":
                CharacterData.AddSave(4);
                break;
            case "SorcererUnit":
                GlobalStatus.sorcererUnit = true;
                break;
            case "SorcererRegain":
                forms = transform.parent.GetComponentsInChildren<FormShower>();
                foreach (FormShower x in forms)
                {
                    if (x.GetAbility() != null && x.GetAbility().listName == "SorcererUnit")
                        if (x.GetConsumablePanel() != null)
                        {
                            RestController restController = FindObjectOfType<RestController>();
                            if (restController != null)
                                restController.AddShortRest(4, x.GetConsumablePanel());
                        }
                }
                break;
            case "FastFeet":
                GlobalStatus.fastFeet = true;
                break;
            case "Alert":
                GlobalStatus.alert = true;
                break;
            case "Observant":
                GlobalStatus.observant = true;
                break;
            case "FightingTechniques":
                forms = transform.parent.GetComponentsInChildren<FormShower>();
                bool flag = false;
                foreach (FormShower x in forms)
                {
                    if (x.GetAbility() != null && x.GetAbility().listName == "DiceOfSuprim")
                        if (x.GetConsumablePanel() != null)
                        {
                            x.GetConsumablePanel().SpawnToggles(1);
                            flag = true;
                        }
                }
                if (!flag)
                {
                    Ability diceOfSuprim = new Ability();
                    diceOfSuprim.head = "Кости превосходства";
                    diceOfSuprim.type = Ability.Type.consumable;
                    diceOfSuprim.level = 1;
                    diceOfSuprim.isUniq = true;
                    diceOfSuprim.listName = "DiceOfSuprim";
                    diceOfSuprim.discription.Add((1, "У вас есть четыре кости превосходства. Это кости к6. Кости превосходства тратятся при использовании. Вы восполняете все потраченные кости в конце короткого или продолжительного отдыха."));
                    diceOfSuprim.discription.Add((1, "Спасброски. Некоторые из ваших приёмов требуют от цели спасброска, чтобы избежать эффекта приёма. Сложность такого спасброска рассчитывается следующим образом:"));
                    diceOfSuprim.discription.Add((3, "Сложность спасброска приёма = 8 + ваш бонус мастерства + ваш модификатор Силы или Ловкости (на ваш выбор)."));
                    diceOfSuprim.consum = new (int, int)[1];
                    diceOfSuprim.consum[0].Item1 = 1;
                    diceOfSuprim.consum[0].Item2 = 1;
                    Instantiate(gameObject, transform.parent).GetComponent<FormShower>().CreateAbility(diceOfSuprim, 1, null);
                }
                break;
            case "DealWielder":
                GlobalStatus.dealWielder = true;
                break;
            case "MediumArmorMaster":
                GlobalStatus.mediumArmorMaster = true;
                break;
            case "Durable":
                GlobalStatus.durable = true;
                break;
            case "Mobile":
                GlobalStatus.mobile = true;
                break;
            case "RitualCaster":
                GlobalStatus.ritualCaster = true;
                break;
        }
    }
}
