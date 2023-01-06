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
    public List<ScrollRect> scrollRects;
    float currentTime;
    float? offSet = null;
    float offSetY = 0;
    float offSetStart = 0;
    float offSetStartY = 0;
    float scaleToCanvas;
    float ab;
    bool firstCalculation = false;
    bool vertical = false;
    CinemachineFramingTransposer transposer;
    void Start()
    {
        transposer = vCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        Vector2 a = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 b = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        Vector2 c = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        ab = b.x - a.x;
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
        foreach (Button x in buttons)
        {

            if (x.name == "Spells" && !GlobalStatus.magic)
            {
                x.gameObject.GetComponent<Image>().color = new Color(0.4078431f, 0.4078431f, 0.4078431f);
            }
        }
    }

    private void Update()
    {
        if (GlobalStatus.dragging)
            return;
        if (GlobalStatus.popoutMenu)
            return;
        if (Input.GetMouseButtonDown(0))
            if (offSet == null)
            {
                offSetStart = Input.mousePosition.x;
                offSetStartY = Input.mousePosition.y;
                offSet = 0;
                offSetY = 0;
                currentTime = 0f;
                firstCalculation = true;
            }
        if (Input.GetMouseButton(0))
            if (offSet != null)
            {
                if (!vertical)
                {
                    currentTime += Time.deltaTime;
                    offSet = offSetStart - Input.mousePosition.x;
                    offSetY = offSetStartY - Input.mousePosition.y;
                    transposer.m_ScreenX = (-(float)offSet / Screen.width + 0.5f);
                    if (firstCalculation && (Mathf.Abs(offSetY) > 0.5f || Mathf.Abs((float)offSet) > 0.5f))
                    {
                        if (Mathf.Abs(offSetY) > Mathf.Abs((float)offSet))
                        {
                            vertical = true;
                            transposer.m_ScreenX = 0.5f;
                        }
                        else
                        {
                            foreach (ScrollRect x in scrollRects)
                            {
                                x.vertical = false;
                            }
                        }
                        firstCalculation = false;
                    }
                }
            }
        if (Input.GetMouseButtonUp(0))
        {
            offSet = null;
            if (!vertical)
            {
                float currentVelocity = (transposer.m_ScreenX - 0.5f) / currentTime;
                //Debug.Log(currentVelocity);
                if (transposer.m_ScreenX < 0.3f || -velocity > currentVelocity)
                {
                    int index = screens.FindIndex(x => x == vCam.Follow.gameObject) + 1;
                    if (index < screens.Count && screens[index].gameObject.activeSelf == true && buttons[index].gameObject.activeSelf)
                    {
                        ChangePanel(screens[index]);
                        DeactivateButton(buttons[index]);
                    }
                }
                if (transposer.m_ScreenX > 0.7f || velocity < currentVelocity)
                {
                    int index = screens.FindIndex(x => x == vCam.Follow.gameObject) - 1;
                    if (index >= 0 && screens[index].gameObject.activeSelf == true && buttons[index].gameObject.activeSelf)
                    {
                        ChangePanel(screens[index]);
                        DeactivateButton(buttons[index]);
                    }
                }
                foreach (ScrollRect x in scrollRects)
                {
                    x.vertical = true;
                }
            }
            transposer.m_ScreenX = 0.5f;
            vertical = false;
        }

        //Touch touch;
    }

    public void ChangePanel(GameObject panel)
    {
        switch (panel.name)
        {
            case "Spells":
                head.text = "Заклинания";
                break;
            case "Battle":
                head.text = "Боевой Экран";
                break;
            case "Inventory":
                head.text = "Снаряжение";
                break;
            case "Main":
                head.text = "Главный Экран";
                break;
            case "Skills":
                head.text = "Навыки";
                break;
            case "Personality":
                head.text = "Личность";
                break;
            case "Notes":
                head.text = "Записи";
                break;
            case "Class":
                head.text = "Класс";
                break;
            case "Race":
                head.text = "Раса";
                break;
            case "Characteristics":
                head.text = "Характеристики";
                break;
            case "Info":
                head.text = "Личность";
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
                if (x.name == "Spells" && !GlobalStatus.magic)
                    continue;
                x.interactable = true;
            }
        }
    }

    public float GetPanelWidth()
    {
        return ab;
    }
}
