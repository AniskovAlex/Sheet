using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataSaverAndLoader
{
    const string featSaveName = "@feat_";
    const string featCountSaveName = "@featCount_";

    const string moneySaveName = "@mon_";
    const string skillSaveName = "@skill_";
    const string noteSaveName = "@note_";
    const string noteHeadSaveName = "@noteHead_";
    const string noteCountSaveName = "@noteCount_";

    const string cellAmountSaveName = "@cellAmount_";
    const string consumAmountSaveName = "@consumAmount_";
    const string consumAmountNameSaveName = "@consumAmountName_";
    const string consumAmountNameCountSaveName = "@consumAmountNameCount_";

    const string spellKnewSaveName = "@spellKnew_";
    const string spellClassKnewSaveName = "@spellClassKnew_";
    const string spellClassKnewCountSaveName = "@spellClassKnewCount_";
    const string spellKnewCountSaveName = "@spellKnewCount_";

    const string spellMasterSaveName = "@spellMaster_";
    const string spellMasterCountSaveName = "@spellMasterCount_";

    const string spellPrepareSaveName = "@spellPrepare_";
    const string spellClassPrepareSaveName = "@spellClassPrepare_";
    const string spellClassPrepareCountSaveName = "@spellClassPrepareCount_";
    const string spellPrepareCountSaveName = "@spellPrepareCount_";

    const string alignmentSaveName = "@alignment_";
    const string natureSaveName = "@nature_";
    const string idealSaveName = "@ideal_";
    const string attachmentSaveName = "@attachment_";
    const string weaknessSaveName = "@weakness_";
    const string backstoryExtendSaveName = "@backstoryExtend_";

    const string attrSaveName = "@atr_";

    const string maxHealthSaveName = "@maxHP_";
    const string healthSaveName = "@HP_";
    const string tempHealthSaveName = "@THP_";
    const string addHealthSaveName = "@ATHP_";

    const string raceSaveName = "@race_";
    const string raceSubRaceSaveName = "@subRace_";
    const string backstorySaveName = "@backstory_";

    const string instrumentsSaveName = "@instruments_";
    const string instrumentsCountSaveName = "@instrumentsCount_";

    const string instrumentsCompSaveName = "@instrumentsComp_";
    const string instrumentsCompCountSaveName = "@instrumentsCompCount_";

    const string languageSaveName = "@language_";
    const string languageCountSaveName = "@languageCount_";

    const string saveThrowSaveName = "@save_";

    const string levelCountSaveName = "@lvlCount_";
    const string levelSaveName = "@lvl_";
    const string levelLabelSaveName = "@lvlLabel_";
    const string classSubClassSaveName = "@subClass_";

    const string weaponProficiencySaveName = "@weaponProficiency_";
    const string weaponProficiencyCountSaveName = "@weaponProficiencyCount_";
    const string bladeProficiencySaveName = "@bladeProficiency_";
    const string bladeProficiencyCountSaveName = "@bladeProficiencyCount_";
    const string armorProficiencySaveName = "@armorProficiency_";
    const string armorProficiencyCountSaveName = "@armorProficiencyCount_";

    const string charactersCountSaveName = "@charactersCount_";
    const string charactersSaveName = "@characters_";

    const string itemAmountSaveName = "@itemA_";
    const string itemsCountSaveName = "@itemsCount_";
    const string itemSaveName = "@itemN_";
    const string itemSaveID = "@itemID_";
    const string itemCostSaveName = "@itemCost_";
    const string itemWeightSaveName = "@itemW_";
    const string itemMTypeSaveName = "@itemMT_";

    const string aCSaveName = "@armorAC_";
    const string aCCapSaveName = "@armorACCap_";
    const string strSaveName = "@armorStr_";
    const string stealthSaveName = "@armorStealth_";
    const string armorTypeSaveName = "@armorType_";

    const string dicesSaveName = "@weaponDices_";
    const string hitDiceSaveName = "@weaponHitDice_";
    const string distSaveName = "@weaponDist_";
    const string maxDistSaveName = "@weaponMaxDist_";
    const string magicSaveName = "@weaponMagicDice_";
    const string damageTypeSaveName = "@weaponDamageType_";
    const string propertiesCountSaveName = "@weaponPropertiesCount_";
    const string propertieSaveName = "@weaponPropertie_";
    const string weaponTypeSaveName = "@weaponType_";
    const string bladeTypeSaveName = "@weaponBladeType_";

    const string armorEquipCountSaveName = "@armorECount_";
    const string armorEquipSaveName = "@armorE_";
    const string weaponEquipSaveName = "@weaponE_";
    const string weaponEquipCountSaveName = "@weaponECount_";

    const string customListCount = "@customListCount_";
    const string customList = "@customList_";

    public static int[] LoadCellsAmount()
    {
        string characterName = CharacterCollection.GetName();
        int[] arr = new int[9];
        for (int i = 0; i < 9; i++)
            arr[i] = PlayerPrefs.GetInt(characterName + cellAmountSaveName + (i + 1));
        return arr;
    }

    public static void SaveCellAmount(int level, int amount)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + cellAmountSaveName + level, amount);
        PlayerPrefs.Save();
    }

    public static int LoadConsumAmount(string name)
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetInt(characterName + consumAmountSaveName + name);
    }

    public static void SaveConsumAmount(string name, int amount)
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + consumAmountNameCountSaveName);
        bool flag = true;
        for (int i = 0; i < count; i++)
        {
            if (PlayerPrefs.GetString(characterName + consumAmountNameSaveName + i) == name)
            {
                flag = false;
                break;
            }
        }
        if (flag)
        {
            PlayerPrefs.SetString(characterName + consumAmountNameSaveName + count, name);
            PlayerPrefs.SetInt(characterName + consumAmountNameCountSaveName, count + 1);
        }
        PlayerPrefs.SetInt(characterName + consumAmountSaveName + name, amount);
        PlayerPrefs.Save();
    }

    public static void SaveNote(string head, string note)
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + noteCountSaveName);
        PlayerPrefs.SetString(characterName + noteHeadSaveName + count, head);
        PlayerPrefs.SetString(characterName + noteSaveName + count, note);
        PlayerPrefs.SetInt(characterName + noteCountSaveName, count + 1);
    }

    public static List<(string, string)> LoadNotes()
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + noteCountSaveName);
        List<(string, string)> list = new List<(string, string)>();
        for (int i = 0; i < count; i++)
        {
            list.Add(
                (
                PlayerPrefs.GetString(characterName + noteHeadSaveName + i),
                PlayerPrefs.GetString(characterName + noteSaveName + i)
                ));
        }
        return list;
    }

    public static void DeleteNotes(int index)
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + noteCountSaveName);
        for (int i = index; i < count - 1; i++)
        {
            PlayerPrefs.SetString(characterName + noteHeadSaveName + i, PlayerPrefs.GetString(characterName + noteHeadSaveName + (i + 1)));
            PlayerPrefs.SetString(characterName + noteSaveName + i, PlayerPrefs.GetString(characterName + noteSaveName + (i + 1)));
        }
        PlayerPrefs.DeleteKey(characterName + noteHeadSaveName + (count - 1));
        PlayerPrefs.DeleteKey(characterName + noteSaveName + (count - 1));
        PlayerPrefs.SetInt(characterName + noteCountSaveName, count - 1);
    }

    public static void DeleteNotes()
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + noteCountSaveName);
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.DeleteKey(characterName + noteHeadSaveName + i);
            PlayerPrefs.DeleteKey(characterName + noteSaveName + i);
        }
        PlayerPrefs.DeleteKey(characterName + noteCountSaveName);
    }

    public static void SaveMaxHealth(int maxHealth)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + maxHealthSaveName, maxHealth);
        PlayerPrefs.Save();
    }

    public static void SaveHealth(int health)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + healthSaveName, health);
        PlayerPrefs.Save();
    }

    public static void SaveTempHealth(int tempHealth)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + tempHealthSaveName, tempHealth);
        PlayerPrefs.Save();
    }

    public static void SaveAddHealth(int health)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + addHealthSaveName, health);
        PlayerPrefs.Save();
    }

    public static int LoadAddHealth()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetInt(characterName + addHealthSaveName);
    }

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

    public static void SaveClass(int id)
    {
        if (id != -1)
        {
            string characterName = CharacterCollection.GetName();
            int count = PlayerPrefs.GetInt(characterName + levelCountSaveName);
            for (int i = 0; i < count; i++)
            {
                if (PlayerPrefs.GetInt(characterName + levelLabelSaveName + i) == id)
                {
                    int classLevel = PlayerPrefs.GetInt(characterName + levelSaveName + i);
                    PlayerPrefs.SetInt(characterName + levelSaveName + i, classLevel + 1);
                    return;
                }
            }
            PlayerPrefs.SetInt(characterName + levelLabelSaveName + count, id);
            PlayerPrefs.SetInt(characterName + levelSaveName + count, 1);
            PlayerPrefs.SetInt(characterName + levelCountSaveName, count + 1);
            PlayerPrefs.Save();
        }
    }

    public static void SaveSubClass(PlayersClass playersClass)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + classSubClassSaveName + playersClass.id, playersClass.GetSubClass().id);
        PlayerPrefs.Save();
    }

    public static void SaveSubRace(Race playersRace)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + raceSubRaceSaveName + playersRace.id, playersRace.GetSubRace().id);
        PlayerPrefs.Save();
    }

    public static int LoadSubClass(PlayersClass playersClass)
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetInt(characterName + classSubClassSaveName + playersClass.id);
    }

    public static int LoadSubRace(Race playersRace)
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetInt(characterName + raceSubRaceSaveName + playersRace.id);
    }

    public static void SaveAttributes(int[] attr)
    {
        string characterName = CharacterCollection.GetName();
        for (int i = 0; i < attr.Length; i++)
            PlayerPrefs.SetInt(characterName + attrSaveName + i, attr[i]);
    }

    public static void SaveBladeProficiency(HashSet<Weapon.BladeType> list)
    {
        string characterName = CharacterCollection.GetName();
        if (PlayerPrefs.HasKey(characterName + bladeProficiencyCountSaveName))
            list.UnionWith(LoadBladeProfiency());
        int i = 0;
        foreach (Weapon.BladeType x in list)
        {
            PlayerPrefs.SetInt(characterName + bladeProficiencySaveName + i, (int)x);
            i++;
        }
        PlayerPrefs.SetInt(characterName + bladeProficiencyCountSaveName, list.Count);
        PlayerPrefs.Save();
    }

    public static HashSet<Weapon.BladeType> LoadBladeProfiency()
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + bladeProficiencyCountSaveName);
        HashSet<Weapon.BladeType> list = new HashSet<Weapon.BladeType>();
        for (int i = 0; i < count; i++)
        {
            list.Add(Weapon.BladeType.WarStaff + PlayerPrefs.GetInt(characterName + bladeProficiencySaveName + i));
        }
        return list;
    }

    public static void SaveWeaponProficiency(HashSet<Weapon.WeaponType> list)
    {
        string characterName = CharacterCollection.GetName();
        if (PlayerPrefs.HasKey(characterName + weaponProficiencyCountSaveName))
            list.UnionWith(LoadWeaponProfiency());
        int i = 0;
        foreach (Weapon.WeaponType x in list)
        {
            PlayerPrefs.SetInt(characterName + weaponProficiencySaveName + i, (int)x);
            i++;
        }
        PlayerPrefs.SetInt(characterName + weaponProficiencyCountSaveName, list.Count);
        PlayerPrefs.Save();
    }

    public static HashSet<Weapon.WeaponType> LoadWeaponProfiency()
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + weaponProficiencyCountSaveName);
        HashSet<Weapon.WeaponType> list = new HashSet<Weapon.WeaponType>();
        for (int i = 0; i < count; i++)
        {
            list.Add(Weapon.WeaponType.CommonMelee + PlayerPrefs.GetInt(characterName + weaponProficiencySaveName + i));
        }
        return list;
    }
    public static void SaveArmorProficiency(HashSet<Armor.ArmorType> list)
    {
        string characterName = CharacterCollection.GetName();
        if (PlayerPrefs.HasKey(characterName + armorProficiencyCountSaveName))
            list.UnionWith(LoadArmorProfiency());
        int i = 0;
        foreach (Armor.ArmorType x in list)
        {
            PlayerPrefs.SetInt(characterName + armorProficiencySaveName + i, (int)x);
            i++;
        }
        PlayerPrefs.SetInt(characterName + armorProficiencyCountSaveName, list.Count);
        PlayerPrefs.Save();
    }

    public static HashSet<Armor.ArmorType> LoadArmorProfiency()
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + armorProficiencyCountSaveName);
        HashSet<Armor.ArmorType> list = new HashSet<Armor.ArmorType>();
        for (int i = 0; i < count; i++)
        {
            list.Add(Armor.ArmorType.Light + PlayerPrefs.GetInt(characterName + armorProficiencySaveName + i));
        }
        return list;
    }

    public static void SaveFeats(List<Feat> list)
    {
        string characterName = CharacterCollection.GetName();
        int i = 0;
        foreach (Feat x in list)
        {
            PlayerPrefs.SetInt(characterName + featSaveName + i, x.id);
            i++;
        }
        PlayerPrefs.SetInt(characterName + featCountSaveName, i);
    }

    public static List<int> LoadFeats()
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + featCountSaveName);
        List<int> list = new List<int>();
        for (int i = 0; i < count; i++)
        {
            list.Add(PlayerPrefs.GetInt(characterName + featSaveName + i));
        }
        return list;
    }

    public static void SaveMoney(List<int> money)
    {
        money.ForEach(g => PlayerPrefs.SetInt(CharacterCollection.GetName() + moneySaveName + money.IndexOf(g), g));
        PlayerPrefs.Save();
    }

    public static void SaveCustomList(string listName, List<int> list)
    {
        string characterName = CharacterCollection.GetName();
        list.ForEach(x => PlayerPrefs.SetInt(characterName + listName + "@Auto_" + list.IndexOf(x), x));
        PlayerPrefs.SetInt(characterName + listName + "@CountAuto_", list.Count);
        int count = PlayerPrefs.GetInt(characterName + customListCount);
        for (int i = 0; i < count; i++)
            if (PlayerPrefs.GetString(characterName + customList + i) == listName)
            {
                PlayerPrefs.Save();
                return;
            }
        PlayerPrefs.SetString(characterName + customList + count, listName);
        PlayerPrefs.SetInt(characterName + customListCount, count + 1);
        PlayerPrefs.Save();
    }

    public static List<int> LoadCustom(string listName)
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + listName + "@CountAuto_");
        List<int> list = new List<int>();
        for (int i = 0; i < count; i++)
            list.Add(PlayerPrefs.GetInt(characterName + listName + "@Auto_" + i));
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

    public static void SaveAddSpellKnew(List<(int, HashSet<int>)> list)
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + spellClassKnewCountSaveName);
        foreach ((int, HashSet<int>) x in list)
        {
            int buf = 0;
            bool flag = false;
            for (int k = 0; k < count; k++)
            {
                buf = PlayerPrefs.GetInt(characterName + spellClassKnewSaveName + k);
                if (buf == x.Item1)
                {
                    flag = true;
                    buf = k;
                }
            }
            int j;
            if (flag)
                j = PlayerPrefs.GetInt(characterName + spellKnewCountSaveName + buf);
            else
                j = 0;
            foreach (int y in x.Item2)
            {
                PlayerPrefs.SetInt(characterName + spellKnewSaveName + x.Item1 + j, y);
                j++;
            }
            if (flag)
            {
                PlayerPrefs.SetInt(characterName + spellKnewCountSaveName + buf, j);
                PlayerPrefs.SetInt(characterName + spellClassKnewSaveName + buf, x.Item1);
            }
            else
            {
                PlayerPrefs.SetInt(characterName + spellKnewCountSaveName + count, j);
                PlayerPrefs.SetInt(characterName + spellClassKnewSaveName + count, x.Item1);
                count++;
            }
        }
        PlayerPrefs.SetInt(characterName + spellClassKnewCountSaveName, count);
        PlayerPrefs.Save();
    }

    public static void DeleteSpellKnew(int classId, int spellId)
    {
        string characterName = CharacterCollection.GetName();
        int count = PlayerPrefs.GetInt(characterName + spellClassKnewCountSaveName);
        int i;
        for (i = 0; i < count; i++)
            if (PlayerPrefs.GetInt(characterName + spellClassKnewSaveName + i) == classId)
                break;
        int countSpell = PlayerPrefs.GetInt(characterName + spellKnewCountSaveName + i);
        for (int j = 0; j < countSpell; j++)
            if (PlayerPrefs.GetInt(characterName + spellKnewSaveName + classId + j) == spellId)
            {
                for (int k = j; k < countSpell - 1; k++)
                {
                    PlayerPrefs.SetInt(characterName + spellKnewSaveName + classId + k, PlayerPrefs.GetInt(characterName + spellKnewSaveName + classId + (k + 1)));
                }
                PlayerPrefs.DeleteKey(characterName + spellKnewSaveName + classId + countSpell);
                countSpell--;
                PlayerPrefs.SetInt(characterName + spellKnewCountSaveName + i, countSpell);
                break;
            }
        PlayerPrefs.Save();

    }

    public static void SaveSpellMaster(HashSet<int> list)
    {
        string characterName = CharacterCollection.GetName();
        List<int> bufList = new List<int>(list);
        int count = list.Count;
        int i = 0;
        foreach (int x in bufList)
        {
            PlayerPrefs.SetInt(characterName + spellMasterSaveName + i, bufList[i]);
            i++;
        }
        if (count != 0)
            PlayerPrefs.SetInt(characterName + spellMasterCountSaveName, count);
        PlayerPrefs.Save();
    }

    public static void SaveSpellPrepared(List<(int, HashSet<int>)> list)
    {
        string characterName = CharacterCollection.GetName();
        int count = list.Count;
        int i = 0;
        foreach ((int, HashSet<int>) x in list)
        {
            int j = 0;
            foreach (int y in x.Item2)
            {
                PlayerPrefs.SetInt(characterName + spellPrepareSaveName + x.Item1 + j, y);
                j++;
            }
            PlayerPrefs.SetInt(characterName + spellPrepareCountSaveName + i, j);
            PlayerPrefs.SetInt(characterName + spellClassPrepareSaveName + i, x.Item1);
            i++;
        }
        PlayerPrefs.SetInt(characterName + spellClassPrepareCountSaveName, count);
        PlayerPrefs.Save();
    }

    public static List<(int, HashSet<int>)> LoadSpellKnew()
    {
        string characterName = CharacterCollection.GetName();
        int classCount = PlayerPrefs.GetInt(characterName + spellClassKnewCountSaveName);
        List<(int, HashSet<int>)> list = new List<(int, HashSet<int>)>();
        for (int i = 0; i < classCount; i++)
        {
            int count = PlayerPrefs.GetInt(characterName + spellKnewCountSaveName + i);
            int classId = PlayerPrefs.GetInt(characterName + spellClassKnewSaveName + i);
            list.Add((classId, new HashSet<int>()));
            for (int j = 0; j < count; j++)
                list[i].Item2.Add(PlayerPrefs.GetInt(characterName + spellKnewSaveName + classId + j));
        }
        return list;
    }

    public static HashSet<int> LoadSpellMaster()
    {
        string characterName = CharacterCollection.GetName();
        int classCount = PlayerPrefs.GetInt(characterName + spellMasterCountSaveName);
        HashSet<int> list = new HashSet<int>();
        for (int i = 0; i < classCount; i++)
        {
            list.Add(PlayerPrefs.GetInt(characterName + spellMasterSaveName + i));
        }
        return list;
    }

    public static List<(int, HashSet<int>)> LoadSpellPrepared()
    {
        string characterName = CharacterCollection.GetName();
        int classCount = PlayerPrefs.GetInt(characterName + spellClassPrepareCountSaveName);
        List<(int, HashSet<int>)> list = new List<(int, HashSet<int>)>();
        for (int i = 0; i < classCount; i++)
        {
            int count = PlayerPrefs.GetInt(characterName + spellPrepareCountSaveName + i);
            int classId = PlayerPrefs.GetInt(characterName + spellClassPrepareSaveName + i);
            list.Add((classId, new HashSet<int>()));
            for (int j = 0; j < count; j++)
                list[i].Item2.Add(PlayerPrefs.GetInt(characterName + spellPrepareSaveName + classId + j));
        }
        return list;
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
                PlayerPrefs.SetInt(characterName + bladeTypeSaveName + item.label, (int)weapon.bladeType);
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
        return LoadSavedItemLogic(label, characterName);
    }

    public static Item LoadSavedItem(string label, string characterName)
    {
        return LoadSavedItemLogic(label, characterName);
    }

    static Item LoadSavedItemLogic(string label, string characterName)
    {
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
            weapon.bladeType = Weapon.BladeType.WarStaff + PlayerPrefs.GetInt(characterName + bladeTypeSaveName + label);
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
                            int buf = PlayerPrefs.GetInt(characterName + weaponEquipSaveName + (equipCount - 1));

                            if (buf == -1)
                            {
                                string buf2 = PlayerPrefs.GetString(characterName + weaponEquipSaveName + (equipCount - 1));
                                PlayerPrefs.SetString(characterName + weaponEquipSaveName + i, buf2);
                            }
                            else
                                PlayerPrefs.SetInt(characterName + weaponEquipSaveName + i, buf);
                            PlayerPrefs.DeleteKey(characterName + weaponEquipSaveName + (equipCount - 1));


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
                            int buf = PlayerPrefs.GetInt(characterName + weaponEquipSaveName + (equipCount - 1));

                            if (buf == -1)
                            {
                                string buf2 = PlayerPrefs.GetString(characterName + weaponEquipSaveName + (equipCount - 1));
                                PlayerPrefs.SetString(characterName + weaponEquipSaveName + i, buf2);
                            }
                            else
                                PlayerPrefs.SetInt(characterName + weaponEquipSaveName + i, buf);
                            PlayerPrefs.DeleteKey(characterName + weaponEquipSaveName + (equipCount - 1));


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
        RemoveItemLogic(item, characterName);
    }

    public static void RemoveItem(Item item, string characterName)
    {
        RemoveItemLogic(item, characterName);
    }

    static void RemoveItemLogic(Item item, string characterName)
    {
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
                            PlayerPrefs.DeleteKey(characterName + bladeTypeSaveName + item.label);
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


    public static void SaveInstruments(HashSet<string> list)
    {
        string characterName = CharacterCollection.GetName();
        int i = 0;
        foreach (string x in list)
        {
            PlayerPrefs.SetString(characterName + instrumentsSaveName + i, x);
            i++;
        }
        PlayerPrefs.SetInt(characterName + instrumentsCountSaveName, list.Count);
    }

    public static HashSet<string> LoadInstruments()
    {
        string characterName = CharacterCollection.GetName();
        HashSet<string> list = new HashSet<string>();
        int count = PlayerPrefs.GetInt(characterName + instrumentsCountSaveName);
        for (int i = 0; i < count; i++)
            list.Add(PlayerPrefs.GetString(characterName + instrumentsSaveName + i));
        return list;
    }

    public static void SaveInstrumentsComp(HashSet<string> list)
    {
        string characterName = CharacterCollection.GetName();
        int i = 0;
        foreach (string x in list)
        {
            PlayerPrefs.SetString(characterName + instrumentsCompSaveName + i, x);
            i++;
        }
        PlayerPrefs.SetInt(characterName + instrumentsCompCountSaveName, list.Count);
    }

    public static HashSet<string> LoadInstrumentsComp()
    {
        string characterName = CharacterCollection.GetName();
        HashSet<string> list = new HashSet<string>();
        int count = PlayerPrefs.GetInt(characterName + instrumentsCompCountSaveName);
        for (int i = 0; i < count; i++)
            list.Add(PlayerPrefs.GetString(characterName + instrumentsCompSaveName + i));
        return list;
    }

    public static void SaveLanguage(HashSet<string> list)
    {
        string characterName = CharacterCollection.GetName();
        int i = 0;
        foreach (string x in list)
        {
            PlayerPrefs.SetString(characterName + languageSaveName + i, x);
            i++;
        }
        PlayerPrefs.SetInt(characterName + languageCountSaveName, list.Count);
    }

    public static HashSet<string> LoadLanguage()
    {
        string characterName = CharacterCollection.GetName();
        HashSet<string> list = new HashSet<string>();
        int count = PlayerPrefs.GetInt(characterName + languageCountSaveName);
        for (int i = 0; i < count; i++)
            list.Add(PlayerPrefs.GetString(characterName + languageSaveName + i));
        return list;
    }

    public static void SaveSaveThrows(HashSet<int> list)
    {
        string characterName = CharacterCollection.GetName();
        foreach (int x in list)
        {
            PlayerPrefs.SetInt(characterName + saveThrowSaveName + x, 1);
        }
    }

    public static void SaveAlignment(int value)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + alignmentSaveName, value);
    }

    public static int LoadAlignment()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetInt(characterName + alignmentSaveName);
    }

    public static void SaveNature(string value)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetString(characterName + natureSaveName, value);
    }

    public static string LoadNature()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetString(characterName + natureSaveName);
    }
    public static void SaveIdeal(string value)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetString(characterName + idealSaveName, value);
    }

    public static string LoadIdeal()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetString(characterName + idealSaveName);
    }

    public static void SaveAttachment(string value)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetString(characterName + attachmentSaveName, value);
    }

    public static string LoadAttachment()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetString(characterName + attachmentSaveName);
    }
    public static void SaveWeakness(string value)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetString(characterName + weaknessSaveName, value);
    }

    public static string LoadWeakness()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetString(characterName + weaknessSaveName);
    }

    public static void SaveBackstoryExtend(string value)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetString(characterName + backstoryExtendSaveName, value);
    }
    public static string LoadBackstoryExtend()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetString(characterName + backstoryExtendSaveName);
    }

    public static void SaveRace(int id)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + raceSaveName, id);
    }

    public static int LoadRace()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetInt(characterName + raceSaveName);
    }

    public static void SaveBackstory(int id)
    {
        string characterName = CharacterCollection.GetName();
        PlayerPrefs.SetInt(characterName + backstorySaveName, id);
    }

    public static int LoadBackstory()
    {
        string characterName = CharacterCollection.GetName();
        return PlayerPrefs.GetInt(characterName + backstorySaveName);
    }
    public static void DeleteCharacter(string name)
    {
        int count = PlayerPrefs.GetInt(name + levelCountSaveName);
        if (count == 0)
            return;
        for (int i = 0; i < count; i++)
        {
            int classId = PlayerPrefs.GetInt(name + levelLabelSaveName + i);
            PlayerPrefs.DeleteKey(name + classSubClassSaveName + classId);
            PlayerPrefs.DeleteKey(name + levelLabelSaveName + i);
            PlayerPrefs.DeleteKey(name + levelSaveName + i);
        }

        string buf = "";
        count = PlayerPrefs.GetInt(name + consumAmountNameCountSaveName);
        for (int i = 0; i < count; i++)
        {
            buf = PlayerPrefs.GetString(name + consumAmountNameSaveName + i);
            PlayerPrefs.DeleteKey(name + consumAmountNameSaveName + i);
            PlayerPrefs.DeleteKey(name + consumAmountSaveName + buf);
        }
        PlayerPrefs.DeleteKey(name + consumAmountNameCountSaveName);

        count = PlayerPrefs.GetInt(name + spellMasterCountSaveName);
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.DeleteKey(name + spellMasterSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + spellMasterCountSaveName);

        count = PlayerPrefs.GetInt(name + featCountSaveName);
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.DeleteKey(name + featSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + featCountSaveName);

        count = PlayerPrefs.GetInt(name + noteCountSaveName);
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.DeleteKey(name + noteHeadSaveName + i);
            PlayerPrefs.DeleteKey(name + noteSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + noteCountSaveName);

        for (int i = 1; i < 10; i++)
            PlayerPrefs.DeleteKey(name + cellAmountSaveName + i);

        count = PlayerPrefs.GetInt(name + spellClassKnewCountSaveName);
        for (int i = 0; i < count; i++)
        {
            int countCount = PlayerPrefs.GetInt(name + spellKnewCountSaveName + i);
            int classId = PlayerPrefs.GetInt(name + spellClassKnewSaveName + i);
            for (int j = 0; j < countCount; j++)
                PlayerPrefs.DeleteKey(name + spellKnewSaveName + classId + j);
            PlayerPrefs.DeleteKey(name + spellClassKnewSaveName + i);
            PlayerPrefs.DeleteKey(name + spellKnewCountSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + spellClassKnewCountSaveName);


        count = PlayerPrefs.GetInt(name + spellClassPrepareCountSaveName);
        for (int i = 0; i < count; i++)
        {
            int countCount = PlayerPrefs.GetInt(name + spellPrepareCountSaveName + i);
            int classId = PlayerPrefs.GetInt(name + spellClassPrepareSaveName + i);
            for (int j = 0; j < countCount; j++)
                PlayerPrefs.DeleteKey(name + spellPrepareSaveName + classId + j);
            PlayerPrefs.DeleteKey(name + spellPrepareCountSaveName + i);
            PlayerPrefs.DeleteKey(name + spellClassPrepareSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + spellClassPrepareCountSaveName);

        int raceId = PlayerPrefs.GetInt(name + raceSaveName);
        PlayerPrefs.DeleteKey(name + raceSubRaceSaveName + raceId);
        PlayerPrefs.DeleteKey(name + raceSaveName);

        PlayerPrefs.DeleteKey(name + levelCountSaveName);
        PlayerPrefs.DeleteKey(name + healthSaveName);
        PlayerPrefs.DeleteKey(name + maxHealthSaveName);
        PlayerPrefs.DeleteKey(name + tempHealthSaveName);
        PlayerPrefs.DeleteKey(name + addHealthSaveName);

        for (int i = 0; i < 3; i++)
            PlayerPrefs.DeleteKey(name + moneySaveName + i);
        int itemsCount = PlayerPrefs.GetInt(name + itemsCountSaveName);
        for (int i = itemsCount - 1; i >= 0; i--)
        {
            int id = PlayerPrefs.GetInt(name + itemSaveID + i);
            string label = PlayerPrefs.GetString(name + itemSaveName + i);
            if ((id == -1) && PlayerPrefs.HasKey(name + itemCostSaveName + label))
            {
                RemoveItem(LoadSavedItem(label, name), name);
            }
            else
            {
                PlayerPrefs.DeleteKey(name + itemSaveID + i);
                PlayerPrefs.DeleteKey(name + itemAmountSaveName + id);
                PlayerPrefs.SetInt(name + itemsCountSaveName, PlayerPrefs.GetInt(name + itemsCountSaveName) - 1);
            }
        }
        PlayerPrefs.DeleteKey(name + itemsCountSaveName);

        int equipCount = PlayerPrefs.GetInt(name + weaponEquipCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + weaponEquipSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + weaponEquipCountSaveName);

        equipCount = PlayerPrefs.GetInt(name + armorEquipCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + armorEquipSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + armorEquipCountSaveName);

        equipCount = PlayerPrefs.GetInt(name + weaponProficiencyCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + weaponProficiencySaveName + i);
        }
        PlayerPrefs.DeleteKey(name + weaponProficiencyCountSaveName);

        equipCount = PlayerPrefs.GetInt(name + bladeProficiencyCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + bladeProficiencySaveName + i);
        }
        PlayerPrefs.DeleteKey(name + bladeProficiencyCountSaveName);

        equipCount = PlayerPrefs.GetInt(name + armorProficiencyCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + armorProficiencySaveName + i);
        }
        PlayerPrefs.DeleteKey(name + armorProficiencyCountSaveName);

        equipCount = PlayerPrefs.GetInt(name + customListCount);
        for (int i = 0; i < equipCount; i++)
        {
            count = PlayerPrefs.GetInt(name + PlayerPrefs.GetString(name + customList + i) + "@CountAuto_");
            for (int j = 0; j < count; j++)
            {
                PlayerPrefs.DeleteKey(name + PlayerPrefs.GetString(name + customList + i) + "@Auto_" + j);
            }
            PlayerPrefs.DeleteKey(name + PlayerPrefs.GetString(name + customList + i) + "@CountAuto_");
            PlayerPrefs.DeleteKey(name + customList + i);
        }
        PlayerPrefs.DeleteKey(name + customListCount);

        count = PlayerPrefs.GetInt(charactersCountSaveName);
        bool flag = false;
        for (int i = 1; i <= count; i++)
        {
            buf = PlayerPrefs.GetString(charactersSaveName + i);
            if (buf == name || flag)
            {
                PlayerPrefs.SetString(charactersSaveName + i, PlayerPrefs.GetString(charactersSaveName + (i + 1)));
                flag = true;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            PlayerPrefs.DeleteKey(name + attrSaveName + i);
            PlayerPrefs.DeleteKey(name + saveThrowSaveName + i);
        }

        for (int i = 0; i < 18; i++)
        {
            PlayerPrefs.DeleteKey(name + skillSaveName + i);
        }

        PlayerPrefs.DeleteKey(name + natureSaveName);
        PlayerPrefs.DeleteKey(name + weaknessSaveName);
        PlayerPrefs.DeleteKey(name + idealSaveName);
        PlayerPrefs.DeleteKey(name + attachmentSaveName);

        PlayerPrefs.DeleteKey(name + alignmentSaveName);

        PlayerPrefs.DeleteKey(name + backstorySaveName);
        PlayerPrefs.DeleteKey(name + backstoryExtendSaveName);

        equipCount = PlayerPrefs.GetInt(name + instrumentsCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + instrumentsSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + instrumentsCountSaveName);

        equipCount = PlayerPrefs.GetInt(name + instrumentsCompCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + instrumentsCompSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + instrumentsCompCountSaveName);

        equipCount = PlayerPrefs.GetInt(name + languageCountSaveName);
        for (int i = 0; i < equipCount; i++)
        {
            PlayerPrefs.DeleteKey(name + languageSaveName + i);
        }
        PlayerPrefs.DeleteKey(name + languageCountSaveName);

        PlayerPrefs.DeleteKey(charactersSaveName + count);
        PlayerPrefs.SetInt(charactersCountSaveName, count - 1);
        PlayerPrefs.Save();

    }
}
