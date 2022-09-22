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
        int modifier = CharacterData.GetAtribute(3) + Utilities.GetSkillProfModifier(prof, PB);
        Text text = GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>();
        Utilities.SetTextSign(modifier, text);
    }

}
