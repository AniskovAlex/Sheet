using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationOpen : MonoBehaviour
{
    [SerializeField] RectTransform ent;
    [SerializeField] LayoutElement layout;
    [SerializeField] Transform arrow;
    [SerializeField] float duration = 1f;

    float size = 0f;
    float start = 0f;
    float time;

    bool change = false;
    bool withoutArrow = true;

    Quaternion open = Quaternion.Euler(0, 0, 0);
    Quaternion close = Quaternion.Euler(0, 0, 270);
    Quaternion startAngle;


    bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        //layout.minHeight = ent.rect.height;
        if (arrow != null)
            withoutArrow = false;
    }

    public void OpenClose()
    {
        state = !state;
        start = layout.minHeight;
        if (!withoutArrow)
            startAngle = arrow.transform.rotation;
        time = 0;
        if (state)
            size = ent.rect.height;
        else
            size = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            if (size != ent.rect.height)
            {
                start = size;
                size = ent.rect.height;
                change = true;
            }
            if (layout.minHeight == size || time > duration)
            {
                layout.minHeight = size;
                if (!withoutArrow)
                    arrow.rotation = open;
                change = false;
                return;
            }
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            if (!change)
            {
                if (!withoutArrow)
                    arrow.rotation = Quaternion.Lerp(startAngle, open, t);
                layout.minHeight = Mathf.Lerp(start, size, t);
            }
            else
                layout.minHeight = Mathf.Lerp(start, size, 1);

            //layout.minHeight = Mathf.Clamp(layout.minHeight + size * speed * Time.deltaTime, 0, size);
            time += Time.deltaTime;
        }
        else
        {
            if (layout.minHeight <= 0 || time > duration)
            {
                layout.minHeight = 0;
                if (!withoutArrow)
                    arrow.rotation = close;
                return;
            }
            float t = time / duration;
            if (!withoutArrow)
                arrow.rotation = Quaternion.Lerp(startAngle, close, t);
            t = t * t * (3f - 2f * t);
            layout.minHeight = Mathf.Lerp(start, size, t);
            //layout.minHeight = Mathf.Clamp(layout.minHeight - size * speed * Time.deltaTime, 0, size);
            time += Time.deltaTime;
        }
    }
}
