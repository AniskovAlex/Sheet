using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggingStat : MonoBehaviour
{
    [SerializeField] GameObject buffer;
    bool dragging = false;
    [SerializeField] Transform parent;
    [SerializeField] BoxCollider2D collider;
    [SerializeField] Text attr;
    [SerializeField] Text mod;
    int sum = 0;

    Vector2 offSet;
    ContactFilter2D filter;
    private void Start()
    {
        filter = new ContactFilter2D();
        int[] num = new int[4];
        int min = 21;
        for (int i = 0; i < num.Length; i++)
        {
            num[i] = Random.Range(1, 7);
            min = Mathf.Min(num[i], min);
            sum += num[i];
        }
        sum -= min;
        attr.text = sum.ToString();
        Utilities.SetTextSign((sum / 2) - 5, mod);
    }

    private void Update()
    {
        if (!dragging) return;

        Vector2 mousePos = GetMousePos();

        transform.position = mousePos - offSet;

    }

    private void OnMouseDown()
    {
        dragging = true;
        GlobalStatus.dragging = true;
        if (buffer != null)
            gameObject.transform.parent = buffer.transform;
        offSet = GetMousePos() - (Vector2)transform.position;

    }

    private void OnMouseUp()
    {
        dragging = false;
        GlobalStatus.dragging = false;
        Collider2D[] overlaps = new Collider2D[2];
        if (collider.OverlapCollider(filter.NoFilter(), overlaps) > 0)
        {
            foreach (Collider2D x in overlaps)
            {
                if (x == null)
                    continue;
                SlotAttr newSlot = new SlotAttr();
                if (x.TryGetComponent(out newSlot))
                {
                    SlotAttr oldStat = parent.GetComponent<SlotAttr>();
                    if (newSlot.stat != null)
                    {
                        oldStat.stat = newSlot.stat;
                        oldStat.stat.transform.SetParent(parent);
                        oldStat.stat.parent = parent;
                        oldStat.stat.transform.position = parent.transform.position;
                    }
                    else
                        oldStat.stat = null;
                    newSlot.stat = this;
                    transform.SetParent(newSlot.transform);
                    transform.position = newSlot.transform.position;
                    parent = newSlot.transform;
                }
            }

        }
        else
            transform.position = parent.transform.position;
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public int GetAttr()
    {
        return sum;
    }
}
