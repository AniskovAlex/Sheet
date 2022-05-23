using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Armor
{
    public enum Type
    {
        Light,
        Medium,
        Heavy,
        Shield
    }

    public string label;
    public int AC;
    public int ACCap;
    public int strReq;
    public bool stealthDis;
    public Type type;
    public Armor(string label, int AC, int ACCap,int strReq, bool stealthDis, Type type)
    {
        this.label = label;
        this.AC = AC;
        this.ACCap = ACCap;
        this.strReq = strReq;
        this.stealthDis = stealthDis;
        this.type = type;
    }
}
