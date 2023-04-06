using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Personality : MonoBehaviour
{
    List<(int, PlayersClass)> classes;
    Race race;
    Backstory backstory;
    [SerializeField] GameObject form;
    [SerializeField] GameObject content;
    [SerializeField] GameObject backstoryObject;
    [SerializeField] Dropdown alignment;
    [SerializeField] InputField nature;
    [SerializeField] InputField ideal;
    [SerializeField] InputField attachment;
    [SerializeField] InputField weakness;
    [SerializeField] InputField backstoryExtend;
    [SerializeField] Text classesName;
    [SerializeField] Text RaceName;
    [SerializeField] Text armorProficiancy;
    [SerializeField] Text weaponProficiancy;
    [SerializeField] Text instrumentProficiancy;
    [SerializeField] Text language;

    // Start is called before the first frame update
    void Start()
    {
        classes = CharacterData.GetClasses();
        foreach ((int, PlayersClass) playersClass in classes)
            if (playersClass.Item2 != null)
            {
                classesName.text += " " + playersClass.Item2.name + " ур." + playersClass.Item1;
                if (playersClass.Item2.GetSubClass() != null)
                    classesName.text += " (" + playersClass.Item2.GetSubClass().GetName() + ")";
                classesName.text += ", ";

                GameObject classBody = Instantiate(form, content.transform);
                classBody.GetComponent<FormShower>().SetHead(playersClass.Item2.name);
                if (playersClass.Item2 != null)
                {
                    Ability[] abilityArr = playersClass.Item2.GetAbilities();
                    Ability[] abilitieSubClassArr = null;
                    if (playersClass.Item2.GetSubClass() != null)
                        abilitieSubClassArr = playersClass.Item2.GetSubClass().GetAbilities();
                    if (abilitieSubClassArr != null)
                        abilityArr = abilityArr.Concat(abilitieSubClassArr).ToArray();
                    foreach (Ability x in abilityArr)
                    {
                        if (x.level <= playersClass.Item1)
                            Instantiate(form, classBody.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(x, playersClass.Item1, playersClass.Item2);
                    }
                }
            }
        classesName.text = classesName.text.Remove(classesName.text.Length - 2);
        race = CharacterData.GetRace();
        if (race != null)
        {
            GameObject raceBody = Instantiate(form, content.transform);
            RaceName.text += " " + race.name;
            raceBody.GetComponent<FormShower>().SetHead(race.name);
            Ability[] abilitieSubRaceArr = race.ChooseSubRace(DataSaverAndLoader.LoadSubRace(race));
            Ability[] abilityArr = race.GetAbilities();
            if (abilitieSubRaceArr != null)
            {
                abilityArr = abilityArr.Concat(abilitieSubRaceArr).ToArray();
                RaceName.text += " (" + race.GetSubRace().GetName() + ")";
            }
            foreach (Ability x in abilityArr)
            {
                Instantiate(form, raceBody.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(x, 1, null);
            }
        }
        backstory = CharacterData.GetBackstory();
        if (backstory != null)
        {
            backstoryObject.GetComponent<FormShower>().SetHead(backstory.name);
            Ability[] abilityArr = backstory.GetAbilities();
            foreach (Ability x in abilityArr)
            {
                Instantiate(form, backstoryObject.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(x, 1, null);
            }
        }
        loadFeats();
        LoadPersonInformation();
        LoadProficiancy();
    }

    void LoadPersonInformation()
    {
        alignment.value = DataSaverAndLoader.LoadAlignment();
        nature.text = DataSaverAndLoader.LoadNature();
        ideal.text = DataSaverAndLoader.LoadIdeal();
        attachment.text = DataSaverAndLoader.LoadAttachment();
        weakness.text = DataSaverAndLoader.LoadWeakness();
        backstoryExtend.text = DataSaverAndLoader.LoadBackstoryExtend();
    }

    void LoadProficiancy()
    {
        foreach (Armor.ArmorType x in CharacterData.GetArmorProficiency())
            switch (x)
            {
                case Armor.ArmorType.Light:
                    armorProficiancy.text += " лёгкие,";
                    break;
                case Armor.ArmorType.Medium:
                    armorProficiancy.text += " средние,";
                    break;
                case Armor.ArmorType.Heavy:
                    armorProficiancy.text += " тяжёлые,";
                    break;
                case Armor.ArmorType.Shield:
                    armorProficiancy.text += " щиты,";
                    break;
            }
        armorProficiancy.text = armorProficiancy.text.Remove(armorProficiancy.text.Length - 1);

        foreach (Weapon.WeaponType x in CharacterData.GetWeaponProficiency())
            switch (x)
            {
                case Weapon.WeaponType.CommonMelee:
                    weaponProficiancy.text += " Простое рукопашное,";
                    break;
                case Weapon.WeaponType.CommonDist:
                    weaponProficiancy.text += " Простое дальнобойное,";
                    break;
                case Weapon.WeaponType.WarMelee:
                    weaponProficiancy.text += " Воинское рукопашное,";
                    break;
                case Weapon.WeaponType.WarDist:
                    weaponProficiancy.text += " Воинское дальнобойное,";
                    break;
            }
        foreach (Weapon.BladeType x in CharacterData.GetBladeProficiency())
            switch (x)
            {
                case Weapon.BladeType.WarStaff:
                    weaponProficiancy.text += " Боевой посох,";
                    break;
                case Weapon.BladeType.Mace:
                    weaponProficiancy.text += " Булава,";
                    break;
                case Weapon.BladeType.Club:
                    weaponProficiancy.text += " Дубинка,";
                    break;
                case Weapon.BladeType.Dagger:
                    weaponProficiancy.text += " Кинжал,";
                    break;
                case Weapon.BladeType.Spear:
                    weaponProficiancy.text += " Копьё,";
                    break;
                case Weapon.BladeType.LightHammer:
                    weaponProficiancy.text += " Лёгкий молот,";
                    break;
                case Weapon.BladeType.ThrowingSpear:
                    weaponProficiancy.text += " Метательное копьё,";
                    break;
                case Weapon.BladeType.HandAxe:
                    weaponProficiancy.text += " Ручной топор,";
                    break;
                case Weapon.BladeType.Sickle:
                    weaponProficiancy.text += " Серп,";
                    break;
                case Weapon.BladeType.LightCrossbow:
                    weaponProficiancy.text += " Лёгкий арбалет,";
                    break;
                case Weapon.BladeType.Dart:
                    weaponProficiancy.text += " Дротик,";
                    break;
                case Weapon.BladeType.Stick:
                    weaponProficiancy.text += " Палица,";
                    break;
                case Weapon.BladeType.ShortBow:
                    weaponProficiancy.text += " Короткий лук,";
                    break;
                case Weapon.BladeType.Sling:
                    weaponProficiancy.text += " Праща,";
                    break;
                case Weapon.BladeType.Halberd:
                    weaponProficiancy.text += " Алебарда,";
                    break;
                case Weapon.BladeType.BattlePickaxe:
                    weaponProficiancy.text += " Боевая кирка,";
                    break;
                case Weapon.BladeType.BattleHammer:
                    weaponProficiancy.text += " Боевой молот,";
                    break;
                case Weapon.BladeType.BattleAxe:
                    weaponProficiancy.text += " Боевой топор,";
                    break;
                case Weapon.BladeType.Glaive:
                    weaponProficiancy.text += " Глефа,";
                    break;
                case Weapon.BladeType.TwohandedSword:
                    weaponProficiancy.text += " Двуручный меч,";
                    break;
                case Weapon.BladeType.LongSpear:
                    weaponProficiancy.text += " Длинное копьё,";
                    break;
                case Weapon.BladeType.LongSword:
                    weaponProficiancy.text += " Длинный меч,";
                    break;
                case Weapon.BladeType.Whip:
                    weaponProficiancy.text += " Кнут,";
                    break;
                case Weapon.BladeType.ShortSword:
                    weaponProficiancy.text += " Короткий меч,";
                    break;
                case Weapon.BladeType.Hammer:
                    weaponProficiancy.text += " Молот,";
                    break;
                case Weapon.BladeType.Morgenstern:
                    weaponProficiancy.text += " Моргенштерн,";
                    break;
                case Weapon.BladeType.Peak:
                    weaponProficiancy.text += " Пика,";
                    break;
                case Weapon.BladeType.Rapier:
                    weaponProficiancy.text += " Рапира,";
                    break;
                case Weapon.BladeType.Poleaxe:
                    weaponProficiancy.text += " Секира,";
                    break;
                case Weapon.BladeType.Scimitar:
                    weaponProficiancy.text += " Скимитар,";
                    break;
                case Weapon.BladeType.Trident:
                    weaponProficiancy.text += " Трезубец,";
                    break;
                case Weapon.BladeType.Flail:
                    weaponProficiancy.text += " Цеп,";
                    break;
                case Weapon.BladeType.HandedCrossbow:
                    weaponProficiancy.text += " Ручной арбалет,";
                    break;
                case Weapon.BladeType.HeavyCrossbow:
                    weaponProficiancy.text += " Тяжёлый арбалет,";
                    break;
                case Weapon.BladeType.LongBow:
                    weaponProficiancy.text += " Длинный лук,";
                    break;
                case Weapon.BladeType.Pipe:
                    weaponProficiancy.text += " Духовая трубка,";
                    break;
                case Weapon.BladeType.Net:
                    weaponProficiancy.text += " Сеть,";
                    break;
            }
        weaponProficiancy.text = weaponProficiancy.text.Remove(weaponProficiancy.text.Length - 1);
        foreach (string x in CharacterData.GetInstruments())
        {
            instrumentProficiancy.text += " " + x;
            if (PresavedLists.compInstruments.Contains(x))
                instrumentProficiancy.text += "*";
            instrumentProficiancy.text += ",";
        }
        instrumentProficiancy.text = instrumentProficiancy.text.Remove(instrumentProficiancy.text.Length - 1);
        foreach (string x in CharacterData.GetLanguage())
            language.text += " " + x + ",";
        language.text = language.text.Remove(language.text.Length - 1);
    }
    void loadFeats()
    {
        List<Feat> feats = PresavedLists.feats;
        if (feats == null || feats.Count == 0) return;
        GameObject featsBody = Instantiate(form, content.transform);
        featsBody.GetComponent<FormShower>().SetHead("Черты");
        foreach (Feat y in feats)
        {
            Instantiate(form, featsBody.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(y.ability, 1, null);
        }
    }
}
