using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetter : MonoBehaviour
{
    Skill[] skillsList;
    // Start is called before the first frame update
    void Start()
    {
        skillsList = GetComponentsInChildren<Skill>();
        string characterName = CharacterCollection.GetName();
        foreach (Skill x in skillsList)
        {
            int atr = x.GetComponentInParent<Box>().index;
            int modifier = CharacterData.GetModifier(atr);
            switch (CharacterData.GetSkill(x.index))
            {
                case -1:
                    modifier += CharacterData.GetProficiencyBonus() / 2;
                    break;
                case 0:
                    break;
                case 1:
                    modifier += CharacterData.GetProficiencyBonus();
                    x.gameObject.GetComponent<RawImage>().color = new Color(189 / 225f, 255 / 225f, 169 / 225f);
                    break;
                case 2:
                    x.GetComponent<RawImage>().color = new Color(231 / 225f, 180 / 225f, 255 / 225f);
                    modifier += CharacterData.GetProficiencyBonus() * 2;
                    break;
            }
            Utilities.SetTextSign(modifier, x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>());
        }
    }

    
}
