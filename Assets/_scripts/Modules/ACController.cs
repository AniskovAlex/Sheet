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

    // Start is called before the first frame update
    void Start()
    {
        UpdateArmorClass(null, false);
    }

    void UploadArmorClass()
    {
        int buf = AC + addArmor;
        foreach (GameObject x in boxs)
            x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = buf.ToString();
    }

    public void UpdateArmorClass(Armor armor, bool equip)
    {
        int dex = CharacterData.GetModifier(1);
        if (equip)
        {
            if (armor.ACCap != -1)
                AC = armor.AC + Mathf.Clamp(dex, -10, armor.ACCap);
            else
                AC = armor.AC;
        }
        else
            AC = naturalArmor + dex;
        UploadArmorClass();
    }
}
