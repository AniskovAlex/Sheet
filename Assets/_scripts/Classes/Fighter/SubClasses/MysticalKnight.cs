using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticalKnight : PlayerSubClass
{
    public MysticalKnight()
    {
        id = 2;
        name = "����������� ������";
        LoadAbilities("MasterOfMartialArt");
    }
    public override int GetMagic()
    {
        return 3;
    }
}
