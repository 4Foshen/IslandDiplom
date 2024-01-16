using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObject : MonoBehaviour, IInteractable
{
    private TextPanel textPanel;
    public string needItemName;
    public string text;

    private void Start()
    {
        textPanel = FindObjectOfType<TextPanel>();
    }
    public void Interact()
    {
        if (PlayerPrefs.HasKey(needItemName))
        {
            GetComponent<IUsable>().Use();
        }
        else
        {
            textPanel.SetText(text);
            StartCoroutine(textPanel.EnablePanel());
        }
    }
}
