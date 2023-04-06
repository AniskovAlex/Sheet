using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    [SerializeField] Text label;
    [SerializeField] Text amountField;
    [SerializeField] Text money;
    [SerializeField] Text weight;
    [SerializeField] Text type;
    Action<Item> inventoryItemRemove;
    ContentSizer content;
    int amount;
    Item item;
    bool isInventory = false;

    public void SetItem(Item item, int amount, bool flag)
    {
        isInventory = flag;
        if (item.label != null && item.label != "")
            label.text = item.label[0].ToString().ToUpper() + item.label.Remove(0, 1);
        transform.parent.TryGetComponent(out content);
        string moneyType = "";
        switch (item.mType)
        {
            case Item.MType.goldCoin:
                moneyType = "ÇÌ:";
                break;
            case Item.MType.silverCoin:
                moneyType = "ÑÌ:";
                break;
            case Item.MType.copperCoin:
                moneyType = "ÌÌ:";
                break;
        }
        money.text = moneyType + " " + item.cost;
        switch (item.weight)
        {
            case -1:
                weight.text = "1/2 ôíò";
                break;
            case -2:
                weight.text = "1/4 ôíò";
                break;
            default:
                weight.text = item.weight + " ôíò";
                break;
        }
        this.amount = amount;
        amountField.text = amount.ToString();
        type.text = "Ï";
        if (item is Weapon)
        {

            if (isInventory)
            {
                WeaponInventory weaponInventory = FindObjectOfType<WeaponInventory>();
                inventoryItemRemove = weaponInventory.OnWeaponRemove;
                for (int i = 0; i < amount; i++)
                    weaponInventory.AddWeapon(item as Weapon);
            }
            type.text = "Î";
        }
        if (item is Armor)
        {
            if (isInventory)
            {
                ArmorInventory armorInventory = FindObjectOfType<ArmorInventory>();
                inventoryItemRemove = armorInventory.OnArmorRemove;
                for (int i = 0; i < amount; i++)
                    armorInventory.AddArmor(item as Armor);
            }
            type.text = "Á";
        }
        if (!isInventory)
        {
            inventoryItemRemove = FindObjectOfType<AdderItemToPrelist>().RemoveItem;
        }
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void AddAmount(int addAmount)
    {
        amount += addAmount;
        amountField.text = amount.ToString();
        if (!isInventory)
            return;
        if (item is Weapon)
        {

            WeaponInventory weaponInventory = FindObjectOfType<WeaponInventory>();
            for (int i = 0; i < addAmount; i++)
                weaponInventory.AddWeapon(item as Weapon);
        }
        if (item is Armor)
        {
            ArmorInventory armorInventory = FindObjectOfType<ArmorInventory>();
            for (int i = 0; i < addAmount; i++)
                armorInventory.AddArmor(item as Armor);
        }
    }

    public void RemoveItem()
    {
        amount -= 1;
        amountField.text = amount.ToString();
        if (isInventory)
        {
            if (item.id != -1)
                DataSaverAndLoader.SaveAmountItem(item.id, amount);
            else
                DataSaverAndLoader.SaveAmountItem(item.label, amount);
        }
        if (inventoryItemRemove != null)
            inventoryItemRemove(item);
        if (amount <= 0)
        {
            if (isInventory)
                DataSaverAndLoader.RemoveItem(item);
            DestroyImmediate(gameObject);
            content.HieghtSizeInit();
        }
    }
}
