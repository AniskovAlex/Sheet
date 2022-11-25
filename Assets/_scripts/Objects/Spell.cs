using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public enum Type
    {
        Abjuration,   // ����������     0
        Conjuration,  // �����          1
        Divination,   // ����������     2
        Enchantment,  // ����������     3
        Evocation,    // ����������     4
        Illusion,     // �������        5
        Necromancy,   // �����������    6
        Transmutation // �������������� 7
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
    public int time; // 1 - ��������, 2 - �������, 3 - 1 ���, 4 - ��������, 5 - 1 ������
                     // 6 - 10 ���
    public string reactionDis;
    public int dist; // -1 - �� ����, 0 - �������, -2 - ������
    public string distDis; 
    public List<Component> comp;
    public string materialDis;
    public int duration; // 1 - ����������, 2 - 1 �����, 3 - 1 ������, 
                         // 4 - 10 �����, 5 - 10 ����, 6 - 1 ���, 7 - 24 ����, 8 - 8 �����
                         // 9 - ���� �� ���������
    public List<int> classes; // 0 - ����, 3 - ���������, 4 - �����, 5 - ����, 6 - ������������, 
                              // 7 - ������, 8 - �������, 11 - ��������, 12 - �������
    public List<(int, int)> subClasses;
    public string discription;
    public bool concentration;
    public bool ritual;
}
