using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopoutConfirm : MonoBehaviour
{
    [SerializeField] SelecterManager manager;
    CharacterTab buf;

    public void Show(CharacterTab name)
    {
        gameObject.SetActive(true);
        buf = name;
    }

    public void Cancle()
    {
        gameObject.SetActive(false);
    }

    public void Confirm()
    {
        manager.DeleteCharacter(buf);
        gameObject.SetActive(false);
        buf = null;
    }
}
