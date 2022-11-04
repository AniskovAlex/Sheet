using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public enum Type
    {
        Abjuration,   // ����������
        Conjuration,  // �����
        Divination,   // ����������
        Enchantment,  // ����������
        Evocation,    // ����������
        Illusion,     // �������
        Necromancy,   // �����������
        Transmutation // ��������������
    }
    public enum Component
    {
        V,
        S,
        M
    }
    
    public int id;
    public string name;
    public int level;
    public Type spellType;
    public int time; // 1 - ��������, 2 - �������
    public string reactionDis;
    public int dist;
    public List<Component> comp;
    public string materialDis;
    public int duration; // 1 - ����������, 2 - 1 �����
    public List<int> classes;
    public List<(int, int)> subClasses;
    public string discription;
    public bool concentration;
    public bool ritual;
}
