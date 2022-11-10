using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSet : MonoBehaviour
{
    [SerializeField] Dropdown type;
    [SerializeField] InputField AC;
    [SerializeField] InputField ACCap;
    [SerializeField] InputField strReq;
    [SerializeField] Toggle dis;
    bool _lock = false;

    public void SetArmor(Armor armor)
    {
        type.value = (int)armor.armorType;
        AC.text = armor.AC.ToString();
        ACCap.text = armor.ACCap.ToString();
        strReq.text = armor.strReq.ToString();
        dis.isOn = armor.stealthDis;
    }

    public void Lock()
    {
        type.interactable = false;
        AC.interactable = false;
        ACCap.interactable = false;
        strReq.interactable = false;
        dis.interactable = false;
        _lock = true;
    }

    public void Unlock()
    {
        type.interactable = true;
        AC.interactable = true;
        ACCap.interactable = true;
        strReq.interactable = true;
        dis.interactable = true;
        _lock = false;
    }

    public bool isLock()
    {
        return _lock;
    }

    public Armor Packaging()
    {
        Armor newArmor = new Armor();
        if (AC.text == "" || ACCap.text == "" || strReq.text == "")
            return null;
        newArmor.armorType = Armor.ArmorType.Light + type.value;
        int.TryParse(AC.text, out newArmor.AC);
        int.TryParse(ACCap.text, out newArmor.ACCap);
        int.TryParse(strReq.text, out newArmor.strReq);
        newArmor.stealthDis = dis.isOn;
        return newArmor;
    }

    private void OnDisable()
    {
        type.value = 0;
        AC.text = "";
        ACCap.text = "";
        strReq.text = "";
        dis.isOn = false;
        Unlock();
    }
}
