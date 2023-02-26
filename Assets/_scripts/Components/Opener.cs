using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] AnimationOpen anim;

    public void click()
    {
        if (anim == null) return;
        anim.OpenClose();
    }
}
