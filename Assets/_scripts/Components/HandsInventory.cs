using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsInventory : MonoBehaviour
{
    [SerializeField] GameObject handObject;
    [SerializeField] GameObject hands;
    [SerializeField] WeaponInventory weaponInventory;

    int maxHands = 2;
    int currentHands = 0;
    public void NewItemEquiptedOrUnequipted(Weapon weapon, bool toggle)
    {
        HandEquipment[] secondHands = hands.GetComponentsInChildren<HandEquipment>();
        if (toggle == true)
        {
            GameObject newHand = Instantiate(handObject, hands.transform);
            HandEquipment handEquipment = newHand.GetComponent<HandEquipment>();
            if (handEquipment != null)
                currentHands += handEquipment.SetHand(weapon);
            if (currentHands > maxHands)
            {
                while(secondHands.Length > 0 && currentHands > maxHands)
                {
                    weaponInventory.RemoveEquippedWeapon(secondHands[0].GetWeapon());
                }
            }
            if (GlobalStatus.duelist)
            {
                foreach (HandEquipment x in secondHands)
                {
                    if (x != handEquipment)
                        x.ReDamage();
                }
            }
            
        }
        else
        {
            foreach (HandEquipment x in secondHands)
            {
                if (x.GetWeapon() == weapon)
                {
                    DestroyImmediate(x.gameObject);
                    currentHands -= x.GetHands();
                    break;
                }
            }
        }
        if (GlobalStatus.dealWielder)
        {
            secondHands = hands.GetComponentsInChildren<HandEquipment>();
            ACController aC = FindObjectOfType<ACController>();
            if (secondHands.Length == 2 && currentHands == 2)
                aC.duelDefence = true;
            else
                aC.duelDefence = false;
            aC.UploadArmorClass();
        }
    }

    public int GetHands()
    {
        return currentHands;
    }
}
