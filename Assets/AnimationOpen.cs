using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationOpen : MonoBehaviour
{
    [SerializeField] Opener opener;
    [SerializeField] RectTransform ent;
    //[SerializeField] LayoutElement layout;
    [SerializeField] Transform arrow;
    [SerializeField] float duration = 1f;
    RectTransform maskRect;
    float size = 0f;
    float start = 0f;
    float time;

    bool change = false;
    bool withoutArrow = true;
    bool ignorArrow = false;

    Quaternion open = Quaternion.Euler(0, 0, 0);
    Quaternion close = Quaternion.Euler(0, 0, 270);
    Quaternion startAngle;
    float saveStart = 0;


    bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        maskRect = GetComponent<RectTransform>();
        saveStart = maskRect.sizeDelta.y;
        state = opener.GetOpenClose();
        if (state)
            size = ent.rect.height;
        else
            size = 0;
        //layout.minHeight = ent.rect.height;
        if (arrow != null)
            withoutArrow = false;
    }

    public void OpenClose()
    {
        state = !state;
        saveStart = 0;
        start = maskRect.sizeDelta.y;
        if (!withoutArrow)
            startAngle = arrow.transform.rotation;
        time = 0;
        if (state)
        {
            size = ent.rect.height;
            saveStart = maskRect.rect.height;
        }
        else
        {
            size = 0;
            saveStart = maskRect.rect.height;
        }
        opener.Resize(size);
        change = true;
    }

    public void Resize(float goalSize)
    {
        start = maskRect.sizeDelta.y;
        time = 0;
        size = goalSize;
        change = true;
        ignorArrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!change) return;
        if (state)
        {
            if (time > duration)
            {
                maskRect.sizeDelta = new Vector2(0, size);
                if (!withoutArrow && !ignorArrow)
                    arrow.rotation = open;
                change = false;
                ignorArrow = false;
                return;
            }
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            if (!withoutArrow && !ignorArrow)
                arrow.rotation = Quaternion.Lerp(startAngle, open, t);
            maskRect.sizeDelta = new Vector2(0, Mathf.Lerp(start, size, t));


            //layout.minHeight = Mathf.Clamp(layout.minHeight + size * speed * Time.deltaTime, 0, size);
            time += Time.deltaTime;
        }
        else
        {
            if (time > duration)
            {
                maskRect.sizeDelta = Vector2.zero;
                if (!withoutArrow && !ignorArrow)
                    arrow.rotation = close;
                change = false;
                ignorArrow = false;
                return;
            }
            float t = time / duration;
            if (!withoutArrow && !ignorArrow)
                arrow.rotation = Quaternion.Lerp(startAngle, close, t);
            t = t * t * (3f - 2f * t);
            maskRect.sizeDelta = new Vector2(0, Mathf.Lerp(start, size, t));
            //layout.minHeight = Mathf.Clamp(layout.minHeight - size * speed * Time.deltaTime, 0, size);
            time += Time.deltaTime;
        }
    }

    public float GetDuration()
    {
        return duration;
    }

    public bool GetState()
    {
        return state;
    }

    public float GetStart()
    {
        return start;
    }

    public float GetTime()
    {
        return time;
    }
}
