using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    private Item item;
    private void Start()
    {
        item = GetComponent<Item>();
    }
    public void Interact()
    {
        PlayerPrefs.SetInt(item.objectName, 1);
        gameObject.SetActive(false);
    }
}
