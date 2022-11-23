using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquipment : MonoBehaviour
{
    [SerializeField] Text label;
    [SerializeField] Text damage;
    [SerializeField] Text distance;
    [SerializeField] Text type;
    [SerializeField] Text properties;
    [SerializeField] Toggle magick;
    [SerializeField] Toggle equip;
    HandsInventory _handsInventory;
    Weapon currentWeapon = null;

    public void SetWeapon(Weapon weapon, HandsInventory handsInventory)
    {
        _handsInventory = handsInventory;
        label.text = weapon.label;
        damage.text = weapon.dices + "к" + weapon.hitDice;
        if (weapon.dist == weapon.maxDist)
            distance.text = weapon.dist + " фт.";
        else
            distance.text = weapon.dist + "/" + weapon.maxDist + " фт.";
        switch (weapon.damageType)
        {
            case Weapon.DamageType.Slashing:
                type.text = "Рубящий";
                break;
            case Weapon.DamageType.Crushing:
                type.text = "Дробящий";
                break;
            case Weapon.DamageType.Piercing:
                type.text = "Колющий";
                break;

        }
        magick.isOn = weapon.magic;
        string propString = "";
        foreach (Weapon.Properties x in weapon.properties)
        {
            switch (x)
            {
                case Weapon.Properties.Ammo:
                    propString += "Боеприпасы, ";
                    break;
                case Weapon.Properties.TwoHanded:
                    propString += "Двуручное, ";
                    break;
                case Weapon.Properties.Reach:
                    propString += "ДОсигаемость, ";
                    break;
                case Weapon.Properties.Distance:
                    propString += "Дистанционное, ";
                    break;
                case Weapon.Properties.Light:
                    propString += "Лёгкое, ";
                    break;
                case Weapon.Properties.Throwing:
                    propString += "Метательное, ";
                    break;
                case Weapon.Properties.Special:
                    propString += "Особое, ";
                    break;
                case Weapon.Properties.Reload:
                    propString += "Перезарядка, ";
                    break;
                case Weapon.Properties.Heavy:
                    propString += "Тяжёлое, ";
                    break;
                case Weapon.Properties.Universal:
                    propString += "Универсанльное, ";
                    break;
                case Weapon.Properties.Fencing:
                    propString += "Фехтовальное, ";
                    break;

            }
        }
        properties.text = propString.Remove(propString.LastIndexOf(','));
        if (_handsInventory != null)
            equip.onValueChanged.AddListener(delegate
            {
                DataSaverAndLoader.WeaponEquipmentChanged(weapon, equip.isOn);
                _handsInventory.NewItemEquiptedOrUnequipted(weapon, equip.isOn);
            });
        currentWeapon = weapon;
    }

    public Weapon GetWeapon()
    {
        return currentWeapon;
    }

    public bool GetEquipped()
    {
        return equip.isOn;
    }

    public void ForceEquip()
    {
        equip.onValueChanged.RemoveAllListeners();
        equip.isOn = true;
        _handsInventory.NewItemEquiptedOrUnequipted(currentWeapon, equip.isOn);
        equip.onValueChanged.AddListener(delegate
        {
            DataSaverAndLoader.WeaponEquipmentChanged(currentWeapon, equip.isOn);
            _handsInventory.NewItemEquiptedOrUnequipted(currentWeapon, equip.isOn);
        });
    }

    public void Unequip()
    {
        equip.isOn = false;
    }
}
