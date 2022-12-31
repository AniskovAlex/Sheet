using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestoreHealthPanel : MonoBehaviour
{
    [SerializeField] RestoreHealthClass classesHealthDices;
    [SerializeField] GameObject entrails;
    [SerializeField] Text healthText;
    HealthController health;
    int healthRestore = 0;
    int maxHealth = 0;
    int currrentHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthController>();
        maxHealth = CharacterData.GetMaxHealth();
        currrentHealth = CharacterData.GetCurrentHealth();
        healthText.text = currrentHealth + "/" + maxHealth;
        List<(int, PlayersClass)> list = CharacterData.GetClasses();
        list.ForEach(g => Instantiate(classesHealthDices, entrails.transform).SetPanel(g.Item1, g.Item2, RestoreHealth));
    }

    public void RestoreHealth(int count, PlayersClass playersClass)
    {
        if (count > 0)
        {
            int addMin = 1;
            if (GlobalStatus.durable)
                addMin = Mathf.Clamp(CharacterData.GetModifier(2) * 2, 2, 999);
            healthRestore += Mathf.Clamp(Random.Range(1, playersClass.healthDice + 1) + CharacterData.GetModifier(2), addMin, 999);
            healthText.text = Mathf.Clamp(currrentHealth + healthRestore, 0, maxHealth) + "/" + maxHealth;
        }
    }

    private void OnDisable()
    {
        if (health != null)
            health.ChangeHP(healthRestore);
    }
}
