using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemCollection : ScriptableObject
{
    List<Item> list = new List<Item>();
    static ItemCollection instance;

    /*void LoadCollection()
    {
        list.Add(new Item("абак", 2, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("алхим. огонь", 50, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("мешок", 1, -1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("блок и лебёдка", 1, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("арбалетные болты", 1, -3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("иглы для трубки", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("снаряды для пращи", 4, -3, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("стрелы", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("бочка", 2, 70, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("бумага (один лист)", 2, 0, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("бурдюк", 2, 5, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("бутылка, стеклянная", 2, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("ведро", 5, 2, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("верёвка пеньковая", 1, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("верёвка, шёлковая", 10, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("весы, торговые", 5, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("воск", 5, 0, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("горшок, железный", 2, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("духи (флакон)", 5, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("замок", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("зелье лечения", 50, -1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("зеркало, стальное", 5, -1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("калтропы", 1, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("кандалы", 2, 6, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("кирка, горняцкая", 2, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("кислота (флакон)", 25, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("книга", 25, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("книга заклинаний", 50, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("колокольчик", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("колчан", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("кольцо-печатка", 5, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("комплект для лазания", 25, 12, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("комплект для рыбалки", 1, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("комплект целителя", 5, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("контейнер для болтов", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("контейнер для карт и свитков", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("корзина", 4, 2, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("кошель", 5, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("крюк-кошка", 2, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("кувшин или графин", 2, 4, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("лампа", 5, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("лестница (10 футов)", 1, 25, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("ломик", 2, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("лопата", 2, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("волшебная палочка", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("жезл", 10, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("кристалл", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("посох", 5, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("сфера", 20, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("масло (фляга)", 1, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("мел (1 кусочек)", 1, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("металлические шарики", 1, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("мешочек с компонентами", 25, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("молот, кузнечный", 2, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("молоток", 1, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("мыло", 2, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("одежда, дорожная", 2, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("одежда, костюм", 5, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("одежда, обычная", 5, 3, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("одежда, отличная", 15, 6, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("одеяло", 5, 3, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("охотничий капкан", 5, 25, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("палатка, двухместная", 2, 20, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("пергамент (один лист)", 1, 0, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("песочные часы", 25, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("писчее перо", 2, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("подзорная труба", 1000, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("противоядие (флакон)", 50, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("рационы (1 день)", 5, 2, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("рюкзак", 2, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("ряса", 1, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("свеча", 1, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("святая вода (фляга)", 25, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("амулет", 5, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("реликварий", 5, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("эмблема", 5, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("сигнальный свисток", 5, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("спальник", 1, 7, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("столовый набор", 2, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("сундук", 5, 25, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("таран, портативный", 4, 35, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("точильный камень", 1, 1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("трутница", 5, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("увеличительное стекло", 100, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("факел", 1, 1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("флакон", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("фляга или большая кружка", 2, 1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("веточка омелы", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("деревянный посох", 5, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("тисовая палочка", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("тотем", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("фонарь, закрытый", 5, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("фонарь, направленный", 10, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("цепь (10 футов)", 5, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("чернила", 10, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("шест (10 футов)", 5, 7, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("шипы, железные (10)", 1, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("шлямбур", 5, -2, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("яд, простой (флакон)", 100, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("стёганый", 5, 8, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("кожаный", 10, 10, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("проклёпанная кожа", 45, 13, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("шкурный", 10, 12, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("кольчужная рубаха", 50, 20, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("чешуйчатый", 50, 45, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("кираса", 400, 20, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("полулаты", 750, 40, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("колечный", 30, 40, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("кольчуга", 75, 55, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("наборный", 200, 60, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("латы", 1500, 65, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("щит", 10, 6, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("боевой посох", 2, 4, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("булава", 5, 4, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("дубинка", 1, 2, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("кинжал", 2, 1, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("копьё", 1, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("лёгкий молот", 2, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("метательное копьё", 5, 2, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("палица", 2, 10, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("ручной топор", 5, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("серп", 1, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("арбалет, лёгкий", 25, 5, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("дротик", 5, -2, Item.MType.copperCoin, Item.Type.weapon));
        list.Add(new Item("короткий лук", 25, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("праща", 1, 0, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("алебарда", 20, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("боевая кирка", 5, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("боевой молот", 15, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("боевой топор", 10, 4, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("глефа", 20, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("двуручный меч", 50, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("длинное копьё", 10, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("длинный меч", 15, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("кнут", 2, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("короткий меч", 10, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("молот", 10, 10, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("моргенштерн", 15, 4, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("пика ", 5, 18, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("рапира", 25, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("секира", 30, 7, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("скимитар", 25, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("цеп", 10, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("арбалет, ручной", 75, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("арбалет, тяжёлый", 50, 18, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("длинный лук", 50, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("духовая трубка", 10, 1, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("сеть", 1, 0, Item.MType.goldCoin, Item.Type.weapon));
    } */

    /*public static ItemCollection GetCollection()
    {
        if (!instance)
        {
            instance = Resources.Load("Item Collection") as ItemCollection;
            instance.LoadCollection();
        }
        return instance;
    }

    public void ShowCollection()
    {
        foreach(Item x in list)
        {
            Debug.Log(x.label + ", " + x.cost+" зм, " + x.weight+" фнт.");
        }
    }
    public List<Item> GetList()
    {
        return list;
    }*/
}
