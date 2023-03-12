using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticePanel : MonoBehaviour
{
    [SerializeField] NoticeBody body;
    public void SetNotice(FormCreater formCreater)
    {
        Instantiate(body, transform).Set("�� ������� �������� ���� \"" + formCreater.GetHead()+"\"");
    }

    public void SetNotice(Text text)
    {
        Instantiate(body, transform).Set("�� ������� �������� ���� \"" + text.text + "\"");
    }

    public void SetNotice(string text)
    {
        Instantiate(body, transform).Set("�� ������� �������� ���� \"" + text + "\"");
    }

    public void SetNotice(int text)
    {
        switch (text)
        {
            case 0:
                Instantiate(body, transform).Set("�� ������� ��� ���������");
                break;
            case 1:
                Instantiate(body, transform).Set("� ��� ��� ���������� �������� � ����� ������");
                break;
            case 2:
                Instantiate(body, transform).Set("��� ���� ������ ������ ���� �������");
                break;
        }
    }
}
