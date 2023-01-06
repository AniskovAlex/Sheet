using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClassDropdown : EventTrigger
{
    Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<Dropdown>();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        PlayersClass playersClass = CharacterData.GetClasses()[0].Item2;
        foreach (Toggle x in dropdown.GetComponentsInChildren<Toggle>(true))
        {
            if (x.GetComponentInChildren<Text>().text == playersClass.name) continue;
            switch (x.GetComponentInChildren<Text>().text)
            {
                case "������":
                    if (CharacterData.GetAtribute(0) < 13)
                        x.interactable = false;
                    break;
                case "����":
                    if (CharacterData.GetAtribute(0) < 13 && CharacterData.GetAtribute(1) < 13)
                        x.interactable = false;
                    break;
                case "����":
                    if (CharacterData.GetAtribute(1) < 13)
                        x.interactable = false;
                    break;
                case "���������":
                    if (CharacterData.GetAtribute(3) < 13)
                        x.interactable = false;
                    break;
                case "�����":
                case "����":
                    if (CharacterData.GetAtribute(4) < 13)
                        x.interactable = false;
                    break;
                case "�����":
                case "��������":
                    if (CharacterData.GetAtribute(1) < 13 || CharacterData.GetAtribute(4) < 13)
                        x.interactable = false;
                    break;
                case "����":
                case "������":
                case "�������":
                    if (CharacterData.GetAtribute(5) < 13)
                        x.interactable = false;
                    break;
                case "�������":
                    if (CharacterData.GetAtribute(0) < 13 || CharacterData.GetAtribute(5) < 13)
                        x.interactable = false;
                    break;
                default:
                    break;
            }
        }
    }
}
