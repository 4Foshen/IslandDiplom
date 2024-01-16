using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPad : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject panel;
    [SerializeField] private InputField field;
    [SerializeField] private string code = "628";
    [SerializeField] private string wrongText = "Эх.. код неверный!";
    [SerializeField] private string rightText = "Вы получили ключ от маяка!";
    private TextPanel textPanel;
    private string playerText;
    private void Start()
    {
        textPanel = FindObjectOfType<TextPanel>();
    }
    public void Interact()
    {
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        FindObjectOfType<PlayerMovement>().enabled = false;
    }

    public void InputCode()
    {
        playerText = field.text;
    }
    public void CheckCode()
    {
        if(playerText == code)
        {
            playerText = "";
            field.text = null;
            PlayerPrefs.SetInt("Key", 12);
            textPanel.SetText(rightText);
            StartCoroutine(textPanel.EnablePanel());
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FindObjectOfType<PlayerMovement>().enabled = true;

            panel.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            playerText = "";
            field.text = null;
            textPanel.SetText(wrongText);
            StartCoroutine(textPanel.EnablePanel());

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            FindObjectOfType<PlayerMovement>().enabled = true;

            panel.SetActive(false);
        }
    }
}
