using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class LoadInventoryManager : MonoBehaviour
{
    Item[] items;
    void Awake()
    {
        List<Item> listItems = new List<Item>();
        List<Weapon> listWeapons = new List<Weapon>();
        List<Armor> listArmors = new List<Armor>();
        string JSONItems = File.ReadAllText("Assets/Resources/items.json");
        string JSONWeapons = File.ReadAllText("Assets/Resources/weapons.json");
        string JSONArmors = File.ReadAllText("Assets/Resources/armors.json");
        /*wep.cost = 1;
        wep.damageType = Weapon.DamageType.Crushing;
        wep.dices = 1;
        wep.dist = 1;
        wep.hitDice = 1;
        wep.id = 2;
        wep.label = "ор";
        wep.magic = true;
        wep.maxDist = 1;
        wep.mType = Item.MType.silverCoin;
        wep.properties = new Weapon.Properties[] { Weapon.Properties.Distance, Weapon.Properties.Heavy };
        wep.weaponType = Weapon.WeaponType.WarDist;
        wep.weight = 1;
        string wee = JsonConvert.SerializeObject(wep);
        File.WriteAllText("Assets/Resources/weapons.txt", wee);*/
        /*Item item = new Item("абак", 2, 2, Item.MType.goldCoin, Item.Type.item);
        list.Add(item);
        list.Add(item);
        string test = JsonConvert.SerializeObject(list);
        File.WriteAllText("Assets/Resources/items.txt", test);*/
        listItems = JsonConvert.DeserializeObject<List<Item>>(JSONItems);
        listWeapons = JsonConvert.DeserializeObject<List<Weapon>>(JSONWeapons);
        listArmors = JsonConvert.DeserializeObject<List<Armor>>(JSONArmors);
        items = listItems.Concat(listWeapons).Concat(listArmors).ToArray();
    }

    public Item[] GetItems()
    {
        return items;
    }

}
