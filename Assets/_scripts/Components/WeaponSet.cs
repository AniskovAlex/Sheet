using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSet : MonoBehaviour
{
    [SerializeField] InputField dices;
    [SerializeField] InputField hitDice;
    [SerializeField] InputField dist;
    [SerializeField] InputField maxDist;
    [SerializeField] Toggle magic;
    [SerializeField] Dropdown type;
    [SerializeField] Dropdown blade;
    [SerializeField] GameObject props;
    [SerializeField] Dropdown weaponPropObj;
    bool _lock = false;

    public void SetWeapon(Weapon weapon)
    {
        type.value = (int)weapon.damageType;
        dices.text = weapon.dices.ToString();
        hitDice.text = weapon.hitDice.ToString();
        dist.text = weapon.dist.ToString();
        maxDist.text = weapon.maxDist.ToString();
        magic.isOn = weapon.magic;
        Dropdown[] buf = props.GetComponentsInChildren<Dropdown>();
        for (int i = 0; i < buf.Length; i++)
        {
            Destroy(buf[i].gameObject);
        }
        int j = 0;
        foreach (Weapon.Properties y in weapon.properties)
        {
            Dropdown dropdown = Instantiate(weaponPropObj, props.transform);
            dropdown.value = (int)weapon.properties[j];
            dropdown.interactable = false;
            dropdown.onValueChanged.AddListener(delegate { AddProperties(dropdown); });
            j++;
        }
        blade.value = (int)weapon.bladeType;
        Lock();
    }

    public void Lock()
    {
        dices.interactable = false;
        hitDice.interactable = false;
        dist.interactable = false;
        maxDist.interactable = false;
        type.interactable = false;
        magic.interactable = false;
        type.interactable = false;
        blade.interactable = false;
        Dropdown[] buf = props.GetComponentsInChildren<Dropdown>();
        if (buf != null)
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i].interactable = false;
            }
        _lock = true;
    }
    public void Unlock()
    {
        dices.interactable = true;
        hitDice.interactable = true;
        dist.interactable = true;
        maxDist.interactable = true;
        type.interactable = true;
        magic.interactable = true;
        type.interactable = true;
        blade.interactable = true;
        Dropdown[] buf = props.GetComponentsInChildren<Dropdown>();
        if (buf != null)
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i].interactable = true;
            }
        if (buf[buf.Length - 1].value < 11)
        {
            Dropdown newDrop = Instantiate(weaponPropObj, props.transform);
            newDrop.value = 11;
            newDrop.onValueChanged.AddListener(delegate { AddProperties(newDrop); });
        }
        _lock = false;
    }

    public bool isLock()
    {
        return _lock;
    }

    public void AddProperties(Dropdown obj)
    {
        if (obj.value >= 11)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            Dropdown[] buf = props.GetComponentsInChildren<Dropdown>();
            if (buf[buf.Length - 1].value < 11)
            {
                Dropdown newDrop = Instantiate(weaponPropObj, props.transform);
                newDrop.value = 11;
                newDrop.onValueChanged.AddListener(delegate { AddProperties(newDrop); });
            }
        }
    }

    public Weapon Packaging()
    {
        Weapon newWeapon = new Weapon();
        if (dices.text == "" || dist.text == "" || maxDist.text == "" || hitDice.text == "")
            return null;
        int.TryParse(dices.text, out newWeapon.dices);
        int.TryParse(hitDice.text, out newWeapon.hitDice);
        int.TryParse(dist.text, out newWeapon.dist);
        int.TryParse(maxDist.text, out newWeapon.maxDist);
        newWeapon.magic = magic.isOn;
        newWeapon.bladeType= Weapon.BladeType.Sword + blade.value;
        switch (newWeapon.bladeType)
        {
            default:
                newWeapon.weaponType = Weapon.WeaponType.CommonMelee;
                break;
        }
        List<Weapon.Properties> list = new List<Weapon.Properties>();
        Dropdown[] buf = props.GetComponentsInChildren<Dropdown>();
        if (buf != null)
            for (int i = 0; i < buf.Length; i++)
            {
                list.Add(Weapon.Properties.Ammo + buf[i].value);
            }
        newWeapon.properties = list.ToArray();
        return newWeapon;
    }

    private void OnDisable()
    {
        type.value = 0;
        dices.text = "1";
        hitDice.text = "4";
        dist.text = "5";
        maxDist.text = "5";
        magic.isOn = false;
        Dropdown[] buf = props.GetComponentsInChildren<Dropdown>();
        for (int i = 0; i < buf.Length; i++)
        {
            Destroy(buf[i].gameObject);
        }
        Dropdown newDrop = Instantiate(weaponPropObj, props.transform);
        newDrop.value = 11;
        newDrop.onValueChanged.AddListener(delegate { AddProperties(newDrop); });
        blade.value = 0;
        Unlock();
    }
}
