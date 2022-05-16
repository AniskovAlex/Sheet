using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    const string atrSaveName = "atr_";
    const string moneySaveName = "mon_";
    const string skillSaveName = "skill_";
    const string playerSaveName = "name_";
    const string classSaveName = "class_";
    const string levelSaveName = "lvl_";
    const string saveSaveName = "save_";
    const string maxHealthSaveName = "maxHP_";
    const string healthSaveName = "HP_";
    const string tempHealthSaveName = "THP_";
    const string armorClassSaveName = "ac_";
    const string speedSaveName = "spd_";
    /*enum atr
    {
        Str,
        Dex,
        Con,
        Int,
        Wis,
        Cha
    }*/
    public InputField playerName;
    public InputField playerClass;
    public InputField level;
    public List<Box> boxList;
    public List<Skill> skillsList;
    public List<Skill> saveList;
    public GameObject armorClass;
    public List<GameObject> healthObjs;
    public GameObject initiative;
    public GameObject profModObj;
    public GameObject passPerObj;
    public GameObject speedObj;
    public List<InputField> money;
    public List<InputField> moneyAdd;

    Dictionary<int, int> _charAtr = new Dictionary<int, int>();
    Dictionary<int, int> _charModifier = new Dictionary<int, int>();
    Dictionary<int, int> _charSkill = new Dictionary<int, int>();
    Dictionary<int, int> _charSave = new Dictionary<int, int>();
    int profMod = 0;
    int maxHealth = 0;
    int health = 0;
    int tempHealth = 0;
    bool healthStatusChanged = false;
    private void Start()
    {
        UploadData();
        SetAttributes();
        SetSkills();
        SetSave();
        SetAdditional();
        SetMoney();
    }

    private void Update()
    {
        if (healthStatusChanged)
        {
            foreach (GameObject x in healthObjs)
            {
                x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = health + "/" + maxHealth;
                x.GetComponentInChildren<Amount>().gameObject.GetComponent<Text>().text = "+" + tempHealth;
            }
            PlayerPrefs.SetInt(healthSaveName, health);
            PlayerPrefs.SetInt(tempHealthSaveName, tempHealth);
            PlayerPrefs.Save();
            healthStatusChanged = false;
        }
    }

    void UploadData()
    {

        foreach (Box x in boxList)
        {
            string saveName = atrSaveName + x.index;
            if (PlayerPrefs.HasKey(saveName))
            {
                _charAtr.Add(x.index, PlayerPrefs.GetInt(saveName));
            }
            else
            {
                _charAtr.Add(x.index, 10);
            }
        }
        playerName.text = PlayerPrefs.GetString(playerSaveName);
        playerClass.text = PlayerPrefs.GetString(classSaveName);
        profMod = PlayerPrefs.GetInt(levelSaveName);
        level.text = profMod.ToString();
        profMod = (profMod - 1) / 4 + 2;
        profModObj.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = "+" + profMod.ToString();
        armorClass.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt(armorClassSaveName).ToString();
        maxHealth = PlayerPrefs.GetInt(maxHealthSaveName);
        health = PlayerPrefs.GetInt(healthSaveName);
        tempHealth = PlayerPrefs.GetInt(tempHealthSaveName);
        speedObj.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt(speedSaveName).ToString();
        if (health == 0)
        {
            health = maxHealth;
            PlayerPrefs.SetInt(healthSaveName, health);
            PlayerPrefs.Save();
        }
        healthStatusChanged = true;
    }

    void SetAttributes()
    {
        foreach (Box x in boxList)
        {
            int value;
            if (_charAtr.TryGetValue(x.index, out value))
            {
                x.GetComponentInChildren<Attribute>().gameObject.GetComponent<Text>().text = value.ToString();
                int modifier = value / 2 - 5;
                _charModifier.Add(x.index, modifier);
                string str;
                if (modifier >= 0)
                    str = "+" + modifier.ToString();
                else
                    str = modifier.ToString();
                x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = str;
            }
        }
    }
    void SetSkills()
    {
        foreach (Skill x in skillsList)
        {
            string skill = skillSaveName + x.index;
            int atr = x.GetComponentInParent<Box>().index;
            int modifier = 0;
            if (_charModifier.TryGetValue(atr, out modifier))
            {
                switch (PlayerPrefs.GetInt(skill))
                {
                    case -1:
                        modifier += profMod / 2;
                        break;
                    case 0:
                        //x.GetComponentInChildren<Toggle>().isOn = false;
                        break;
                    case 1:
                        modifier += profMod;
                        //x.GetComponentInChildren<Toggle>().isOn = true;
                        Debug.Log(x.gameObject.GetComponent<RawImage>().color);
                        x.gameObject.GetComponent<RawImage>().color = new Color(189 / 225f, 255 / 225f, 169 / 225f);
                        Debug.Log(x.gameObject.GetComponent<RawImage>().color);
                        break;
                    case 2:
                        x.GetComponent<RawImage>().color = new Color(231 / 225f, 180 / 225f, 255 / 225f);
                        modifier += profMod * 2;
                        break;
                }
            }
            _charSkill.Add(x.index, modifier);
            if (modifier >= 0)
                x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = "+" + modifier;
            else
                x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = modifier.ToString();
        }
    }

    void SetSave()
    {
        foreach (Skill x in saveList)
        {
            string save = saveSaveName + x.index;
            int atr = x.GetComponentInParent<Box>().index;
            int modifier = 0;
            _charModifier.TryGetValue(atr, out modifier);
            switch (PlayerPrefs.GetInt(save))
            {
                case -1:
                    modifier += profMod / 2;
                    break;
                case 0:
                    x.GetComponentInChildren<Toggle>().isOn = false;
                    break;
                case 1:
                    modifier += profMod;
                    x.GetComponentInChildren<Toggle>().isOn = true;
                    break;
                case 2:
                    modifier += profMod * 2;
                    break;
            }
            if (modifier >= 0)
                x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = "+" + modifier;
            else
                x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = modifier.ToString();
        }
    }

    void SetAdditional()
    {
        int dex;
        if (_charModifier.TryGetValue(1, out dex))
        {
            if (dex >= 0)
                initiative.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = "+" + dex.ToString();
            else
                initiative.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = dex.ToString();
        }
        else
            initiative.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = "+0";
        int passPer;
        if (_charSkill.TryGetValue(9, out passPer))
        {
            passPer += 10;
            passPerObj.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = passPer.ToString();
        }
    }
    void SetMoney()
    {
        foreach (InputField x in money)
        {
            int moneyIndex = money.IndexOf(x);
            string saveName = moneySaveName + moneyIndex;
            if (PlayerPrefs.HasKey(saveName))
            {
                x.text = PlayerPrefs.GetInt(saveName).ToString();
            }
        }
    }
    public void SaveMoney(Box x)
    {
        int moneyInt;
        int.TryParse(money[x.index].text, out moneyInt);
        PlayerPrefs.SetInt(moneySaveName + x.index, moneyInt);
        PlayerPrefs.Save();
    }

    public void MoneyCon()
    {
        List<(int, int)> m = new List<(int, int)>();
        InputField x;
        for (int index = 0; index < money.Count; index++)
        {
            x = money[index];
            int moneyInt = 0;
            int moneyAddInt = 0;
            int.TryParse(x.text, out moneyInt);
            int.TryParse(moneyAdd[index].text, out moneyAddInt);
            m.Add((moneyInt, moneyAddInt));
            moneyAdd[index].text = "";
        }
        bool flag = false;
        for (int index = 0; index < m.Count; index++)
        {
            int diver = m[index].Item1 - m[index].Item2;
            if (diver < 0)
            {
                if (index != 0 && !flag)
                {
                    (int, int) buf2 = m[index];
                    buf2.Item1 += 10;
                    m[index] = buf2;
                    index--;
                    buf2 = m[index];
                    buf2.Item2 += 1;
                    m[index] = buf2;
                    index--;
                }
                else
                {
                    flag = true;
                    /*(int, int) buf2 = m[index];
                    buf2.Item1 = 0;
                    m[index] = buf2;*/
                }
            }
            else
            {
                (int, int) buf2 = m[index];
                buf2.Item1 = diver;
                buf2.Item2 = 0;
                m[index] = buf2;
            }

        }
        for (int index = 0; index < money.Count; index++)
        {
            x = money[index];
            x.text = m[index].Item1.ToString();
            string saveName = moneySaveName + index;
            PlayerPrefs.SetInt(saveName, m[index].Item1);
        }
        PlayerPrefs.Save();
    }
    public void MoneyPlus()
    {
        List<(int, int)> m = new List<(int, int)>();
        InputField x;
        for (int index = 0; index < money.Count; index++)
        {
            x = money[index];
            int moneyInt = 0;
            int moneyAddInt = 0;
            int.TryParse(x.text, out moneyInt);
            int.TryParse(moneyAdd[index].text, out moneyAddInt);
            m.Add((moneyInt, moneyAddInt));
            moneyAdd[index].text = "";
        }
        for (int index = m.Count - 1; index >= 0; index--)
        {
            int diver = m[index].Item1 + m[index].Item2;
            if (diver >= 10 && index != 0)
            {
                int extra = diver / 10;
                diver %= 10;
                (int, int) buf2 = m[index];
                buf2.Item1 = diver;
                m[index] = buf2;
                buf2 = m[index - 1];
                buf2.Item2 += extra;
                m[index - 1] = buf2;
            }
            else
            {
                (int, int) buf2 = m[index];
                buf2.Item1 = diver;
                m[index] = buf2;
            }
            x = money[index];
            x.text = m[index].Item1.ToString();
            string saveName = moneySaveName + index;
            PlayerPrefs.SetInt(saveName, m[index].Item1);
        }
        PlayerPrefs.Save();
    }

    public void ChangeHP(int value)
    {
        if (value < 0)
        {
            tempHealth += value;
            if (tempHealth < 0)
            {
                health = Mathf.Clamp(health + tempHealth, -999, maxHealth);
                tempHealth = 0;
            }
        }
        else
            health = Mathf.Clamp(health + value, -999, maxHealth);
        healthStatusChanged = true;
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
        healthStatusChanged = true;
    }

    public void LoadBuilder()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Single);
    }
}