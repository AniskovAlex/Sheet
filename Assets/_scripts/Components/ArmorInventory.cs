using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorInventory : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] GameObject armorObject;

    public void AddArmor(Armor armor)
    {
        GameObject newWeaponObject = Instantiate(armorObject, content.transform);
        newWeaponObject.GetComponent<ArmorEquipment>().SetArmor(armor);
    }

    public void LoadEquited()
    {
        ArmorEquipment[] armors = content.GetComponentsInChildren<ArmorEquipment>();
        List<(int, string)> equipted = CharacterData.GetArmorEquip();
        foreach ((int, string) x in new List<(int, string)>(equipted))
        {
            foreach (ArmorEquipment y in armors)
            {
                if (!y.GetEquipped())
                    if (x.Item1 != -1 && x.Item1 == y.GetArmor().id)
                    {
                        y.ForceEquip();
                        break;
                    }
                    else
                    {
                        if (x.Item2 == y.GetArmor().label)
                        {
                            y.ForceEquip();
                            break;
                        }
                    }
            }
        }
    }

    public void OnArmorRemove(Item armor)
    {
        Armor arm = armor as Armor;
        if (arm.armorType == Armor.ArmorType.Shield)
        {
            FindObjectOfType<WeaponInventory>().OnWeaponRemove(armor);
            return;
        }
        ArmorEquipment[] armors = content.GetComponentsInChildren<ArmorEquipment>();
        ArmorEquipment bufArmor = null;
        foreach (ArmorEquipment x in armors)
        {
            if (x.GetArmor() == arm)
            {
                Toggle toggle = x.gameObject.GetComponentInChildren<Type>().GetComponent<Toggle>();
                if (toggle.isOn)
                    bufArmor = x;
                else
                {
                    Destroy(x.gameObject);
                    return;
                }
            }
        }
        if (bufArmor != null)
        {
            bufArmor.Unequip();
            Destroy(bufArmor.gameObject);
        }
    }

    public void RemoveEquippedArmor(ArmorEquipment newArmor, bool equip)
    {
        if (equip == true)
        {
            ArmorEquipment[] armors = content.GetComponentsInChildren<ArmorEquipment>();
            foreach (ArmorEquipment x in armors)
            {
                Toggle toggle = x.gameObject.GetComponentInChildren<Type>().GetComponent<Toggle>();
                if (x != newArmor && toggle.isOn)
                {
                    toggle.isOn = false;
                }
            }
        }
    }
}
