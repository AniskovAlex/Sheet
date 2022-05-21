using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon
{

    public enum DamageType
    {
        Slashing,
        Crushing,
        Piercing
    }

    public enum Properties
    {
        Ammo,
        TwoHanded,
        Reach,
        Distance,
        Light,
        Throwing,
        Special,
        Reload,
        Heavy,
        Universal,
        Fencing
    }

    public string label;
    public int dices;
    public int hitDice;
    public int dist;
    public int maxDist;
    public bool magic;
    public DamageType damageType;
    public Properties[] properties;

    public Weapon(string label,int dices,int hitDice ,int dist, int maxDist, bool magic,DamageType damageType, Properties[] properties)
    {
        this.label = label;
        this.dices = dices;
        this.hitDice = hitDice;
        this.dist = dist;
        this.maxDist = maxDist;
        this.magic = magic;
        this.damageType = damageType;
        this.properties = properties;
    }
}
