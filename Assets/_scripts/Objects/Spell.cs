using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
    public enum Type
    {
        Abjuration,   // Ограждение     0
        Conjuration,  // Вызов          1
        Divination,   // Прорицание     2
        Enchantment,  // Очарование     3
        Evocation,    // Воплащение     4
        Illusion,     // Иллюзия        5
        Necromancy,   // Некромантия    6
        Transmutation // Преобразование 7
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
    public int time; // 1 - действие, 2 - реакция, 3 - 1 час, 4 - бонусное, 5 - 1 минута
                     // 6 - 10 мин
    public string reactionDis;
    public int dist; // -1 - на себя, 0 - касание, -2 - особая
    public string distDis; 
    public List<Component> comp;
    public string materialDis;
    public int duration; // 1 - мгновенное, 2 - 1 раунд, 3 - 1 минута, 
                         // 4 - 10 минут, 5 - 10 дней, 6 - 1 час, 7 - 24 часа, 8 - 8 часов
                         // 9 - пока не рассеется
    public List<int> classes; // 0 - бард, 3 - волшебник, 4 - друид, 5 - жрец, 6 - изобретатель, 
                              // 7 - колдун, 8 - паладин, 11 - следопыт, 12 - чародей
    public List<(int, int)> subClasses;
    public string discription;
    public bool concentration;
    public bool ritual;
}
