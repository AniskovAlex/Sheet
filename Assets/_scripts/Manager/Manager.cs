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
    public GameObject healthObj;
    public GameObject initiative;
    public GameObject profModObj;
    public GameObject passPerObj;
    public GameObject speedObj;
    public List<InputField> money;
    public List<InputField> moneyAdd;
    public List<Button> buttons;
    public CinemachineVirtualCamera camera;
    public Text head;
    Dictionary<int, int> _charAtr = new Dictionary<int, int>();
    Dictionary<int, int> _charModifier = new Dictionary<int, int>();
    Dictionary<int, int> _charSkill = new Dictionary<int, int>();
    Dictionary<int, int> _charSave = new Dictionary<int, int>();
    int profMod = 0;
    int maxHealth = 0;
    int health = 0;

    private void Start()
    {
        UploadData();
        SetAttributes();
        SetSkills();
        SetSave();
        SetAdditional();
        SetMoney();
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
        speedObj.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = PlayerPrefs.GetInt(speedSaveName).ToString();
        if (health == 0)
        {
            health = maxHealth;
            PlayerPrefs.SetInt(healthSaveName, health);
            PlayerPrefs.Save();
        }
        healthObj.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = health + "/" + maxHealth;
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
        foreach (InputField x in money)
        {
            int moneyInt;
            int moneyAddInt;
            int moneyIndex = money.IndexOf(x);
            if (int.TryParse(x.text, out moneyInt) && int.TryParse(moneyAdd[moneyIndex].text, out moneyAddInt))
            {
                moneyInt -= moneyAddInt;
                if (moneyInt < 0)
                    moneyInt = 0;
                x.text = moneyInt.ToString();
                string saveName = moneySaveName + moneyIndex;
                PlayerPrefs.SetInt(saveName, moneyInt);
            }
            moneyAdd[moneyIndex].text = "";

        }
        PlayerPrefs.Save();
    }
    public void MoneyPlus()
    {
        foreach (InputField x in money)
        {
            int moneyInt;
            int moneyAddInt;
            int moneyIndex = money.IndexOf(x);
            if (int.TryParse(x.text, out moneyInt) && int.TryParse(moneyAdd[moneyIndex].text, out moneyAddInt))
            {
                moneyInt += moneyAddInt;
                if (moneyInt > 999999)
                    moneyInt = 999999;
                x.text = moneyInt.ToString();
                string saveName = moneySaveName + moneyIndex;
                PlayerPrefs.SetInt(saveName, moneyInt);
            }
            moneyAdd[moneyIndex].text = "";
        }
        PlayerPrefs.Save();
    }

    public void ChangeHP(int value)
    {
        health = Mathf.Clamp(health + value, 0, maxHealth);
        healthObj.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = health + "/" + maxHealth;
        PlayerPrefs.SetInt(healthSaveName, health);
        PlayerPrefs.Save();
    }

    public void ChangePanel(GameObject panel)
    {
        switch (panel.name)
        {
            case "Inventory":
                head.text = "Инвентарь";
                break;
            case "Main":
                head.text = "Главный Экран";
                break;
            case "Skills":
                head.text = "Навыки";
                break;
        }
        camera.Follow = panel.transform;
    }

    public void DeactivateButton(Button button)
    {
        foreach (Button x in buttons)
        {

            if (x == button)
            {
                x.interactable = false;
            }
            else
            {
                x.interactable = true;
            }
        }
    }

    public void LoadBuilder()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("CharacterBuilder", LoadSceneMode.Single);
    }
}
