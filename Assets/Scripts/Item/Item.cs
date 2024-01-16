using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string objectName;
    private bool isPickuped;

    private void Start()
    {
        CheckIfPickuped();
    }
    private void CheckIfPickuped()
    {
        if (PlayerPrefs.HasKey(objectName))
        {
            isPickuped = true;
            Debug.Log("Disable " + gameObject.name);
            gameObject.SetActive(false);
        }
    }
}
