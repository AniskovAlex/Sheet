using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalDouble : PlayerSubClass
{
    public MysticalDouble()
    {
        id = 3;
        name = "Мистический ловкач";
        LoadAbilities("MysticalDouble");
    }
    public override int GetMagic()
    {
        return 3;
    }
}
