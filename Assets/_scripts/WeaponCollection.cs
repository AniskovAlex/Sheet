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
        list.Add(new Weapon("арбалет, лёгкий", 1, 8, 80, 320, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reload
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("дротик", 1, 4, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance,
            Weapon.Properties.Fencing
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("короткий лук", 1, 6, 80, 320, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("праща", 1, 4, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("алебарда", 1, 10, 10, 10, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reach,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("боевая кирка", 1, 8, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("боевой молот", 1, 8, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Universal
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("боевой топор", 1, 8, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("глефа", 1, 10, 10, 10, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reach,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("двуручный меч", 2, 6, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("длинное копьё", 1, 12, 10, 10, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Reach,
            Weapon.Properties.Special
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("длинный меч", 1, 8, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Universal
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("кнут", 1, 4, 10, 10, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Reach,
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("короткий меч", 1, 6, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("молот", 2, 6, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("моргенштерн", 1, 8, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("пика", 1, 10, 10, 10, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reach,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("рапира", 1, 8, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("секира", 1, 12, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("скимитар", 1, 6, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("трезубец", 1, 6, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance,
            Weapon.Properties.Universal
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("арбалет, ручной", 1, 6, 30, 120, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.Light,
            Weapon.Properties.Reload
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("длинный лук", 1, 8, 150, 600, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("духовая трубка", 1, 1, 25, 100, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.Reload
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("сеть", 1, 0, 5, 15, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.Special
        }, Weapon.Type.WarDist));
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
