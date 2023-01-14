using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeBody : MonoBehaviour
{
    [SerializeField] float time = 3f;
    [SerializeField] Text notice;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

    public void Set(string text)
    {
        notice.text = text;
    }
}
