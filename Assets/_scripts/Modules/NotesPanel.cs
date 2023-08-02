using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesPanel : MonoBehaviour
{
    [SerializeField] GameObject noteForm;
    [SerializeField] GameObject content;
    [SerializeField] GameObject noteCreate;
    [SerializeField] ContentSizer contentSizer;

    private void Start()
    {
        CharacterData.load += LoadNotes;
    }

    void LoadNotes()
    {
        List<(string, string)> list = CharacterData.GetNotes() /*DataSaverAndLoader.LoadNotes()*/;
        foreach ((string, string) x in list)
        {
            CreateNote(x.Item1, x.Item2);
        }
        noteCreate.transform.SetAsLastSibling();
        contentSizer.HieghtSizeInit();
    }

    public void AddNewNote(string head, string note)
    {
        CreateNote(head, note);
        CharacterData.AddNote(head, note);
        //DataSaverAndLoader.SaveNote(head, note);
    }

    void CreateNote(string head, string note)
    {
        GameObject newObject = Instantiate(noteForm, content.transform);
        newObject.GetComponentInChildren<Opener>().GetComponentInChildren<Text>().text = head;
        newObject.GetComponentInChildren<Discription>().GetComponentInChildren<Text>().text = note;
        newObject.GetComponentInChildren<Discription>().GetComponentInChildren<Button>().onClick.AddListener(delegate { DeleteNote(newObject); });
    }

    public void DeleteNote(GameObject note)
    {
        Opener[] transforms = content.GetComponentsInChildren<Opener>();
        int i;
        for (i = 0; i < transforms.Length; i++)
            if (transforms[i].transform.parent.gameObject == note)
            {
                break;
            }
        CharacterData.DeleteNote(i);
        //DataSaverAndLoader.DeleteNotes(i);

        DestroyImmediate(note);
        contentSizer.HieghtSizeInit();
    }
}
