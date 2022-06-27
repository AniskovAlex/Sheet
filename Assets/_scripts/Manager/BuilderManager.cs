using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BuilderManager : MonoBehaviour
{

    const string atrSaveName = "atr_";
    const string skillSaveName = "skill_";
    const string playerSaveName = "name_";
    const string classSaveName = "class_";
    const string levelSaveName = "lvl_";
    const string saveSaveName = "save_";
    const string maxHealthSaveName = "maxHP_";
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

    public List<Box> boxList;
    public List<Skill> skillList;
    public List<Skill> saveList;
    public InputField playerName;
    public InputField className;
    public InputField level;
    public InputField armorClass;
    public InputField maxHealth;
    public InputField speed;
    public ClassDropdown classStat = null;
    Dictionary<int, int> _charAtr = new Dictionary<int, int>();

    private void Start()
    {
        for (int a = 0; a <= 5; a++)
        {
            if (PlayerPrefs.HasKey(atrSaveName + a))
            {
                _charAtr.Add(a, PlayerPrefs.GetInt(atrSaveName + a));
            }
            else
            {
                _charAtr.Add(a, 10);
            }
        }
        int index = 0;
        foreach (Box x in boxList)
        {
            int value;
            if (_charAtr.TryGetValue(index, out value))
            {
                x.GetComponentInChildren<InputField>().text = value.ToString();
                int modifier = value / 2 - 5;
                string str;
                if (modifier >= 0)
                    str = "+" + modifier.ToString();
                else
                    str = modifier.ToString();
                x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Text>().text = str;
            }
            index++;
        }
        foreach (Skill x in skillList)
        {
            if (PlayerPrefs.HasKey(skillSaveName + x.index))
            {
                int prof = PlayerPrefs.GetInt(skillSaveName + x.index);
                switch (prof)
                {
                    case 0:
                        x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Toggle>().isOn = false;
                        x.GetComponentInChildren<Attribute>().gameObject.GetComponent<Toggle>().isOn = false;
                        break;
                    case 1:
                        x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Toggle>().isOn = true;
                        x.GetComponentInChildren<Attribute>().gameObject.GetComponent<Toggle>().isOn = false;
                        break;
                    case 2:
                        x.GetComponentInChildren<Modifier>().gameObject.GetComponent<Toggle>().isOn = true;
                        x.GetComponentInChildren<Attribute>().gameObject.GetComponent<Toggle>().isOn = true;
                        break;
                }
            }
        }
        foreach (Skill x in saveList)
        {
            if (PlayerPrefs.HasKey(saveSaveName + x.index))
            {
                int prof = PlayerPrefs.GetInt(saveSaveName + x.index);
                if (prof == 0)
                    x.GetComponentInChildren<Toggle>().isOn = false;
                else
                    x.GetComponentInChildren<Toggle>().isOn = true;
            }
        }
        playerName.text = PlayerPrefs.GetString(playerSaveName);
        className.text = PlayerPrefs.GetString(classSaveName);
        level.text = PlayerPrefs.GetInt(levelSaveName).ToString();
        armorClass.text = PlayerPrefs.GetInt(armorClassSaveName).ToString();
        maxHealth.text = PlayerPrefs.GetInt(maxHealthSaveName).ToString();
        speed.text = PlayerPrefs.GetInt(speedSaveName).ToString();
    }

    public void DataUpdate(InputField inputField)
    {
        int valueInt;
        if (int.TryParse(inputField.text, out valueInt))
        {
            int atribute = 0;
            atribute += inputField.GetComponentInParent<Box>().index;
            PlayerPrefs.SetInt(atrSaveName + atribute, valueInt);
        }
        Save();
    }

    public void SkillUpdate(Skill updatedSkill)
    {
        int prof = 0;
        if (updatedSkill.GetComponentInChildren<Modifier>().gameObject.GetComponent<Toggle>().isOn)
            prof = 1;
        if (prof == 1 && updatedSkill.GetComponentInChildren<Attribute>().gameObject.GetComponent<Toggle>().isOn)
            prof = 2;
        PlayerPrefs.SetInt(skillSaveName + updatedSkill.index, prof);
        Save();
    }

    public void SaveUpdate(Skill updatedSave)
    {
        int prof;
        if (updatedSave.GetComponentInChildren<Toggle>().isOn)
            prof = 1;
        else
            prof = 0;
        PlayerPrefs.SetInt(saveSaveName + updatedSave.index, prof);
        Save();
    }

    public void NameUpdate(InputField updatedName)
    {
        PlayerPrefs.SetString(playerSaveName, updatedName.text);
        Save();
    }

    public void ClassUpdate(InputField updatedClass)
    {
        PlayerPrefs.SetString(classSaveName, updatedClass.text);
        Save();
    }

    public void LevelUpdate(InputField updatedLevel)
    {
        int lvl;
        if (int.TryParse(updatedLevel.text, out lvl))
        {
            PlayerPrefs.SetInt(levelSaveName, lvl);
            Save();
        }
    }

    public void MaxHealthUpdate(InputField updatedHP)
    {
        int hp;
        if (int.TryParse(updatedHP.text, out hp))
        {
            PlayerPrefs.SetInt(maxHealthSaveName, hp);
            Save();
        }
    }

    public void ArmorClassUpdate(InputField updatedAC)
    {
        int ac;
        if (int.TryParse(updatedAC.text, out ac))
        {
            PlayerPrefs.SetInt(armorClassSaveName, ac);
            Save();
        }
    }
    public void SpeedUpdate(InputField updatedSpeed)
    {
        int spd;
        if (int.TryParse(updatedSpeed.text, out spd))
        {
            PlayerPrefs.SetInt(speedSaveName, spd);
            Save();
        }
    }

    void Save()
    {
        PlayerPrefs.Save();
    }

    public void Del()
    {
        PlayerPrefs.DeleteAll();
    }

    public void LoadView()
    {
        if(classStat!= null)
        {
            classStat.SaveClass();
        }
        SaveSkills();
        PlayerPrefs.Save();
        SceneManager.LoadScene("view", LoadSceneMode.Single);
    }

    void SaveSkills()
    {
        List<string> list = PresavedLists.skills;
        foreach (string x in list)
        {
            switch (x)
            {
                case "Атлетика":
                    PlayerPrefs.SetInt(skillSaveName + 0, 1);
                    break;
                case "Акробатика":
                    PlayerPrefs.SetInt(skillSaveName + 1, 1);
                    break;
                case "Ловкость рук":
                    PlayerPrefs.SetInt(skillSaveName + 2, 1);
                    break;
                case "Скрытность":
                    PlayerPrefs.SetInt(skillSaveName + 3, 1);
                    break;
                case "Анализ":
                    PlayerPrefs.SetInt(skillSaveName + 4, 1);
                    break;
                case "История":
                    PlayerPrefs.SetInt(skillSaveName + 5, 1);
                    break;
                case "Магия":
                    PlayerPrefs.SetInt(skillSaveName + 6, 1);
                    break;
                case "Природа":
                    PlayerPrefs.SetInt(skillSaveName + 7, 1);
                    break;
                case "Религия":
                    PlayerPrefs.SetInt(skillSaveName + 8, 1);
                    break;
                case "Внимательность":
                    PlayerPrefs.SetInt(skillSaveName + 9, 1);
                    break;
                case "Выживание":
                    PlayerPrefs.SetInt(skillSaveName + 10, 1);
                    break;
                case "Медицина":
                    PlayerPrefs.SetInt(skillSaveName + 11, 1);
                    break;
                case "Проницательность":
                    PlayerPrefs.SetInt(skillSaveName + 12, 1);
                    break;
                case "Уход за животными":
                    PlayerPrefs.SetInt(skillSaveName + 13, 1);
                    break;
                case "Выступление":
                    PlayerPrefs.SetInt(skillSaveName + 14, 1);
                    break;
                case "Запугивание":
                    PlayerPrefs.SetInt(skillSaveName + 15, 1);
                    break;
                case "Обман":
                    PlayerPrefs.SetInt(skillSaveName + 16, 1);
                    break;
                case "убеждение":
                    PlayerPrefs.SetInt(skillSaveName + 17, 1);
                    break;
            }
        }
        PresavedLists.skills.Clear();
    }
}
