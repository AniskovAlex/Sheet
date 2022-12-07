using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassivePerception : MonoBehaviour
{

    void Start()
    {
        int prof = CharacterData.GetSkill(9);
        int PB = CharacterData.GetProficiencyBonus();
        int modifier = CharacterData.GetModifier(3) + Utilities.GetSkillProfModifier(prof, PB);
        Text text = GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>();
        text.text = (10 + modifier).ToString();
    }

}
