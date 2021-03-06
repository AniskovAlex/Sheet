using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weapon
{
    static string dicesSaveName = "weaponDices_";
    static string hitDiceSaveName = "weaponHitDice_";
    static string distSaveName = "weaponDist_";
    static string maxDistSaveName = "weaponMaxDist_";
    static string magicSaveName = "weaponMagicDice_";
    static string damageTypeSaveName = "weaponDamageType_";
    static string propertiesCountSaveName = "weaponPropertiesCount_";
    static string propertieSaveName = "weaponPropertie_";
    static string typeSaveName = "weaponType_";


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

    public enum Type
    {
        CommonMelee,
        CommonDist,
        WarMelee,
        WarDist,
        Shield

    }

    public string label;
    public int dices;
    public int hitDice;
    public int dist;
    public int maxDist;
    public bool magic;
    public DamageType damageType;
    public Properties[] properties;
    public Type type;

    public Weapon(string label, int dices, int hitDice, int dist, int maxDist, bool magic, DamageType damageType, Properties[] properties, Type type)
    {
        this.label = label;
        this.dices = dices;
        this.hitDice = hitDice;
        this.dist = dist;
        this.maxDist = maxDist;
        this.magic = magic;
        this.damageType = damageType;
        this.properties = properties;
        this.type = type;
    }

    public static Weapon? LoadWeapon(string label)
    {
        if (!PlayerPrefs.HasKey(dicesSaveName + label))
            return null;
        int dices = PlayerPrefs.GetInt(dicesSaveName + label);
        int hitDice = PlayerPrefs.GetInt(hitDiceSaveName + label);
        int dist = PlayerPrefs.GetInt(distSaveName + label);
        int maxDist = PlayerPrefs.GetInt(maxDistSaveName + label);
        bool magic;
        if (PlayerPrefs.GetInt(magicSaveName + label) == 1)
            magic = true;
        else
            magic = false;
        DamageType damageType = DamageType.Slashing + PlayerPrefs.GetInt(damageTypeSaveName + label);
        int propCount = PlayerPrefs.GetInt(propertiesCountSaveName + label);
        Properties[] properties = new Properties[propCount];
        for (int i = 0; i < propCount; i++)
        {
            properties[i] = Properties.Ammo + PlayerPrefs.GetInt(propertieSaveName + label + i);
        }
        Type type = Type.CommonMelee + PlayerPrefs.GetInt(typeSaveName + label);
        Weapon? newWeapon = new Weapon(label, dices, hitDice, dist, maxDist, magic, damageType, properties, type);
        return newWeapon;
    }

    public static void SaveWeapon(Weapon x)
    {
        PlayerPrefs.SetInt(dicesSaveName + x.label, x.dices);
        PlayerPrefs.SetInt(hitDiceSaveName + x.label, x.hitDice);
        PlayerPrefs.SetInt(distSaveName + x.label, x.dist);
        PlayerPrefs.SetInt(maxDistSaveName + x.label, x.maxDist);
        int buf = 0;
        if (x.magic)
            buf = 1;
        PlayerPrefs.SetInt(magicSaveName + x.label, buf);
        PlayerPrefs.SetInt(damageTypeSaveName + x.label, (int)x.damageType);
        PlayerPrefs.SetInt(propertiesCountSaveName + x.label, x.properties.Length);
        for (int i = 0; i < x.properties.Length; i++)
            PlayerPrefs.SetInt(propertieSaveName + x.label + i, (int)x.properties[i]);
        PlayerPrefs.SetInt(typeSaveName + x.label, (int)x.type);
        PlayerPrefs.Save();
    }

    public static void DeleteWeapon(string x)
    {
        PlayerPrefs.DeleteKey(dicesSaveName + x);
        PlayerPrefs.DeleteKey(hitDiceSaveName + x);
        PlayerPrefs.DeleteKey(distSaveName + x);
        PlayerPrefs.DeleteKey(maxDistSaveName + x);
        PlayerPrefs.DeleteKey(magicSaveName + x);
        PlayerPrefs.DeleteKey(damageTypeSaveName + x);
        int buf = PlayerPrefs.GetInt(propertiesCountSaveName + x);
        for (int i = 0; i < buf; i++)
            PlayerPrefs.DeleteKey(damageTypeSaveName + x + i);
        PlayerPrefs.DeleteKey(propertiesCountSaveName + x);
        PlayerPrefs.DeleteKey(typeSaveName + x);
        PlayerPrefs.Save();
    }
}
