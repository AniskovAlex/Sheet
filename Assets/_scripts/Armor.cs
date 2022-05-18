using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Armor
{
    public string label;
    public int AC;
    public int ACCap;
    public Armor(string label, int AC, int ACCap)
    {
        this.label = label;
        this.AC = AC;
        this.ACCap = ACCap;
    }
}
