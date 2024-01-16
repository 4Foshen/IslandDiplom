using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text panelText;

    public void SetText(string text)
    {
        panelText.text = text;
    }
    public IEnumerator EnablePanel()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
    }
}
