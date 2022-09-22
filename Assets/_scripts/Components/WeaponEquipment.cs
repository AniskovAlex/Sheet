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
    Weapon currentWeapon = null;

    public void SetWeapon(Weapon weapon)
    {
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
                    propString += "Боеприпасы, ";
                    break;
                case Weapon.Properties.Reach:
                    propString += "Боеприпасы, ";
                    break;
                case Weapon.Properties.Distance:
                    propString += "Боеприпасы, ";
                    break;
                case Weapon.Properties.Light:
                    propString += "Боеприпасы, ";
                    break;
                case Weapon.Properties.Throwing:
                    propString += "Боеприпасы, ";
                    break;
                case Weapon.Properties.Special:
                    propString += "Боеприпасы, ";
                    break;
                case Weapon.Properties.Reload:
                    propString += "Боеприпасы, ";
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
        propString.Remove(propString.LastIndexOf(','));
        HandsInventory hand = FindObjectOfType<HandsInventory>();
        if (hand != null)
            equip.onValueChanged.AddListener(delegate { 
                hand.NewItemEquiptedOrUnequipted(weapon, equip.isOn);
                DataSaverAndLoader.WeaponEquipmentChanged(weapon, equip.isOn);
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
        equip.onValueChanged.RemoveListener(delegate
        {
            DataSaverAndLoader.WeaponEquipmentChanged(currentWeapon, equip.isOn);
        });
        equip.isOn = true;
        equip.onValueChanged.AddListener(delegate
        {
            DataSaverAndLoader.WeaponEquipmentChanged(currentWeapon, equip.isOn);
        });
    }

    public void Unequip()
    {
        equip.isOn = false;
    }
}
