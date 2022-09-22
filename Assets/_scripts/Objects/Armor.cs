using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor: Item
{
    /*const string ACSaveName = "armorAC_";
    const string ACCapSaveName = "armorACCap_";
    const string StrSaveName = "armorStr_";
    const string StealthSaveName = "armorStealth_";
    const string TypeSaveName = "armorType_";*/

    public enum ArmorType
    {
        Light,
        Medium,
        Heavy,
        Shield
    }

    public int AC;
    public int ACCap;
    public int strReq;
    public bool stealthDis;
    public ArmorType armorType;

    /*public static Armor? LoadArmor(string label)
    {
        if (!PlayerPrefs.HasKey(ACSaveName + label))
            return null;
        int AC = PlayerPrefs.GetInt(ACSaveName + label);
        int ACCap = PlayerPrefs.GetInt(ACCapSaveName + label);
        int str = PlayerPrefs.GetInt(StrSaveName + label);
        bool stealth;
        if (PlayerPrefs.GetInt(StealthSaveName + label) == 1)
            stealth = true;
        else
            stealth = false;
        Type type = Type.Light + PlayerPrefs.GetInt(TypeSaveName + label);
        return new Armor(label, AC, ACCap, str, stealth, type);
    }

    public static void SaveArmor(Armor x)
    {
        PlayerPrefs.SetInt(ACSaveName + x.label, x.AC);
        PlayerPrefs.SetInt(ACCapSaveName + x.label, x.ACCap);
        PlayerPrefs.SetInt(StrSaveName + x.label, x.strReq);
        int buf = 0;
        if (x.stealthDis)
            buf = 1;
        PlayerPrefs.SetInt(StealthSaveName + x.label, buf);
        PlayerPrefs.SetInt(TypeSaveName + x.label, (int)x.type);
        PlayerPrefs.Save();

    }

    public static void DeleteArmor(string x)
    {
        PlayerPrefs.DeleteKey(ACSaveName + x);
        PlayerPrefs.DeleteKey(ACCapSaveName + x);
        PlayerPrefs.DeleteKey(StrSaveName + x);
        PlayerPrefs.DeleteKey(StealthSaveName + x);
        PlayerPrefs.DeleteKey(TypeSaveName + x);
        PlayerPrefs.Save();
    }*/

}
