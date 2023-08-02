using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject weaponObject;
    [SerializeField] HandsInventory handsInventory;

    public void AddWeapon(Weapon weapon)
    {
        GameObject newWeaponObject = Instantiate(weaponObject, content.transform);
        newWeaponObject.GetComponent<WeaponEquipment>().SetWeapon(weapon, handsInventory);
    }

    public void LoadEquited()
    {
        WeaponEquipment[] weapons = content.GetComponentsInChildren<WeaponEquipment>();
        List<(int, string)> equipted = CharacterData.GetWeaponEquip();
        foreach ((int, string) x in new List<(int, string)>(equipted))
        {
            foreach (WeaponEquipment y in weapons)
            {
                if (!y.GetEquipped())
                    if (x.Item1 != -1 && x.Item1 == y.GetWeapon().id)
                    {
                        y.ForceEquip();
                        break;
                    }
                    else
                    {
                        if (x.Item2 == y.GetWeapon().label)
                        {
                            y.ForceEquip();
                            break;
                        }
                    }
            }
        }
    }

    public void OnWeaponRemove(Item weapon)
    {
        WeaponEquipment[] weapons = content.GetComponentsInChildren<WeaponEquipment>();
        WeaponEquipment bufWeapon = null;
        foreach (WeaponEquipment x in weapons)
        {
            if (x.GetWeapon() == weapon as Weapon || ((weapon as Armor) != null && (weapon as Armor).armorType == Armor.ArmorType.Shield && x.GetWeapon().weaponType == Weapon.WeaponType.Shield))
            {
                Toggle toggle = x.gameObject.GetComponentInChildren<Type>().GetComponent<Toggle>();
                if (toggle.isOn)
                    bufWeapon = x;
                else
                {
                    Destroy(x.gameObject);
                    return;
                }
            }
        }
        if (bufWeapon != null)
        {
            bufWeapon.Unequip();
            Destroy(bufWeapon.gameObject);
        }
    }

    public void RemoveEquippedWeapon(Weapon weapon)
    {
        WeaponEquipment[] weapons = content.GetComponentsInChildren<WeaponEquipment>();
        foreach (WeaponEquipment x in weapons)
        {
            //Toggle toggle = x.gameObject.GetComponentInChildren<Type>().GetComponent<Toggle>();
            if (x.GetWeapon() == weapon && x.GetEquipped())
            {
                x.Unequip();
                return;
            }
        }
    }
}
