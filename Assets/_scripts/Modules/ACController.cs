using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ACController : MonoBehaviour
{
    [SerializeField] List<GameObject> boxs;
    int naturalArmor = 10;
    int addArmor = 0;
    int AC = 10;
    bool shieldEquip = false;
    public bool duelDefence;

    // Start is called before the first frame update
    void Start()
    {
        UpdateArmorClass(null, false);
    }

    public void UploadArmorClass()
    {
        int buf = AC + addArmor;
        if (shieldEquip)
            buf += 2;
        if (duelDefence && GlobalStatus.dealWielder)
            buf += 1;
        foreach (GameObject x in boxs)
            x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = buf.ToString();
        //addArmor = 0;
    }

    public void UpdateArmorClass(Armor armor, bool equip)
    {
        addArmor = 0;
        int dex = CharacterData.GetModifier(1);
        if (equip)
        {
        int capAdd = 0;
            if (GlobalStatus.mediumArmorMaster)
                capAdd = 1;
            if (armor.ACCap != -1)
                AC = armor.AC + Mathf.Clamp(dex, -10, armor.ACCap + capAdd) + addArmor;
            else
                AC = armor.AC + addArmor;
            if (GlobalStatus.defence)
                addArmor += 1;
        }
        else
        {
            if (GlobalStatus.barbarianDefence)
                addArmor = Mathf.Max(addArmor, CharacterData.GetModifier(2));
            if (GlobalStatus.monkDefence)
                addArmor = Mathf.Max(addArmor, CharacterData.GetModifier(4));
            AC = naturalArmor + dex;
            if (GlobalStatus.defence)
                addArmor -= 1;
        }
        UploadArmorClass();
    }
}
