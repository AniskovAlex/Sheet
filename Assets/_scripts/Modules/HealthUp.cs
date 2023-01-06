using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUp : MonoBehaviour
{
    [SerializeField] Text healthDice;
    [SerializeField] Text content;
    [SerializeField] InputField userContent;
    ClassesAbilities classes;

    private void Start()
    {
        classes = GetComponentInParent<ClassesAbilities>();

        if (classes.GetClass() == null) return;
        PlayersClass playerClass = classes.GetClass();

        healthDice.text = "1ê" + playerClass.healthDice;
        if (CharacterData.GetLevel() == 0)
        {
            content.text = playerClass.healthDice.ToString();
            userContent.gameObject.SetActive(false);
        }
        else
        {
            userContent.gameObject.SetActive(true);
            content.gameObject.SetActive(false);
        }
    }

    public int GetHealth()
    {
        int health = 0;
        if (userContent.IsActive())
            int.TryParse(userContent.text, out health);
        else
            health = classes.GetClass().healthDice;
        return health;
    }

}
