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
        if (toggle == true)
        {
            HandEquipment[] secondHands = hands.GetComponentsInChildren<HandEquipment>();
            GameObject newHand = Instantiate(handObject, hands.transform);
            HandEquipment handEquipment = newHand.GetComponent<HandEquipment>();
            if (handEquipment != null)
                currentHands += handEquipment.SetHand(weapon);
            if (currentHands > maxHands)
            {
                if (secondHands.Length > 0)
                {
                    weaponInventory.RemoveEquippedWeapon(secondHands[0].GetWeapon());
                }
            }
        }
        else
        {
            HandEquipment[] secondHands = hands.GetComponentsInChildren<HandEquipment>();
            foreach (HandEquipment x in secondHands)
            {
                if (x.GetWeapon() == weapon)
                {
                    Destroy(x.gameObject);
                    return;
                }
            }
        }
    }
}
