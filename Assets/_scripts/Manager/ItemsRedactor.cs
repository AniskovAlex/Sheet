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
    [SerializeField] GameObject weaponSet;
    [SerializeField] GameObject armorSet;
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
                armorSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value = (int)armorItem.armorType;
                armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text = armorItem.AC.ToString();
                armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().text = armorItem.ACCap.ToString();
                armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().text = armorItem.strReq.ToString();
                armorSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn = armorItem.stealthDis;

                flag = true;
            }
            if (item is Weapon)
            {
                Weapon weaponItem = item as Weapon;
                type.value = 2;
                weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value = (int)weaponItem.damageType;
                weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().text = weaponItem.dices.ToString();
                weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text = weaponItem.hitDice.ToString();
                weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().text = weaponItem.dist.ToString();
                weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().text = weaponItem.maxDist.ToString();
                weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn = weaponItem.magic;
                Skill[] buf = weaponSet.GetComponentsInChildren<Skill>();
                for (int i = 0; i < buf.Length; i++)
                {
                    Destroy(buf[i].gameObject);
                }
                int j = 0;
                foreach (Weapon.Properties y in weaponItem.properties)
                {
                    GameObject newGameObject = Instantiate(weaponPropertieObject, weaponProperties.transform);
                    newGameObject.GetComponent<Dropdown>().value = (int)weaponItem.properties[j];
                    newGameObject.GetComponent<Dropdown>().interactable = false;
                    j++;
                }
                weaponSet.GetComponentInChildren<Amount>().GetComponent<Dropdown>().value = (int)weaponItem.bladeType;
                weaponSet.GetComponentInChildren<Amount>().GetComponent<Dropdown>().interactable = false;
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
                Armor newArmor = new Armor();
                newArmor.id = newItem.id;
                newArmor.label = newItem.label;
                newArmor.mType = newItem.mType;
                newArmor.cost = newItem.cost;
                newArmor.weight = newItem.weight;
                if (armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text == ""
                    || armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().text == ""
                    || armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().text == ""
                    ) return (null, 0);
                newArmor.AC = int.Parse(armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text);
                newArmor.ACCap = int.Parse(armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().text);
                newArmor.stealthDis = armorSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn;
                newArmor.strReq = int.Parse(armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().text);
                newArmor.armorType = Armor.ArmorType.Light + armorSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value;
                ClearFields();
                return (newArmor, amount);
            case 2:
                Weapon newWeapon = new Weapon();
                newWeapon.id = newItem.id;
                newWeapon.label = newItem.label;
                newWeapon.mType = newItem.mType;
                newWeapon.cost = newItem.cost;
                newWeapon.weight = newItem.weight;
                if (weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().text == ""
                    || weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text == ""
                    || weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().text == ""
                    || weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().text == ""
                    ) return (null, 0);
                newWeapon.damageType = Weapon.DamageType.Slashing + weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value;
                newWeapon.dices = int.Parse(weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().text);
                newWeapon.hitDice = int.Parse(weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text);
                newWeapon.dist = int.Parse(weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().text);
                newWeapon.maxDist = int.Parse(weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().text);
                newWeapon.magic = weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn;
                List<Weapon.Properties> list = new List<Weapon.Properties>();
                Dropdown[] prop = weaponSet.GetComponentsInChildren<Dropdown>();
                foreach (Dropdown x in prop)
                {
                    list.Add(Weapon.Properties.Ammo + x.value);
                }
                newWeapon.properties = list.ToArray();
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
        armorSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value = 0;
        armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text = "";
        armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().text = "";
        armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().text = "";
        armorSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn = false;
        weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().value = 0;
        weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().text = "1";
        weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().text = "4";
        weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().text = "5";
        weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().text = "5";
        weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().isOn = false;
        Skill[] buf = weaponSet.GetComponentsInChildren<Skill>();
        for (int i = 0; i < buf.Length; i++)
        {
            Destroy(buf[i].gameObject);
        }
        currentItem = null;
    }

    public void UnlockRedact()
    {
        costField.interactable = true;
        weightField.interactable = true;
        mType.interactable = true;
        type.interactable = true;
        weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().interactable = true;
        weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().interactable = true;
        weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().interactable = true;
        weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().interactable = true;
        weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().interactable = true;
        weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().interactable = true;
        armorSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().interactable = true;
        armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().interactable = true;
        armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().interactable = true;
        armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().interactable = true;
        armorSet.GetComponentInChildren<Type>().GetComponent<Toggle>().interactable = true;
        Dropdown[] prop = weaponSet.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in prop)
        {
            x.interactable = true;
        }
    }

    void LockRedact()
    {
        costField.interactable = false;
        weightField.interactable = false;
        mType.interactable = false;
        type.interactable = false;
        weaponSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().interactable = false;
        weaponSet.GetComponentInChildren<Attribute>().GetComponent<InputField>().interactable = false;
        weaponSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().interactable = false;
        weaponSet.GetComponentInChildren<Money>().GetComponent<InputField>().interactable = false;
        weaponSet.GetComponentInChildren<MType>().GetComponent<InputField>().interactable = false;
        weaponSet.GetComponentInChildren<Type>().GetComponent<Toggle>().interactable = false;
        armorSet.GetComponentInChildren<Weight>().GetComponent<Dropdown>().interactable = false;
        armorSet.GetComponentInChildren<Modifier>().GetComponent<InputField>().interactable = false;
        armorSet.GetComponentInChildren<Money>().GetComponent<InputField>().interactable = false;
        armorSet.GetComponentInChildren<Amount>().GetComponent<InputField>().interactable = false;
        armorSet.GetComponentInChildren<Type>().GetComponent<Toggle>().interactable = false;
        Dropdown[] prop = weaponSet.GetComponentsInChildren<Dropdown>();
        foreach (Dropdown x in prop)
        {
            x.interactable = false;
        }
        GameObject newGameObject = Instantiate(weaponPropertieObject, weaponProperties.transform);
        newGameObject.GetComponent<Dropdown>().value = 11;
    }

    void ActivateArmor()
    {
        armorSet.SetActive(true);
        weaponSet.SetActive(false);
    }

    void ActivateWeapon()
    {
        weaponSet.SetActive(true);
        armorSet.SetActive(false);
    }
    void ActivateItem()
    {
        weaponSet.SetActive(false);
        armorSet.SetActive(false);
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

    public void AddProperties(Dropdown obj)
    {
        if (obj.value >= 11)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            if (weaponProperties.GetComponentsInChildren<Skill>()[weaponProperties.GetComponentsInChildren<Skill>().Length - 1].GetComponent<Dropdown>().value < 11)
            {
                GameObject newGameObject = Instantiate(weaponPropertieObject, weaponProperties.transform);
                newGameObject.GetComponent<Dropdown>().onValueChanged.AddListener(delegate { AddProperties(newGameObject.GetComponent<Dropdown>()); });
            }
        }
    }
}
