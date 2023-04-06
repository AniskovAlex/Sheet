using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opener : MonoBehaviour
{
    public GameObject panel;
    [SerializeField] AnimationOpen anim;
    [SerializeField] RectTransform form;
    [SerializeField] RectTransform head;
    [SerializeField] float verticalPadding;
    [SerializeField] float horizontalPadding;
    [SerializeField] float space;
    public Action<float, int> Resized;
    bool open;
    public bool init = false;
    bool rebuild;
    public bool resizing = false;
    bool relocation = false;
    float timeResize = 0f;
    float timeRelocate = 0f;
    float goalFormSize;
    float goalEntSize;
    float goalLoaction;
    float startFormSize;
    float startEntSize;
    float formWithoutEnt;
    float goalSizeThis;
    List<(Transform, float)> startLocations = new List<(Transform, float)>();
    float duration;
    int chId;

    bool net = false;
    bool save = false;
    float save_startFormSize;
    float save_startEntSize;
    List<(Transform, float)> save_startLocations = new List<(Transform, float)>();

    private void Start()
    {
        HieghtSizeInit();
        if (anim != null)
            duration = anim.GetDuration();
    }

    public void click()
    {
        if (anim == null) return;
        anim.OpenClose();
        open = !open;
    }

    public void HieghtSizeInit()
    {
        float hieght = 0f;
        resizing = false;
        Debug.Log(panel.transform.childCount);
        RectTransform buf = panel.GetComponent<RectTransform>();
        int childCount = panel.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            RectTransform childRectTransform = panel.transform.GetChild(i).GetComponent<RectTransform>();
            childRectTransform.transform.localPosition = new Vector2(0, -(hieght + verticalPadding + (i * space)));
            childRectTransform.sizeDelta = new Vector2(buf.sizeDelta.x - (horizontalPadding * 2), childRectTransform.sizeDelta.y);
            Opener opener;
            Text text;
            if (childRectTransform.TryGetComponent(out text))
            {
                Vector2 genExtents = new Vector2(childRectTransform.rect.width, 0f);
                Debug.Log(childRectTransform.rect.width);
                TextGenerationSettings settings = text.GetGenerationSettings(genExtents);

                TextGenerator generator = new TextGenerator();
                float height = generator.GetPreferredHeight(text.text, settings);
                childRectTransform.sizeDelta = new Vector2(childRectTransform.sizeDelta.x, height);

            }
            if (childRectTransform.transform.childCount >= 1 && childRectTransform.GetChild(0).TryGetComponent<Opener>(out opener))
            {
                opener.Resized = EntResize;
                if (opener.init == false)
                    opener.HieghtSizeInit();
            }
            hieght += childRectTransform.rect.height;
        }
        Debug.Log(hieght);
        buf.sizeDelta = new Vector2(0, hieght + space * (childCount - 1) + verticalPadding * 2);
        if (open)
            anim.GetComponent<RectTransform>().sizeDelta = buf.sizeDelta;
        else
            anim.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

        form.sizeDelta = new Vector2(-(horizontalPadding * 2), head.sizeDelta.y + anim.GetComponent<RectTransform>().sizeDelta.y);
        formWithoutEnt = form.sizeDelta.y - anim.GetComponent<RectTransform>().sizeDelta.y;
        ContentSizer contentSizer;
        if (form.transform.parent != null && form.transform.parent.TryGetComponent(out contentSizer))
            Resized = contentSizer.EntResize;
        init = true;
    }
    public void Resize(float goalSize)
    {
        SetResizing(goalSize, false, -1);
        if (Resized != null)
            Resized(goalSize + head.sizeDelta.y, form.transform.GetSiblingIndex());
    }

    public void EntResize(float goalChildSize, int childID)
    {
        RectTransform buf = panel.GetComponent<RectTransform>();
        SetResizing(goalChildSize, true, childID);
        anim.Resize(goalEntSize);
        SetRelocation(goalChildSize, childID);
        if (Resized != null)
            Resized(goalFormSize, form.transform.GetSiblingIndex());
    }

    void SetResizing(float goalSize, bool isEnt, int childID)
    {
        timeResize = 0;
        resizing = true;
        startEntSize = panel.GetComponent<RectTransform>().sizeDelta.y;
        startFormSize = form.GetComponent<RectTransform>().sizeDelta.y;
        if (isEnt)
        {
            goalFormSize = 0;
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                if (i == childID)
                    goalFormSize += goalSize;
                else
                    goalFormSize += panel.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
            }
            goalEntSize = goalFormSize + space * (panel.transform.childCount - 1) + verticalPadding * 2;
            goalFormSize = goalEntSize + head.sizeDelta.y;
        }
        else
        {
            goalFormSize = goalSize + head.sizeDelta.y;
            goalEntSize = startEntSize;
        }
    }

    public void SetRelocation(float goalChildSize, int childId)
    {
        chId = childId;
        timeRelocate = 0;
        relocation = true;
        float hieght = panel.transform.GetChild(childId).GetComponent<RectTransform>().sizeDelta.y - goalChildSize;
        goalLoaction = hieght;
        startLocations.Clear();
        for (int i = chId + 1; i < panel.transform.childCount; i++)
        {
            startLocations.Add((panel.transform.GetChild(i).transform, panel.transform.GetChild(i).transform.localPosition.y));
        }
    }

    public void ResizeHead(float hieght)
    {
        head.sizeDelta = new Vector2(head.sizeDelta.x, head.sizeDelta.y + hieght);
        anim.transform.localPosition += new Vector3(0, -hieght);
        Resize(panel.GetComponent<RectTransform>().sizeDelta.y);
    }

    private void Update()
    {
        if (resizing)
        {
            RectTransform buf = panel.GetComponent<RectTransform>();
            if (timeResize > duration)
            {
                form.sizeDelta = new Vector2(form.sizeDelta.x, goalFormSize);
                buf.sizeDelta = new Vector2(0, goalEntSize);
                resizing = false;
                return;
            }
            float t = timeResize / duration;
            t = t * t * (3f - 2f * t);
            float size = Mathf.Lerp(startFormSize, goalFormSize, t);
            form.sizeDelta = new Vector2(form.sizeDelta.x, size);

            timeResize += Time.deltaTime;
        }
        if (relocation)
        {
            if (timeRelocate > duration)
            {
                foreach ((Transform, float) x in startLocations)
                {
                    x.Item1.localPosition = new Vector3(x.Item1.localPosition.x, x.Item2 + goalLoaction);
                }
                relocation = false;
                return;
            }
            float t = timeRelocate / duration;
            t = t * t * (3f - 2f * t);
            float locate = Mathf.Lerp(0, goalLoaction, t);
            foreach ((Transform, float) x in startLocations)
            {
                x.Item1.localPosition = new Vector3(x.Item1.localPosition.x, x.Item2 + locate);
            }
            timeRelocate += Time.deltaTime;
        }

    }

    public void AddConsum(ConsumablePanel consumablePanel)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Text text;
            if (child.TryGetComponent(out text))
            {
                child.GetComponent<RectTransform>().sizeDelta = new Vector2(-GetComponent<RectTransform>().rect.width / 2, child.GetComponent<RectTransform>().sizeDelta.y);
                child.localPosition = new Vector3(-GetComponent<RectTransform>().rect.width / 2 + 20, child.localPosition.y);
            }
            if (child.gameObject == consumablePanel.gameObject)
            {
                consumablePanel.widthMax = GetComponent<RectTransform>().rect.width / 2 - 160;
                child.localPosition = new Vector3(-40, child.localPosition.y);
            }
        }
    }

    public bool GetOpenClose()
    {
        return anim.GetState();
    }

}
