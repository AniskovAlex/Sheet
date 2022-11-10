using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsRedactor : MonoBehaviour
{
    [SerializeField] InputField costField;
    [SerializeField] InputField weightField;
    [SerializeField] InputField nameField;
    [SerializeField] Dropdown mType;
    [SerializeField] Dropdown type;
    [SerializeField] GameObject weaponProperties;
    [SerializeField] GameObject weaponPropertieObject;
    [SerializeField] WeaponSet weaponSet;
    [SerializeField] ArmorSet armorSet;
    [SerializeField] InputField amountField;
    Item currentItem = null;

    public void SetItem(Item item)
    {
        if (item != null)
        {
            nameField.text = item.label[0].ToString().ToUpper() + item.label.Remove(0, 1);
            costField.text = item.cost.ToString();
            weightField.text = item.weight.ToString();
            mType.value = (int)item.mType;
            bool flag = false;
            if (item is Armor)
            {
                Armor armorItem = item as Armor;
                type.value = 1;
                armorSet.SetArmor(armorItem);
                flag = true;
            }
            if (item is Weapon)
            {
                Weapon weapon = item as Weapon;
                type.value = 2;
                weaponSet.SetWeapon(weapon);
                flag = true;
            }
            if (!flag)
            {
                type.value = 0;
            }
            LockRedact();
            currentItem = item;
        }
        else
        {
            UnlockRedact();
            currentItem = null;
        }
    }

    public (Item, int) Itempackaging()
    {
        if (amountField.text == "")
            return (null, 0);
        int amount = int.Parse(amountField.text);
        if (currentItem != null)
        {
            (Item, int) result = (currentItem, amount);
            ClearFields();
            return result;
        }
        Item newItem = new Item();
        if (nameField.text == "" || costField.text == "" || weightField.text == "")
            return (null, 0);
        newItem.id = -1;
        newItem.label = nameField.text;
        newItem.mType = Item.MType.goldCoin + mType.value;
        newItem.cost = int.Parse(costField.text);
        newItem.weight = int.Parse(weightField.text);
        switch (type.value)
        {

            case 1:
                Armor newArmor = armorSet.Packaging();
                if (newArmor == null)
                    return (null, 0);
                newArmor.id = newItem.id;
                newArmor.label = newItem.label;
                newArmor.mType = newItem.mType;
                newArmor.cost = newItem.cost;
                newArmor.weight = newItem.weight;
                ClearFields();
                return (newArmor, amount);
            case 2:
                Weapon newWeapon = weaponSet.Packaging();
                if (newWeapon == null)
                    return (null, 0);
                newWeapon.id = newItem.id;
                newWeapon.label = newItem.label;
                newWeapon.mType = newItem.mType;
                newWeapon.cost = newItem.cost;
                newWeapon.weight = newItem.weight;
                ClearFields();
                return (newWeapon, amount);
        }
        ClearFields();
        return (newItem, amount);
    }

    void ClearFields()
    {
        nameField.text = "";
        costField.text = "";
        weightField.text = "";
        mType.value = 0;
        type.value = 0;
        amountField.text = "1";
        currentItem = null;
    }

    public void UnlockRedact()
    {
        costField.interactable = true;
        weightField.interactable = true;
        mType.interactable = true;
        type.interactable = true;
        weaponSet.Unlock();
        armorSet.Unlock();
    }

    void LockRedact()
    {
        costField.interactable = false;
        weightField.interactable = false;
        mType.interactable = false;
        type.interactable = false;
        weaponSet.Lock();
        armorSet.Lock();
    }

    void ActivateArmor()
    {
        armorSet.gameObject.SetActive(true);
        weaponSet.gameObject.SetActive(false);
    }

    void ActivateWeapon()
    {
        weaponSet.gameObject.SetActive(true);
        armorSet.gameObject.SetActive(false);
    }
    void ActivateItem()
    {
        weaponSet.gameObject.SetActive(false);
        armorSet.gameObject.SetActive(false);
    }

    public void TypeChanged()
    {
        switch (type.value)
        {
            case 0:
                ActivateItem();
                break;
            case 1:
                ActivateArmor();
                break;
            case 2:
                ActivateWeapon();
                break;
        }
    }

}
