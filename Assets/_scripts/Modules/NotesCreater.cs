using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesCreater : MonoBehaviour
{
    [SerializeField] InputField head;
    [SerializeField] InputField note;
    [SerializeField] GameObject form;
    [SerializeField] GameObject newNoteButton;
    [SerializeField] NotesPanel notesPanel;
    [SerializeField] ContentSizer contentSizer;

    public void AddNote()
    {
        if (notesPanel != null)
            notesPanel.AddNewNote(head.text, note.text);
        transform.SetAsLastSibling();
        Cancle();
    }

    public void ShowNewNote()
    {
        form.SetActive(true);
        head.text = "";
        note.text = "";
        newNoteButton.SetActive(false);
        contentSizer.HieghtSizeInit();
    }

    public void Cancle()
    {
        form.SetActive(false);
        newNoteButton.SetActive(true);
        contentSizer.HieghtSizeInit();
    }

    public string GetHead()
    {
        return head.text;
    }

}
