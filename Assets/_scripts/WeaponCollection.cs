using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WeaponCollection : ScriptableObject
{
    List<Weapon> list = new List<Weapon>();
    static WeaponCollection instance;

    void LoadCollection()
    {
        list.Add(new Weapon("���", 1, 8, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("�������, ������", 1, 10, 100, 400, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("������ �����", 1, 6, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Universal
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("������", 1, 6, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] { }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("�������", 1, 4, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Light
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("������", 1, 4, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
               Weapon.Properties.Light,
               Weapon.Properties.Throwing,
               Weapon.Properties.Distance,
               Weapon.Properties.Fencing
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("�����", 1, 6, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance,
            Weapon.Properties.Universal
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("����� �����", 1, 4, 20, 60, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("����������� �����", 1, 6, 30, 120, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("������", 1, 8, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("������ �����", 1, 6, 20, 60, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("����", 1, 4, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Light
        }, Weapon.Type.CommonMelee));
        list.Add(new Weapon("�������, �����", 1, 8, 80, 320, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reload
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("������", 1, 4, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance,
            Weapon.Properties.Fencing
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("�������� ���", 1, 6, 80, 320, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("�����", 1, 4, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance
        }, Weapon.Type.CommonDist));
        list.Add(new Weapon("��������", 1, 10, 10, 10, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reach,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("������ �����", 1, 8, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("������ �����", 1, 8, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.Universal
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("������ �����", 1, 8, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("�����", 1, 10, 10, 10, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reach,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("��������� ���", 2, 6, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("������� �����", 1, 12, 10, 10, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Reach,
            Weapon.Properties.Special
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("������� ���", 1, 8, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Universal
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("����", 1, 4, 10, 10, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Reach,
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("�������� ���", 1, 6, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("�����", 2, 6, 5, 5, false, Weapon.DamageType.Crushing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("�����������", 1, 8, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] { }, Weapon.Type.WarMelee));
        list.Add(new Weapon("����", 1, 10, 10, 10, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Reach,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("������", 1, 8, 5, 5, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("������", 1, 12, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("��������", 1, 6, 5, 5, false, Weapon.DamageType.Slashing, new Weapon.Properties[] {
            Weapon.Properties.Light,
            Weapon.Properties.Fencing
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("��������", 1, 6, 20, 60, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Throwing,
            Weapon.Properties.Distance,
            Weapon.Properties.Universal
        }, Weapon.Type.WarMelee));
        list.Add(new Weapon("�������, ������", 1, 6, 30, 120, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.Light,
            Weapon.Properties.Reload
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("������� ���", 1, 8, 150, 600, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.TwoHanded,
            Weapon.Properties.Heavy
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("������� ������", 1, 1, 25, 100, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
            Weapon.Properties.Ammo,
            Weapon.Properties.Distance,
            Weapon.Properties.Reload
        }, Weapon.Type.WarDist));
        list.Add(new Weapon("����", 1, 0, 5, 15, false, Weapon.DamageType.Piercing, new Weapon.Properties[] {
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
