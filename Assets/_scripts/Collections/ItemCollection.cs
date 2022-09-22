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
        list.Add(new Item("����", 2, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����. �����", 50, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 1, -1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("���� � ������", 1, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("���������� �����", 1, -3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("���� ��� ������", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������� ��� �����", 4, -3, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("������", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 2, 70, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������ (���� ����)", 2, 0, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("������", 2, 5, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("�������, ����������", 2, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 5, 2, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("������ ���������", 1, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������, ��������", 10, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����, ��������", 5, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����", 5, 0, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("������, ��������", 2, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("���� (������)", 5, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����� �������", 50, -1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������, ��������", 5, -1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("��������", 1, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������", 2, 6, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����, ���������", 2, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������� (������)", 25, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 25, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����� ����������", 50, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����������", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������-�������", 5, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������� ��� �������", 25, 12, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������� ��� �������", 1, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������� ��������", 5, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("��������� ��� ������", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("��������� ��� ���� � �������", 1, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������", 4, 2, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("������", 5, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("����-�����", 2, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������ ��� ������", 2, 4, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("�����", 5, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("�������� (10 �����)", 1, 25, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("�����", 2, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������", 2, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("��������� �������", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����", 10, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("��������", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 5, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 20, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����� (�����)", 1, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("��� (1 �������)", 1, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("������������� ������", 1, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������� � ������������", 25, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����, ���������", 2, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������", 1, 3, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����", 2, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("������, ��������", 2, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������, ������", 5, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������, �������", 5, 3, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("������, ��������", 15, 6, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������", 5, 3, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("��������� ������", 5, 25, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������, �����������", 2, 20, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("��������� (���� ����)", 1, 0, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("�������� ����", 25, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������ ����", 2, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("��������� �����", 1000, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����������� (������)", 50, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������� (1 ����)", 5, 2, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("������", 2, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����", 1, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 1, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("������ ���� (�����)", 25, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������", 5, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����������", 5, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������", 5, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("���������� �������", 5, 0, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("��������", 1, 7, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������� �����", 2, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("������", 5, 25, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����, �����������", 4, 35, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("��������� ������", 1, 1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("��������", 5, 1, Item.MType.silverCoin, Item.Type.item));
        list.Add(new Item("�������������� ������", 100, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 1, 1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("������", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("����� ��� ������� ������", 2, 1, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("������� �����", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("���������� �����", 5, 4, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������� �������", 10, 1, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�����", 1, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������, ��������", 5, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("������, ������������", 10, 2, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("���� (10 �����)", 5, 10, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������", 10, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("���� (10 �����)", 5, 7, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("����, �������� (10)", 1, 5, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������", 5, -2, Item.MType.copperCoin, Item.Type.item));
        list.Add(new Item("��, ������� (������)", 100, 0, Item.MType.goldCoin, Item.Type.item));
        list.Add(new Item("�������", 5, 8, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("�������", 10, 10, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("����������� ����", 45, 13, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("�������", 10, 12, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("���������� ������", 50, 20, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("����������", 50, 45, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("������", 400, 20, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("��������", 750, 40, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("��������", 30, 40, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("��������", 75, 55, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("��������", 200, 60, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("����", 1500, 65, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("���", 10, 6, Item.MType.goldCoin, Item.Type.armor));
        list.Add(new Item("������ �����", 2, 4, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("������", 5, 4, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�������", 1, 2, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("������", 2, 1, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�����", 1, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("����� �����", 2, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("����������� �����", 5, 2, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("������", 2, 10, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("������ �����", 5, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("����", 1, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�������, �����", 25, 5, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������", 5, -2, Item.MType.copperCoin, Item.Type.weapon));
        list.Add(new Item("�������� ���", 25, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�����", 1, 0, Item.MType.silverCoin, Item.Type.weapon));
        list.Add(new Item("��������", 20, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������ �����", 5, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������ �����", 15, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������ �����", 10, 4, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�����", 20, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("��������� ���", 50, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������� �����", 10, 6, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������� ���", 15, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("����", 2, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�������� ���", 10, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�����", 10, 10, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�����������", 15, 4, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("���� ", 5, 18, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������", 25, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������", 30, 7, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("��������", 25, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("���", 10, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�������, ������", 75, 3, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("�������, ������", 50, 18, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������� ���", 50, 2, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("������� ������", 10, 1, Item.MType.goldCoin, Item.Type.weapon));
        list.Add(new Item("����", 1, 0, Item.MType.goldCoin, Item.Type.weapon));
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
            Debug.Log(x.label + ", " + x.cost+" ��, " + x.weight+" ���.");
        }
    }
    public List<Item> GetList()
    {
        return list;
    }*/
}
