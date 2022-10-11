using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSetter : MonoBehaviour
{
    Save[] savesList;

    // Start is called before the first frame update
    void Start()
    {
        savesList = GetComponentsInChildren<Save>();
        foreach (Save x in savesList)
        {
            int atr = x.GetComponentInParent<Box>().index;
            int modifier = CharacterData.GetModifier(atr);
            switch (CharacterData.GetSave(x.index))
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
