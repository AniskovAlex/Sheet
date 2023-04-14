using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class SheetControler : MonoBehaviour
{
    [SerializeField] Toggle sheetButton;
    [SerializeField] GameObject panel;
    GameObject currentPanel;
    public Action<List<Spell>, bool, GameObject> changeSpells;
    ToggleGroup group;
    RectTransform rect;
    float maxWidth;
    float currentWidth;
    Toggle firstToggle = null;
    bool init = true;
    //Opener opener;
    // Start is called before the first frame update

    private void Update()
    {
        if (!init && firstToggle != null)
        {
            init = true;
            firstToggle.isOn = false;
            firstToggle.isOn = true;
        }
    }

    public void SetButtons(List<(int, string, List<Spell>)> spellSheets, bool add, GameObject notThatPanel)
    {
        while (transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
        group = GetComponent<ToggleGroup>();
        rect = GetComponent<RectTransform>();
        firstToggle = null;
        maxWidth = rect.rect.width;
        currentWidth = 20;
        currentPanel = Instantiate(panel, transform);
        int id = 0;
        //maxWidth = currentPanel.GetComponent<RectTransform>().sizeDelta.x;
        init = false;
        foreach ((int, string, List<Spell>) x in spellSheets)
        {
            currentWidth += 120;
            if (id < x.Item1 || currentWidth >= maxWidth)
            {
                currentPanel = Instantiate(panel, transform);
                currentWidth = 140;
            }
            id = x.Item1;
            Toggle button = Instantiate(sheetButton, currentPanel.transform);
            button.isOn = false;
            if (firstToggle == null)
                firstToggle = button;
            Text text = button.GetComponentInChildren<Text>();
            text.text = x.Item2;
            button.group = group;
            button.onValueChanged.AddListener(delegate
            {
                ChangeSpells(x.Item3, button, add, notThatPanel);
            });
        }
    }

    void ChangeSpells(List<Spell> spells, Toggle toggle, bool add, GameObject notThatPanel)
    {
        if (toggle.isOn)
            if (changeSpells != null)
                changeSpells(spells, add, notThatPanel);
    }
}
