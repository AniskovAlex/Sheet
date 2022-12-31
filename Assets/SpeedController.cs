using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    [SerializeField] List<GameObject> boxs;
    int add = 0;
    void Start()
    {
        int speed = CharacterData.GetSpeed();
        if (GlobalStatus.fastFeet)
            speed = 35;
        if (GlobalStatus.barbatianFastMove)
            add += 10;
        if (GlobalStatus.mobile)
            add += 10;
        if (GlobalStatus.monkSpeed)
        { 
            int level = CharacterData.GetLevel(8);
            if (level >= 2 && level <= 5)
                add += 10;
            if (level >= 6 && level <= 9)
                add += 15; 
            if (level >= 10 && level <= 13)
                add += 20;
            if (level >= 14 && level <= 17)
                add += 25;
            if (level >= 18)
                add += 30;
        }
        foreach (GameObject x in boxs)
        {
            x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = (speed + add).ToString();
        }
    }
}
