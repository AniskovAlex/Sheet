using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] List<GameObject> boxs;
    int health = 0;
    int maxHealth = 0;
    int tempHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        CharacterData.GetHealth(out maxHealth, out health, out tempHealth);
        UpdataHP();
    }

    void UpdataHP()
    {
        foreach(GameObject x in boxs)
        {
            x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = health + "/" + maxHealth;
            x.GetComponentInChildren<Amount>().gameObject.GetComponent<Text>().text = tempHealth.ToString();
        }
        CharacterData.SetHealth(health, tempHealth);
    }

    public void ChangeHP(int value)
    {
        if (value < 0)
        {
            tempHealth += value;
            if (tempHealth < 0)
            {
                health = Mathf.Clamp(health + tempHealth, 0, maxHealth);
                tempHealth = 0;
            }
        }
        else
            health = Mathf.Clamp(health + value, 0, maxHealth);
        UpdataHP();
    }

    public void ConHP(InputField value)
    {
        int buf;
        int.TryParse(value.text, out buf);
        value.text = "";
        ChangeHP(-buf);
    }

    public void ProsHP(InputField value)
    {
        int buf;
        int.TryParse(value.text, out buf);
        value.text = "";
        ChangeHP(buf);
    }

    public void AddTempHP(InputField value)
    {
        int buf;
        int.TryParse(value.text, out buf);
        value.text = "";
        tempHealth = buf;
        UpdataHP();
    }

    public void ResetHealth()
    {
        health = maxHealth;
        tempHealth = 0;
        UpdataHP();
    }
}
