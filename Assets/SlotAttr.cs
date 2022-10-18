using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotAttr : MonoBehaviour
{
    [SerializeField] public DraggingStat stat = null;
    [SerializeField] public int attr = -1;

    public int GetAttr()
    {
        if (stat == null) return -1;

        return stat.GetAttr();
    }
}
