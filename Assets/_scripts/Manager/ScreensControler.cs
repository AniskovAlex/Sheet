using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class ScreensControler : MonoBehaviour
{
    public Camera cam;
    public CinemachineVirtualCamera vCam;
    public Canvas can;
    public List<GameObject> screens;
    public List<Button> buttons;
    public Text head;
    public float velocity = 0.1f;
    float currentTime;
    float? offSet = null;
    float offSetStart = 0;
    float scaleToCanvas;
    CinemachineFramingTransposer transposer;
    void Start()
    {
        transposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        Vector2 a = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 b = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        Vector2 c = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        float ab = b.x - a.x;
        float ac = c.y - a.y;
        scaleToCanvas = can.GetComponent<RectTransform>().lossyScale.x;
        ab /= scaleToCanvas;
        ac /= scaleToCanvas;
        int i = 0;
        foreach (GameObject x in screens)
        {
            x.GetComponent<RectTransform>().sizeDelta = new Vector2(ab, ac - 300);
            x.GetComponent<RectTransform>().localPosition = new Vector2(i * ab, 0);
            i++;
        }
        Debug.Log("");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (offSet == null)
            {
                offSetStart = Input.mousePosition.x;
                offSet = 0;
                currentTime = 0f;
            }
        if (Input.GetMouseButton(0))
            if (offSet != null)
            {
                currentTime += Time.deltaTime;
                offSet = offSetStart - Input.mousePosition.x;
                transposer.m_ScreenX = (-(float)offSet / Screen.width + 0.5f);
            }
        if (Input.GetMouseButtonUp(0))
        {
            offSet = null;
            float currentVelocity = (transposer.m_ScreenX - 0.5f) / currentTime;
            Debug.Log(currentVelocity);
            if (transposer.m_ScreenX < 0.3f || -velocity > currentVelocity)
            {
                int index = screens.FindIndex(x => x == vCam.Follow.gameObject) + 1;
                if (index < screens.Count)
                {
                    ChangePanel(screens[index]);
                    DeactivateButton(buttons[index]);
                }
            }
            if (transposer.m_ScreenX > 0.7f || velocity < currentVelocity)
            {
                int index = screens.FindIndex(x => x == vCam.Follow.gameObject) - 1;
                if (index >= 0)
                {
                    ChangePanel(screens[index]);
                    DeactivateButton(buttons[index]);
                }
            }
            transposer.m_ScreenX = 0.5f;
        }
        //Touch touch;
    }
    public void ChangePanel(GameObject panel)
    {
        switch (panel.name)
        {
            case "Inventory":
                head.text = "Инвентарь";
                break;
            case "Main":
                head.text = "Главный Экран";
                break;
            case "Skills":
                head.text = "Навыки";
                break;
        }
        vCam.Follow = panel.transform;
    }

    public void DeactivateButton(Button button)
    {
        foreach (Button x in buttons)
        {

            if (x == button)
            {
                x.interactable = false;
            }
            else
            {
                x.interactable = true;
            }
        }
    }
}
