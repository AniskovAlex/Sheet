using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataSaverAndLoader
{
    const string moneySaveName = "mon_";
    const string levelCountSaveName = "lvlCount_";
    const string levelSaveName = "lvl_";
    const string levelLabelSaveName = "lvlLabel_";
    const string classSubClassSaveName = "subClass_";
    const string charactersCountSaveName = "charactersCount_";
    const string charactersSaveName = "characters_";
    const string itemAmountSaveName = "itemA_";
    const string itemsCountSaveName = "itemsCount_";
    const string itemSaveName = "itemN_";
    const string itemSaveID = "itemID_";
    const string itemCostSaveName = "itemCost_";
    const string itemWeightSaveName = "itemW_";
    const string itemMTypeSaveName = "itemMT_";
    const string aCSaveName = "armorAC_";
    const string aCCapSaveName = "armorACCap_";
    const string strSaveName = "armorStr_";
    const string stealthSaveName = "armorStealth_";
    const string armorTypeSaveName = "armorType_";
    const string dicesSaveName = "weaponDices_";
    const string hitDiceSaveName = "weaponHitDice_";
    const string distSaveName = "weaponDist_";
    const string maxDistSaveName = "weaponMaxDist_";
    const string magicSaveName = "weaponMagicDice_";
    const string damageTypeSaveName = "weaponDamageType_";
    const string propertiesCountSaveName = "weaponPropertiesCount_";
    const string propertieSaveName = "weaponPropertie_";
    const string weaponTypeSaveName = "weaponType_";

    const string armorEquipCountSaveName = "armorECount_";
    const string armorEquipSaveName = "armorE_";
    const string weaponEquipSaveName = "weaponE_";
    const string weaponEquipCountSaveName = "weaponECount_";

    public static void SaveCharacter(string characterName)
    {
        if (PlayerPrefs.HasKey(charactersCountSaveName))
        {
            int count = PlayerPrefs.GetInt(charactersCountSaveName) + 1;
            PlayerPrefs.SetString(charactersSaveName + count, characterName);
            PlayerPrefs.SetInt(charactersCountSaveName, count);
        }
        else
        {
            PlayerPrefs.SetInt(charactersCountSaveName, 1);
            PlayerPrefs.SetString(charactersSaveName + 1, characterName);
        }
    }

    public static void SaveClass(string className)
    {
        if (className != "")
        {
            string characterName = CharacterCollection.GetName();
            int count = PlayerPrefs.GetInt(characterName + levelCountSaveName);
            for (int i = 0; i < count; i++)
            {
                if (PlayerPrefs.GetString(characterName + levelLabelSaveName + i) == className)
                {
                    int classLevel = PlayerPrefs.GetInt(characterName + levelSaveName + i);
                    PlayerPrefs.SetInt(characterName + levelSaveName + i, classLevel + 1);
                    return;
                }
            }
            PlayerPrefs.SetString(characterName + levelLabelSaveName + count, className);
            PlayerPrefs.SetInt(characterName + levelSaveName + count, 1);
            PlayerPrefs.SetInt(characterName + levelCountSaveName, count + 1);
            PlayerPrefs.Save();
        }
    }

    public static void SaveSubClass(PlayersClass playersClass)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetString(characterName + classSubClassSaveName + playersClass.name, playersClass.GetSubClass().GetName());
        PlayerPrefs.Save();
    }

    public static string LoadSubClass(PlayersClass playersClass)
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetString(characterName + classSubClassSaveName + playersClass.name);
    }

    public static void SaveMoney(List<int> money)
    {
        money.ForEach(g => PlayerPrefs.SetInt(CharacterCollection.GetName() + moneySaveName + money.IndexOf(g), g));
        PlayerPrefs.Save();
    }

    public static void SaveCustomList(string listName ,List<string> list)
    {
        list.ForEach(x => PlayerPrefs.SetString(CharacterCollection.GetName() + listName + "Auto_" + list.IndexOf(x), x));
        PlayerPrefs.SetInt(CharacterCollection.GetName() + listName + "CountAuto_", list.Count);
        PlayerPrefs.Save();
    }

    public static List<string> LoadCustom(string listName)
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + listName + "CountAuto_");
        List<string> list = new List<string>();
        for (int i = 0; i < count; i++)
            list.Add(PlayerPrefs.GetString(characterName + listName + "Auto_" + i));
        return list;
    }

    public static void SaveAmountItem(string label, int amount)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + itemAmountSaveName + label, amount);
        PlayerPrefs.Save();
    }

    public static void SaveAmountItem(int id, int amount)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + itemAmountSaveName + id, amount);
        PlayerPrefs.Save();
    }

    public static bool SaveNewItem(Item item, int itemsCount, int amount)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + itemSaveID + itemsCount, item.id);
        if (item.id == -1)
        {
            if (PlayerPrefs.HasKey(characterName + itemMTypeSaveName + item.label))
                return false;
            PlayerPrefs.SetInt(characterName + itemAmountSaveName + item.label, amount);
            PlayerPrefs.SetString(characterName + itemSaveName + itemsCount, item.label);
            PlayerPrefs.SetInt(characterName + itemMTypeSaveName + item.label, (int)item.mType);
            PlayerPrefs.SetInt(characterName + itemCostSaveName + item.label, item.cost);
            PlayerPrefs.SetInt(characterName + itemWeightSaveName + item.label, item.weight);
            if (item is Armor)
            {
                Armor armor = item as Armor;
                PlayerPrefs.SetInt(characterName + aCSaveName + item.label, armor.AC);
                PlayerPrefs.SetInt(characterName + aCCapSaveName + item.label, armor.ACCap);
                PlayerPrefs.SetInt(characterName + strSaveName + item.label, armor.strReq);
                int buf = 0;
                if (armor.stealthDis)
                    buf = 1;
                PlayerPrefs.SetInt(characterName + stealthSaveName + item.label, buf);
                PlayerPrefs.SetInt(characterName + armorTypeSaveName + item.label, (int)armor.armorType);
            }
            if (item is Weapon)
            {
                Weapon weapon = item as Weapon;
                PlayerPrefs.SetInt(characterName + dicesSaveName + item.label, weapon.dices);
                PlayerPrefs.SetInt(characterName + hitDiceSaveName + item.label, weapon.hitDice);
                PlayerPrefs.SetInt(characterName + distSaveName + item.label, weapon.dist);
                PlayerPrefs.SetInt(characterName + maxDistSaveName + item.label, weapon.maxDist);
                int buf = 0;
                if (weapon.magic)
                    buf = 1;
                PlayerPrefs.SetInt(characterName + magicSaveName + item.label, buf);
                PlayerPrefs.SetInt(characterName + damageTypeSaveName + item.label, (int)weapon.damageType);
                PlayerPrefs.SetInt(characterName + propertiesCountSaveName + item.label, weapon.properties.Length);
                for (int i = 0; i < weapon.properties.Length; i++)
                    PlayerPrefs.SetInt(characterName + propertieSaveName + item.label + i, (int)weapon.properties[i]);
                PlayerPrefs.SetInt(characterName + weaponTypeSaveName + item.label, (int)weapon.weaponType);
            }
        }
        else
            PlayerPrefs.SetInt(characterName + itemAmountSaveName + item.id, amount);
        itemsCount++;
        PlayerPrefs.SetInt(characterName + itemsCountSaveName, itemsCount);
        PlayerPrefs.Save();
        return true;
    }

    public static Item LoadSavedItem(string label)
    {
        string characterName = CharacterCollection.GetName();
        Item item = new Item();
        item.mType = Item.MType.goldCoin + PlayerPrefs.GetInt(characterName + itemMTypeSaveName + label);
        item.label = label;
        item.id = -1;
        item.cost = PlayerPrefs.GetInt(characterName + itemCostSaveName + label);
        item.weight = PlayerPrefs.GetInt(characterName + itemWeightSaveName + label);
        if (PlayerPrefs.HasKey(characterName + dicesSaveName + item.label))
        {
            Weapon weapon = new Weapon();
            weapon.id = item.id;
            weapon.label = item.label;
            weapon.mType = item.mType;
            weapon.cost = item.cost;
            weapon.weight = item.weight;
            weapon.dices = PlayerPrefs.GetInt(characterName + dicesSaveName + label);
            weapon.hitDice = PlayerPrefs.GetInt(characterName + hitDiceSaveName + label);
            weapon.dist = PlayerPrefs.GetInt(characterName + distSaveName + label);
            weapon.maxDist = PlayerPrefs.GetInt(characterName + maxDistSaveName + label);
            int buf = PlayerPrefs.GetInt(characterName + magicSaveName + label);
            if (buf == 0)
                weapon.magic = false;
            else
                weapon.magic = true;
            weapon.damageType = Weapon.DamageType.Slashing + PlayerPrefs.GetInt(characterName + weaponTypeSaveName + label);
            List<Weapon.Properties> list = new List<Weapon.Properties>();
            int propCount = PlayerPrefs.GetInt(characterName + propertiesCountSaveName + label);
            for (int i = 0; i < propCount; i++)
            {
                list.Add(Weapon.Properties.Ammo + PlayerPrefs.GetInt(characterName + propertieSaveName + label + i));
            }
            weapon.properties = list.ToArray();
            return weapon;
        }
        if (PlayerPrefs.HasKey(characterName + aCSaveName + item.label))
        {
            Armor armor = new Armor();
            armor.id = item.id;
            armor.label = item.label;
            armor.mType = item.mType;
            armor.cost = item.cost;
            armor.weight = item.weight;
            armor.AC = PlayerPrefs.GetInt(characterName + aCSaveName + label);
            armor.ACCap = PlayerPrefs.GetInt(characterName + aCCapSaveName + label);
            armor.strReq = PlayerPrefs.GetInt(characterName + strSaveName + label);
            int buf = PlayerPrefs.GetInt(characterName + stealthSaveName + label);
            if (buf == 0)
                armor.stealthDis = false;
            else
                armor.stealthDis = true;
            armor.armorType = Armor.ArmorType.Light + PlayerPrefs.GetInt(characterName + armorTypeSaveName + label);
            return armor;
        }
        return item;
    }

    public static (int, string)[] LoadEquitedWeapon()
    {
        string characterName = CharacterCollection.GetName();
        int equipCount = PlayerPrefs.GetInt(characterName + weaponEquipCountSaveName);
        List<(int, string)> equiptedList = new List<(int, string)>();
        for (int i = 0; i < equipCount; i++)
        {
            int id = PlayerPrefs.GetInt(characterName + weaponEquipSaveName + i);
            if (id > 0)
            {
                equiptedList.Add((id, null));
            }
            else
            {
                string label = PlayerPrefs.GetString(characterName + weaponEquipSaveName + i);
                equiptedList.Add((-1, label));
            }
        }
        return equiptedList.ToArray();
    }

    public static (int, string)[] LoadEquitedArmor()
    {
        string characterName = CharacterCollection.GetName();
        int equipCount = PlayerPrefs.GetInt(characterName + armorEquipCountSaveName);
        List<(int, string)> equiptedList = new List<(int, string)>();
        for (int i = 0; i < equipCount; i++)
        {
            int id = PlayerPrefs.GetInt(characterName + armorEquipSaveName + i);
            if (id > 0)
            {
                equiptedList.Add((id, null));
            }
            else
            {
                string label = PlayerPrefs.GetString(characterName + armorEquipSaveName + i);
                equiptedList.Add((-1, label));
            }
        }
        return equiptedList.ToArray();
    }

    public static void WeaponEquipmentChanged(Weapon weapon, bool toggle)
    {
        string characterName = CharacterCollection.GetName();
        int equipCount = PlayerPrefs.GetInt(characterName + weaponEquipCountSaveName);
        if (toggle)
        {

            if (weapon.id != -1)
                PlayerPrefs.SetInt(characterName + weaponEquipSaveName + equipCount, weapon.id);
            else
                PlayerPrefs.SetString(characterName + weaponEquipSaveName + equipCount, weapon.label);
            equipCount++;
            PlayerPrefs.SetInt(characterName + weaponEquipCountSaveName, equipCount);
        }
        else
        {
            if (weapon.id != -1)
            {
                for (int i = 0; i < equipCount; i++)
                {
                    int equip = PlayerPrefs.GetInt(characterName + weaponEquipSaveName + i);

                    if (weapon.id == equip)
                    {
                        PlayerPrefs.DeleteKey(characterName + weaponEquipSaveName + i);
                        if (i != equipCount - 1)
                        {
                            PlayerPrefs.DeleteKey(characterName + weaponEquipSaveName + i);
                            int buf = PlayerPrefs.GetInt(characterName + weaponEquipSaveName + (equipCount - 1));

                            if (buf == 0)
                            {
                                string buf2 = PlayerPrefs.GetString(characterName + weaponEquipSaveName + (equipCount - 1));
                                PlayerPrefs.SetString(characterName + weaponEquipSaveName + i, buf2);
                            }
                            else
                                PlayerPrefs.SetInt(characterName + weaponEquipSaveName + i, buf);


                        }
                        break;
                    }

                }
            }
            else
            {
                for (int i = 0; i < equipCount; i++)
                {
                    string equip = PlayerPrefs.GetString(characterName + weaponEquipSaveName + i);

                    if (weapon.label == equip)
                    {
                        PlayerPrefs.DeleteKey(characterName + weaponEquipSaveName + i);
                        if (i != equipCount - 1)
                        {
                            PlayerPrefs.DeleteKey(characterName + weaponEquipSaveName + i);
                            int buf = PlayerPrefs.GetInt(characterName + weaponEquipSaveName + (equipCount - 1));

                            if (buf == 0)
                            {
                                string buf2 = PlayerPrefs.GetString(characterName + weaponEquipSaveName + (equipCount - 1));
                                PlayerPrefs.SetString(characterName + weaponEquipSaveName + i, buf2);
                            }
                            else
                                PlayerPrefs.SetInt(characterName + weaponEquipSaveName + i, buf);


                        }
                        break;
                    }

                }
            }
            equipCount--;
            PlayerPrefs.SetInt(characterName + weaponEquipCountSaveName, equipCount);
        }
        PlayerPrefs.Save();
    }

    public static void ArmorEquipmentChanged(Armor armor, bool toggle)
    {
        string characterName = CharacterCollection.GetName();
        int equipCount = PlayerPrefs.GetInt(characterName + armorEquipCountSaveName);
        if (toggle)
        {

            if (armor.id != -1)
                PlayerPrefs.SetInt(characterName + armorEquipSaveName + equipCount, armor.id);
            else
                PlayerPrefs.SetString(characterName + armorEquipSaveName + equipCount, armor.label);
            equipCount++;
            PlayerPrefs.SetInt(characterName + armorEquipCountSaveName, equipCount);
        }
        else
        {
            if (armor.id != -1)
            {
                for (int i = 0; i < equipCount; i++)
                {
                    int equip = PlayerPrefs.GetInt(characterName + armorEquipSaveName + i);

                    if (armor.id == equip)
                    {
                        PlayerPrefs.DeleteKey(characterName + armorEquipSaveName + i);
                        if (i != equipCount - 1)
                        {
                            PlayerPrefs.DeleteKey(characterName + armorEquipSaveName + i);
                            int buf = PlayerPrefs.GetInt(characterName + armorEquipSaveName + (equipCount - 1));

                            if (buf == 0)
                            {
                                string buf2 = PlayerPrefs.GetString(characterName + armorEquipSaveName + (equipCount - 1));
                                PlayerPrefs.SetString(characterName + armorEquipSaveName + i, buf2);
                            }
                            else
                                PlayerPrefs.SetInt(characterName + armorEquipSaveName + i, buf);


                        }
                        break;
                    }

                }
            }
            else
            {
                for (int i = 0; i < equipCount; i++)
                {
                    string equip = PlayerPrefs.GetString(characterName + armorEquipSaveName + i);

                    if (armor.label == equip)
                    {
                        PlayerPrefs.DeleteKey(characterName + armorEquipSaveName + i);
                        if (i != equipCount - 1)
                        {
                            PlayerPrefs.DeleteKey(characterName + armorEquipSaveName + i);
                            int buf = PlayerPrefs.GetInt(characterName + armorEquipSaveName + (equipCount - 1));

                            if (buf == 0)
                            {
                                string buf2 = PlayerPrefs.GetString(characterName + armorEquipSaveName + (equipCount - 1));
                                PlayerPrefs.SetString(characterName + armorEquipSaveName + i, buf2);
                            }
                            else
                                PlayerPrefs.SetInt(characterName + armorEquipSaveName + i, buf);


                        }
                        break;
                    }

                }
            }
            equipCount--;
            PlayerPrefs.SetInt(characterName + armorEquipCountSaveName, equipCount);
        }
        PlayerPrefs.Save();
    }

    public static void RemoveItem(Item item)
    {
        string characterName = CharacterCollection.GetName();
        int itemsCount = PlayerPrefs.GetInt(characterName + itemsCountSaveName);
        bool founded = false;
        for (int i = 0; i < itemsCount; i++)
        {
            int id = PlayerPrefs.GetInt(characterName + itemSaveID + i);
            if (id == item.id)
            {
                if (item.id == -1)
                {
                    if (PlayerPrefs.GetString(characterName + itemSaveName + i) == item.label)
                    {
                        PlayerPrefs.DeleteKey(characterName + itemAmountSaveName + item.label);
                        PlayerPrefs.DeleteKey(characterName + itemSaveName + i);
                        PlayerPrefs.DeleteKey(characterName + itemMTypeSaveName + item.label);
                        PlayerPrefs.DeleteKey(characterName + itemCostSaveName + item.label);
                        PlayerPrefs.DeleteKey(characterName + itemWeightSaveName + item.label);
                        if (item is Armor)
                        {
                            PlayerPrefs.DeleteKey(characterName + aCSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + aCCapSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + strSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + stealthSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + armorTypeSaveName + item.label);
                        }
                        if (item is Weapon)
                        {
                            Weapon weapon = item as Weapon;
                            PlayerPrefs.DeleteKey(characterName + dicesSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + hitDiceSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + distSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + maxDistSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + magicSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + damageTypeSaveName + item.label);
                            PlayerPrefs.DeleteKey(characterName + propertiesCountSaveName + item.label);
                            for (int j = 0; j < weapon.properties.Length; j++)
                                PlayerPrefs.DeleteKey(characterName + propertieSaveName + item.label + j);
                            PlayerPrefs.DeleteKey(characterName + weaponTypeSaveName + item.label);
                        }
                    }
                    else
                        continue;
                }
                if (i == itemsCount - 1)
                    PlayerPrefs.DeleteKey(characterName + itemSaveID + i);
                else
                {
                    int lastId = PlayerPrefs.GetInt(characterName + itemSaveID + (itemsCount - 1));
                    PlayerPrefs.SetInt(characterName + itemSaveID + i, lastId);
                    PlayerPrefs.DeleteKey(characterName + itemSaveID + (itemsCount - 1));
                    if (lastId == -1)
                    {
                        PlayerPrefs.SetString(characterName + itemSaveName + i, PlayerPrefs.GetString(characterName + itemSaveName + (itemsCount - 1)));
                        PlayerPrefs.DeleteKey(characterName + itemSaveName + (itemsCount - 1));
                    }

                }
                founded = true;
                break;
            }
        }
        if (founded)
        {
            PlayerPrefs.SetInt(characterName + itemsCountSaveName, itemsCount - 1);
            PlayerPrefs.DeleteKey(characterName + itemAmountSaveName + item.id);
            PlayerPrefs.Save();
        }
    }
}
