using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipment : MonoBehaviour
{
    [SerializeField] Text label;
    [SerializeField] Text attackBonus;
    [SerializeField] Text hitDices;
    [SerializeField] Text distance;
    [SerializeField] Text damageType;
    [SerializeField] Toggle magick;
    int hands = 1;
    Weapon currentWeapon;

    public int SetHand(Weapon weapon)
    {
        label.text = weapon.label;
        if (weapon.weaponType != Weapon.WeaponType.Shield)
        {
            int attack = CharacterData.GetModifier(0);
            if (weapon.weaponType == Weapon.WeaponType.CommonDist || weapon.weaponType == Weapon.WeaponType.WarDist)
                attack = CharacterData.GetModifier(1);
            int dist = weapon.dist;
            int maxDist = weapon.maxDist;
            string damage = weapon.dices + "к" + weapon.hitDice;
            if (GlobalStatus.monkWeapon && (weapon.weaponType == Weapon.WeaponType.CommonMelee || weapon.bladeType == Weapon.BladeType.ShortSword))
                attack = Mathf.Max(attack, CharacterData.GetModifier(1));
            foreach (Weapon.Properties x in weapon.properties)
            {
                switch (x)
                {
                    case Weapon.Properties.Fencing:
                        attack = Mathf.Max(attack, CharacterData.GetModifier(1));
                        break;
                    case Weapon.Properties.Reach:
                        maxDist = dist + 5;
                        break;
                    case Weapon.Properties.TwoHanded:
                        hands = 2;
                        break;
                    case Weapon.Properties.Universal:
                        damage += "(" + (weapon.hitDice + 2) + ")";
                        break;

                }

            }
            hitDices.text = damage;
            int addDamage = 0;

            if (GlobalStatus.archer && (weapon.weaponType == Weapon.WeaponType.CommonDist || weapon.weaponType == Weapon.WeaponType.WarDist))
                addDamage += 2;

            if (GlobalStatus.duelist && hands == 1)
            {
                HandsInventory handsInventory = GetComponentInParent<HandsInventory>();
                HandEquipment[] secondHands = handsInventory.GetComponentsInChildren<HandEquipment>();
                bool flag = true;
                foreach (HandEquipment x in secondHands)
                    if (x.gameObject.transform != transform)
                        if (x.currentWeapon.weaponType != Weapon.WeaponType.Shield)
                            flag = false;
                if (flag)
                    addDamage += 2;
            }

            if ((attack + addDamage) != 0)
            {
                if ((attack + addDamage) >= 0)
                    hitDices.text += "+" + (attack + addDamage);
                else
                    hitDices.text += (attack + addDamage);
            }
            if (CharacterData.GetBladeProficiency().Contains(weapon.bladeType) || CharacterData.GetWeaponProficiency().Contains(weapon.weaponType))
                attack += CharacterData.GetProficiencyBonus();

            if (attack > 0)
                attackBonus.text = "+" + attack;
            else
                attackBonus.text = attack.ToString();

            if (dist == maxDist)
                distance.text = dist + "фт.";
            else
                distance.text = dist + "/" + maxDist;

            magick.isOn = weapon.magic;

            switch (weapon.damageType)
            {
                case Weapon.DamageType.Slashing:
                    damageType.text = "Рубящий";
                    break;
                case Weapon.DamageType.Crushing:
                    damageType.text = "Дробящий";
                    break;
                case Weapon.DamageType.Piercing:
                    damageType.text = "Колющий";
                    break;

            }
        }
        else
        {
            ACController aCController = FindObjectOfType<ACController>();
            if (aCController != null)
            {
                aCController.shieldEquip = true;
                aCController.UploadArmorClass();
            }
        }
        currentWeapon = weapon;
        return hands;
    }

    public Weapon GetWeapon()
    {
        return currentWeapon;
    }

    public int GetHands()
    {
        return hands;
    }

    public void ReDamage()
    {
        int attack = CharacterData.GetModifier(0);
        int addDamage = 0;
        string damage = currentWeapon.dices + "к" + currentWeapon.hitDice;

        foreach (Weapon.Properties x in currentWeapon.properties)
        {
            switch (x)
            {
                case Weapon.Properties.Fencing:
                    attack = Mathf.Max(attack, CharacterData.GetModifier(1));
                    break;
                case Weapon.Properties.Universal:
                    damage += "(" + (currentWeapon.hitDice + 2) + ")";
                    break;

            }

        }
        hitDices.text = damage;

        if (GlobalStatus.archer && (currentWeapon.weaponType == Weapon.WeaponType.CommonDist || currentWeapon.weaponType == Weapon.WeaponType.WarDist))
            addDamage += 2;

        if (GlobalStatus.duelist && hands == 1)
        {
            HandsInventory handsInventory = GetComponentInParent<HandsInventory>();
            HandEquipment[] secondHands = handsInventory.GetComponentsInChildren<HandEquipment>();
            bool flag = true;
            foreach (HandEquipment x in secondHands)
                if (x.gameObject.transform != transform)
                    if (x.currentWeapon.weaponType != Weapon.WeaponType.Shield)
                        flag = false;
            if (flag)
                addDamage += 2;
        }

        if ((attack + addDamage) != 0)
        {
            if ((attack + addDamage) >= 0)
                hitDices.text += "+" + (attack + addDamage);
            else
                hitDices.text += (attack + addDamage);
        }
    }

    private void OnDestroy()
    {
        if (GlobalStatus.duelist)
        {
            HandsInventory handsInventory = GetComponentInParent<HandsInventory>();
            if (handsInventory == null) return;
            HandEquipment[] secondHands = handsInventory.GetComponentsInChildren<HandEquipment>();
            foreach (HandEquipment x in secondHands)
                x.ReDamage();
        }
        if (currentWeapon.weaponType == Weapon.WeaponType.Shield)
        {
            ACController aCController = FindObjectOfType<ACController>();
            if (aCController != null)
            {
                aCController.shieldEquip = false;
                aCController.UploadArmorClass();
            }
        }
    }
}
