using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopoutController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] AnimationPopout animationPopout;

    public void Init()
    {
        if (animationPopout != null)
            animationPopout.Init();
    }

    bool wait = false;

    private void OnEnable()
    {
        GlobalStatus.popoutMenu = true;
    }

    public void SetPopout(List<GameObject> list)
    {
        gameObject.SetActive(true);
        if (animationPopout != null)
            animationPopout.OpenClose(true);
        foreach (GameObject x in list)
            Instantiate(x, panel.transform);
    }

    private void OnDisable()
    {
        GlobalStatus.popoutMenu = false;
        for (int i = 0; i < panel.transform.childCount; i++)
            Destroy(panel.transform.GetChild(i).gameObject);
    }

    public void Cancle()
    {
        wait = true;
        if (animationPopout != null)
            animationPopout.OpenClose(false);
    }

    private void Update()
    {
        if (!wait) return;
        if (animationPopout == null)
        {
            gameObject.SetActive(false);
            wait = false;
            return;
        }
        if (animationPopout.isEnd())
        {
            gameObject.SetActive(false);
            wait = false;
        }
    }
}
