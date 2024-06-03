using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnSystem : MonoBehaviour
{
    private void Awake()
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
        float yRotate = transform.rotation.eulerAngles.y;

        PlayerPrefs.SetFloat("X", x);
        PlayerPrefs.SetFloat("Y", y);
        PlayerPrefs.SetFloat("Z", z);
        PlayerPrefs.SetFloat("yRotate", yRotate);
    }
    private void LoadPos()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("X"), PlayerPrefs.GetFloat("Y"), PlayerPrefs.GetFloat("Z"));
        transform.rotation = Quaternion.Euler(new Vector3(0f, -PlayerPrefs.GetFloat("yRotate"), 0f));
        PlayerPrefs.DeleteKey("X");
        PlayerPrefs.DeleteKey("Y");
        PlayerPrefs.DeleteKey("Z");
        PlayerPrefs.DeleteKey("yRotate");
    }
}
