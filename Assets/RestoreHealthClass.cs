using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestoreHealthClass : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Text className;
    [SerializeField] Text HealthDice;
    [SerializeField] Text rest;
    int restInt;
    public void SetPanel(int count, PlayersClass playersClass, Action<int, PlayersClass> action)
    {
        restInt = count;
        button.onClick.AddListener(delegate
        {
            action(restInt, playersClass);
            UpdatePanel();
        });
        className.text = playersClass.name;
        HealthDice.text = "ê" + playersClass.healthDice;
        rest.text = count.ToString();
    }

    void UpdatePanel()
    {
        if (restInt <= 0) return;
        restInt--;
        rest.text = restInt.ToString();
    }
}
