using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestSpellCellRestore : MonoBehaviour
{
    [SerializeField] Text left;
    [SerializeField] RestSpellCellRestoreRow row;
    SpellController controller;

    int restMax = 0;

    private void Start()
    {
        controller = FindObjectOfType<SpellController>();

        foreach ((int, PlayersClass) x in CharacterData.GetClasses())
            if (x.Item2.id == 3 || (x.Item2.id == 4 && x.Item1 >= 2 && x.Item2.GetSubClass() != null && x.Item2.GetSubClass().id == 1))
                restMax += x.Item1 + 1 / 2;
            
        if (restMax == 0) Destroy(gameObject);
        left.text = "ќсталось уровней €чеек: " + restMax.ToString();
        if (controller == null) return;
        int[] cellsCurrent = controller.GetCellsCurrent();
        int[] cells = controller.GetCellsMax();
        for (int i = 0; i < cells.Length && i < 5; i++)
        {
            if (cells[i] <= 0) continue;
            RestSpellCellRestoreRow newRow = Instantiate(row, GetComponentInChildren<Discription>().transform);
            newRow.update += UpdateCell;
            newRow.SetRow(i + 1, cellsCurrent[i], cells[i]);
        }


    }

    void UpdateCell(int level, int add)
    {
        restMax = Mathf.Clamp(restMax - level * add, 0, 999);
        left.text = "ќсталось уровней €чеек: " + restMax.ToString();
        UpdateRow();
        controller.ResetSpellCell(level - 1, add);
    }
    void UpdateRow()
    {
        RestSpellCellRestoreRow[] buttons = GetComponentsInChildren<RestSpellCellRestoreRow>();
        foreach (RestSpellCellRestoreRow x in buttons)
            x.UpdateRow(restMax);

    }
}
