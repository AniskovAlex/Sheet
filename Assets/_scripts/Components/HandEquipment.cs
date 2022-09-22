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
    Weapon currentWeapon;

    public int SetHand(Weapon weapon)
    {
        label.text = weapon.label;
        int attack = CharacterData.GetModifier(0);
        int dist = weapon.dist;
        int maxDist = weapon.maxDist;
        int hands = 1;
        string damage = weapon.dices + "к" + weapon.hitDice;
        foreach (Weapon.Properties x in weapon.properties)
        {
            switch (x)
            {
                case Weapon.Properties.Fencing:
                    attack = Mathf.Max(attack, CharacterData.GetModifier(1));
                    break;
                case Weapon.Properties.Reach:
                    dist += 5;
                    maxDist += 5;
                    break;
                case Weapon.Properties.TwoHanded:
                    hands = 2;
                    break;
                case Weapon.Properties.Universal:
                    damage += "(" + (weapon.hitDice + 2) + ")";
                    break;

            }

        }

        if (attack > 0)
            hitDices.text = weapon.dices + "к" + weapon.hitDice + "+" + attack;
        else
            hitDices.text = weapon.dices + "к" + weapon.hitDice + "" + attack;

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
        currentWeapon = weapon;
        return hands;
    }

    public Weapon GetWeapon()
    {
        return currentWeapon;
    }
}
