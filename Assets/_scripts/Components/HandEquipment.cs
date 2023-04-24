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
            string damage = weapon.dices + "�" + weapon.hitDice;
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
                distance.text = dist + "��.";
            else
                distance.text = dist + "/" + maxDist;

            magick.isOn = weapon.magic;

            switch (weapon.damageType)
            {
                case Weapon.DamageType.Slashing:
                    damageType.text = "�������";
                    break;
                case Weapon.DamageType.Crushing:
                    damageType.text = "��������";
                    break;
                case Weapon.DamageType.Piercing:
                    damageType.text = "�������";
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

    public void SetFist()
    {
        int attack = CharacterData.GetModifier(0);
        label.text = "���������� �����";
        int dice = 0;
        string damage = Mathf.Clamp(1 + attack, 1, 999).ToString();
        if (GlobalStatus.monkWeapon)
        {
            attack = Mathf.Max(attack, CharacterData.GetModifier(1));
            dice = 4;
            int level = CharacterData.GetLevel(8);
            if (level >= 5) dice = 6;
            if (level >= 11) dice = 8;
            if (level >= 17) dice = 10;
            damage = "1�" + dice;
            if (attack > 0)
                damage += "+" + attack;
            if (attack < 0)
                damage += attack;
        }
        if (GlobalStatus.cockerel)
            dice = Mathf.Max(dice, 8);
        if (dice != 0)
        {
            damage = "1�" + dice;
            if (attack > 0)
                damage += "+" + attack;
            if (attack < 0)
                damage += attack;
        }
        hitDices.text = damage;
        if (attack > 0)
            attackBonus.text = "+" + (attack + CharacterData.GetProficiencyBonus());
        else
            attackBonus.text = (attack + CharacterData.GetProficiencyBonus()).ToString();
        distance.text = 5 + "��.";
        damageType.text = "��������";
        magick.isOn = false;
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
        string damage = currentWeapon.dices + "�" + currentWeapon.hitDice;

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
        if (currentWeapon != null && currentWeapon.weaponType == Weapon.WeaponType.Shield)
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
