using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public enum Type
    {
        Abjuration,   // Ограждение
        Conjuration,  // Вызов
        Divination,   // Прорицание
        Enchantment,  // Очарование
        Evocation,    // Воплащение
        Illusion,     // Иллюзия
        Necromancy,   // Некромантия
        Transmutation // Преобразование
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
    public int time; // 1 - действие, 2 - реакция
    public string reactionDis;
    public int dist; // -1 - на себя, 0 - касание
    public List<Component> comp;
    public string materialDis;
    public int duration; // 1 - мгновенное, 2 - 1 раунд, 3 - 1 минута, 4 - 10 минут
    public List<int> classes;
    public List<(int, int)> subClasses;
    public string discription;
    public bool concentration;
    public bool ritual;
}
