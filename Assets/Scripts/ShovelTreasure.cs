using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelTreasure : MonoBehaviour, IUsable
{
    [SerializeField] private GameObject chest;
    [SerializeField] private string itemGet = "Rope";
    [SerializeField] private string text = "Вы нашли веревку!";
    private TextPanel textPanel;

    private void Start()
    {
        textPanel = FindObjectOfType<TextPanel>();
    }
    public void Use()
    {
        chest.SetActive(true);
        PlayerPrefs.SetInt(itemGet, 1);
        textPanel.SetText(text);
        StartCoroutine(textPanel.EnablePanel());
    }
}
