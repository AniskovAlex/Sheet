using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUp : MonoBehaviour
{
    [SerializeField] Text healthDice;
    [SerializeField] Text content;
    [SerializeField] Text label;
    [SerializeField] InputField userContent;
    ClassesAbilities classes;
    int midHealth = 0;

    private void Start()
    {
        classes = GetComponentInParent<ClassesAbilities>();

        if (classes.GetClass() == null) return;
        PlayersClass playerClass = classes.GetClass();
        midHealth = playerClass.healthDice / 2 + 1;
        healthDice.text = "1к" + playerClass.healthDice;
        if (CharacterData.GetLevel() == 0)
        {
            content.text = playerClass.healthDice.ToString() + " + Мод. Телосложения";
            userContent.gameObject.SetActive(false);
            label.text = "Начальное здоровье: ";
        }
        else
        {
            userContent.gameObject.SetActive(true);
            content.gameObject.SetActive(false);
            userContent.GetComponentInChildren<Text>().text = (midHealth).ToString();
        }
    }

    public int GetHealth()
    {
        int health = 0;
        if (userContent.IsActive())
        {
            if (userContent.text != "")
                int.TryParse(userContent.text, out health);
            else
                health = midHealth;
        }
        else
            health = classes.GetClass().healthDice;
        return health;
    }

}
