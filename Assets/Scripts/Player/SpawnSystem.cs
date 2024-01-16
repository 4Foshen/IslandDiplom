using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnSystem : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "IslandScene" && PlayerPrefs.HasKey("X"))
        {
            LoadPos();
        }
    }
    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name == "IslandScene")
        {
            SavePlayerPos();
        }
    }
    private void SavePlayerPos()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        PlayerPrefs.SetFloat("X", x);
        PlayerPrefs.SetFloat("Y", y);
        PlayerPrefs.SetFloat("Z", z);
    }
    private void LoadPos()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
        PlayerPrefs.DeleteKey("X");
        PlayerPrefs.DeleteKey("Y");
        PlayerPrefs.DeleteKey("Z");
    }
}
