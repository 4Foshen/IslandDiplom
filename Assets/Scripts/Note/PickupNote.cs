using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNote : MonoBehaviour, IInteractable
{
    private Notes notes;
    public int noteIndex;

    private void Start()
    {
        notes = FindObjectOfType<Notes>();
    }
    public void Interact()
    {
        notes.ShowNote(noteIndex);
    }
}
