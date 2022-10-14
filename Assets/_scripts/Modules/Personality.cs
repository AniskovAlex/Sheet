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
        LoadPersonInformation();
        LoadProficiancy();
        classes = CharacterData.GetClasses();
        foreach ((int, PlayersClass) playersClass in classes)
            if (playersClass.Item2 != null)
            {
                classesName.text += " " + playersClass.Item2.name + " ур." + playersClass.Item1 + ",";
                GameObject classBody = Instantiate(form, content.transform);
                classBody.GetComponent<FormShower>().SetHead(playersClass.Item2.name);
                if (playersClass.Item2 != null)
                {
                    Ability[] abilitieSubClassArr = playersClass.Item2.ChooseSubClass(DataSaverAndLoader.LoadSubClass(playersClass.Item2));
                    Ability[] abilityArr = playersClass.Item2.GetAbilities();
                    if (abilitieSubClassArr != null)
                        abilityArr.Concat(abilitieSubClassArr).ToArray();
                    foreach (Ability x in abilityArr)
                    {
                        if (x.level <= playersClass.Item1)
                            Instantiate(form, classBody.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(x);
                    }
                }
            }
        classesName.text.Remove(classesName.text.Length - 1);
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
                abilityArr.Concat(abilitieSubRaceArr).ToArray();
                RaceName.text += "(" + race.GetSubRace().GetName() + ")";
            }
            foreach (Ability x in abilityArr)
            {
                Instantiate(form, raceBody.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(x);
            }
        }
        backstory = CharacterData.GetBackstory();
        if(backstory != null)
        {
            backstoryObject.GetComponent<FormShower>().SetHead(backstory.name);
            Ability[] abilityArr = backstory.GetAbilities();
            foreach (Ability x in abilityArr)
            {
                Instantiate(form, backstoryObject.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(x);
            }
        }
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
        armorProficiancy.text.Remove(armorProficiancy.text.Length - 1);

        foreach (Weapon.WeaponType x in CharacterData.GetWeaponProficiency())
            switch (x)
            {
                case Weapon.WeaponType.CommonMelee:
                    weaponProficiancy.text += " простое рукопашное,";
                    break;
                case Weapon.WeaponType.CommonDist:
                    weaponProficiancy.text += " простое дальнобойное,";
                    break;
                case Weapon.WeaponType.WarMelee:
                    weaponProficiancy.text += " воинское рукопашное,";
                    break;
                case Weapon.WeaponType.WarDist:
                    weaponProficiancy.text += " воинское дальнобойное,";
                    break;
            }
        foreach (Weapon.BladeType x in CharacterData.GetBladeProficiency())
            switch (x)
            {
                case Weapon.BladeType.Sword:
                    weaponProficiancy.text += " длинный меч,";
                    break;
                case Weapon.BladeType.ShortBow:
                    weaponProficiancy.text += " короткий лук,";
                    break;
                case Weapon.BladeType.LongBow:
                    weaponProficiancy.text += " длинный лук,";
                    break;
                case Weapon.BladeType.Club:
                    weaponProficiancy.text += " дубинка,";
                    break;
            }
        weaponProficiancy.text.Remove(weaponProficiancy.text.Length - 1);
        foreach (string x in CharacterData.GetInstruments())
            instrumentProficiancy.text += " " + x + ",";
        instrumentProficiancy.text.Remove(instrumentProficiancy.text.Length - 1);
        foreach (string x in CharacterData.GetLanguage())
            language.text += " " + x + ",";
        language.text.Remove(language.text.Length - 1);
    }
}
