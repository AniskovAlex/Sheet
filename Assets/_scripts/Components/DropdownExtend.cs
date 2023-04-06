using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownExtend : MonoBehaviour
{
    public string currentValueText = "";
    //public int currentValueInt = 0;
    public Text discriptionText;

    public void Resize()
    {
        ContentSizer contentSizer;
        if (transform.parent != null && transform.parent.TryGetComponent<ContentSizer>(out contentSizer))
            contentSizer.Resize();
    }
}
