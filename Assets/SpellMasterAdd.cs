using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellMasterAdd : MonoBehaviour
{
    [SerializeField] SpellBody spellBody;
    [SerializeField] GameObject choose;
    [SerializeField] GameObject chosen;
    [SerializeField] Text head;
    [SerializeField] Text levelOneLeft;
    [SerializeField] Text levelTwoLeft;
    int lvlOL = 1;
    int lvlTL = 1;

    private void Start()
    {
        head.text = "Мастерство заклинателя";
        bool flag = false;
        List<Spell> list = null;
        foreach ((int, List<Spell>) x in SpellController.spellKnew)
        {
            if (x.Item1 == 3)
            {
                list = new List<Spell>(x.Item2);
            }
        }
        if (list == null) return;
        List<Spell> listMaster = SpellController.spellMaster;
        if (listMaster != null)
            for (int i = 0; i < listMaster.Count; i++)
                if (!list.Contains(listMaster[i]))
                    listMaster.Remove(listMaster[i]);
        list.RemoveAll(g => (!(g.level == 1 || g.level == 2) && !listMaster.Contains(g)));
        foreach (Spell x in list)
        {
            SpellBody newSpell = Instantiate(spellBody, choose.transform);
            newSpell.SetSpell(x);
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, 3); });
            }
        }
        foreach (Spell x in listMaster)
        {
            if (x.level != 1 && x.level != 2) return;
            SpellBody newSpell = Instantiate(spellBody, chosen.transform);
            newSpell.SetSpell(x);
            if (newSpell.GetSpell().level == 1)
            {
                lvlOL = Mathf.Clamp(lvlOL - 1, 0, 999);
            }
            if (newSpell.GetSpell().level == 2)
            {
                lvlTL = Mathf.Clamp(lvlTL - 1, 0, 999);
            }
            Amount buf = newSpell.GetComponentInChildren<Amount>();
            if (buf != null)
            {
                Button button = buf.GetComponent<Button>();
                if (button != null)
                {
                    button.GetComponentInChildren<Text>().text = "-";
                    button.onClick.AddListener(delegate { ChangeSection(newSpell, 3); });
                }
            }
        }
        levelOneLeft.text = "Закл. 1-го ур. осталось: " + lvlOL.ToString();
        levelTwoLeft.text = "Закл. 2-го ур. осталось: " + lvlTL.ToString();
    }

    void ChangeSection(SpellBody spellBody, int id)
    {
        Amount buf = spellBody.GetComponentInChildren<Amount>();
        if (buf == null) return;
        Button button = buf.GetComponent<Button>();
        if (button == null) return;

        if (spellBody.transform.parent == chosen.transform)
        {
            if (spellBody.GetSpell().level == 1)
            {
                lvlOL = Mathf.Clamp(lvlOL + 1, 0, 999);
                levelOneLeft.gameObject.SetActive(true);
            }
            if (spellBody.GetSpell().level == 2)
            {
                lvlTL = Mathf.Clamp(lvlTL + 1, 0, 999);
                levelTwoLeft.gameObject.SetActive(true);
            }
            if (button != null)
                button.GetComponentInChildren<Text>().text = "+";
            spellBody.transform.parent = choose.transform;
            levelOneLeft.text = "Закл. 1-го ур. осталось: " + lvlOL.ToString();
            levelTwoLeft.text = "Закл. 2-го ур. осталось: " + lvlTL.ToString();
            SpellController.spellMaster.Remove(spellBody.GetSpell());
        }
        else
        {
            if ((spellBody.GetSpell().level == 1 && lvlOL <= 0) || (spellBody.GetSpell().level == 2 && lvlTL <= 0)) return;
            if (spellBody.GetSpell().level == 1)
            {
                lvlOL = Mathf.Clamp(lvlOL - 1, 0, 999);
            }
            if (spellBody.GetSpell().level == 2)
            {
                lvlTL = Mathf.Clamp(lvlTL - 1, 0, 999);
            }
            if (button != null)
                button.GetComponentInChildren<Text>().text = "-";
            spellBody.transform.parent = chosen.transform;
            levelOneLeft.text = "Закл. 1-го ур. осталось: " + lvlOL.ToString();
            levelTwoLeft.text = "Закл. 2-го ур. осталось: " + lvlTL.ToString();
            SpellController.spellMaster.Add(spellBody.GetSpell());
        }
        spellBody.transform.SetAsLastSibling();
        Resize();
    }

    void Resize()
    {
        Opener opener;
        opener = choose.transform.parent.parent.GetComponentInChildren<Opener>();
        if (opener != null)
        {
            opener.HieghtSizeInit();
        }
        opener = chosen.transform.parent.parent.GetComponentInChildren<Opener>();
        if (opener != null)
        {
            opener.HieghtSizeInit();
        }

        opener = GetComponentInChildren<Opener>();
        opener.HieghtSizeInit();
        Transform obj = this.transform;
        for (obj = transform.parent; obj != null; obj = obj.parent)
        {
            ContentSizer contentSizer;
            if (obj.TryGetComponent<ContentSizer>(out contentSizer))
            {
                contentSizer.HieghtSizeInit();
            }
            Opener opener1;
            if (obj.parent != null && obj.parent.GetChild(0).TryGetComponent<Opener>(out opener1))
            {
                opener1.HieghtSizeInit();
            }
        }
    }

    private void OnDestroy()
    {
        HashSet<int> list = new HashSet<int>();
        foreach (Spell x in SpellController.spellMaster)
            list.Add(x.id);
        DataSaverAndLoader.SaveSpellMaster(list);
        //SpellController.ReloadSpells();
    }
}
