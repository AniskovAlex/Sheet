using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLoad : MonoBehaviour
{
    Transform icon;
    [SerializeField] float duration = 1f;

    Quaternion start = Quaternion.Euler(0, 0, 0);
    float time;

    // Start is called before the first frame update
    void Start()
    {
        icon = gameObject.transform;
    }

    private void OnEnable()
    {
        gameObject.transform.rotation = start;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (time > duration)
        {
            icon.rotation = start;
            time = 0;
            return;
        }
        float t = time / duration;
        t = easeInOutBack(t);

        float angle = Mathf.Lerp(0, 360, t);
        Quaternion rotation = Quaternion.AngleAxis(angle: angle, axis: Vector3.back); 
        icon.rotation = rotation;

        time += Time.deltaTime;
    }

    float easeInOutBack(float x)
    {
        return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
    }
}
