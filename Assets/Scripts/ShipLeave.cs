using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipLeave : MonoBehaviour, IInteractable
{
    [SerializeField] private string[] objectNames;
    [SerializeField] private string errorText;
    private TextPanel textPanel;

    private void Start()
    {
        textPanel = FindObjectOfType<TextPanel>();
    }
    public void Interact()
    {
        foreach(string name in objectNames)
        {
            if(!PlayerPrefs.HasKey(name))
            {
                textPanel.SetText(errorText);
                StartCoroutine(textPanel.EnablePanel());
                return;
            }
            else
            {
                SceneManager.LoadScene("EndingScene");
            }
        }
    }
}
