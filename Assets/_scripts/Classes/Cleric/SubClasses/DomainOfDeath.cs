using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainOfDeath : PlayerSubClass
{
    public DomainOfDeath()
    {
        id = 8;
        name = "Домен смерти";
        LoadAbilities("DomainOfDeath");
    }

    public override HashSet<Weapon.WeaponType> GetWeaponProficiency()
    {
        return new HashSet<Weapon.WeaponType>() { Weapon.WeaponType.WarMelee, Weapon.WeaponType.CommonMelee, Weapon.WeaponType.CommonDist, Weapon.WeaponType.WarDist };
    }
}
