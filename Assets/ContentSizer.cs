using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentSizer : MonoBehaviour
{
    [SerializeField] float verticalPadding;
    [SerializeField] float horizontalPadding;
    [SerializeField] float space;
    public bool init = false;
    bool resizing = false;
    bool relocation = false;
    float timeResize = 0f;
    float timeRelocate = 0f;
    float goalFormSize;
    float goalLoaction;
    int chId;
    float duration;
    public Action<float, int> Resized;
    float startFormSize;
    bool net = false;
    bool save = false;
    float save_startFormSize;
    List<(Transform, float)> save_startLocations = new List<(Transform, float)>();
    List<(Transform, float)> startLocations = new List<(Transform, float)>();
    // Start is called before the first frame update
    void Start()
    {
        HieghtSizeInit();
    }

    private void OnEnable()
    {
        HieghtSizeInit();
    }

    private void OnDisable()
    {
        init = false;
        Resized = null;
    }

    public void HieghtSizeInit()
    {
        duration = 0.28f;
        float hieght = 0f;
        resizing = false;
        int childCount = transform.childCount;
        ContentSizer contentSizer;
        init = true;
        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).gameObject.active)
                continue;
            RectTransform childRectTransform = transform.GetChild(i).GetComponent<RectTransform>();
            childRectTransform.transform.localPosition = new Vector2(0, -(hieght + verticalPadding + (i * space)));
            childRectTransform.sizeDelta = new Vector2(-(horizontalPadding * 2), childRectTransform.sizeDelta.y);
            Text text;
            if (childRectTransform.TryGetComponent(out text))
            {
                Vector2 genExtents = new Vector2(childRectTransform.rect.width, 0f);
                TextGenerationSettings settings = text.GetGenerationSettings(genExtents);

                TextGenerator generator = new TextGenerator();
                float height = generator.GetPreferredHeight(text.text, settings);
                if (height < 50)
                    height = 50;
                childRectTransform.sizeDelta = new Vector2(childRectTransform.sizeDelta.x, height);

            }
            Opener opener;
            if (childRectTransform.transform.childCount >= 1 && childRectTransform.GetChild(0).TryGetComponent<Opener>(out opener))
            {
                opener.Resized = EntResize;
                if (opener.init == false)
                    opener.HieghtSizeInit();
            }
            if ((childRectTransform.TryGetComponent(out contentSizer) || (childRectTransform.transform.childCount >= 1 && childRectTransform.GetChild(0).TryGetComponent(out contentSizer))))
            {
                contentSizer.Resized = EntResize;
                if (contentSizer.init == false)
                    contentSizer.HieghtSizeInit();
            }
            hieght += childRectTransform.rect.height;
        }
        RectTransform buf = GetComponent<RectTransform>();
        buf.sizeDelta = new Vector2(buf.sizeDelta.x, hieght + space * (childCount - 1) + verticalPadding * 2);

    }

    public void EntResize(float hieght, int childID)
    {
        SetResizing(hieght, childID);
        SetRelocation(hieght, childID);
        if (Resized != null)
            Resized(goalFormSize, transform.GetSiblingIndex());
    }

    void SetResizing(float goalSize, int childID)
    {
        timeResize = 0;
        resizing = true;
        startFormSize = GetComponent<RectTransform>().sizeDelta.y;

        goalFormSize = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == childID)
                goalFormSize += goalSize;
            else
                goalFormSize += transform.GetChild(i).GetComponent<RectTransform>().sizeDelta.y;
        }
        goalFormSize = goalFormSize + space * (transform.childCount - 1) + verticalPadding * 2;

    }

    public void SetRelocation(float hieght, int childId)
    {
        chId = childId;
        timeRelocate = 0;
        relocation = true;
        goalLoaction = transform.GetChild(childId).GetComponent<RectTransform>().sizeDelta.y - hieght;
        startLocations.Clear();
        for (int i = chId + 1; i < transform.childCount; i++)
        {
            startLocations.Add((transform.GetChild(i).transform, transform.GetChild(i).transform.localPosition.y));
        }
    }

    private void Update()
    {
        if (resizing)
        {
            RectTransform buf = GetComponent<RectTransform>();
            if (timeResize > duration)
            {
                buf.sizeDelta = new Vector2(buf.sizeDelta.x, goalFormSize);
                resizing = false;
                return;
            }
            float t = timeResize / duration;
            t = t * t * (3f - 2f * t);
            //buf.sizeDelta = new Vector2(0, Mathf.Lerp(startEntSize, goalEntSize, t));
            buf.sizeDelta = new Vector2(buf.sizeDelta.x, Mathf.Lerp(startFormSize, goalFormSize, t));


            //layout.minHeight = Mathf.Clamp(layout.minHeight + size * speed * Time.deltaTime, 0, size);
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


            //layout.minHeight = Mathf.Clamp(layout.minHeight + size * speed * Time.deltaTime, 0, size);
            timeRelocate += Time.deltaTime;
        }
    }

    public void Resize()
    {
        Transform obj = this.transform;
        HieghtSizeInit();
        for (obj = transform.parent; obj != null; obj = obj.parent)
        {
            ContentSizer contentSizer;
            if (obj.TryGetComponent<ContentSizer>(out contentSizer))
            {
                contentSizer.HieghtSizeInit();
            }
            Opener opener;
            if (obj.parent != null && obj.parent.GetChild(0).TryGetComponent<Opener>(out opener))
            {
                opener.HieghtSizeInit();
            }
        }
    }
}
