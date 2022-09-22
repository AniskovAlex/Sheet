using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorEquipment : MonoBehaviour
{
    [SerializeField] Text label;
    [SerializeField] Text AC;
    [SerializeField] Text ACCap;
    [SerializeField] Text type;
    [SerializeField] Text strReq;
    [SerializeField] Toggle stealth;
    [SerializeField] Toggle equip;
    Armor currentArmor = null;

    public void SetArmor(Armor armor)
    {
        label.text = armor.label;
        AC.text = armor.AC.ToString();
        if (armor.ACCap == 0)
            ACCap.text = "-";
        if (armor.ACCap == -1)
            ACCap.text = "+ мод Лов.";
        if (armor.ACCap > 0)
            ACCap.text = "+ мод Лов. (Макс. 2)";
        if (armor.strReq > 0)
            strReq.text = "Сил. " + armor.strReq.ToString();
        else
            strReq.text = "-";
        switch (armor.armorType)
        {
            case Armor.ArmorType.Light:
                type.text = "Лёгкие";
                break;
            case Armor.ArmorType.Medium:
                type.text = "Средние";
                break;
            case Armor.ArmorType.Heavy:
                type.text = "Тяжёлые";
                break;
            case Armor.ArmorType.Shield:
                type.text = "Щит";
                break;

        }
        stealth.isOn = armor.stealthDis;
        ACController body = FindObjectOfType<ACController>();
        ArmorInventory inventory = FindObjectOfType<ArmorInventory>();
        if (body != null)
            equip.onValueChanged.AddListener(delegate {
                inventory.RemoveEquippedArmor(this, equip.isOn);
                body.UpdateArmorClass(armor, equip.isOn);
                DataSaverAndLoader.ArmorEquipmentChanged(armor, equip.isOn);
            });
        currentArmor = armor;
    }

    public Armor GetArmor()
    {
        return currentArmor;
    }

    public bool GetEquipped()
    {
        return equip.isOn;
    }

    public void ForceEquip()
    {
        equip.onValueChanged.RemoveListener(delegate
        {
            DataSaverAndLoader.ArmorEquipmentChanged(currentArmor, equip.isOn);
        });
        equip.isOn = true;
        equip.onValueChanged.AddListener(delegate
        {
            DataSaverAndLoader.ArmorEquipmentChanged(currentArmor, equip.isOn);
        });
    }

    public void Unequip()
    {
        equip.isOn = false;
    }
}
