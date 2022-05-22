using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WeaponCollection : ScriptableObject
{
    List<Weapon> list = new List<Weapon>();
    static WeaponCollection instance;

    void LoadCollection()
    {
        list.Add(new Weapon("цеп", 1, 8, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("арбалет, тяжёлый", 1, 10, 100, 400, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("боевой посох", 1, 6, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Universal
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("булава", 1, 6, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] { }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("дубинка", 1, 4, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Light
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("кинжал", 1, 4, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
               Weapon.Properties.Light,
               Weapon.Properties.Throwing,
               Weapon.Properties.Distance,
               Weapon.Properties.Fencing
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("копьё", 1, 6, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance,
            Weapon.Properties.Universal
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("лёгкий молот", 1, 4, 20, 60, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("метательное копьё", 1, 6, 30, 120, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("палица", 1, 8, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("ручной топор", 1, 6, 20, 60, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("серп", 1, 4, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Light
        }, Weapon.Type.CommonMelee));

    }

    public static WeaponCollection GetCollection()
    {
        if (!instance)
        {
            instance = Resources.Load("Weapon Collection") as WeaponCollection;
            instance.LoadCollection();
        }
        return instance;
    }

    public void ShowCollection()
    {
        foreach (Weapon x in list)
        {
            Debug.Log(x.label);
        }
    }
    public List<Weapon> GetList()
    {
        return list;
    }
}
