using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] GameObject _loadCanvas;
    public static LoadSceneManager Instance;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Загружает новую сцену
    /// </summary>
    /// <param name="newScene">Имя новой сцены</param>
    public void LoadScene(string newScene)
    {
        if (newScene != null)
        {
            StartCoroutine(AsyncLoadScene(newScene));
        }
    }

    IEnumerator AsyncLoadScene(string newScene)
    {
        _loadCanvas.SetActive(true);
        //yield return new WaitForSeconds(5);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        _loadCanvas.SetActive(false);
    }
}
