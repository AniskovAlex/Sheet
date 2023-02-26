using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Manager : MonoBehaviour
{
    [SerializeField] PopoutController popout;
    private void Start()
    {
        popout.Init();
        GlobalStatus.load = false;
    }

    public void LoadScene(string sceneName)
    {
        LoadSceneManager.Instance.LoadScene(sceneName);
    }
}
