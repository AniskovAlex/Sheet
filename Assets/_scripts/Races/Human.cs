using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Human : Race
{
    public Human()
    {
        id = -1;
        name = "Человек";
        LoadAbilities("Human");
    }

}
