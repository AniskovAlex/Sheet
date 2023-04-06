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
                classesName.text += " " + playersClass.Item2.name + " ��." + playersClass.Item1;
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
                    armorProficiancy.text += " �����,";
                    break;
                case Armor.ArmorType.Medium:
                    armorProficiancy.text += " �������,";
                    break;
                case Armor.ArmorType.Heavy:
                    armorProficiancy.text += " ������,";
                    break;
                case Armor.ArmorType.Shield:
                    armorProficiancy.text += " ����,";
                    break;
            }
        armorProficiancy.text = armorProficiancy.text.Remove(armorProficiancy.text.Length - 1);

        foreach (Weapon.WeaponType x in CharacterData.GetWeaponProficiency())
            switch (x)
            {
                case Weapon.WeaponType.CommonMelee:
                    weaponProficiancy.text += " ������� ����������,";
                    break;
                case Weapon.WeaponType.CommonDist:
                    weaponProficiancy.text += " ������� ������������,";
                    break;
                case Weapon.WeaponType.WarMelee:
                    weaponProficiancy.text += " �������� ����������,";
                    break;
                case Weapon.WeaponType.WarDist:
                    weaponProficiancy.text += " �������� ������������,";
                    break;
            }
        foreach (Weapon.BladeType x in CharacterData.GetBladeProficiency())
            switch (x)
            {
                case Weapon.BladeType.WarStaff:
                    weaponProficiancy.text += " ������ �����,";
                    break;
                case Weapon.BladeType.Mace:
                    weaponProficiancy.text += " ������,";
                    break;
                case Weapon.BladeType.Club:
                    weaponProficiancy.text += " �������,";
                    break;
                case Weapon.BladeType.Dagger:
                    weaponProficiancy.text += " ������,";
                    break;
                case Weapon.BladeType.Spear:
                    weaponProficiancy.text += " �����,";
                    break;
                case Weapon.BladeType.LightHammer:
                    weaponProficiancy.text += " ˸���� �����,";
                    break;
                case Weapon.BladeType.ThrowingSpear:
                    weaponProficiancy.text += " ����������� �����,";
                    break;
                case Weapon.BladeType.HandAxe:
                    weaponProficiancy.text += " ������ �����,";
                    break;
                case Weapon.BladeType.Sickle:
                    weaponProficiancy.text += " ����,";
                    break;
                case Weapon.BladeType.LightCrossbow:
                    weaponProficiancy.text += " ˸���� �������,";
                    break;
                case Weapon.BladeType.Dart:
                    weaponProficiancy.text += " ������,";
                    break;
                case Weapon.BladeType.Stick:
                    weaponProficiancy.text += " ������,";
                    break;
                case Weapon.BladeType.ShortBow:
                    weaponProficiancy.text += " �������� ���,";
                    break;
                case Weapon.BladeType.Sling:
                    weaponProficiancy.text += " �����,";
                    break;
                case Weapon.BladeType.Halberd:
                    weaponProficiancy.text += " ��������,";
                    break;
                case Weapon.BladeType.BattlePickaxe:
                    weaponProficiancy.text += " ������ �����,";
                    break;
                case Weapon.BladeType.BattleHammer:
                    weaponProficiancy.text += " ������ �����,";
                    break;
                case Weapon.BladeType.BattleAxe:
                    weaponProficiancy.text += " ������ �����,";
                    break;
                case Weapon.BladeType.Glaive:
                    weaponProficiancy.text += " �����,";
                    break;
                case Weapon.BladeType.TwohandedSword:
                    weaponProficiancy.text += " ��������� ���,";
                    break;
                case Weapon.BladeType.LongSpear:
                    weaponProficiancy.text += " ������� �����,";
                    break;
                case Weapon.BladeType.LongSword:
                    weaponProficiancy.text += " ������� ���,";
                    break;
                case Weapon.BladeType.Whip:
                    weaponProficiancy.text += " ����,";
                    break;
                case Weapon.BladeType.ShortSword:
                    weaponProficiancy.text += " �������� ���,";
                    break;
                case Weapon.BladeType.Hammer:
                    weaponProficiancy.text += " �����,";
                    break;
                case Weapon.BladeType.Morgenstern:
                    weaponProficiancy.text += " �����������,";
                    break;
                case Weapon.BladeType.Peak:
                    weaponProficiancy.text += " ����,";
                    break;
                case Weapon.BladeType.Rapier:
                    weaponProficiancy.text += " ������,";
                    break;
                case Weapon.BladeType.Poleaxe:
                    weaponProficiancy.text += " ������,";
                    break;
                case Weapon.BladeType.Scimitar:
                    weaponProficiancy.text += " ��������,";
                    break;
                case Weapon.BladeType.Trident:
                    weaponProficiancy.text += " ��������,";
                    break;
                case Weapon.BladeType.Flail:
                    weaponProficiancy.text += " ���,";
                    break;
                case Weapon.BladeType.HandedCrossbow:
                    weaponProficiancy.text += " ������ �������,";
                    break;
                case Weapon.BladeType.HeavyCrossbow:
                    weaponProficiancy.text += " ������ �������,";
                    break;
                case Weapon.BladeType.LongBow:
                    weaponProficiancy.text += " ������� ���,";
                    break;
                case Weapon.BladeType.Pipe:
                    weaponProficiancy.text += " ������� ������,";
                    break;
                case Weapon.BladeType.Net:
                    weaponProficiancy.text += " ����,";
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
        featsBody.GetComponent<FormShower>().SetHead("�����");
        foreach (Feat y in feats)
        {
            Instantiate(form, featsBody.GetComponent<FormShower>().GetDiscription().transform).GetComponent<FormShower>().CreateAbility(y.ability, 1, null);
        }
    }
}
