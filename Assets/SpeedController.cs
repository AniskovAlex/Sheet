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
        if (GlobalStatus.barbatianFastMove)
            add += 1;
        foreach (GameObject x in boxs)
        {
            Utilities.SetTextSign(CharacterData.GetSpeed() + add, x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>());
        }
    }
}
