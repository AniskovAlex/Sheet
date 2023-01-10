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
            ACCap.text = "+ ��� ���.";
        if (armor.ACCap > 0)
            ACCap.text = "+ ��� ���. (����. 2)";
        if (armor.strReq > 0)
            strReq.text = "���. " + armor.strReq.ToString();
        else
            strReq.text = "-";
        switch (armor.armorType)
        {
            case Armor.ArmorType.Light:
                type.text = "˸����";
                break;
            case Armor.ArmorType.Medium:
                type.text = "�������";
                break;
            case Armor.ArmorType.Heavy:
                type.text = "������";
                break;
            case Armor.ArmorType.Shield:
                Weapon shield = new Weapon();
                shield.id = armor.id;
                shield.cost = armor.cost;
                shield.label= armor.label;
                shield.mType= armor.mType;
                shield.weaponType = Weapon.WeaponType.Shield;
                shield.weight = armor.weight;
                FindObjectOfType<WeaponInventory>().AddWeapon(shield);
                DestroyImmediate(gameObject);
                return;

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
