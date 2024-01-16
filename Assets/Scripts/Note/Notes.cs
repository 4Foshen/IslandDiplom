using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] private GameObject[] notes;
    private int noteIndex;

    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            CloseAllNotes();
        }
    }
    public void ShowNote(int index)
    {
        CloseAllNotes();
        notes[index].SetActive(true);
        noteIndex = index;
    }
    public void CloseAllNotes()
    {
        foreach(var note in notes)
        {
            note.SetActive(false);
        }
    }
}
