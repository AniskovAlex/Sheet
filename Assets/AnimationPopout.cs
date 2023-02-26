using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationPopout : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] Transform body;
    [SerializeField] float duration;

    Color backgroundColor;
    Color bodyColor;
    float bodyPosition;
    Image bodyImage;

    bool state;
    bool end = false;
    float time = 9999f;
    bool init = false;


    public void Init()
    {
        if (init) return;
        backgroundColor = background.color;
        bodyImage = body.GetComponent<Image>();
        bodyColor = bodyImage.color;
        bodyPosition = body.position.y;
        init = true;
    }

    public void OpenClose(bool newState)
    {
        state = newState;
        end = false;
        if (state)
        {
            background.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, 0);
            //bodyImage.color = new Color(bodyColor.r, bodyColor.g, bodyColor.b, 0);
            body.position = new Vector2(body.position.x, -12);
        }
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state)
        {
            if (time > duration)
            {
                background.color = backgroundColor;
                //bodyImage.color = new Color(bodyColor.r, bodyColor.g, bodyColor.b, 0);
                body.position = new Vector2(body.position.x, bodyPosition);
                return;
            }
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            background.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, Mathf.Lerp(0, backgroundColor.a, t));
            //bodyImage.color = new Color(bodyColor.r, bodyColor.g, bodyColor.b, Mathf.Lerp(0, bodyColor.a, t));
            body.position = new Vector2(body.position.x, Mathf.Lerp(-12, bodyPosition, t));

            time += Time.deltaTime;
        }
        else
        {
            if (time > duration)
            {
                background.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, 0);
                //bodyImage.color = new Color(bodyColor.r, bodyColor.g, bodyColor.b, Mathf.Lerp(bodyColor.a, 0, t));
                body.position = new Vector2(body.position.x, -12);
                end = true;
                return;
            }
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            background.color = new Color(backgroundColor.r, backgroundColor.g, backgroundColor.b, Mathf.Lerp(backgroundColor.a, 0, t));
            //bodyImage.color = new Color(bodyColor.r, bodyColor.g, bodyColor.b, Mathf.Lerp(bodyColor.a, 0, t));
            body.position = new Vector2(body.position.x, Mathf.Lerp(bodyPosition, -12, t));
            time += Time.deltaTime;
        }
    }

    public bool isEnd()
    {
        if (!state && end) return true;
        return false;
    }
}
