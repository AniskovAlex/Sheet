using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeChosen : MonoBehaviour
{
    [SerializeField] Text head;
    [SerializeField] Dropdown change;
    [SerializeField] Text changeText;
    [SerializeField] Dropdown changed;
    [SerializeField] Text changedText;
    [SerializeField] GameObject discription;
    [SerializeField] SpellBody spellBody;
    [SerializeField] SpellChoose spellChoose;

    bool secrets = false;

    List<(int, string, string, int)> list;
    List<(int, string, string, int)> changeList = new List<(int, string, string, int)>();
    List<(int, string, string, int)> constList;
    Ability ability;
    int id = -1;
    public void SetList(Ability _ability)
    {
        ability = _ability;
        head.text = "Замена: " + _ability.head;
        if (ability.isUniq)
            constList = FileSaverAndLoader.LoadList(ability.pathToList);
        else
            constList = new List<(int, string, string, int)>(ability.list);
        list = new List<(int, string, string, int)>(constList);
        bool found = false;
        List<(string, List<int>)> buf = PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName);
        if (buf.Count <= 0)
            PresavedLists.preLists.Add((ability.listName, DataSaverAndLoader.LoadCustom(ability.listName)));
        buf = PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName);
        foreach ((string, List<int>) x in buf)
        {
            foreach (int y in x.Item2)
            {
                for (int i = 0; i < list.Count; i++)
                    if (list[i].Item1 == y)
                    {
                        changeList.Add(list[i]);
                        list.RemoveAt(i);
                        break;
                    }
            }
            found = true;
        }
        SelectFromList(list);

        PresavedLists.ChangePing += UpdateOptions;

        changeText.text = "";
        change.GetComponent<DropdownExtend>().discriptionText = changeText;
        change.ClearOptions();
        foreach ((int, string, string, int) x in changeList)
            change.options.Add(new Dropdown.OptionData(x.Item2));
        change.options.Add(new Dropdown.OptionData("Пусто"));
        change.options.Add(new Dropdown.OptionData("Пусто"));
        change.onValueChanged.AddListener(delegate
        {
            ChangeSelected(change, true);
            change.GetComponent<DropdownExtend>().Resize();
        });
        change.value = changeList.Count;

        changedText.text = "";
        changed.GetComponent<DropdownExtend>().discriptionText = changedText;
        changed.ClearOptions();
        foreach ((int, string, string, int) x in list)
            changed.options.Add(new Dropdown.OptionData(x.Item2));
        changed.options.Add(new Dropdown.OptionData("Пусто"));
        changed.onValueChanged.AddListener(delegate
        {
            ChangeSelected(changed, false);
            changed.GetComponent<DropdownExtend>().Resize();
        });
        changed.value = list.Count;
    }

    void ChangeSelected(Dropdown dropdown, bool change)
    {
        if (dropdown.GetComponent<DropdownExtend>().currentValueText == dropdown.captionText.text) return;
        int oldValue = -1;
        int newValue = -1;
        dropdown.GetComponent<DropdownExtend>().discriptionText.text = "";
        for (int i = 0; i < constList.Count; i++)
        {
            if (constList[i].Item2 == dropdown.captionText.text)
            {
                dropdown.GetComponent<DropdownExtend>().discriptionText.text = constList[i].Item3;
                newValue = constList[i].Item1;
            }
            if (dropdown.GetComponent<DropdownExtend>().currentValueText == constList[i].Item2)
            {
                oldValue = constList[i].Item1;
            }
        }
        if (change)
            id = newValue;
        dropdown.GetComponent<DropdownExtend>().currentValueText = dropdown.captionText.text;
        if (!change)
        {
            Change(oldValue, newValue);
        }

        int textChildIndex = -1;
        for (int i = 0; i + 1 < discription.transform.childCount; i++)
        {
            if (discription.transform.GetChild(i).gameObject == dropdown.GetComponent<DropdownExtend>().discriptionText.gameObject)
            {
                textChildIndex = i;
                SpellBody spellBodyCur;
                if (discription.transform.GetChild(i + 1).TryGetComponent(out spellBodyCur))
                    DestroyImmediate(spellBodyCur.gameObject);
            }
        }
        if (ability.consum != null)
            foreach ((int, int) k in ability.consum)
                if (k.Item1 == newValue)
                {
                    if (k.Item2 < 0) break;
                    Spell[] spells;
                    spells = LoadSpellManager.GetSpells();
                    SpellBody spellBodyCur;
                    for (int i = 0; i < spells.Length; i++)
                        if (spells[i].id == k.Item2)
                        {
                            spellBodyCur = Instantiate(spellBody, discription.transform);
                            spellBodyCur.SetSpell(spells[i]);
                            if (textChildIndex > 0)
                                spellBodyCur.transform.SetSiblingIndex(textChildIndex + 1);
                        }
                }
    }

    void UpdateOptions(string listName)
    {
        if (listName == ability.listName)
        {
            list = new List<(int, string, string, int)>(constList);
            foreach ((string, List<int>) x in PresavedLists.preLists.FindAll(x => x.Item1 == ability.listName))
                foreach (int y in x.Item2)
                {
                    for (int i = 0; i < list.Count; i++)
                        if (list[i].Item1 == y)
                        {
                            list.RemoveAt(i);
                            break;
                        }
                }
            SelectFromList(list);

            changed.options.Clear();
            foreach ((int, string, string, int) y in list)
                changed.options.Add(new Dropdown.OptionData(y.Item2));
            changed.options.Add(new Dropdown.OptionData("Пусто"));
            changed.options.Add(new Dropdown.OptionData(changed.captionText.text));
            changed.value = changed.options.Count - 1;
        }
    }

    void Change(int oldValue, int newValue)
    {
        if (newValue == -1)
            PresavedLists.RemoveFromPrelist(ability.listName, oldValue);
        else
            PresavedLists.UpdatePrelist(ability.listName, oldValue, newValue);
        if (ability.listName == "Appeals")
        {
            switch (oldValue)
            {
                case 15:
                    Destroy(discription.GetComponentInChildren<SpellChoose>().gameObject);
                    break;
                case 10:
                    List<int> save = DataSaverAndLoader.LoadCustom(ability.listName + "skills");
                    List<string> remove = new List<string>() { "Обман", "Убеждение" };
                    if (save != null && save.Count > 0)
                        foreach (int x in save)
                        {
                            if (x == 0) remove.Remove("Обман");
                            if (x == 1) remove.Remove("Убеждение");
                        }
                    foreach (string x in remove)
                        PresavedLists.RemoveFromSkills(x);
                    break;
            }
            switch (newValue)
            {
                case 15:
                    Instantiate(spellChoose, discription.transform).SetSpells(-3, 2, 1, -3);
                    break;
                case 10:
                    List<int> save = new List<int>();
                    if (PresavedLists.skills.Contains("Обман"))
                        save.Add(0);
                    if (PresavedLists.skills.Contains("Убеждение"))
                        save.Add(1);
                    PresavedLists.UpdateSkills("", "Обман");
                    PresavedLists.UpdateSkills("", "Убеждение");
                    if (save.Count > 0)
                        DataSaverAndLoader.SaveCustomList(ability.listName + "skills", save);
                    break;
            }
        }
    }

    void SelectFromList(List<(int, string, string, int)> list)
    {
        ClassesAbilities classesAbilities = GetComponentInParent<ClassesAbilities>();
        PlayersClass playersClass = null;
        int level = -1;
        if (classesAbilities != null)
            playersClass = classesAbilities.GetClass();
        if (playersClass != null)
            foreach ((int, PlayersClass) x in CharacterData.GetClasses())
                if (x.Item2.id == playersClass.id)
                    level = x.Item1 + 1;
        switch (ability.listName)
        {
            case "Appeals":
                bool flag = false;
                int item = -1;
                foreach ((string, List<int>) x in PresavedLists.preLists)
                    if (x.Item1 == "ItemOfContract")
                    {
                        if (x.Item2.Count > 0)
                        {
                            flag = true;
                            item = x.Item2[0];
                        }
                    }
                for (int i = 0; i < list.Count; i++)
                    switch (list[i].Item4)
                    {
                        default:
                            if (level < 0) break;
                            if (level < list[i].Item4)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -1:
                            if (!flag || item != 0)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -2:
                            if (!flag || item != 1)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -3:
                            if (!flag || item != 2)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -4:
                            bool flag1 = false;
                            foreach ((int, HashSet<int>) x in PresavedLists.spellKnew)
                                if (x.Item2.Contains(139))
                                    flag1 = true;
                            foreach ((int, HashSet<int>) x in DataSaverAndLoader.LoadSpellKnew())
                                if (x.Item2.Contains(139))
                                    flag1 = true;
                            if (!flag1)
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -5:
                            if (!flag || !(item == 1 && level >= 5))
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -6:
                            if (!flag || !(item == 1 && level >= 12))
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                        case -7:
                            if (!flag || !(item == 2 && level >= 15))
                            {
                                list.RemoveAt(i);
                                i--;
                            }
                            break;
                    }
                break;
            case "ElementalPractice":
                for (int i = 0; i < list.Count; i++)
                {
                    if (level < 0) break;
                    if (level < list[i].Item4)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
                break;
            case "BattleStyles":
                if (ability.consum == null || ability.consum.Length <= 0) break;
                for (int i = 0; i < list.Count; i++)
                {
                    bool flagList = true; ;
                    foreach ((int, int) x in ability.consum)
                    {
                        if (x.Item1 == list[i].Item1)
                        {
                            flagList = false;
                            break;
                        }
                    }
                    if (flagList)
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
                break;
        }
    }

    public Ability GetAbility()
    {
        return ability;
    }

    public int GetRemoveID()
    {
        return id;
    }

    public bool Validate()
    {
        if ((change.captionText.text != "" && change.captionText.text != "Пусто") == (changed.captionText.text != "" && changed.captionText.text != "Пусто"))
            return true;
        else
            return false;
    }

    private void OnDestroy()
    {
        PresavedLists.ChangePing -= UpdateOptions;
        int oldValue = -1;
        for (int i = 0; i < constList.Count; i++)
        {
            if (changed.GetComponent<DropdownExtend>().currentValueText == constList[i].Item2)
            {
                oldValue = constList[i].Item1;
            }
        }
        Change(oldValue, -1);
    }
}
